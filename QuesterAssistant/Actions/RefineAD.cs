using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using MyNW.Internals;
using System;

namespace QuesterAssistant
{
    public class RefineAD : Astral.Quester.Classes.Action
    {
        // Properties
        public override string ActionLabel => "RefineAD";
        public override string Category => "QuesterAssistant";
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => "RefineAD";
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

        // Methods
        public override void GatherInfos() {}
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        public override ActionResult Run()
        {
            EntityManager.LocalPlayer.RefineAstralDiamonds();
            return ActionResult.Completed;
        }
    }
}

