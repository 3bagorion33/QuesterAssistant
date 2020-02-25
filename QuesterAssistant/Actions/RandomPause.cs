using System.ComponentModel;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Actions
{
    public class RandomPause : Action
    {
        public override string ActionLabel => $"{GetType().Name} : {pause.Left.CheckZero(pause.TimeOut)} ms";
        public override string Category => Core.Category;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => string.Empty;
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        protected override ActionValidity InternalValidity
        {
            get
            {
                pause = new Pause(TimeOut);
                return new ActionValidity();
            }
        }

        private Pause pause;

        public RandomPause()
        {
            pause = new Pause(TimeOut);
        }

        public override ActionResult Run()
        {
            pause.Reset();
            pause.WaitingRandom();
            return ActionResult.Completed;
        }

        [Description("In msec")]
        [TypeConverter(typeof(PropertySorter))]
        public MinMaxPair<uint> TimeOut { get; set; } = new MinMaxPair<uint>(2000, 3000);
    }
}