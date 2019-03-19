using System.Collections.Generic;

namespace QuesterAssistant.Classes.Common.Extensions
{
    internal static class ListEx
    {
        public static int GetHashCodeEx<T>(this List<T> list)
        {
            if (list == null) return 0;
            int hash = 0;
            for (int i = 0; i < list?.Count; i++)
            {
                hash ^= i * list[i].GetHashCode();
            }
            return hash;
        }
    }
}
