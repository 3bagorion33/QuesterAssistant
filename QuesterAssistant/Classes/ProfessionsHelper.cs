using System.Collections.Generic;
using Astral.Classes;
using Astral.Logic.NW;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;

namespace QuesterAssistant.Classes
{
    internal static class ProfessionsHelper
    {
        private static readonly Timeout refreshTo = new Timeout(500);

        public static string GetDisplayName(string taskName) =>
            Professions.AllTasks.Find(d => d.InternalName == taskName)?.ToString() ?? taskName;

        public static bool HaveRequiredConsumables(Definition definition, Dictionary<int, int> hqTable = null)
        {
            if (hqTable == null)
                hqTable = new Dictionary<int, int>();

            for (int i = 0; i < definition.Requirements.ItemCost.Count; i++)
            {
                ItemCost itemCost = definition.Requirements.ItemCost[i];
                if (hqTable.ContainsKey(i))
                {
                    if (EntityManager.LocalPlayer.GetItemCountByInternalName(itemCost.ItemDef.InternalName + "_Hq") < (uint)hqTable[i])
                    {
                        return false;
                    }
                }
                else if (EntityManager.LocalPlayer.GetItemCountByInternalName(itemCost.ItemDef.InternalName) < itemCost.Count)
                {
                    return false;
                }
            }
            return true;
        }

        public static void RefreshAssignments()
        {
            if (refreshTo.IsTimedOut)
            {
                EntityManager.LocalPlayer.Player.RefreshAssignments();
                refreshTo.Reset();
            }
        }
    }
}