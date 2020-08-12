using System;
using System.Collections.Generic;
using System.Linq;
using Astral.Classes.ItemFilter;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;

namespace QuesterAssistant.Classes.ItemFilter
{
    internal static class ItemFilterCoreEx
    {
        public static bool IsMatch(this ItemFilterCore itemFilterCore, Item item)
        {
            bool flag = false;
            foreach (var entry in itemFilterCore.Entries)
            {
                if (entry.IsMatch(item))
                {
                    switch (entry.Mode)
                    {
                        case ItemFilterMode.Include:
                            flag = true;
                            break;
                        case ItemFilterMode.Exclude:
                            return false;
                    }
                }
            }
            return flag;
        }

        public static List<ItemFilterType> GetFilterTypes(ItemFilterCoreType itemFilterCoreType)
        {
            List<ItemFilterType> list = new List<ItemFilterType>();
            switch (itemFilterCoreType)
            {
                case ItemFilterCoreType.Items:
                    list.Add(ItemFilterType.ItemName);
                    list.Add(ItemFilterType.ItemID);
                    list.Add(ItemFilterType.ItemCatergory);
                    list.Add(ItemFilterType.ItemType);
                    list.Add(ItemFilterType.ItemFlag);
                    list.Add(ItemFilterType.ItemQuality);
                    break;
                case ItemFilterCoreType.Loots:
                    list.Add(ItemFilterType.Loot);
                    break;
                case ItemFilterCoreType.ItemsID:
                    list.Add(ItemFilterType.ItemID);
                    list.Add(ItemFilterType.ItemCatergory);
                    list.Add(ItemFilterType.ItemType);
                    list.Add(ItemFilterType.ItemFlag);
                    list.Add(ItemFilterType.ItemQuality);
                    break;
            }
            return list;
        }

        public static ItemFilterCollection GetFilterCollection(ItemFilterType itemFilterType, bool flag = false)
        {
            var itemFilterCollection = new ItemFilterCollection();

            switch (itemFilterType)
            {
                case ItemFilterType.ItemName:
                    itemFilterCollection.Description = "Filter items by name, sensitive to game language";
                    itemFilterCollection.Values = EntityManager.LocalPlayer.AllItems.Select(s => s.Item.DisplayName)
                        .Distinct().ToList();
                    break;

                case ItemFilterType.ItemID:
                    itemFilterCollection.Description = "Filer items by id, useful for advanced filters";
                    itemFilterCollection.Values = flag
                        ? EntityManager.LocalPlayer.AllItems
                            .Select(s => $"{s.Item.DisplayName} [{s.Item.ItemDef.InternalName}]").Distinct()
                            .ToList()
                        : EntityManager.LocalPlayer.AllItems
                            .Select(s => $"{s.Item.ItemDef.InternalName} [{s.Item.DisplayName}]").Distinct()
                            .ToList();
                    break;

                case ItemFilterType.ItemCatergory:
                    itemFilterCollection.Description = "Filter items by category name more specific than type";
                    itemFilterCollection.Values = Astral.Logic.NW.General.CategoriesWithItems.ToList();
                    break;

                case ItemFilterType.ItemType:
                    itemFilterCollection.Description = "Filter items by type, more generic than category";
                    itemFilterCollection.Values = Enum.GetNames(typeof(ItemType)).ToList();
                    break;

                case ItemFilterType.ItemFlag:
                    itemFilterCollection.Description = "Flags including bound information";
                    itemFilterCollection.Values = Enum.GetNames(typeof(ItemFlags)).ToList();
                    break;

                case ItemFilterType.Loot:
                    itemFilterCollection.Description = "Filter by loot bag name";
                    itemFilterCollection.Values = Astral.Logic.NW.Inventory.LootRewardTables;
                    break;

                case ItemFilterType.ItemQuality:
                    itemFilterCollection.Description = "Filter items by quality";
                    itemFilterCollection.Values = Enum.GetNames(typeof(ItemQuality)).ToList();
                    break;
            }
            return itemFilterCollection;
        }
    }
}
