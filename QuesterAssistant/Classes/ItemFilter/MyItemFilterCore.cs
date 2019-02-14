using Astral.Classes.ItemFilter;
using MyNW.Classes;
using System.Collections.Generic;

namespace QuesterAssistant.Classes.ItemFilter
{
    class MyItemFilterCore
    {
        List<MyItemFilterEntry> Entries { get; set; } = new List<MyItemFilterEntry>();

        internal MyItemFilterCore(ItemFilterCore filterCore)
        {
            filterCore.Entries.ForEach(x => Entries.Add((MyItemFilterEntry)x));
        }

        public static explicit operator MyItemFilterCore(ItemFilterCore filterCore)
        {
            return new MyItemFilterCore(filterCore);
        }

        internal bool IsMatch(Item item)
        {
            bool flag = false;
            foreach (var entry in Entries)
            {
                if (entry.StrType(item))
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
    }
}
