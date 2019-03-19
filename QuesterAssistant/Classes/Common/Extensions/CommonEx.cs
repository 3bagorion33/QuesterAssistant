namespace QuesterAssistant.Classes.Common.Extensions
{
    internal static class CommonEx
    {
        public static int GetSafeHashCode<T>(this T value) where T : class
        {
            return value == null ? 0 : value.GetHashCode();
        }
    }
}