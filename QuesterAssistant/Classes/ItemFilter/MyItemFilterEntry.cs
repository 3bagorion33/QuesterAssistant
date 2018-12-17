using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Professions.Functions;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MyNW.Patchables.Enums;

namespace QuesterAssistant.Classes.ItemFilter
{
    class MyItemFilterEntry
    {
        // Properties
        internal ItemFilterMode Mode { get; set; }
        internal ItemFilterStringType StringType { get; set; }
        internal string Text { get; set; }
        internal ItemFilterType Type { get; set; }

        // Constructor
        internal MyItemFilterEntry(ItemFilterEntry filterEntry)
        {
            this.Mode = filterEntry.Mode;
            this.StringType = filterEntry.StringType;
            this.Text = filterEntry.Text;
            this.Type = filterEntry.Type;
        }

        // Translator
        public static explicit operator MyItemFilterEntry(ItemFilterEntry filterEntry)
        {
            return new MyItemFilterEntry(filterEntry);
        }

        // method_5
        // method_7
        bool ParseString (string text)
        {
            string searchText = this.Text;

            Core.DebugWriteLine("SearchText: " + searchText);
            if (searchText == "*")
            {
                return true;
            }
            if (searchText.StartsWith("*") && searchText.EndsWith("*"))
            {
                string str = searchText.Remove(0, 1);
                str = str.Remove(str.Length - 1);
                return text.Contains(str);
            }
            if (searchText.StartsWith("*"))
            {
                string str2 = searchText.Remove(0, 1);
                return text.EndsWith(str2);
            }
            if (searchText.EndsWith("*"))
            {
                string str3 = searchText.Remove(searchText.Length - 1);
                Core.DebugWriteLine("Search pattern: " + str3);
                Core.DebugWriteLine("Pattern match: " + text.StartsWith(str3).ToString());
                return text.StartsWith(str3);
            }
            return searchText == text;
        }

        // method_4
        // method_6
        bool RegexIsMatch (string text)
        {
            return Regex.IsMatch(text, this.Text);
        }

        // method_1
        internal bool StrType(Item item)
        {
            switch (this.StringType)
            {
                case ItemFilterStringType.Simple:
                    Core.DebugWriteLine("Filter Simple");
                    return Enumerable.Any<string>(this.ItemType(item), new Func<string, bool>(this.ParseString));

                case ItemFilterStringType.Regex:
                    return Enumerable.Any<string>(this.ItemType(item), new Func<string, bool>(this.RegexIsMatch));
            }
            return false;
        }

        // method_0
        List<string> ItemType (Item item)
        {
            switch (this.Type)
            {
                case ItemFilterType.ItemName:
                    return new List<string>(new string[] { item.ItemDef.DisplayName });

                case ItemFilterType.ItemID:
                    Core.DebugWriteLine("Type: ItemID -> " + item.ItemDef.InternalName);
                    Core.DebugWriteLine(new List<string>(new string[] { item.ItemDef.InternalName }).ToString());
                    return new List<string>(new string[] { item.ItemDef.InternalName });

                case ItemFilterType.ItemCatergory:
                    return new List<string>(Enumerable.Select
                        (item.ItemDef.Categories, ItemFlag.Category ?? 
                        (ItemFlag.Category = new Func<ItemCategory, string>(ItemFlag.Get.GetItemCategory))));

                case ItemFilterType.ItemType:
                    return new List<string>(new string[] { item.ItemDef.Type.ToString() });

                case ItemFilterType.ItemFlag:
                    return new List<string>(Enumerable.Select
                        (item.ActiveFlags, ItemFlag.Flags ?? 
                        (ItemFlag.Flags = new Func<ItemFlags, string>(ItemFlag.Get.GetItemFlags))));
            }
            return new List<string>();
        }

        private sealed class ItemFlag
        {
            public static ItemFlag Get { get; } = new ItemFlag();
            public static Func<ItemCategory, string> Category { get; set; }
            public static Func<ItemFlags, string> Flags { get; set; }

            // method_0
            internal string GetItemCategory (ItemCategory itemCategory)
            {
                return itemCategory.ToString();
            }

            // method_1
            internal string GetItemFlags (ItemFlags itemFlags)
            {
                return itemFlags.ToString();
            }
        }
    }
}
