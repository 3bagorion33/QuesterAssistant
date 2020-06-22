using System;
using System.Diagnostics;

namespace QuesterAssistant.Classes
{
    internal static class Debug
    {
        [Conditional("DEBUG")]
        internal static void WriteLine(string text)
        {
            Astral.Logger.WriteLine($"[{DateTime.Now.Second}.{DateTime.Now.Millisecond}] {text}");
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
