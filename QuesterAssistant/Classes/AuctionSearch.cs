﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        private static string CachedSearchFile => Path.Combine(Core.SettingsPath, "AuctionSearchCache.bin");
        private static readonly Astral.Classes.Timeout timeOut = new Astral.Classes.Timeout(0);
        private readonly ItemDef itemDef;
        private List<SearchResult> cachedSearch;

        private string loggerMessage;
        public SearchResult Result { get; }

        public AuctionSearch(ItemDef itemDef, bool checkInternalName = true)
        {
            this.itemDef = itemDef;
            Result = GetResult(checkInternalName);
        }
        
        private SearchResult GetResult(bool checkInternalName = true)
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

            cachedSearch = File.Exists(CachedSearchFile) ? BinFile.Load<List<SearchResult>>(CachedSearchFile) : new List<SearchResult>();
            var cachedValue = cachedSearch.Find(l => ResultFilter(l) && l.DateTime.Subtract(DateTime.Now).TotalHours > -1);

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
                Thread.Sleep(2500);
                while (Auction.SearchWaiting)
                    Thread.Sleep(250);
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
            BinFile.Save(cachedSearch, CachedSearchFile);
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
                public uint Price;
                public uint Count;
                public double PricePerItem => Price > Count ? Price / Count : 1;
            }

            public SearchResult(ItemDef item, List<AuctionLot> auctionLots)
            {
                DisplayName = item.DisplayName;
                InternalName = item.InternalName;
                Lots = new List<Lot>();
                foreach (var lot in auctionLots)
                {
                    Lots.Add(new Lot { Price = lot.Price, Count = lot.Items.First().Item.Count });
                }
                DateTime = DateTime.Now;
            }
        }
    }
}
