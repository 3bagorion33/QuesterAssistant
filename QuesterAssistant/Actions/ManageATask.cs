using System.ComponentModel;
using System.Drawing.Design;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using Astral.Quester.Forms;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Actions
{
    public class ManageATask : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Action} [{Task}]";
        public override string Category => "Tasks";
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        protected override bool IntenalConditions => true;

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (string.IsNullOrEmpty(Task))
                    return new ActionValidity("Task not set.");
                if (Action != ModeDef.Collect && Remaining == 0 && Duration == 0)
                    return new ActionValidity($"Need to determine {nameof(Remaining)} of {nameof(Duration)} conditions");
                return new ActionValidity();
            }
        }

        public override void GatherInfos()
        {
            string activeTask = GetAnId.GetActiveTask();
            if (!string.IsNullOrEmpty(activeTask))
            {
                Task = activeTask;
            }
        }

        public override ActionResult Run()
        {
            bool Finder(Assignment a) =>
                a.Def.InternalName.FindPattern(Task) &&
                (Action == ModeDef.Collect ? 
                    a.ReadyToComplete : 
                    !a.ReadyToComplete && 
                    (Remaining == 0 || RemainingRelation.Compare(a.Remaining, Remaining)) && 
                    (Duration == 0 || DurationRelation.Compare(a.Duration, Duration)));

            var assignments =
                EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.FindAll(Finder);
            if (assignments.Count == 0)
                return ActionResult.Completed;

            switch (Action)
            {
                case ModeDef.Cancel:
                    GameCommands.Execute($"ItemAssignmentCancelActiveAssignment {assignments[0].ID}");
                    break;
                case ModeDef.Complete:
                    GameCommands.Execute($"ItemAssignmentsCompleteNowById {assignments[0].ID}");
                    break;
                case ModeDef.Collect:
                    EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ItemAssignmentCollectAllRewards(assignments);
                    Logger.WriteLine($"{Action} all ready tasks '{Task}' ...");
                    Pause.Sleep(500);
                    return ActionResult.Completed;
            }
            Logger.WriteLine($"{Action} task '{assignments[0]}' ...");
            Pause.RandomSleep(300, 500);
            return ActionResult.Running;
        }

        public ModeDef Action { get; set; } = ModeDef.Complete;

        [Description("Can use * character at the start and end as jocker")]
        [Editor(typeof(ActiveTaskEditor), typeof(UITypeEditor))]
        public string Task { get; set; }

        [Description("in sec")]
        public uint Remaining { get; set; } = 0;
        public Condition.Relation RemainingRelation { get; set; } = Condition.Relation.Equal;

        [Description("in sec")]
        public uint Duration { get; set; } = 0;
        public Condition.Relation DurationRelation { get; set; } = Condition.Relation.Equal;

        public enum ModeDef { Cancel, Complete, Collect }
    }
}