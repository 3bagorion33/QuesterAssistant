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
using QuesterAssistant.Classes.ItemFilter;

namespace QuesterAssistant.Actions
{
    public class AuctionSellItems : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => GetType().Namespace.Split(char.Parse("."))[0];
        public override string InternalDisplayName => ActionLabel;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        private List<InventorySlot> itemsToSell;
        private Dictionary<string, SearchResult> cachedSearch = new Dictionary<string, SearchResult>();

        protected override bool IntenalConditions
        {
            get
            {
                Debug.WriteLine(ActionLabel + ": IntenalConditions");
                if (Auction.GetRemainingPostings() <= 0)
                {
                    Logger.WriteLine("No available slot to sell...");
                    return false;
                }
                var bags = EntityManager.LocalPlayer.BagsItems;
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
                itemsToSell = bags.FindAll
                    (s => !s.Item.IsBound &&
                          !s.Item.IsItemFlagActive(ItemFlags.BoundToAccount) &&
                          !s.Item.IsItemFlagActive(ItemFlags.ProtectedItem) &&
                          ((MyItemFilterCore)ItemsFilter).IsMatch(s.Item));
                if (itemsToSell.Any())
                {
                    return true;
                }
                Logger.WriteLine("No items to sell.");
                return false;
            }
        }

        public override void InternalReset()
        {
            Debug.WriteLine(ActionLabel + ": InternalReset()");
            cachedSearch.Clear();
        }

