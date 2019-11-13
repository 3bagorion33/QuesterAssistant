﻿using System.Collections;

namespace QuesterAssistant.Classes.Extensions
{
    public static class CommonEx
    {
        public static int GetSafeHashCode<T>(this T @this)
        {
            if (@this == null) return 0;
            if (@this.GetType().GetInterface(nameof(IEnumerable)) != null)
            {
                var @enum = @this as IEnumerable;
                int hash = 0;
                int i = 0;
                foreach (var item in @enum)
                {
                    i++;
                    hash ^= i * item.GetSafeHashCode();
                }
                return hash;
            }
            return @this.GetHashCode();
        }

        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static int CheckZero(this int @int, int value)
        {
            return DynCheckZero(@int, value);
        }

        public static uint CheckZero(this uint @uint, uint value)
        {
            return DynCheckZero(@uint, value);
        }

        private static dynamic DynCheckZero(dynamic @this, dynamic value)
        {
            return @this == 0 ? value : @this;
        }
    }
}