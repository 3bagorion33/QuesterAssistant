namespace Astral.Classes.ItemFilter
{
    using Astral.Logic.NW;
    using MyNW.Classes;
    using MyNW.Patchables.Enums;
    using ns1;
    using ns17;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [Serializable]
    public class ItemFilterCore
    {
        // Methods
        public ItemFilterCore()
        {
            this.Entries = new List<ItemFilterEntry>();
        }

        public bool method_0(Item item_0)
        {
            bool flag = false;
            using (List<ItemFilterEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemFilterEntry current = enumerator.Current;
                    if (current.method_1(item_0))
                    {
                        switch (current.Mode)
                        {
                            case ItemFilterMode.Include:
                                flag = true;
                                break;

                            case ItemFilterMode.Exclude:
                                goto Label_003F;
                        }
                    }
                }
                return flag;
                Label_003F:
                flag = false;
            }
            return flag;
        }

        public bool method_1(Item item_0)
        {
            using (List<ItemFilterEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.method_1(item_0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool method_2(List<InventoryBag> list_0)
        {
            bool flag = false;
            using (List<ItemFilterEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemFilterEntry current = enumerator.Current;
                    if (current.method_3(list_0))
                    {
                        switch (current.Mode)
                        {
                            case ItemFilterMode.Include:
                                flag = true;
                                break;

                            case ItemFilterMode.Exclude:
                                goto Label_003F;
                        }
                    }
                }
                return flag;
                Label_003F:
                flag = false;
            }
            return flag;
        }

        public bool method_3(List<InventoryBag> list_0)
        {
            using (List<ItemFilterEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.method_3(list_0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<ItemFilterType> smethod_0(ItemFilterCoreType itemFilterCoreType_0)
        {
            List<ItemFilterType> list = new List<ItemFilterType>();
            switch (itemFilterCoreType_0)
            {
                case ItemFilterCoreType.Items:
                    list.Add(ItemFilterType.ItemName);
                    list.Add(ItemFilterType.ItemID);
                    list.Add(ItemFilterType.ItemCatergory);
                    list.Add(ItemFilterType.ItemType);
                    list.Add(ItemFilterType.ItemFlag);
                    return list;

                case ItemFilterCoreType.Loots:
                    list.Add(ItemFilterType.Loot);
                    return list;

                case ItemFilterCoreType.ItemsID:
                    list.Add(ItemFilterType.ItemID);
                    list.Add(ItemFilterType.ItemCatergory);
                    list.Add(ItemFilterType.ItemType);
                    list.Add(ItemFilterType.ItemFlag);
                    return list;
            }
            return list;
        }

        public static ItemFilterCore smethod_1(List<string> list_0, [Optional, DefaultParameterValue(0)] ItemFilterType itemFilterType_0)
        {
            ItemFilterCore core = new ItemFilterCore();
            foreach (string str in list_0)
            {
                ItemFilterEntry item = new ItemFilterEntry
                {
                    Text = str,
                    Type = itemFilterType_0
                };
                core.Entries.Add(item);
            }
            return core;
        }

        public static Class127 smethod_2(ItemFilterType itemFilterType_0, [Optional, DefaultParameterValue(false)] bool bool_0)
        {
            switch (itemFilterType_0)
            {
                case ItemFilterType.ItemName:
                    return new Class127 { Description = "Filter items by name, sensitive to game language", Values = Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.Select<InventorySlot, string>(Class1.LocalPlayer.get_AllItems(), <> c.<> 9__12_2 ?? (<> c.<> 9__12_2 = new Func<InventorySlot, string>(<> c.<> 9.method_2))))) };

                case ItemFilterType.ItemID:
                    return new Class127 { Description = "Filer items by id, useful for advanced filters", Values = bool_0 ? Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.Select<InventorySlot, string>(Class1.LocalPlayer.get_AllItems(), <> c.<> 9__12_0 ?? (<> c.<> 9__12_0 = new Func<InventorySlot, string>(<> c.<> 9.method_0))))) : Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.Select<InventorySlot, string>(Class1.LocalPlayer.get_AllItems(), <> c.<> 9__12_1 ?? (<> c.<> 9__12_1 = new Func<InventorySlot, string>(<> c.<> 9.method_1))))) };

                case ItemFilterType.ItemCatergory:
                    return new Class127 { Description = "Filter items by category name more specific than type", Values = Enumerable.ToList<string>(Enumerable.Cast<string>(General.CategoriesWithItems)) };

                case ItemFilterType.ItemType:
                    return new Class127 { Description = "Filter items by type, more generic than category", Values = Enumerable.ToList<string>(Enum.GetNames(typeof(ItemType))) };

                case ItemFilterType.ItemFlag:
                    return new Class127 { Description = "Flags include bound informations", Values = Enumerable.ToList<string>(Enum.GetNames(typeof(ItemFlags))) };

                case ItemFilterType.Loot:
                    return new Class127 { Description = "Filter by loot bag name", Values = Inventory.LootRewardTables };
            }
            return new Class127();
        }

        string object.ToString()
        {
            return (this.Entries.Count + " entries in filter");
        }

        // Properties
        public List<ItemFilterEntry> Entries { get; set; }

        // Nested Types
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            // Fields
            public static readonly ItemFilterCore.<>c<>9 = new ItemFilterCore.<>c();
        public static Func<InventorySlot, string> <>9__12_0;
            public static Func<InventorySlot, string> <>9__12_1;
            public static Func<InventorySlot, string> <>9__12_2;

            // Methods
            internal string method_0(InventorySlot inventorySlot_0)
        {
            return ("[" + inventorySlot_0.get_Item().get_DisplayName() + "] " + inventorySlot_0.get_Item().get_ItemDef().get_InternalName());
        }

        internal string method_1(InventorySlot inventorySlot_0)
        {
            return (inventorySlot_0.get_Item().get_ItemDef().get_InternalName() + " [" + inventorySlot_0.get_Item().get_DisplayName() + "]");
        }

        internal string method_2(InventorySlot inventorySlot_0)
        {
            return inventorySlot_0.get_Item().get_ItemDef().get_DisplayName();
        }
    }
}
}

