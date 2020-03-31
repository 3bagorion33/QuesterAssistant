using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Panels;
using Action = Astral.Quester.Classes.Action;

namespace QuesterAssistant.Actions
{
    public class InteractNodeExt : Action
    {
		private List<string> interacted = new List<string>();
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
        protected override bool IntenalConditions => true;
        public override bool UseHotSpots => true;
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override void InternalReset() => currentNode = null;

        protected override Vector3 InternalDestination
        {
            get
            {
                if (currentNode != null && currentNode.Node.IsValid)
                {
                    return currentNode.Node.WorldInteractionNode.Location;
                }
                return new Vector3();
            }
        }

        public override bool NeedToRun
        {
            get
            {
                if (currentNode == null || !currentNode.Node.IsValid)
                {
                    foreach (TargetableNode targetableNode in EntityManager.LocalPlayer.Player.InteractStatus
                        .TargetableNodes.OrderBy(n => n.WorldInteractionNode.Location.Distance3DFromPlayer))
                    {
                        if (!OneInteractionByNode || !interacted.Contains(targetableNode.WorldInteractionNode.Key))
                        {
                            var loc = targetableNode.WorldInteractionNode.Location;
                            if (targetableNode.RequirementName.Length > 0 && !Kits.HaveRequiredKit(new Interact.DynaNode(targetableNode.WorldInteractionNode.Key)))
                                continue;
                            var flag = WhiteList.Exists(v => v.X == loc.X && v.Y == loc.Y && v.Z == loc.Z)
                                        || !BlackList.Exists(v => v.X == loc.X && v.Y == loc.Y && v.Z == loc.Z);
                            if (flag && targetableNode.WorldInteractionNode.Location.Distance3DFromPlayer <
                                ReactionRange)
                            {
                                if (NodeCategory.Length != 0 || targetableNode.Categories.Count != 0)
                                {
                                    if (!targetableNode.Categories.Contains(NodeCategory))
                                        continue;
                                }
                                currentNode = new Interact.DynaNode(targetableNode.WorldInteractionNode.Key);
                                break;
                            }
                        }
                    }
                }
                if (IgnoreCombat) API.IgnoreCombat = true;
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
            Brush green = Brushes.Green;
            foreach (Vector3 worldPos in WhiteList)
            {
                graph.drawFillEllipse(worldPos, new Size(10, 10), green);
            }
            if (currentNode != null && currentNode.Node.IsValid)
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(currentNode.Node.WorldInteractionNode.Location, new Size(8, 8), beige);
            }
        }

        public InteractNodeExt()
        {
            NodeCategory = string.Empty;
            LocalizedName = string.Empty;
            Dialogs = new List<string>();
            OneInteractionByNode = false;
            InteractTime = 1000;
            ReactionRange = 35;
            BlackList = new List<Vector3>();
            WhiteList = new List<Vector3>();
            IgnoreCombat = true;
        }
        
        public override ActionResult Run()
        {
            if (IgnoreCombat)
                API.IgnoreCombat = false;
            if (currentNode == null || !currentNode.Node.IsValid)
                return ActionResult.Running;
            Interact.DynaNode dynaNode = currentNode;
            Astral.Logic.NW.General.CloseContactDialog();
            Approach.NodeForInteraction(dynaNode);
            if (Attackers.InCombat)
                return ActionResult.Running;
            API.Engine.Navigation.Stop();
            Pause.Sleep(500);
            dynaNode.Node.WorldInteractionNode.Interact();
            Pause.Sleep(500);
            dynaNode.Node.WorldInteractionNode.Interact();
            if (OneInteractionByNode)
                interacted.Add(dynaNode.Node.WorldInteractionNode.Key);
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
                    EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SelectOptionByKey(key, "");
                    Pause.Sleep(1500);
                }
                EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();
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
            currentNode = null;
            return ActionResult.Completed;
        }

        public string NodeCategory { get; set; }
        public string LocalizedName { get; set; }
        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        public List<string> Dialogs { get; set; }
        public bool OneInteractionByNode { get; set; }
        public int InteractTime { get; set; }
        [Description("Maximum distance of node from path")]
        public int ReactionRange { get; set; }
        [Description("Only the nodes in this list will be interacted, loot all if empty. Green on map.")]
        [Editor(typeof(PositionNodeListEditorr), typeof(UITypeEditor))]
        public List<Vector3> WhiteList { get; set; }
        [Description("The nodes is this list will not be interacted.")]
        [Editor(typeof(PositionNodeListEditorr), typeof(UITypeEditor))]
        public List<Vector3> BlackList { get; set; }
        public bool IgnoreCombat { get; set; }
    }
}