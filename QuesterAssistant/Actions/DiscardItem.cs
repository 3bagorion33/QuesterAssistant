using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading.Tasks;
using Astral;
using QuesterAssistant.Classes.ItemFilter;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class DiscardItem : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => nameof(Astral.Quester.Classes.Actions.DiscardItems) + "Ext";
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ItemIdFilter.Entries.Count == 0)
                {
                    return new ActionValidity("No filter option set.");
                }
                return new ActionValidity();
            }
        }

        public override void GatherInfos() {}
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        private void DiscardItems(InventorySlot slot)
        {
            slot.RemoveAll();
            Logger.WriteLine($"Discard '{slot.Item.ItemDef.DisplayName}' ...");
        }

        public override ActionResult Run()
        {
            bool Finder(InventorySlot x) =>
                !x.Item.ItemDef.CantDiscard &&
                SpecificGrade.Items.Exists(g => g == x.Item.ItemDef.Quality) &&
                ItemIdFilter.IsMatch(x.Item);

            List<Task> tasks = new List<Task>();
            Inventory.Slots.FindAll(Finder).ForEach(s => tasks.Add(Task.Factory.StartNew(() => DiscardItems(s))));
            Task.WaitAll(tasks.ToArray());
            Pause.Sleep(800);
            return ActionResult.Completed;
        }

        [Description("Items to discard")]
        [Editor(typeof(Astral.Quester.UIEditors.ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemIdFilter { get; set; } = new ItemFilterCore();

        [Description("Discard selected grade only")]
        [Editor(typeof(CheckedListBoxEditor<ItemQuality>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<ItemQuality> SpecificGrade { get; set; } = new CheckedListBoxSelector<ItemQuality>();

        [Editor(typeof(InventorySelectEditor), typeof(UITypeEditor))]
        public InventorySelect Inventory { get; set; } = new InventorySelect();
    }
}
