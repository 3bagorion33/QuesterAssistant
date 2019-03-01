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

namespace QuesterAssistant.Classes
{
    internal static class AuctionSearch
    {
        private static string CachedSearchFile => Path.Combine(Core.SettingsPath, "AuctionSearchCache.bin");
        private static List<Result> cachedSearch;

        internal static Result Get(Item item)
        {
            uint PricePerItem(AuctionLot lot)
            {
                var price = lot.Price;
                var count = lot.Items.First().Item.Count;
                return (price > count) ? price / count : 1;
            }
            if (File.Exists(CachedSearchFile))
                cachedSearch = BinFile.Load<List<Result>>(CachedSearchFile);
            else
                cachedSearch = new List<Result>();

            var cachedValue = cachedSearch.Find(l => (l.DisplayName == item.DisplayName) && (l.DateTime.Subtract(DateTime.Now).Hours > -1));

            if (cachedValue != null)
            {
                Logger.WriteLine($"Use cached search for '{cachedValue.DisplayName}' at {cachedValue.DateTime.GetDateTimeFormats('t').First()}");
                return cachedValue;
            }

            Logger.WriteLine($"Try to search actual price for '{item.DisplayName}'...");
            Auction.LotsSearch(item.DisplayName);
            Thread.Sleep(2500);
            while (Auction.SearchWaiting)
                Thread.Sleep(250);

            var availableLots = Auction.AuctionLotList.Lots
                .FindAll(l => l.Price > 0 && l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .OrderBy(l => PricePerItem(l)).ToList();

            cachedSearch.AddOrReplace(l => l.DisplayName == item.DisplayName,
                new Result(item.DisplayName, availableLots.FindAll(l => l.Owner != EntityManager.LocalPlayer.InternalName)));

            BinFile.Save(cachedSearch, CachedSearchFile);
            return cachedSearch.Find(l => l.DisplayName == item.DisplayName);
        }

        [Serializable]
        internal sealed class Result
        {
            public string DisplayName;
            public List<Lot> Lots;
            public int OwnLotsCount => Auction.AuctionSellList.Lots.FindAll(l => l.Items.First().Item.DisplayName == DisplayName).Count;
            public DateTime DateTime;

            [Serializable]
            internal sealed class Lot
            {
                public uint Price;
                public uint Count;
                public uint PricePerItem => (Price > Count) ? (Price / Count) : 1;
            }

            internal Result(string displayName, List<AuctionLot> auctionLots)
            {
                DisplayName = displayName;
                Lots = new List<Lot>();
                foreach (var lot in auctionLots)
                {
                    Lots.Add(new Lot() { Price = lot.Price, Count = lot.Items.First().Item.Count });
                }
                DateTime = DateTime.Now;
            }
        }
    }
}
