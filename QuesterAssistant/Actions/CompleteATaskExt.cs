using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Forms;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Actions
{
    public class CompleteATaskExt : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Task}";
        public override string Category => "Tasks";
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        protected override bool IntenalConditions => true;

        protected override ActionValidity InternalValidity =>
            string.IsNullOrEmpty(Task)
                ? new ActionValidity("Task not set.")
                : new ActionValidity();

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
            Assignment assignment =
                EntityManager.LocalPlayer.Player.ItemAssignmentPersistedData.ActiveAssignments.FirstOrDefault(a =>
                    a.Def.InternalName.FindPattern(Task) && !a.ReadyToComplete && a.Remaining > 30) ?? new Assignment(IntPtr.Zero);
            if (!assignment.IsValid)
                return ActionResult.Completed;
            GameCommands.Execute("ItemAssignmentsCompleteNowById " + assignment.ID);
            Pause.RandomSleep(500, 1000);
            Logger.WriteLine($"Complete task '{assignment}' ...");
            return ActionResult.Running;
        }

        [Description("Can use * character at the start and end as jocker")]
        [Editor(typeof(ActiveTaskEditor), typeof(UITypeEditor))]
        public string Task { get; set; }
    }
}