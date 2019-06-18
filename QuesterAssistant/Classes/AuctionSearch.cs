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
    internal static class AuctionSearch
    {
        private static string CachedSearchFile => Path.Combine(Core.SettingsPath, "AuctionSearchCache.bin");
        private static List<Result> cachedSearch;
        private static string loggerMessage;

        public static Result Get(Item item)
        {
            if (string.IsNullOrEmpty(item.DisplayName))
                return new Result(null, null);

            uint PricePerItem(AuctionLot lot)
            {
                var price = lot.Price;
                var count = lot.Items.First().Item.Count;
                return price > count ? price / count : 1;
            }
            cachedSearch = File.Exists(CachedSearchFile) ? BinFile.Load<List<Result>>(CachedSearchFile) : new List<Result>();
            var cachedValue = cachedSearch.Find(l => l.InternalName == item.ItemDef.InternalName && l.DateTime.Subtract(DateTime.Now).TotalHours > -1);

            if (cachedValue != null)
            {
                loggerMessage = $"Use cached search for '{cachedValue.DisplayName}' at {cachedValue.DateTime.GetDateTimeFormats('t').First()}"
                    .CarryOnLength();
                return cachedValue;
            }

            Logger.WriteLine($"Try to search actual price for '{item.DisplayName}'...".CarryOnLength());
            Auction.LotsSearch(item.DisplayName);
            Thread.Sleep(2500);
            while (Auction.SearchWaiting)
                Thread.Sleep(250);

            var availableLots = Auction.AuctionLotList.Lots
                .FindAll(l => l.Price > 0 && l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .OrderBy(PricePerItem).ToList();

            cachedSearch.AddOrReplace(l => l.InternalName == item.ItemDef.InternalName,
                new Result(item, availableLots.FindAll(l => l.OptionalData.OwnerHandle != EntityManager.LocalPlayer.AccountLoginUsername)));

            BinFile.Save(cachedSearch, CachedSearchFile);
            return cachedSearch.Find(l => l.InternalName == item.ItemDef.InternalName);
        }

        public static void WriteLogMessage()
        {
            if (loggerMessage != null) Logger.WriteLine(loggerMessage);
        }

        [Serializable]
        internal sealed class Result
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

            public Result(Item item, List<AuctionLot> auctionLots)
            {
                DisplayName = item.ItemDef.DisplayName;
                InternalName = item.ItemDef.InternalName;
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
