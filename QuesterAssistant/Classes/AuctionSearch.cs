using System;
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

        public AuctionSearch(ItemDef itemDef)
        {
            this.itemDef = itemDef;
            Result = GetResult();
        }
        
        private SearchResult GetResult()
        {
            if (string.IsNullOrEmpty(itemDef.DisplayName))
                return new SearchResult(null, null);

            uint PricePerItem(AuctionLot lot)
            {
                var price = lot.Price;
                var count = lot.Items.First().Item.Count;
                return price > count ? price / count : 1;
            }
            cachedSearch = File.Exists(CachedSearchFile) ? BinFile.Load<List<SearchResult>>(CachedSearchFile) : new List<SearchResult>();
            var cachedValue = cachedSearch.Find(l => l.InternalName == itemDef.InternalName && l.DateTime.Subtract(DateTime.Now).TotalHours > -1);

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
            while (!(availableLots = Auction.AuctionLotList.Lots
                             .FindAll(l => l.Items.First().Item.ItemDef.InternalName == itemDef.InternalName))
                         .Any()
                     && searchTrying < 3);

            availableLots = availableLots.FindAll(l => l.Price > 0).OrderBy(PricePerItem).ToList();

            Logger.WriteLine($"Try to search actual price for '{itemDef.DisplayName}'... Result contains {availableLots.Count} items.".CarryOnLength());
            
            cachedSearch.AddOrReplace(l => l.InternalName == itemDef.InternalName,
                new SearchResult(itemDef, availableLots.FindAll(l => l.OptionalData.OwnerHandle != EntityManager.LocalPlayer.AccountLoginUsername)));

            BinFile.Save(cachedSearch, CachedSearchFile);
            return cachedSearch.Find(l => l.InternalName == itemDef.InternalName);
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
                public uint PricePerItem => Price > Count ? Price / Count : 1;
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
