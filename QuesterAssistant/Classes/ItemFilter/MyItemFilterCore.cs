using Astral.Classes.ItemFilter;
using MyNW.Classes;

namespace QuesterAssistant.Classes.ItemFilter
{
    internal static class MyItemFilterCore
    {
        public static bool IsMatch(this ItemFilterCore itemFilterCore, Item item)
        {
            bool flag = false;
            foreach (var entry in itemFilterCore.Entries)
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
