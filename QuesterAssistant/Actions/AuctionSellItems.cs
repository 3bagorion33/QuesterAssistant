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

        private List<InventorySlot> itemsToSell;
        private double Multiply => (PriceType == SellingPriceType.Fixed) ? 1 : (double)PricePercent / 100;

        protected override bool IntenalConditions
        {
            get
            {
                var bags = EntityManager.LocalPlayer.BagsItems;
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
                itemsToSell = bags.FindAll
                    (s => !s.Item.IsBound &&
                          !s.Item.IsItemFlagActive(ItemFlags.BoundToAccount) &&
                          !s.Item.IsItemFlagActive(ItemFlags.ProtectedItem) &&
                          ItemsFilter.IsMatch(s.Item));
                if (itemsToSell.Any() || Auction.AuctionSellList.Lots.Exists(IsSellLotMatch))
                {
                    return true;
                }
                Logger.WriteLine("No items to sell.");
                return false;
            }
        }

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ItemsFilter.Entries.Count == 0)
                    return new ActionValidity("List of items is empty!");

                if (PriceValue == 0)
                    return new ActionValidity("PriceValue should not be zero");

                if ((PriceType > SellingPriceType.Fixed) && ((PricePercent < 1) || (PricePercent > 199)))
                    return new ActionValidity("PricePercent should be a percent (1-199 range)");

                if (PriceStartingBid > 99)
                    return new ActionValidity("StartingBid must be in 0-99 range");

                if (TimeOutMax <= TimeOutMin)
                    return new ActionValidity("TimeOutMax must be greater than TimeOutMin");

                return new ActionValidity();
            }
        }

        private bool IsSellLotMatch(AuctionLot l)
        {
            var item = l.Items.First().Item;
            return ItemsFilter.IsMatch(item) &&
                (
                    (l.TimeLeft < ((uint)Duration / 5)) ||
                    ((item.Count == StackSize) && ((l.Price / item.Count * 0.99) > (GetActualPrice(item) * Multiply)))
                );
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
                            var stacksize = (int)((StackSize == 0) ? item.Count : StackSize);
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
                    result = (PriceMinimum > itemPrice) ? PriceMinimum : itemPrice;
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
            var random = new Random();

            void RandomWaiting()
            {
                Thread.Sleep(random.Next((int)TimeOutMin, (int)TimeOutMax));
            }

            void Waiting()
            {
                Thread.Sleep(2000);
            }

            if (!Interact.Auctions())
                return ActionResult.Fail;
            Auction.RequestAuctionsForPlayer();
            GameCommands.Execute("GenSendMessage Auction_Myconsignments_Tabbutton Clicked");
            
            if (ActiveLots == ActiveLotType.Resell)
            {
                Waiting();
                Logger.WriteLine("Collect items for reselling...");
                while (Auction.AuctionSellList.Lots.Exists(IsSellLotMatch))
                {
                    Auction.AuctionSellList.Lots.Find(IsSellLotMatch).Remove();
                    Waiting();
                }
            }

            if (IntenalConditions)
            {
                int GetIterationCount(Item item)
                {
                    // Если размер стака не определен, продаем слот как есть. Размер стака в инвентаре и на ауке совпадают.
                    if (StackSize == 0)
                        return 1;
                    int ownLotsCount = AuctionSearch.Get(item).OwnLotsCount;
                    var sellStacks = (int)SellStacks - ownLotsCount;
                    var stacksCount = item.Count / StackSize;
                    // Если ноль, то продаем все, разбивая по стакам. +1 нужно, чтобы продать неполный стак. Если итемы кончатся, вывалимся по IsValid
                    if (SellStacks == 0)
                        return (int)stacksCount + 1;
                    // Если сюда дошли, то выбираем минимум. Второе значение <=0, если уже продается заданное число стаков.
                    return Math.Min(sellStacks, (int)stacksCount);
                }

                foreach (var slot in itemsToSell)
                {
                    var itemToSell = slot.Item;
                    var itemPrice = GetActualPrice(itemToSell);
                    Logger.WriteLine(AuctionSearch.LoggerMessage);
                    Logger.WriteLine($"Best price for '{itemToSell.DisplayName}' is {itemPrice}AD");
                    var iterationCount = GetIterationCount(itemToSell);
                    for (int i = 0; i < iterationCount; i++)
                    {
                        if (Auction.GetRemainingPostings() <= 0 || !itemToSell.IsValid)
                        {
                            goto Exit;
                        }
                        var itemCount = (StackSize > 0 && StackSize < itemToSell.Count) ? StackSize : itemToSell.Count;

                        var buyoutPrice = MathTools.Round((int)(Math.Max(itemPrice * Multiply, PriceMinimum) * itemCount),
                            RoundDigits, RoundFilledBy);
                        var startingBid = MathTools.Round((int)((double)PriceStartingBid / 100 * itemPrice),
                            RoundDigits, RoundFilledBy);

                        Logger.WriteLine($"Sell '{itemToSell.DisplayName}' {itemCount} of {itemToSell.Count} for {buyoutPrice}AD");
                        var count = Auction.GetRemainingPostings();
                        Auction.CreateLot(itemToSell, itemCount, buyoutPrice, startingBid, Duration);
                        RandomWaiting();
                    }
                }
            }

            Exit:
            if (Auction.IsAuctionFrameVisible())
                Auction.CloseAuctionFrame();
            return ActionResult.Completed;
        }

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        [Description("Item to sell filter")]
        public ItemFilterCore ItemsFilter { get; set; } = new ItemFilterCore();

        public Auction.AuctionDuration Duration { get; set; } = Auction.AuctionDuration.Long;

        [Description("Keep : active lots count is determined by SellStacks | Resell : cancel active lots before")]
        public ActiveLotType ActiveLots { get; set; }

        [Description("Fixed : using PriceValue | Minimal, Average, Median : detecting current price on Auction")]
        public SellingPriceType PriceType { get; set; }

        [Description("Fixed price, used as default if not found in Auction")]
        public uint PriceValue { get; set; }

        [Description("Try to decrease range for more correct detection")]
        public PriceDetectionType PriceDetectionRange { get; set; }

        [Description("Minimum price for one item, used with Minimal, Average, Median if actual price less than this")]
        public uint PriceMinimum { get; set; }

        [Description("Used with Minimal, Average, Median price type")]
        public uint PricePercent { get; set; }

        [Description("Percent of buyout price")]
        public uint PriceStartingBid { get; set; }

        [Description("Sell all stacks if zero")]
        public uint SellStacks { get; set; }

        [Description("Sell slot as is if zero")]
        public uint StackSize { get; set; }

        [Description("Round by number of digits, not work if zero")]
        public uint RoundDigits { get; set; }

        [Description("Zero : price = 123000 | Nine : price = 122999 | Last : price = 123333")]
        public MathTools.RoundType RoundFilledBy { get; set; }

        [Description("Timeout range between lots operations, min value in ms")]
        public uint TimeOutMin { get; set; } = 2000;
        [Description("Timeout range between lots operations, max value in ms")]
        public uint TimeOutMax { get; set; } = 3000;

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
            Resell = 1
        }
    }
}
