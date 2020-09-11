using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes;

namespace QuesterAssistant.Actions
{
    public class CloseAllFrames : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Value}";
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
            Pause.Sleep(1000);
            return ActionResult.Completed;
        }

        public bool Value { get; set; } = true;
    }
}
