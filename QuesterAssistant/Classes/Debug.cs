using System;

namespace QuesterAssistant.Classes
{
    internal static class Debug
    {
        internal static void WriteLine(string text)
        {
#if DEBUG
            Astral.Logger.WriteLine("[" + DateTime.Now.Second + "." + DateTime.Now.Millisecond + "] " + text);
#endif
        }

        internal static string DeprecatedMessage(string actionLabel, string actionInstead)
        {
            return string.Format("{0} is an obsolete action, use {1} instead.", actionLabel, actionInstead);
        }

        internal static string DeprecatedWriteLine(string actionLabel)
        {
            return string.Format("{0} is an obsolete action, profile need update, stop bot", actionLabel);
        }
    }
}
