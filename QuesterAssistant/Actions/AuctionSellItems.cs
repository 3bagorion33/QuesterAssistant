using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
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
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        protected override bool IntenalConditions => true;

        private IDictionary<string, IEnumerable<InventorySlot>> slotsGroupList;
        private double Multiply => PriceType == SellingPriceType.Fixed ? 1 : (double)PricePercent / 100;
        private bool isFollowedAccount;

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ItemsFilter.Entries.Count == 0)
                    return new ActionValidity("List of items is empty!");

                if (PriceValue == 0)
                    return new ActionValidity($"{nameof(PriceValue)} should not be zero");

                if (PriceType > SellingPriceType.Fixed && (PricePercent < 1 || PricePercent > 199))
                    return new ActionValidity($"{nameof(PricePercent)} should be a percent (1-199 range)");

                if (PriceValue < PriceMinimum)
                    return new ActionValidity($"{nameof(PriceValue)} must be greater than PriceMinimum");

                if (PriceStartingBid > 99)
                    return new ActionValidity($"{nameof(PriceStartingBid)} must be in 0-99 range");

                if (ActiveLots == ActiveLotType.Resell && (Tolerance <= 0 || Tolerance >= 1))
                {
                    return new ActionValidity($"{nameof(Tolerance)} must be greater than 0 and less then 1");
                }

                return new ActionValidity();
            }
        }

        private double GetActualPrice(Item item)
        {
            double result = 0;
            if (PriceType == SellingPriceType.Fixed)
                result = PriceValue;

            if (PriceType > SellingPriceType.Fixed)
            {
                var availableLots = new AuctionSearch(item.ItemDef, CheckInternalName).Result.Lots;
                if (availableLots.Any())
                {
                    var validLots = new List<AuctionSearch.SearchResult.Lot>();
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

                    double itemPrice = 0;
                    isFollowedAccount = !string.IsNullOrEmpty(OrientToAccount) && validLots.Exists(l => l.Owner.Contains(OrientToAccount));
                    if (!isFollowedAccount)
                    {
                        switch (PriceType)
                        {
                            case SellingPriceType.Minimal:
                                itemPrice = validLots.First().PricePerItem;
                                break;
                            case SellingPriceType.Average:
                                itemPrice = (uint) validLots.Average(x => x.PricePerItem);
                                break;
                            case SellingPriceType.Median:
                                itemPrice = validLots.ElementAt(validLots.Count / 2).PricePerItem;
                                break;
                            case SellingPriceType.Top3:
                                itemPrice = validLots.Count > 2 ? validLots.ElementAt(2).PricePerItem : PriceValue;
                                break;
                            case SellingPriceType.Top5:
                                itemPrice = validLots.Count > 4 ? validLots.ElementAt(4).PricePerItem : PriceValue;
                                break;
                        }
                    }
                    else
                    {
                        itemPrice = validLots.First(l => l.Owner.Contains(OrientToAccount)).PricePerItem;
                    }
                    result = itemPrice;
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
            Pause pause = new Pause(TimeOut);

            bool OpenFrame()
            {
                if (!Auction.IsAuctionFrameVisible() && !Interact.Auctions())
                    return false;
                AuctionSearch.RequestAuctionsForPlayer();
                return true;
            }
            
            if (ActiveLots != ActiveLotType.Keep && OpenFrame())
            {
                bool IsSellLotMatch(AuctionLot l)
                {
                    var item = l.Items.First().Item;
                    if (!ItemsFilter.IsMatch(item))
                        return false;
                    var actualPrice = Math.Max(GetActualPrice(item) * Multiply, PriceMinimum);
                    var lotPrice = (double) l.Price / item.Count;
                    return
                        l.BiddingInfo.CurrentBid == 0 &&
                        (
                            ActiveLots == ActiveLotType.Force ||
                            l.TimeLeft < (uint) Duration / 5 ||
                            item.Count == StackSize &&
                            (lotPrice * (1 - Tolerance) > actualPrice || (1 + Tolerance) * lotPrice < actualPrice)
                        );
                }

                Pause.Sleep(2000);
                while (Auction.AuctionSellList.Lots.Exists(IsSellLotMatch))
                {
                    var prevLotsCount = Auction.AuctionSellList.LotsCount;
                    var lot = Auction.AuctionSellList.Lots.Find(IsSellLotMatch);
                    Logger.WriteLine($"Collect '{lot.Items.First().Item.DisplayName}' with price {lot.Price}AD".CarryOnLength());
                    lot.Remove();
                    Pause.Sleep(2000);
                    if (Auction.AuctionSellList.LotsCount == prevLotsCount)
                        AuctionSearch.RequestAuctionsForPlayer();
                }
            }

            if (Auction.GetRemainingPostings() <= 0)
            {
                Logger.WriteLine("No slots available to place, skip...");
                goto Exit;
            }

            new GroupItems { ItemIdFilter = ItemsFilter }.Run();

            bool GetItemsToSell()
            {
                var bags = EntityManager.LocalPlayer.BagsItems;
                EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.Overflow).GetItems.ForEach(s => bags.Remove(s));
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.Currency).GetItems);
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
                var stacksToSell = (int)SellStacks - item.GetOwnLotsCount(StackSize);

                // Если размер стака не определен, а количество слотов не определено или имеется, продаем как есть.
                if (StackSize == 0 && (SellStacks == 0 || stacksToSell > 0))
                    return item.Count;
                // Если размер стака указан, а количество не лимитировано или имеется, возвращаем StackSize или 0, если нет нужного кол-ва
                if (StackSize > 0 && (SellStacks == 0 || stacksToSell > 0))
                    return StackSize <= item.Count ? StackSize : 0;

                return 0;
            }

            if (GetItemsToSell() && OpenFrame())
            {
                foreach (KeyValuePair<string, IEnumerable<InventorySlot>> slotsList in slotsGroupList)
                {
                    var item = slotsList.Value.First().Item;
                    var auctionSearch = new AuctionSearch(item.ItemDef);
                    auctionSearch.WriteLogMessage();
                    var itemPrice = GetActualPrice(item);
                    Logger.WriteLine($"Best price for '{item.DisplayName}' is {itemPrice}AD".CarryOnLength());
                    foreach (InventorySlot slot in slotsList.Value)
                    {
                        var itemToSell = slot.Item;
                        if (itemToSell.IsValid && itemToSell.ItemDef.InternalName == slotsList.Key)
                        {
                            uint itemCount;

                            while (itemToSell.IsValid &&
                                   itemToSell.ItemDef.InternalName == slotsList.Key &&
                                   (itemCount = GetItemsCount(itemToSell)) > 0)
                            {
                                if (Auction.GetRemainingPostings() <= 0)
                                    goto Exit;

                                itemPrice = GetActualPrice(itemToSell);

                                int buyoutPrice = (int) (itemPrice * Multiply * itemCount);
                                if (!isFollowedAccount && buyoutPrice != PriceValue)
                                    buyoutPrice = MathTools.Round(buyoutPrice, RoundDigits, RoundFilledBy);

                                buyoutPrice = MathTools.Max(buyoutPrice, (int) (PriceMinimum * itemCount));

                                int startingBid = MathTools.Round((int)((double)PriceStartingBid / 100 * buyoutPrice),
                                    RoundDigits, RoundFilledBy);

                                if ((VIP.Rank < 8 || VIP.ExpirationSecondsLeft == 0) &&
                                    EntityManager.LocalPlayer.Inventory.AstralDiamonds < (int) (startingBid.CheckZero(buyoutPrice) * 0.02))
                                {
                                    Logger.WriteLine($"Not enough AD to pay collateral value for '{itemToSell.DisplayName}'x{itemCount}".CarryOnLength());
                                    break;
                                }

                                pause.RandomWaiting();
                                Logger.WriteLine($"Sell '{itemToSell.DisplayName}' {itemCount} of {itemToSell.Count} for {buyoutPrice}AD".CarryOnLength());
                                Auction.CreateLot(itemToSell, itemCount, buyoutPrice, startingBid, Duration);

                                pause.Reset();
                                Pause.Sleep(1500);
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

        [Browsable(false), DefaultValue(0)]
        public uint TimeOutMin { get; set; }
        [Browsable(false), DefaultValue(0)]
        public uint TimeOutMax { get; set; }

        [Category("Interaction")]
        [Description("Keep : active lots count is determined by SellStacks | Resell : cancel active lots before")]
        public ActiveLotType ActiveLots { get; set; }
        [Category("Interaction")]
        [Description("Tolerance of actual Price to resell items")]
        public double Tolerance { get; set; } = 0.01;
        [Category("Interaction")]
        [Description("Leave Auction frame opening, improve cascade selling")]
        public bool DontCloseAuctionFrame { get; set; } = false;
        private MinMaxPair<uint> timeOut = new MinMaxPair<uint>(2000, 3000);
        [Category("Interaction")]
        [TypeConverter(typeof(PropertySorter))]
        [Description("Set min & max values of pause between interactions")]
        public MinMaxPair<uint> TimeOut
        {
            get
            {
                if (TimeOutMin > 0 && TimeOutMax > 0)
                {
                    timeOut = new MinMaxPair<uint>(TimeOutMin, TimeOutMax);
                    TimeOutMin = 0;
                    TimeOutMax = 0;
                }
                return timeOut;
            }
            set => timeOut = value;
        }

        [Category("Items")]
        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        [Description("Item to sell filter")]
        public ItemFilterCore ItemsFilter { get; set; } = new ItemFilterCore();
        [Category("Items")]
        [Description("Verify InternalName, uses DisplayName only if disabled")]
        public bool CheckInternalName { get; set; } = true;

        [Category("Lot settings")]
        public Auction.AuctionDuration Duration { get; set; } = Auction.AuctionDuration.Long;
        [Category("Lot settings")]
        [Description("Sell all stacks if zero")]
        public uint SellStacks { get; set; }
        [Category("Lot settings")]
        [Description("Sell slot as is if zero")]
        public uint StackSize { get; set; }

        [Category("Price settings")]
        [Description("Follow price of this account")]
        public string OrientToAccount { get; set; }
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
            Median = 3,
            Top3 = 4,
            Top5 = 5
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
