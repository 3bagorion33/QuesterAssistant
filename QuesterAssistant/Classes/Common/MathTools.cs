using System;

namespace QuesterAssistant.Classes.Common
{
    public static class MathTools
    {
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
                for (int i = 0; i < (digitsCount - roundDigits); i++)
                {
                    result = result * 10 + mod;
                }
                return result;
            }
            return result;
        }
        public enum RoundType
        {
            Zero,
            Nine,
            Last
        }
    }
}
