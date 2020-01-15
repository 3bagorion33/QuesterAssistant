using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using MyNW.Classes;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Classes.ItemFilter;
using QuesterAssistant.UIEditors;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;

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

        public override ActionResult Run()
        {
            bool Finder(InventorySlot x)
            {
                var f1 = !x.Item.ItemDef.CantDiscard;
                var f2 = SpecificGrade.Items.Exists(g => g == x.Item.ItemDef.Quality);
                var f3 = ItemIdFilter.IsMatch(x.Item);

                return f1 && f2 && f3;
            }

            Inventory.Slots.FindAll(Finder).ForEach(Interact.DiscardItem);
            return ActionResult.Completed;
        }

        [Description("Items to discard")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemIdFilter { get; set; } = new ItemFilterCore();

        [Description("Discard selected grade only")]
        [Editor(typeof(CheckedListBoxEditor<ItemQuality>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<ItemQuality> SpecificGrade { get; set; } = new CheckedListBoxSelector<ItemQuality>();

        [Editor(typeof(InventorySelectEditor), typeof(UITypeEditor))]
        public InventorySelect Inventory { get; set; } = new InventorySelect();
    }
}
