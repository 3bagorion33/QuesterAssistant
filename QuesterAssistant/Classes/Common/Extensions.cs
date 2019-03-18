using System;
using System.Collections.Generic;

namespace QuesterAssistant.Classes.Common
{
    internal static class Extensions
    {
        public static bool CaseContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (value == string.Empty)
            {
                return false;
            }
            return text.IndexOf(value, stringComparison) >= 0;
        }

        public static int GetHashCodeExt<T>(this List<T> list)
        {
            if (list == null) return 0;
            int hash = 0;
            for (int i = 0; i < list?.Count; i++)
            {
                hash ^= i * list[i].GetHashCode();
            }
            return hash;
        }

        public static int GetSafeHashCode<T>(this T value) where T : class
        {
            return value == null ? 0 : value.GetHashCode();
        }
    }
}
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerMemberNameAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerFilePathAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerLineNumberAttribute : Attribute { }
}