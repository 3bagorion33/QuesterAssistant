using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using System;
using System.ComponentModel;

namespace QuesterAssistant
{
    public class PathFinding : Astral.Quester.Classes.Action
    {
        // Properties
        public override string ActionLabel => "PathFinding: " + this.Value;
        public override string Category => "QuesterAssistant";
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => "PathFinding";
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

        public PStat Value { get; set; }

        public enum PStat
        {
            Enabled,
            Disabled
        }

        // Methods
        public override void GatherInfos() {}
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        public PathFinding()
        {
            this.Value = PStat.Enabled;
        }

        public override ActionResult Run()
        {
            switch (this.Value)
            {
                case PStat.Enabled:
                    API.CurrentSettings.UsePathfinding3 = true;
                    break;
                case PStat.Disabled:
                    API.CurrentSettings.UsePathfinding3 = false;
                    break;
                default:
                    return ActionResult.Fail;
            }
            return ActionResult.Completed;
        }
    }
}

