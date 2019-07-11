using System.Linq;
using MyNW.Classes;
using MyNW.Classes.Auction;
using MyNW.Internals;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class ItemEx
    {
        public static int GetOwnLotsCount(this Item item, uint stackSize = 0)
        {
            bool Selector(AuctionLot l)
            {
                var i = l.Items.First().Item;
                return i.ItemDef.InternalName == item.ItemDef.InternalName && (stackSize == 0 || i.Count == stackSize);
            }

            return Auction.AuctionSellList.Lots
                .Count(Selector);
        }

        public static int GetSellItemsCount(this Item item)
        {
            return Auction.AuctionSellList.Lots
                .FindAll(l => l.Items.First().Item.ItemDef.InternalName == item.ItemDef.InternalName)
                .Sum(l => (int) l.Items.First().Item.Count);
        }
    }
}
