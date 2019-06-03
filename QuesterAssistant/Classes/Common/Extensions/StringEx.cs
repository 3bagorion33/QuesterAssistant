using System;
using System.Linq;

namespace QuesterAssistant.Classes.Common.Extensions
{
    public static class StringEx
    {
        public static bool CaseContains(this string @this, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (value == string.Empty)
            {
                return false;
            }
            return @this.IndexOf(value, stringComparison) >= 0;
        }

        public static string RandomHide(this string @this, int startIndex = 3, int interval = 3)
        {
            string @string = @this;
            Random random = new Random();
            int count = Math.Max(1, (@this.Length - startIndex) / interval);
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(startIndex, @this.Length);
                @string = @string.Remove(index, 1).Insert(index, "*");
            }
            return @string;
        }

        public static string Hide(this string @this, int startIndex = 3, int interval = 3)
        {
            string @string = @this;
            for (int i = startIndex; i < @this.Length; i += interval)
            {
                @string = @string.Remove(i, 1).Insert(i, "*");
            }
            return @string;
        }

        public static byte[] ToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
        }
    }
}
