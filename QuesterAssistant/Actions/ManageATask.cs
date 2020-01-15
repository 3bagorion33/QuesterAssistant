using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
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
                if (Remaining == 0 && Duration == 0)
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
            bool Finder(Assignment a)
            {
                var def = a.Def.InternalName.FindPattern(Task) && !a.ReadyToComplete;
                if (def && Remaining > 0)
                    def &= RemainingRelation.Compare(a.Remaining, Remaining);
                if (def && Duration > 0)
                    def &= DurationRelation.Compare(a.Duration, Duration);
                return def;
            }

            Assignment assignment =
                EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.FirstOrDefault(Finder) ??
                new Assignment(IntPtr.Zero);
            if (!assignment.IsValid)
                return ActionResult.Completed;

            switch (Action)
            {
                case ModeDef.Cancel:
                    GameCommands.Execute($"ItemAssignmentCancelActiveAssignment {assignment.ID}");
                    break;
                case ModeDef.Complete:
                    GameCommands.Execute($"ItemAssignmentsCompleteNowById {assignment.ID}");
                    break;
            }
            Pause.Sleep(500);
            Logger.WriteLine($"{Action} task '{assignment}' ...");
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

        public enum ModeDef { Cancel, Complete }
    }
}