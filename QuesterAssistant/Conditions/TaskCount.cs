using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral.Classes;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Conditions
{
    public class TaskCount : Condition
    {
        private Timeout refreshTo = new Timeout(1500);
        private Func<Assignment, bool> tasksCountFinder;
        private StatusDef status = StatusDef.Active;

        public override void Reset() { }

        public TaskCount()
        {
            SetTasksCountMethod();
        }

        public override string TestInfos => $"{Task} {Status} tasks : {TasksCount()}";
        public override string ToString() => $"{Task} {Status} tasks {Sign} to {Value}";

        private int TasksCount() =>
            EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.Count(tasksCountFinder);

        private void SetTasksCountMethod()
        {
            switch (Status)
            {
                case StatusDef.All:
                    tasksCountFinder = AllTasksCount;
                    break;
                case StatusDef.Active:
                    tasksCountFinder = ActiveTasksCount;
                    break;
                case StatusDef.Completed:
                    tasksCountFinder = CompletedTasksCount;
                    break;
            }
        }

        private bool AllTasksCount(Assignment a) =>
            (!OnlyProfessionsTasks || a.Def.Category > 1) &&
            a.Def.InternalName.FindPattern(Task);

        private bool ActiveTasksCount(Assignment a) =>
            (!OnlyProfessionsTasks || a.Def.Category > 1) &&
            !a.ReadyToComplete &&
            a.Def.InternalName.FindPattern(Task);

        private bool CompletedTasksCount(Assignment a) =>
            (!OnlyProfessionsTasks || a.Def.Category > 1) &&
            a.ReadyToComplete &&
            a.Def.InternalName.FindPattern(Task);

        public override bool IsValid
        {
            get
            {
                if (refreshTo.IsTimedOut)
                {
                    EntityManager.LocalPlayer.Player.RefreshAssignments();
                    refreshTo.Reset();
                }

                int num = TasksCount();
                switch (Sign)
                {
                    case Relation.Equal:
                        return num == Value;
                    case Relation.NotEqual:
                        return num != Value;
                    case Relation.Inferior:
                        return num < Value;
                    case Relation.Superior:
                        return num > Value;
                    default:
                        return false;
                }
            }
        }

        public StatusDef Status
        {
            get => status;
            set
            {
                status = value;
                SetTasksCountMethod();
            }
        }

        public Relation Sign { get; set; }
        public int Value { get; set; }
        public bool OnlyProfessionsTasks { get; set; } = true;

        [Description("Can use * character at the start and end as jocker")]
        [Editor(typeof(ActiveTaskEditor), typeof(UITypeEditor))]
        public string Task { get; set; } = "*";

        public enum StatusDef
        {
            All,
            Active,
            Completed
        }
    }
}