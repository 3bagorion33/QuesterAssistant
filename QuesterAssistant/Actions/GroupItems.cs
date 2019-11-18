using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.ItemFilter;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class GroupItems : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => GetType().Namespace.Split(char.Parse("."))[0];
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

        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            var bags = EntityManager.LocalPlayer.BagsItems;
            bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
            bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);

            bags.FindAll(s => ItemIdFilter.IsMatch(s.Item))
                .GroupBy(s => new { s.Item.ItemDef.InternalName, s.Item.Flags },
                    (key, group) => new { Key1 = key.InternalName, Key2 = key.Flags, Value = group.First() })
                .ToList()
                .ForEach(s => s.Value.Group());

            return ActionResult.Completed;
        }

        [Description("Items to group")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemIdFilter { get; set; } = new ItemFilterCore();
    }
}
