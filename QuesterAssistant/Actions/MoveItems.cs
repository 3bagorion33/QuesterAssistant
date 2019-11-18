using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.ItemFilter;
using QuesterAssistant.UIEditors;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class MoveItems : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : to {DestinationBag}";
        public override string Category => GetType().Namespace.Split(char.Parse("."))[0];
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();

        protected override bool IntenalConditions => 
            ItemsToMove.Entries.Any() && SourceBags.Items.Any() && DestinationBag != InvBagIDs.None;

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (!ItemsToMove.Entries.Any())
                    return new ActionValidity($"{nameof(ItemsToMove)} empty!");

                if (!SourceBags.Items.Any())
                    return new ActionValidity($"Need to choose any of {nameof(SourceBags)}!");

                if (DestinationBag == InvBagIDs.None)
                    return new ActionValidity($"Need to choose {nameof(DestinationBag)}!");

                return new ActionValidity();
            }
        }

        public override void OnMapDraw(GraphicsNW graph) {}
        public override void InternalReset(){}
        public override void GatherInfos(){}

        public override ActionResult Run()
        {
            var bags = new List<InventorySlot>();
            SourceBags.Items.ForEach(i => bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(i).GetItems));
            var items = bags.FindAll(s => ItemsToMove.IsMatch(s.Item));
            if (!items.Any())
            {
                Logger.WriteLine("No items found to move!");
                return ActionResult.Skip;
            }
            var destBag = EntityManager.LocalPlayer.GetInventoryBagById(DestinationBag);

            foreach (var slot in items)
            {
                if (destBag.MaxSlots - destBag.FilledSlots == 0)
                {
                    Logger.WriteLine($"{DestinationBag} is filled!");
                    return ActionResult.Completed;
                }
                Logger.WriteLine($"Move '{slot.Item.DisplayName}' to {DestinationBag}");
                var count = MathTools.Min(CountToMove.CheckZero(slot.Item.Count), slot.Item.Count);
                slot.Move(DestinationBag, count);
                Pause.RandomSleep(200, 300);
            }
            
            return ActionResult.Completed;
        }

        [Description("Items to move")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemsToMove { get; set; } = new ItemFilterCore();

        [Description("Move all if zero")]
        public uint CountToMove { get; set; } = 0;

        [Description("Choose source bags for search")]
        [Editor(typeof(CheckedListBoxEditor<InvBagIDs>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<InvBagIDs> SourceBags { get; set; } = new CheckedListBoxSelector<InvBagIDs>();

        [Description("Choose destination bag")]
        public InvBagIDs DestinationBag { get; set; } = InvBagIDs.None;
    }
}
