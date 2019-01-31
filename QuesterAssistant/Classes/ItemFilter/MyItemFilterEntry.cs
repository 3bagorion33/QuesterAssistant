using Astral.Classes.ItemFilter;
using MyNW.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuesterAssistant.Classes.ItemFilter
{
    class MyItemFilterEntry
    {
        internal ItemFilterMode Mode { get; set; }
        internal ItemFilterStringType StringType { get; set; }
        internal string Text { get; set; }
        internal ItemFilterType Type { get; set; }

        internal MyItemFilterEntry(ItemFilterEntry filterEntry)
        {
            this.Mode = filterEntry.Mode;
            this.StringType = filterEntry.StringType;
            this.Text = filterEntry.Text;
            this.Type = filterEntry.Type;
        }

        public static explicit operator MyItemFilterEntry(ItemFilterEntry filterEntry)
        {
            return new MyItemFilterEntry(filterEntry);
        }

        bool ParseString (string text)
        {
            if (Text == "*")
                return true;

            if (Text.StartsWith("*") && Text.EndsWith("*"))
                return text.Contains(Text.Remove(Text.Length - 1).Remove(0, 1));

            if (Text.StartsWith("*"))
                return text.EndsWith(Text.Remove(0, 1));

            if (Text.EndsWith("*"))
                return text.StartsWith(Text.Remove(Text.Length - 1));

            return Text == text;
        }

        internal bool StrType(Item item)
        {
            switch (this.StringType)
            {
                case ItemFilterStringType.Simple:
                    return ItemType(item).Any(x => ParseString(x));

                case ItemFilterStringType.Regex:
                    return ItemType(item).Any(x => Regex.IsMatch(x, this.Text));
            }
            return false;
        }

        List<string> ItemType (Item item)
        {
            var _tmp = new List<string>();
            switch (this.Type)
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
