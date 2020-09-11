using Astral.Logic.NW;
using Astral.Quester;
using Astral.Quester.Classes;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class ActionEx
    {
        public static void IgnoreCombat(this Action action)
        {
            if (action is IIgnoreCombat a)
                API.IgnoreCombat = a.IgnoreCombat;
        }

        public static void EnableCombat(this Action action)
        {
            if (action is IIgnoreCombat a && a.IgnoreCombat)
            {
                Attackers.List.Clear();
                API.IgnoreCombat = false;
            }
        }
    }
}