using MyNW.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.UIEditors;
using Astral.Quester.UIEditors.Forms;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using Action = Astral.Quester.Classes.Action;

namespace QuesterAssistant.Actions
{
    public class InteractSpecificNPCExt : Action, IIgnoreCombat
    {
		private bool combat;
        private Entity target = new Entity(IntPtr.Zero);

        public override string ActionLabel
        {
            get
            {
                string text = GetType().Name;
                if (DisplayName.Length > 0)
                {
                    text += $": {DisplayName}";
                }
                return text;
            }
        }

        public override string InternalDisplayName => string.Empty;
        public override string Category => Core.Category;
        protected override ActionValidity InternalValidity =>
            !Position.IsValid ? new ActionValidity("No valid npc position.") : new ActionValidity();
        protected override Vector3 InternalDestination => Position;
        public override bool UseHotSpots => false;
        public override void InternalReset() { }

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (Position.IsValid)
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(Position, new Size(10, 10), beige);
            }
        }
        
        public InteractSpecificNPCExt()
        {
            NPCCostume = string.Empty;
            Dialogs = new List<string>();
            DisplayName = string.Empty;
            Position = new Vector3();
            InteractTime = 2000;
            InteractDistance = 15;
            Tolerance = 1;
            NPCUntranslatedName = string.Empty;
            IgnoreCombat = true;
        }

        public override void GatherInfos()
        {
            MessageBoxSpc.ShowDialog("Target npc and press ok.");
            Entity betterEntityToInteract = Interact.GetBetterEntityToInteract();
            if (betterEntityToInteract.IsValid)
            {
                NPCUntranslatedName = betterEntityToInteract.NameUntranslated;
                DisplayName = betterEntityToInteract.Name;
                Position = betterEntityToInteract.Location.Clone();
            }
            if (QMessageBox.ShowDialog("Add a dialog ? (open the dialog window before)") == DialogResult.OK)
                DialogEdit.Show(Dialogs);
        }

        protected override bool IntenalConditions => true;

        private Approach.BreakInfos CheckCombat()
        {
            if (Attackers.List.Count > 0)
            {
                combat = true;
                return Approach.BreakInfos.ApproachFail;
            }
            return Approach.BreakInfos.Continue;
        }

        private bool GetNPC()
        {
            foreach (Entity entity in Entities.GetContactEntities())
            {
                if ((string.IsNullOrEmpty(NPCCostume) || entity.CostumeRef.CostumeName == NPCCostume) &&
                    (NPCUntranslatedName.Length <= 0 || entity.NameUntranslated == NPCUntranslatedName) &&
                    (entity.Location.Distance3D(Position) < Tolerance &&
                     entity.Location.Distance3DFromPlayer < InteractDistance + 5 &&
                     Astral.Logic.General.ZAxisDiffFromPlayer(entity.Location) < 10.0 || entity.CanInteract))
                {
                    target = new Entity(entity.Pointer);
                    return true;
                }
            }
            return false;
        }

        public override bool NeedToRun
        {
            get
            {
                this.IgnoreCombat();
                if (GetNPC()) return true;
                if (Position.Distance3DFromPlayer < InteractDistance)
                {
                    Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(5000);
                    while (!timeout.IsTimedOut)
                    {
                        if (GetNPC())
                            return true;
                        Pause.Sleep(500);
                    }
                    return true;
                }
                return false;
            }
        }

        public override ActionResult Run()
        {
            this.EnableCombat();
            if (!target.IsValid)
            {
                Logger.WriteLine("NPC Not founded.");
                return ActionResult.Skip;
            }
            combat = false;
            if (Approach.EntityForInteraction(target, CheckCombat))
            {
                target.Interact();
                Pause.Sleep(InteractTime);
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
                    Pause.Sleep(500);
                    foreach (var str in Dialogs)
                    {
                        EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SelectOptionByKey(str);
                        Pause.Sleep(1000);
                    }
                }
                EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();
                target = new Entity(IntPtr.Zero);
                return ActionResult.Completed;
            }
            if (combat)
            {
                return ActionResult.Running;
            }
            return ActionResult.Fail;
        }

        [Editor(typeof(NPCId), typeof(UITypeEditor))]
        public string NPCCostume { get; set; }
        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        public List<string> Dialogs { get; set; }
        public new string DisplayName { get; set; }
        [Editor(typeof(PositionEditor), typeof(UITypeEditor))]
        public Vector3 Position { get; set; }
        public int InteractTime { get; set; }
        [Description("Some npc can be not exactly at the same position which at origin, up this value only if necessary.")]
        public int Tolerance { get; set; }
        [Editor(typeof(NPCId2), typeof(UITypeEditor))]
        public string NPCUntranslatedName { get; set; }
        public uint InteractDistance { get; set; }
        public bool IgnoreCombat { get; set; }
    }
}