        private sealed class SearchResult
        {
            public List<AuctionLot> Lots;
            public int OwnLotsCount;
        }

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ItemsFilter.Entries.Count == 0)
                    return new ActionValidity("List of items is empty!");

                if (PriceType == SellingPriceType.Fixed && PriceValue == 0)
                    return new ActionValidity("PriceValue should not be zero");

                if ((PriceType > SellingPriceType.Fixed) && ((PricePercent < 1) || (PricePercent > 199)))
                    return new ActionValidity("PricePercent should be a percent (1-199 range)");

                if (PriceStartingBid > 99)
                    return new ActionValidity("StartingBid must be in 0-99 range");

                return new ActionValidity();
            }
        }

        public override ActionResult Run()
        {
            if (!Interact.Auctions())
                return ActionResult.Fail;

            if (IntenalConditions)
            {
                foreach (var slot in itemsToSell)
                {
                    var itemToSell = slot.Item;
                    var itemPrice = GetActualPrice(itemToSell);
                    var iterationCount = GetIterationCount(itemToSell);
                    Debug.WriteLine(ActionLabel + ": Total Iteration = " + iterationCount);
                    for (int i = 0; i < iterationCount; i++)
                    {
                        Debug.WriteLine(ActionLabel + ": Iteration = " + i);
                        if (Auction.GetRemainingPostings() <= 0 || !itemToSell.IsValid)
                        {
                            goto Exit;
                        }
                        var itemCount = (StackSize > 0 && StackSize < itemToSell.Count) ? StackSize : itemToSell.Count;

                        Debug.WriteLine(ActionLabel + ": itemToSell.Count = " + itemToSell.Count);

                        var multiply = (PriceType == SellingPriceType.Fixed) ? 1 : (double)PricePercent / 100;
                        var buyoutPrice = Round((int)(Math.Max(itemPrice * multiply, PriceMinimum) * itemCount));
                        var startingBid = Round((int)((double)PriceStartingBid / 100 * itemPrice));

                        Logger.WriteLine($"Sell '{itemToSell.DisplayName}' {itemCount} of {itemToSell.Count} for {buyoutPrice}AD");
                        Auction.CreateLot(itemToSell, itemCount, buyoutPrice, startingBid, Duration);
                        cachedSearch[itemToSell.DisplayName].OwnLotsCount++;
                        Thread.Sleep(2500);
                    }
                }
            }
            Exit:
            if (Auction.IsAuctionFrameVisible())
                Auction.CloseAuctionFrame();
            return ActionResult.Completed;
        }

        private int GetIterationCount(Item item)
        {
            // Если размер стака не определен, продаем слот как есть. Размер стака в инвентаре и на ауке совпадают.
            if (StackSize == 0)
            {
                Debug.WriteLine(ActionLabel + ": return 1");
                return 1;
            }
            int ownLotsCount = AuctionSearch(item).OwnLotsCount;
            Debug.WriteLine(ActionLabel + ": ownLotsCount => " + ownLotsCount);
            var sellStacks = (int)SellStacks - ownLotsCount;
            Debug.WriteLine(ActionLabel + ": sellStacks => " + sellStacks);
            var stacksCount = item.Count / StackSize;
            Debug.WriteLine(ActionLabel + ": stacksCount => " + stacksCount);
            // Если ноль, то продаем все, разбивая по стакам. +1 нужно, чтобы продать неполный стак. Если итемы кончатся, вывалимся по IsValid
            if (SellStacks == 0)
            {
                Debug.WriteLine(ActionLabel + ": return stacksCount + 1");
                return (int)stacksCount + 1;
            }
            Debug.WriteLine(ActionLabel + ": return sellStacks");
            // Если сюда дошли, то выбираем минимум. Второе значение <=0, если уже продается заданное число стаков.
            return Math.Min(sellStacks, (int)stacksCount);
        }

        private uint GetActualPrice(Item item)
        {
            uint result = 0;
            Debug.WriteLine(ActionLabel + ": " + PriceType);
            if (PriceType == SellingPriceType.Fixed)
            {
                result = PriceValue;
            }
            if (PriceType > SellingPriceType.Fixed)
            {
                List<AuctionLot> availableLots = AuctionSearch(item).Lots;
                if (availableLots.Any())
                {
                    List<AuctionLot> validLots = new List<AuctionLot>();
                    if (PriceDetectionRange == PriceDetectionType.Full)
                    {
                        validLots = availableLots;
                    }
                    if (PriceDetectionRange == PriceDetectionType.NearStackSize)
                    {
                        uint range = 0;
                        while (!validLots.Any())
                        {
                            validLots = availableLots.FindAll(l => l.Items.First().Item.Count >= (StackSize - range) && l.Items.First().Item.Count <= (StackSize + range));
                            range++;
                            Debug.WriteLine($"{ActionLabel} : availableLots => {validLots.Count}");
                        }
                    }

                    Debug.WriteLine(ActionLabel + ": availableLots is not empty");
                    uint itemPrice = 0;
                    if (PriceType == SellingPriceType.Minimal)
                        itemPrice = PricePerItem(validLots.First());
                    if (PriceType == SellingPriceType.Average)
                        itemPrice = (uint)validLots.Average(x => PricePerItem(x));
                    if (PriceType == SellingPriceType.Median)
                        itemPrice = PricePerItem(validLots.ElementAt(validLots.Count / 2));
                    result = (PriceMinimum > itemPrice) ? PriceMinimum : itemPrice;
                }
                else
                {
                    result = PriceValue;
                }
            }
            Logger.WriteLine($"Best price for '{item.DisplayName}' is {result}AD");
            return result;
        }

        private SearchResult AuctionSearch(Item item)
        {
            if (cachedSearch.ContainsKey(item.DisplayName))
            {
                Debug.WriteLine(ActionLabel + ": using cachedSearch");
                return cachedSearch[item.DisplayName];
            }
            Debug.WriteLine(ActionLabel + ": try Auction search");
            Auction.LotsSearch(item.DisplayName);
            Thread.Sleep(2500);
            while (Auction.SearchWaiting)
                Thread.Sleep(250);
            var availableLots = Auction.AuctionLotList.Lots
                .FindAll(l => l.Price > 0 && l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .OrderBy(l => PricePerItem(l)).ToList();
            if (!cachedSearch.ContainsKey(item.DisplayName))
                cachedSearch.Add(item.DisplayName, new SearchResult()
                {
                    Lots = availableLots.FindAll(l => l.Owner != EntityManager.LocalPlayer.InternalName),
                    OwnLotsCount = availableLots.FindAll(l => l.Owner == EntityManager.LocalPlayer.InternalName).Count
                });
            return cachedSearch[item.DisplayName];
        }

        private uint PricePerItem(AuctionLot lot)
        {
            var price = lot.Price;
            var count = lot.Items.First().Item.Count;
            Debug.WriteLine(ActionLabel + ": Price = " + price + " / " + count);
            return (price > count) ? price / count : 1;
        }

        private int Round(int number)
        {
            // Если не задано, не округляем
            if (number < 10 || RoundDigits == 0)
            {
                return number;
            }
            // Определяем число разрядов
            int digitsCount = 1;
            double tmp = number;
            while (tmp > 10)
            {
                tmp/= 10;
                digitsCount++;
            }
            // Собственно округление
            int result = (int)Math.Floor(tmp * Math.Pow(10, RoundDigits - 1));

            if (RoundFilledBy == RoundType.Zero)
            {
                return (int)(result * Math.Pow(10, digitsCount - RoundDigits));
            }
            if (RoundFilledBy == RoundType.Nine)
            {
                return (int)(result * Math.Pow(10, digitsCount - RoundDigits)) - 1;
            }
            if (RoundFilledBy == RoundType.Last)
            {
                var mod = result % 10;
                for (int i = 0; i < (digitsCount - RoundDigits); i++)
                {
                    result = result * 10 + mod;
                }
                return result;
            }
            return result;
        }

        [Description("Item to sell filter"), Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemsFilter { get; set; } = new ItemFilterCore();

        public Auction.AuctionDuration Duration { get; set; } = Auction.AuctionDuration.Long;

        [Description("Fixed : using PriceValue\nMinimal, Average, Median : detecting current price on Auction")]
        public SellingPriceType PriceType { get; set; }

        [Description("Fixed price, used as default if not found in Auction")]
        public uint PriceValue { get; set; }

        [Description("Try to decrease range for more correct detection")]
        public PriceDetectionType PriceDetectionRange { get; set; }

        [Description("Minimum price for one item, used with Minimal, Average, Median if actual price less than this.")]
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

        [Description("Zero : price = 123000 | Nine : price = 122999 | Last : price = 122222")]
        public RoundType RoundFilledBy { get; set; }

        public enum RoundType
        {
            Zero,
            Nine,
            Last
        }

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
    }
}
