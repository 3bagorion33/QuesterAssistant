using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Classes.ItemFilter;
using QuesterAssistant.UIEditors;
using MyNW.Patchables.Enums;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class DiscardItem : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => GetType().Namespace.Split(char.Parse("."))[0];
        public override bool NeedToRun => true;
        public override string InternalDisplayName => ActionLabel;
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
            EntityManager.LocalPlayer.BagsItems.FindAll
                (x => !x.Item.ItemDef.CantDiscard &&
                SpecificGrade.Items.Exists(g => g == x.Item.ItemDef.Quality) &&
                ((MyItemFilterCore)ItemIdFilter).IsMatch(x.Item)).ForEach(Interact.DiscardItem);
            return ActionResult.Completed;
        }

        [Description("Items to discard")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemIdFilter { get; set; } = new ItemFilterCore();

        [Description("Discard selected grade only")]
        [Editor(typeof(CheckedListBoxEditor<ItemQuality>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<ItemQuality> SpecificGrade { get; set; } = new CheckedListBoxSelector<ItemQuality>();
    }
}
