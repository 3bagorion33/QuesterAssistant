using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Threading;
using Astral;
using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Classes.Auction;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.ItemFilter;

namespace QuesterAssistant.Actions
{
    public class AuctionSellItems : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override string InternalDisplayName => ActionLabel;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }

        private IDictionary<string, IEnumerable<InventorySlot>> slotsGroupList;
        private double Multiply => PriceType == SellingPriceType.Fixed ? 1 : (double)PricePercent / 100;
        protected override bool IntenalConditions => true;

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ItemsFilter.Entries.Count == 0)
                    return new ActionValidity("List of items is empty!");

                if (TimeOutMax <= TimeOutMin)
                    return new ActionValidity("TimeOutMax must be greater than TimeOutMin");

                if (PriceValue == 0)
                    return new ActionValidity("PriceValue should not be zero");

                if (PriceType > SellingPriceType.Fixed && (PricePercent < 1 || PricePercent > 199))
                    return new ActionValidity("PricePercent should be a percent (1-199 range)");

                if (PriceValue < PriceMinimum)
                    return new ActionValidity("PriceValue must be greater than PriceMinimum");

                if (PriceStartingBid > 99)
                    return new ActionValidity("StartingBid must be in 0-99 range");

                return new ActionValidity();
            }
        }

        private uint GetActualPrice(Item item)
        {
            uint result = 0;
            if (PriceType == SellingPriceType.Fixed)
                result = PriceValue;

            if (PriceType > SellingPriceType.Fixed)
            {
                var availableLots = AuctionSearch.Get(item).Lots;
                if (availableLots.Any())
                {
                    var validLots = new List<AuctionSearch.Result.Lot>();
                    if (PriceDetectionRange == PriceDetectionType.Full)
                    {
                        validLots = availableLots;
                    }
                    if (PriceDetectionRange == PriceDetectionType.NearStackSize)
                    {
                        int range = 0;
                        while (!validLots.Any())
                        {
                            var stacksize = (int)(StackSize == 0 ? item.Count : StackSize);
                            var minrange = stacksize - range;
                            var maxrange = stacksize + range;
                            validLots = availableLots.FindAll(l => (int)l.Count >= minrange && (int)l.Count <= maxrange);
                            range++;
                        }
                    }

                    uint itemPrice = 0;
                    if (PriceType == SellingPriceType.Minimal)
                        itemPrice = validLots.First().PricePerItem;
                    if (PriceType == SellingPriceType.Average)
                        itemPrice = (uint)validLots.Average(x => x.PricePerItem);
                    if (PriceType == SellingPriceType.Median)
                        itemPrice = validLots.ElementAt(validLots.Count / 2).PricePerItem;
                    result = PriceMinimum > itemPrice ? PriceMinimum : itemPrice;
                }
                else
                {
                    result = PriceValue;
                }
            }
            return result;
        }

        public override ActionResult Run()
        {
            Random random = new Random((ActionID + DateTime.Now.ToString()).GetHashCode());
            Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(2500);

            void RandomWaiting()
            {
                Thread.Sleep(timeout.Left);
                timeout.ChangeTime(random.Next((int)TimeOutMin, (int)TimeOutMax));
            }
            void Waiting()
            {
                Thread.Sleep(2000);
            }
            bool OpenFrame()
            {
                if (!Auction.IsAuctionFrameVisible() && !Interact.Auctions())
                    return false;
                Auction.RequestAuctionsForPlayer();
                return true;
            }
            
            if (ActiveLots != ActiveLotType.Keep && OpenFrame())
            {
                bool IsSellLotMatch(AuctionLot l)
                {
                    var item = l.Items.First().Item;
                    var actualPrice = GetActualPrice(item) * Multiply;
                    var lotPrice = l.Price / item.Count;
                    return ItemsFilter.IsMatch(item) &&
                           (
                               ActiveLots == ActiveLotType.Force ||
                               l.TimeLeft < (uint)Duration / 5 ||
                               item.Count == StackSize &&
                               (lotPrice * 0.99 > actualPrice || 1.01 * lotPrice < actualPrice)
                           );
                }

                Waiting();
                Logger.WriteLine("Try to collect items for reselling...");
                while (Auction.AuctionSellList.Lots.Exists(IsSellLotMatch))
                {
                    var prevLotsCount = Auction.AuctionSellList.LotsCount;
                    Auction.AuctionSellList.Lots.Find(IsSellLotMatch).Remove();
                    Waiting();
                    if (Auction.AuctionSellList.LotsCount == prevLotsCount)
                        Auction.RequestAuctionsForPlayer();
                }
            }

            new GroupItems { ItemIdFilter = ItemsFilter }.Run();

            bool GetItemsToSell()
            {
                var bags = EntityManager.LocalPlayer.BagsItems;
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.FashionItems).GetItems);
                slotsGroupList = bags.FindAll
                    (s => !s.Item.IsBound &&
                          !s.Item.IsItemFlagActive(ItemFlags.BoundToAccount) &&
                          !s.Item.IsItemFlagActive(ItemFlags.ProtectedItem) &&
                          ItemsFilter.IsMatch(s.Item))
                          .GroupBy(s => s.Item.ItemDef.InternalName, (g, s) => new KeyValuePair<string, IEnumerable<InventorySlot>>(g, s))
                          .ToDictionary(s => s.Key, s => s.Value);
                if (slotsGroupList.Any())
                    return true;
                Logger.WriteLine("No items to sell.");
                return false;
            }

            uint GetItemsCount(Item item)
            {
                int ownLotsCount = Auction.AuctionSellList.Lots.Count(l => l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName);
                var stacksToSell = (int)SellStacks - ownLotsCount;

                // Если размер стака не определен, а количество слотов не определено или имеется, продаем как есть.
                if (StackSize == 0 && (SellStacks == 0 || stacksToSell > 0))
                    return item.Count;
                // Если размер стака указан, а количество не лимитировано или имеется, возвращаем StackSize или 0, если нет нужного кол-ва
                if (StackSize > 0 && (SellStacks == 0 || stacksToSell > 0))
                    return (StackSize <= item.Count) ? StackSize : 0;

                return 0;
            }

            if (GetItemsToSell() && OpenFrame())
            {
                foreach (KeyValuePair<string, IEnumerable<InventorySlot>> slotsList in slotsGroupList)
                {
                    foreach (InventorySlot slot in slotsList.Value)
                    {
                        var itemToSell = slot.Item;
                        if (itemToSell.IsValid && itemToSell.ItemDef.InternalName == slotsList.Key)
                        {
                            var itemPrice = GetActualPrice(itemToSell);
                            AuctionSearch.WriteLogMessage();
                            Logger.WriteLine($"Best price for '{itemToSell.DisplayName}' is {itemPrice}AD".CarryOnLenght());
                            uint itemCount;

                            while (itemToSell.IsValid && itemToSell.ItemDef.InternalName == slotsList.Key && (itemCount = GetItemsCount(itemToSell)) > 0)
                            {
                                if (Auction.GetRemainingPostings() <= 0)
                                    goto Exit;

                                var buyoutPrice = MathTools.Round((int)(Math.Max(itemPrice * Multiply, PriceMinimum) * itemCount),
                                    RoundDigits, RoundFilledBy);
                                var startingBid = MathTools.Round((int)((double)PriceStartingBid / 100 * buyoutPrice),
                                    RoundDigits, RoundFilledBy);

                                RandomWaiting();
                                Logger.WriteLine($"Sell '{itemToSell.DisplayName}' {itemCount} of {itemToSell.Count} for {buyoutPrice}AD".CarryOnLenght());
                                Auction.CreateLot(itemToSell, itemCount, buyoutPrice, startingBid, Duration);

                                timeout.Reset();
                                Thread.Sleep(1500);
                                slot.Group();
                            }
                        }
                    }
                }
            }

            Exit:
            if (Auction.IsAuctionFrameVisible() && !DontCloseAuctionFrame)
                Auction.CloseAuctionFrame();
            return ActionResult.Completed;
        }

        [Category("Interaction")]
        [Description("Keep : active lots count is determined by SellStacks | Resell : cancel active lots before")]
        public ActiveLotType ActiveLots { get; set; }
        [Category("Interaction")]
        [Description("Timeout range between lots operations, min value in ms")]
        public uint TimeOutMin { get; set; } = 2000;
        [Category("Interaction")]
        [Description("Timeout range between lots operations, max value in ms")]
        public uint TimeOutMax { get; set; } = 3000;
        [Category("Interaction")]
        [Description("Leave Auction frame opening, improve cascade selling")]
        public bool DontCloseAuctionFrame { get; set; } = false;

        [Category("Items")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        [Description("Item to sell filter")]
        public ItemFilterCore ItemsFilter { get; set; } = new ItemFilterCore();

        [Category("Lot settings")]
        public Auction.AuctionDuration Duration { get; set; } = Auction.AuctionDuration.Long;
        [Category("Lot settings")]
        [Description("Sell all stacks if zero")]
        public uint SellStacks { get; set; }
        [Category("Lot settings")]
        [Description("Sell slot as is if zero")]
        public uint StackSize { get; set; }

        [Category("Price settings")]
        [Description("Fixed : using PriceValue | Minimal, Average, Median : detecting current price on Auction")]
        public SellingPriceType PriceType { get; set; }
        [Category("Price settings")]
        [Description("Fixed price, used as default if not found in Auction")]
        public uint PriceValue { get; set; }
        [Category("Price settings")]
        [Description("Try to decrease range for more correct detection")]
        public PriceDetectionType PriceDetectionRange { get; set; }
        [Category("Price settings")]
        [Description("Minimum price for one item, used with Minimal, Average, Median if actual price less than this")]
        public uint PriceMinimum { get; set; }
        [Category("Price settings")]
        [Description("Used with Minimal, Average, Median price type")]
        public uint PricePercent { get; set; }
        [Category("Price settings")]
        [Description("Percent of buyout price")]
        public uint PriceStartingBid { get; set; }
        [Category("Price settings")]
        [Description("Round by number of digits, not work if zero")]
        public uint RoundDigits { get; set; }
        [Category("Price settings")]
        [Description("Zero : price = 123000 | Nine : price = 122999 | Last : price = 123333")]
        public MathTools.RoundType RoundFilledBy { get; set; }

        public enum SellingPriceType
        {
            Fixed = 0,
            Minimal = 1,
            Average = 2,
            Median = 3
        }
        public enum PriceDetectionType
        {
            Full,
            NearStackSize
        }
        public enum ActiveLotType
        {
            Keep = 0,
            Resell = 1,
            Force = 2
        }
    }
}
