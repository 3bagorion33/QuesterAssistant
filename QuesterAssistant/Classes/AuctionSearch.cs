using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Astral;
using DevExpress.Utils.Extensions;
using MyNW.Classes;
using MyNW.Classes.Auction;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Classes
{
    internal class AuctionSearch
    {
        private static readonly string CachedSearchFile = Path.Combine(Core.SettingsPath, "AuctionSearchCache.bin");
        private static readonly Astral.Classes.Timeout timeOut = new Astral.Classes.Timeout(0);
        private readonly ItemDef itemDef;
        private readonly bool checkInternalName;
        private List<SearchResult> cachedSearch;

        private string loggerMessage;
        public SearchResult Result { get; }

        public AuctionSearch(ItemDef itemDef, bool checkInternalName = true, uint cacheLifeTime = 10)
        {
            this.itemDef = itemDef;
            this.checkInternalName = checkInternalName;
            Result = GetResult(cacheLifeTime);
        }
        
        private SearchResult GetResult(uint cacheLifeTime = 10)
        {
            if (string.IsNullOrEmpty(itemDef.DisplayName))
                return new SearchResult(null, null);

            uint PricePerItem(AuctionLot lot)
            {
                var price = lot.Price;
                var count = lot.Items.First().Item.Count;
                return price > count ? price / count : 1;
            }

            bool ResultFilter(SearchResult l)
            {
                return checkInternalName
                    ? l.InternalName == itemDef.InternalName
                    : l.DisplayName == itemDef.DisplayName;
            }

            cachedSearch = File.Exists(CachedSearchFile) ? BinFileHelper.Load<List<SearchResult>>(CachedSearchFile) : new List<SearchResult>();
            var cachedValue = cachedSearch.Find(l => l.DateTime.Subtract(DateTime.Now).TotalMinutes > -cacheLifeTime && ResultFilter(l));

            if (cachedValue != null)
            {
                loggerMessage = $"Use cached search for '{cachedValue.DisplayName}' at {cachedValue.DateTime.GetDateTimeFormats('t').First()}"
                    .CarryOnLength();
                return cachedValue;
            }

            List<AuctionLot> availableLots;
            int searchTrying = 0;
            do
            {
                searchTrying++;
                Auction.LotsSearch(itemDef.DisplayName);
                Pause.Sleep(2500);
                while (Auction.SearchWaiting)
                    Pause.Sleep(250);
            }
            while (!(availableLots = Auction.AuctionLotList.Lots).Any() && searchTrying < 3);

            availableLots = availableLots
                .FindAll(l => l.Items.First().Item.ItemDef.DisplayName == itemDef.DisplayName && l.Price > 0)
                .OrderBy(PricePerItem).ToList();

            Logger.WriteLine($"Try to search actual price for '{itemDef.DisplayName}'... Result contains {availableLots.Count} items.".CarryOnLength());

            if (availableLots.Any())
            {
                var groupedLots = availableLots
                    .GroupBy(l => l.Items.First().Item.ItemDef.InternalName,
                        (g, l) => new KeyValuePair<string, IEnumerable<AuctionLot>>(g, l))
                    .ToDictionary(l => l.Key, l => l.Value.ToList());

                foreach (var lots in groupedLots)
                {
                    cachedSearch.AddOrReplace(l => l.InternalName == lots.Key,
                        new SearchResult(lots.Value.First().Items.First().Item.ItemDef,
                            lots.Value.FindAll(l =>
                                l.OptionalData.OwnerHandle != EntityManager.LocalPlayer.AccountLoginUsername)));
                }
            }
            else
            {
                cachedSearch.AddOrReplace(l => l.InternalName == itemDef.InternalName,
                    new SearchResult(itemDef, new List<AuctionLot>()));
            }
            BinFileHelper.Save(cachedSearch, CachedSearchFile);
            return cachedSearch.Find(ResultFilter) ?? new SearchResult(itemDef, new List<AuctionLot>());
        }

        public void WriteLogMessage()
        {
            if (loggerMessage != null) Logger.WriteLine(loggerMessage);
        }

        public static void RequestAuctionsForPlayer()
        {
            if (timeOut.IsTimedOut)
            {
                Auction.RequestAuctionsForPlayer();
                timeOut.ChangeTime(Pause.Random(1000, 2000));
                timeOut.Reset();
            }
        }

        public static int GetCharacterLotsCount(ItemDef itemDef, uint stackSize = 0)
        {
            bool Selector(AuctionLot l)
            {
                var i = l.Items[0].Item;
                return i.ItemDef.InternalName == itemDef.InternalName && (stackSize == 0 || i.Count == stackSize);
            }

            return Auction.AuctionSellList.Lots.Count(Selector);
        }

        public int GetAccountLotsCount(uint stackSize = 0)
        {
            bool Selector(AuctionLot l)
            {
                var i = l.Items[0].Item;
                return l.OptionalData.OwnerHandle == EntityManager.LocalPlayer.AccountLoginUsername &&
                       i.ItemDef.InternalName == itemDef.InternalName && 
                       (stackSize == 0 || i.Count == stackSize);
            }
            GetResult(1);
            return Auction.AuctionLotList.LotsCount == 0 ? 0 : Auction.AuctionLotList.Lots.Count(Selector);
        }

        public static int GetSellItemsCount(Item item)
        {
            return Auction.AuctionSellList.Lots
                .FindAll(l => l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .Sum(l => (int)l.Items.First().Item.Count);
        }

        [Serializable]
        internal sealed class SearchResult
        {
            public string DisplayName;
            public string InternalName;
            public List<Lot> Lots;
            public DateTime DateTime;

            [Serializable]
            public sealed class Lot
            {
                public string Owner;
                public uint Price;
                public uint Count;
                public double PricePerItem => (double) Price / Count;
            }

            public SearchResult(ItemDef item, List<AuctionLot> auctionLots)
            {
                DisplayName = item.DisplayName;
                InternalName = item.InternalName;
                Lots = new List<Lot>();
                foreach (var lot in auctionLots)
                {
                    Lots.Add(new Lot {Owner = lot.Owner, Price = lot.Price, Count = lot.Items.First().Item.Count});
                }
                DateTime = DateTime.Now;
            }
        }
    }
}
