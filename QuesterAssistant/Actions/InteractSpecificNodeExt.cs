using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Astral;
using Astral.Controllers.BotComs;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Conditions;
using QuesterAssistant.Panels;
using Action = Astral.Quester.Classes.Action;
using API = Astral.Quester.API;

namespace QuesterAssistant.Actions
{
    public class InteractSpecificNodeExt : Action, IIgnoreCombat, Frames.IActionCloseFrame
    {
        private Interact.DynaNode currentNode;

        public override string ActionLabel
        {
            get
            {
                string text = GetType().Name;
                if (LocalizedName.Length > 0)
                    text += $" ({LocalizedName})";
                return text;
            }
        }
        public override string InternalDisplayName => string.Empty;
        public override string Category => Core.Category;
        protected override bool IntenalConditions => new CheckNode
            {Position = Position, Tested = Tested, VisibilityDistance = VisibilityDistance}.IsValid;
        public override bool UseHotSpots => false;
        protected override Action.ActionValidity InternalValidity => !Position.IsValid
            ? new ActionValidity("No valid node position.")
            : new ActionValidity();
        protected override Vector3 InternalDestination =>
            currentNode != null && currentNode.Node.IsValid
                ? currentNode.Node.WorldInteractionNode.Location
                : Position;
        public override void InternalReset() => currentNode = null;

        public override bool NeedToRun
        {
            get
            {
                this.IgnoreCombat();
                if ((currentNode == null || !currentNode.Node.IsValid) && !GetNode() &&
                    (SkipTimeout > 0 || Common.Enabled) && Position.Distance3DFromPlayer < 15.0)
                {
                    Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(SkipTimeout);
                    if (Common.Enabled && SkipTimeout == 0)
                        timeout.ChangeTime(4000);
                    while (!GetNode())
                    {
                        if (timeout.IsTimedOut)
                            return true;
                        if (Attackers.InCombat)
                            return false;
                        Pause.Sleep(500);
                    }
                }
                return currentNode != null && currentNode.Node.IsValid &&
                       Astral.Logic.General.ZAxisDiffFromPlayer(currentNode.Node.WorldInteractionNode
                           .InteractLocation) < 10.0 &&
                       currentNode.Node.WorldInteractionNode.InteractLocation.Distance3DFromPlayer < 15.0;
            }
        }

        public override void GatherInfos()
        {
            while (MessageBoxSpc.ShowDialog("Target the node and press ok.") == DialogResult.OK)
            {
                if (EntityManager.LocalPlayer.Player.InteractStatus.pMouseOverNode == IntPtr.Zero)
                    QMessageBox.ShowError("You don't have target !");
                else
                {
                    var overCursorNode =
                        EntityManager.LocalPlayer.Player.InteractStatus.TargetableNodes.Find(n => n.IsMouseOver);
                    if (overCursorNode.RequirementName.Length > 0)
                    {
                        QMessageBox.ShowError("Don't use objectives with skill nodes!");
                        return;
                    }
                    var targetableNode = new TargetableNode(overCursorNode.Pointer);
                    LocalizedName = overCursorNode.WorldInteractionNode.Name;
                    Position = overCursorNode.WorldInteractionNode.Location.Clone();
                    NodeCategory = string.Empty;
                    if (overCursorNode.Categories.Count > 0)
                        NodeCategory = overCursorNode.Categories[0];
                    if (HotSpots.Count == 0)
                        HotSpots.Add(overCursorNode.WorldInteractionNode.Location.Clone());
                    if (targetableNode.IsValid)
                        break;
                }
            }
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (Position.IsValid)
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(Position, new Size(12, 12), beige);
            }
        }

        public InteractSpecificNodeExt()
        {
            NodeCategory = string.Empty;
            LocalizedName = string.Empty;
            Dialogs = new List<string>();
            InteractTime = 1000;
            Position = new Vector3();
            RewardItemChoose = string.Empty;
            SkipTimeout = 0;
            IgnoreCombat = true;
            VisibilityDistance = 40;
        }

        private bool GetNode()
        {
            foreach (TargetableNode targetableNode in EntityManager.LocalPlayer.Player.InteractStatus.TargetableNodes)
            {
                if (targetableNode.WorldInteractionNode.Location.Distance3D(Position) < 1.0)
                {
                    currentNode = new Interact.DynaNode(targetableNode.WorldInteractionNode.Key);
                    return true;
                }
            }
            return false;
        }

        public override ActionResult Run()
        {
            this.EnableCombat();
            if (currentNode == null || !currentNode.Node.IsValid)
            {
                Logger.WriteLine("Node not founded.");
                return ActionResult.Skip;
            }
            if (!currentNode.Node.IsValid)
            {
                return ActionResult.Running;
            }
            var dynaNode = currentNode;
            currentNode = null;
            Astral.Logic.NW.General.CloseContactDialog();
            Approach.NodeForInteraction(dynaNode);
            if (Attackers.InCombat)
                return ActionResult.Running;
            API.Engine.Navigation.Stop();
            Pause.Sleep(500);
            if (!dynaNode.Node.WorldInteractionNode.Interact())
                return ActionResult.Running;
            int num = InteractTime;
            if (num < 1000)
                num = 1000;
            Pause.Sleep(num);
            Interact.WaitForInteraction();
            if (Dialogs.Count > 0)
            {
                Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(5000);
                while (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Options.Count == 0)
                {
                    if (timeout.IsTimedOut)
                        return ActionResult.Fail;
                    Pause.Sleep(100);
                }
                foreach (string key in Dialogs)
                {
                    EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SelectOptionByKey(key, RewardItemChoose);
                    Pause.Sleep(1500);
                }
            }
            if (Game.IsLootFrameVisible())
            {
                GameCommands.SimulateFKey();
                Astral.Classes.Timeout timeout2 = new Astral.Classes.Timeout(5000);
                while (Game.IsLootFrameVisible())
                {
                    if (timeout2.IsTimedOut)
                    {
                        Game.CloseLootFrame();
                        return ActionResult.Fail;
                    }
                    Pause.Sleep(200);
                }
            }
            foreach (ClientDialogBox clientDialogBox in UIManager.ClientDialogBoxes)
            {
                clientDialogBox.PerformOK();
                Pause.Sleep(500);
            }
            if (Astral.API.CurrentSettings.UsePathfinding3 && EntityManager.LocalPlayer.CurrentZoneMapInfo.MapType == ZoneMapType.Mission)
            {
                Pause.Sleep(2500);
                if (EntityManager.LocalPlayer.IsValid && !EntityManager.LocalPlayer.IsLoading)
                {
                    Logger.WriteLine("Refresh tiles after specific node interaction ...");
                    PathFinding.RegenTilesNearPlayer();
                }
            }
            return ActionResult.Completed;
        }

        public string NodeCategory { get; set; }
        public string LocalizedName { get; set; }
        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        public List<string> Dialogs { get; set; }
        public int InteractTime { get; set; }
        [Editor(typeof(PositionEditor), typeof(UITypeEditor))]
        public Vector3 Position { get; set; }
        [Description("Optionnal, used with some reward chests.")]
        [Editor(typeof(RewardsEditor), typeof(UITypeEditor))]
        public string RewardItemChoose { get; set; }
        [Description("In milliseconds, skip action after timeout, 0 to disable")]
        public int SkipTimeout { get; set; }
        public bool IgnoreCombat { get; set; }

        [Category("NodeCheck")]
        public NodeState Tested { get; set; }
        [Category("NodeCheck")]
        public double VisibilityDistance { get; set; }

        [Category("Interaction")]
        public bool CloseFrame { get; set; } = true;
    }
}