using Astral.Classes.ItemFilter;
using MyNW.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Classes.ItemFilter
{
    internal static class ItemFilterEntryEx
    {
        public static bool IsMatch(this ItemFilterEntry itemFilterEntry, Item item)
        {
            switch (itemFilterEntry.StringType)
            {
                case ItemFilterStringType.Simple:
                    return itemFilterEntry.GetStringsByFilterType(item).Any(x => x.FindPattern(itemFilterEntry.Text));
                case ItemFilterStringType.Regex:
                    return itemFilterEntry.GetStringsByFilterType(item).Any(x => Regex.IsMatch(x, itemFilterEntry.Text));
            }
            return false;
        }

        public static List<string> GetStringsByFilterType(this ItemFilterEntry itemFilterEntry, Item item)
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
    }
}
