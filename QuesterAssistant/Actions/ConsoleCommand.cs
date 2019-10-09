using Astral.Logic.Classes.Map;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.ComponentModel;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class ConsoleCommand : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {GameCommand}";
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override string Category => Core.Category;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();

        [Description("Type ingame console command without starting /")]
        public string GameCommand { get; set; }

        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            GameCommands.Execute(GameCommand);
            return ActionResult.Completed;
        }
    }
}
