using System.Linq;
using MyNW.Classes;
using MyNW.Internals;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class ItemEx
    {
        public static int GetOwnLotsCount(this Item item)
        {
            return Auction.AuctionSellList.Lots
                .Count(l => l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName);
        }

        public static int GetSellItemsCount(this Item item)
        {
            return Auction.AuctionSellList.Lots
                .FindAll(l => l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .Sum(l => (int) l.Items.First().Item.Count);
        }
    }
}
