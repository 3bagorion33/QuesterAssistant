using System;
using System.ComponentModel;
using System.Drawing.Design;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;

namespace QuesterAssistant.Conditions
{
    public class TaskMoraleAndLevel : Condition
    {
        private Definition Definition =>
            Professions.AllTasks.Find(d => d.InternalName == Task) ?? new Definition(IntPtr.Zero);
        private uint Cost => Professions2.TaskIsOrder(Task) ? Definition.ForceCompleteCost : 0;
        private uint RequiredLevel => Definition.Requirements.RequiredNumericValue - 5;
        private int ActualLevel =>
            EntityManager.LocalPlayer.Inventory.GetNumericCount(Definition.Requirements.RequiredNumericItem.InternalName);

        public override void Reset() { }

        public override bool IsValid =>
            Definition.IsValid &&
            (!CheckLevel || RequiredLevel <= ActualLevel) &&
            (IsMoraleEnough ? Definition.ForceCompleteCost <= Professions2.Morale : Definition.ForceCompleteCost > Professions2.Morale);

        public override string TestInfos => 
            $"[{Definition}] task:\n" +
            $"Cost is {Cost} morale.\n\t" +
            $"Character has {Professions2.Morale}.\n" +
            $"Min required profession level is {RequiredLevel}.\n\t" +
            $"Character has {ActualLevel}.";
        public override string ToString() => $"[{Definition}] task costs {Cost} morale";

        [Editor(typeof(ActiveTaskEditor), typeof(UITypeEditor))]
        public string Task { get; set; } = string.Empty;
        [Description("Includes profession required level")]
        public bool CheckLevel { get; set; } = true;
        public bool IsMoraleEnough { get; set; } = true;
    }
}