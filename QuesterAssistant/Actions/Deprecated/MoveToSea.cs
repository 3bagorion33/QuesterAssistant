using System;
using System.ComponentModel;
using System.Drawing.Design;
using Astral.Logic.Classes.Map;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes.Patches;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Actions.Deprecated
{
    [Serializable]
    public class MoveToSea : Astral.Quester.Classes.Action
    {
        private Astral.Quester.Classes.Actions.MoveTo moveTo = new Astral.Quester.Classes.Actions.MoveTo();

        public override string ActionLabel => string.IsNullOrEmpty(Description) ? "SailTo" : $"Sail [{Description}]";
        public override string Category => Core.Deprecated;
        public override string InternalDisplayName => string.Empty;
        public override bool NeedToRun => moveTo.NeedToRun;
        public override bool UseHotSpots => moveTo.UseHotSpots;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => moveTo.Position;
        protected override ActionValidity InternalValidity =>
            new ActionValidity($"Action '{GetType().Name}' is {Core.Deprecated}.\n" +
                               $"Use action '{nameof(Astral.Quester.Classes.Actions.MoveTo)}' instead.\n" +
                               $"The '{nameof(WayPointFilter)}' will filter the waypoint automatically.");

        public override void InternalReset() => moveTo.InternalReset();
        public override void OnMapDraw(GraphicsNW graph) => moveTo.OnMapDraw(graph);
        public override ActionResult Run() => moveTo.Run();

        [Editor(typeof(PositionEditor), typeof(UITypeEditor))]
        [Description("Final destination")]
        public Vector3 Position
        {
            get => moveTo.Position;
            set => moveTo.Position = value;
        }

        [Description("Minimum distance between waypoints")]
        public int Filter { get; set; } = 90;

        [Description("Stop moving if True")]
        public bool IgnoreCombat
        {
            get => moveTo.IgnoreCombat;
            set => moveTo.IgnoreCombat = value;
        }

        [Description("Description of the target location (not necessary)")]
        public string Description { get; set; }

        public override void GatherInfos()
        {
            QMessageBox.ShowInfo("Place mark on the InGame's map and press OK");
            moveTo.Position = EntityManager.LocalPlayer.Player.MyFirstWaypoint.Position.Clone();
        }
    }
}