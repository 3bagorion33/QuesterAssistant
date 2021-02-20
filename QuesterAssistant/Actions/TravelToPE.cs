using Astral.Logic.Classes.Map;
using MyNW.Classes;
using System.Collections.Generic;
using Astral.Quester.Classes;
using Astral.Quester.Classes.Actions;
using Astral.Quester.Classes.Conditions;
using MyNW.Internals;
using QuesterAssistant.Classes;

namespace QuesterAssistant.Actions
{
    public class TravelToPE : Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        private Action travelToPe = new RemoteDialog
        {
            RemoteContact = "Remote_Teleport_Info",
            Dialogs = new List<string> { "OptionsList.SpecialDialog.Chult_2", "SpecialDialog.action_0" },
            PlayWhileConditionsAreOk = true,
            Conditions = new List<Condition>
            {
                new CurrentMap
                {
                    MapName = "Neverwinter_Protectors_Enclave",
                    Tested = Condition.Presence.NotEquel
                }
            }
        };

        public override ActionResult Run()
        {
            if (CheckMap && !travelToPe.ConditionsAreOK)
            {
                return ActionResult.Completed;
            }

            var result = travelToPe.Run();

            while (EntityManager.LocalPlayer.IsLoading)
                Pause.Sleep(500);

            return result;
        }

        public bool CheckMap { get; set; } = true;
    }
}
