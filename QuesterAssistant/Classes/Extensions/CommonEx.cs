using System.Collections;

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

        public static int CheckZero(this int @int, int value)
        {
            return (@int == 0) ? value : @int;
        }
    }
}