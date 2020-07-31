using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
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
    public class ManageATask : Action
    {
        private List<Assignment> Assignments =>
            EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.FindAll(Finder);
        private Assignment FirstAssignment => Assignments[0];
        public override string ActionLabel => $"{GetType().Name} : {Action} [{ProfessionsHelper.GetDisplayName(Task)}]";
        public override string Category => "Tasks";
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }

        protected override bool IntenalConditions =>
            Action != ModeDef.Complete || 
            Assignments.Count > 0 && FirstAssignment.Def.ForceCompleteCost <= Professions2.Morale;

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

        private bool Finder(Assignment a) =>
            a.Def.InternalName.FindPattern(Task) &&
            (Action == ModeDef.Collect ?
                a.ReadyToComplete :
                !a.ReadyToComplete &&
                (Remaining == 0 || RemainingRelation.Compare(a.Remaining, Remaining)) &&
                (Duration == 0 || DurationRelation.Compare(a.Duration, Duration)));

        public override ActionResult Run()
        {
            EntityManager.LocalPlayer.Player.RefreshAssignments();
            if (Assignments.Count == 0)
                return ActionResult.Completed;

            if (EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.Any(a =>
                a.Def.InternalName.FindPattern(Task) &&
                !a.ReadyToComplete &&
                a.Duration == 5))
            {
                Pause.Sleep(6000);
                return ActionResult.Running;
            }

            switch (Action)
            {
                case ModeDef.Cancel:
                    Logger.WriteLine($"{Action} task '{FirstAssignment}' ...");
                    GameCommands.Execute($"ItemAssignmentCancelActiveAssignment {FirstAssignment.ID}");
                    break;
                case ModeDef.Complete:
                    if (FirstAssignment.Def.ForceCompleteCost > Professions2.Morale)
                        return ActionResult.Completed;
                    Logger.WriteLine($"{Action} task '{FirstAssignment}' ...");
                    GameCommands.Execute($"ItemAssignmentsCompleteNowById {FirstAssignment.ID}");
                    break;
                case ModeDef.Collect:
                    Logger.WriteLine($"{Action} all ready tasks '{Task}' ...");
                    EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ItemAssignmentCollectAllRewards(Assignments);
                    break;
            }
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