using Astral.Logic.NW;

namespace QuesterAssistant.Classes
{
    internal static class ProfessionsHelper
    {
        public static string GetDisplayName(string taskName) =>
            Professions.AllTasks.Find(d => d.InternalName == taskName)?.ToString() ?? taskName;
    }
}