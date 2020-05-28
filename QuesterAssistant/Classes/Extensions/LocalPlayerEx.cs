using System.Collections.Generic;
using MyNW.Classes;
using MyNW.Patchables.Enums;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class LocalPlayerEx
    {
        public static List<InventorySlot> GetBagsForSale(this LocalPlayerEntity player)
        {
            var bags = player.BagsItems;
            player.GetInventoryBagById(InvBagIDs.Overflow).GetItems.ForEach(s => bags.Remove(s));
            bags.AddRange(player.GetInventoryBagById(InvBagIDs.Currency).GetItems);
            bags.AddRange(player.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
            bags.AddRange(player.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
            bags.AddRange(player.GetInventoryBagById(InvBagIDs.FashionItems).GetItems);
            return bags;
        }
    }
}