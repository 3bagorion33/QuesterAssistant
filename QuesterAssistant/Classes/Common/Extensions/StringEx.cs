using System;

namespace QuesterAssistant.Classes.Common.Extensions
{
    internal static class StringEx
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
    }
}
