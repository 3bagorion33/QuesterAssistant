using Astral.Logic.Classes.Map;
using MyNW.Classes;
using MyNW.Internals;

namespace QuesterAssistant.Actions
{
    public class RefineAD : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => string.Empty;
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

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

