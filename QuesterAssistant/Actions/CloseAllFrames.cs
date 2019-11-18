using Astral.Logic.Classes.Map;
using MyNW.Classes;
using MyNW.Internals;

namespace QuesterAssistant.Actions
{
    public class CloseAllFrames : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override bool IntenalConditions => true;
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            if (Game.IsCursorModeEnabled)
            {
                Game.ToggleCursorMode(false);
                System.Threading.Thread.Sleep(1000);
                return ActionResult.Completed;
            }
            return ActionResult.Skip;
        }
    }
}
