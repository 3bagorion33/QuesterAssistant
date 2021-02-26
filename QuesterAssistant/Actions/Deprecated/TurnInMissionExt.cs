using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.Forms;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Actions.Deprecated
{
    public class TurnInMissionExt : Action, IIgnoreCombat, Frames.IActionCloseFrame
    {
        private Astral.Classes.Timeout failTo;

        public override string ActionLabel => $"{GetType().Name} [{MissionId}]";
        public override string Category => Core.Category;
        public override void InternalReset() => failTo = null;
        public override string InternalDisplayName => $"TurnIn {Missions.GetMissionDisplayNameByPath(MissionId)}";
        protected override bool IntenalConditions => Missions.GetMissionByPath(MissionId).IsValid;
        protected override Vector3 InternalDestination => GiverPosition.Clone();
        public override bool UseHotSpots => false;

        public override bool NeedToRun
        {
            get
            {
                foreach (ContactInfo contactInfo in EntityManager.LocalPlayer.Player.InteractInfo.NearbyContacts)
                {
                    Entity entity = contactInfo.Entity;
                    if (entity.IsValid && entity.InternalName == GiverId &&
                        entity.Location.Distance3D(GiverPosition) < InteractDistance &&
                        (entity.Location.Distance3DFromPlayer < InteractDistance &&
                         Astral.Logic.General.ZAxisDiffFromPlayer(entity.Location) < 15.0 || entity.CanInteract))
                    {
                        return true;
                    }
                }
                this.IgnoreCombat();
                return false;
            }
        }

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (!GiverPosition.IsValid)
                {
                    return new ActionValidity("Giver position invalid.");
                }
                if (MissionId.Length == 0)
                {
                    return new ActionValidity("Invalid mission id.");
                }
                return new ActionValidity();
            }
        }

        public override void GatherInfos()
        {
            MissionId = GetMissionId.Show(true);
            MessageBoxSpc.ShowDialog("Target mission giver and press ok.");
            Entity betterEntityToInteract = Interact.GetBetterEntityToInteract();
            if (betterEntityToInteract.IsValid)
            {
                GiverId = betterEntityToInteract.InternalName;
                GiverPosition = betterEntityToInteract.Location.Clone();
            }
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (GiverPosition.IsValid)
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(GiverPosition, new Size(10, 10), beige);
            }
        }

        public TurnInMissionExt()
        {
            GiverId = string.Empty;
            GiverPosition = new Vector3();
            MissionId = string.Empty;
            IgnoreCombat = true;
            CloseFrame = false;
            InteractDistance = 5;
        }

        public override ActionResult Run()
        {
            this.EnableCombat();
            foreach (ContactInfo contactInfo in EntityManager.LocalPlayer.Player.InteractInfo.NearbyContacts)
            {
                Entity entity = contactInfo.Entity;
                if (entity.IsValid && entity.InternalName == GiverId && entity.Location.Distance3D(GiverPosition) < InteractDistance)
                {
                    if (Missions.TurnInMission(entity, MissionId) == Missions.MissionInteractResult.Success)
                    {
                        failTo = null;
                        return ActionResult.Completed;
                    }
                    EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();
                    Pause.Sleep(2000);
                    if (failTo == null)
                        failTo = new Astral.Classes.Timeout(30000);
                    if (failTo.IsTimedOut)
                    {
                        Logger.WriteLine("Mission turn in timeout...");
                        return ActionResult.Fail;
                    }
                    break;
                }
            }
            return ActionResult.Running;
        }

        public string GiverId { get; set; }
        public uint InteractDistance { get; set; }
        [Editor(typeof(MainMissionEditor), typeof(UITypeEditor))]
        public string MissionId { get; set; }
        [Browsable(false)]
        [XmlIgnore]
        public new string AssociateMission => string.Empty;
        [Editor(typeof(PositionEditor), typeof(UITypeEditor))]
        public Vector3 GiverPosition { get; set; }
        public bool IgnoreCombat { get; set; }
        public bool CloseFrame { get; set; }
    }
}