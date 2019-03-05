using Astral.Classes.ItemFilter;
using MyNW.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuesterAssistant.Classes.ItemFilter
{
    internal static class MyItemFilterEntry
    {
        public static bool StrType(this ItemFilterEntry itemFilterEntry, Item item)
        {
            switch (itemFilterEntry.StringType)
            {
                case ItemFilterStringType.Simple:
                    return itemFilterEntry.ItemType(item).Any(x => itemFilterEntry.ParseString(x));

                case ItemFilterStringType.Regex:
                    return itemFilterEntry.ItemType(item).Any(x => Regex.IsMatch(x, itemFilterEntry.Text));
            }
            return false;
        }

        public static List<string> ItemType(this ItemFilterEntry itemFilterEntry, Item item)
        {
            var _tmp = new List<string>();
            switch (itemFilterEntry.Type)
            {
                case ItemFilterType.ItemName:
                    _tmp.Add(item.DisplayName);
                    break;

                case ItemFilterType.ItemID:
                    _tmp.Add(item.ItemDef.InternalName);
                    break;

                case ItemFilterType.ItemCatergory:
                    _tmp.AddRange(item.ItemDef.Categories.Select(x => x.ToString()));
                    break;

                case ItemFilterType.ItemType:
                    _tmp.Add(item.ItemDef.Type.ToString());
                    break;

                case ItemFilterType.ItemFlag:
                    _tmp.AddRange(item.ActiveFlags.Select(x => x.ToString()));
                    break;
            }
            return _tmp;
        }

        public static bool ParseString(this ItemFilterEntry itemFilterEntry, string text)
        {
            var t = itemFilterEntry.Text;
            if (t == "*")
                return true;

            if (t.StartsWith("*") && t.EndsWith("*"))
                return text.Contains(t.Remove(t.Length - 1).Remove(0, 1));

            if (t.StartsWith("*"))
                return text.EndsWith(t.Remove(0, 1));

            if (t.EndsWith("*"))
                return text.StartsWith(t.Remove(t.Length - 1));

            return t == text;
        }
    }
}
