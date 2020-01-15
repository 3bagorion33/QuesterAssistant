using System;

namespace QuesterAssistant.Classes.Common
{
    public static class MathTools
    {
        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static int CheckZero(this int @int, int value) => DynCheckZero(@int, value);

        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static uint CheckZero(this uint @uint, uint value) => DynCheckZero(@uint, value);

        private static dynamic DynCheckZero(dynamic @this, dynamic value)
        {
            return @this == 0 ? value : @this;
        }

        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static int CheckNegative(this int @int, int value) => DynCheckNegative(@int, value);

        /// <summary>
        /// Check <see cref="@double"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static double CheckNegative(this double @int, double value) => DynCheckNegative(@int, value);

        private static dynamic DynCheckNegative(dynamic @this, dynamic value)
        {
            return @this < 0 ? value : @this;
        }

        public static int Round(int number, uint roundDigits, RoundType roundFilledBy)
        {
            // Если не задано, не округляем
            if (number < 10 || roundDigits == 0)
            {
                return number;
            }
            // Определяем число разрядов
            int digitsCount = 1;
            double tmp = number;
            while (tmp > 10)
            {
                tmp /= 10;
                digitsCount++;
            }
            // Собственно округление
            int result = (int)Math.Floor(tmp * Math.Pow(10, roundDigits - 1));

            if (roundFilledBy == RoundType.Zero)
            {
                return (int)(result * Math.Pow(10, digitsCount - roundDigits));
            }
            if (roundFilledBy == RoundType.Nine)
            {
                return (int)(result * Math.Pow(10, digitsCount - roundDigits)) - 1;
            }
            if (roundFilledBy == RoundType.Last)
            {
                var mod = result % 10;
                for (int i = 0; i < digitsCount - roundDigits; i++)
                {
                    result = result * 10 + mod;
                }
                return result;
            }
            return result;
        }

        public static dynamic Min(params dynamic[] num)
        {
            if (num.Length == 1) return num[0];
            var value = Math.Min(num[0], num[1]);
            for (int i = 2; i < num.Length - 2; i++)
                value = Math.Min(value, num[i]);
            return value;
        }

        public static dynamic Max(params dynamic[] num)
        {
            if (num.Length == 1) return num[0];
            var value = Math.Max(num[0], num[1]);
            for (int i = 2; i < num.Length - 2; i++)
                value = Math.Max(value, num[i]);
            return value;
        }

        public enum RoundType
        {
            Zero,
            Nine,
            Last
        }
    }
}
