using System;

namespace QuesterAssistant.Classes.Common
{
    internal static class Debug
    {
        internal static void WriteLine(string text)
        {
#if DEBUG
            Astral.Logger.WriteLine($"[{DateTime.Now.Second}.{DateTime.Now.Millisecond}] {text}");
#endif
        }

        internal static string DeprecatedMessage(string actionLabel, string actionInstead)
        {
            return $"{actionLabel} is an obsolete action, use {actionInstead} instead.";
        }

        internal static string DeprecatedWriteLine(string actionLabel)
        {
            return $"{actionLabel} is an obsolete action, profile need update, stop bot";
        }
    }
}
