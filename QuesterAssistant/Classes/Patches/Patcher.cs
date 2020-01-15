using System.Linq;
using System.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    internal class Patcher
    {
        private static BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        private static Patch Astral_Logic_UCC_Forms_AddClass_Show =
            new Patch(
                typeof(Astral.Logic.UCC.Forms.AddClass).GetMethod(nameof(Astral.Logic.UCC.Forms.AddClass.Show), binding),
                typeof(AddClass).GetMethod(nameof(AddClass.Show), binding));

        private static Patch Astral_Professions_FSM_States_Main_RandomPause = 
            new Patch(
                typeof(Astral.Professions.FSM.States.Main).GetMethod("RandomPause", binding),
                typeof(ProfessionsPausePatch).GetMethod(nameof(ProfessionsPausePatch.Astral_Professions_FSM_States_Main_RandomPause), binding));

        private static Patch Astral_Professions_Functions_Tasks_GetNextTaskInfos = 
            new Patch(
                typeof(Astral.Professions.Functions.Tasks).GetMethod("GetNextTaskInfos"),
                typeof(GetNextTaskInfosPatch).GetMethod(nameof(GetNextTaskInfosPatch.Astral_Professions_Functions_Tasks_GetNextTaskInfos)),
                Core.SettingsCore.Data.Patches.ProfessionPatch
            );

        public static void Apply()
        {
            typeof(Patcher).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .ToList()
                .FindAll(l => l.FieldType == typeof(Patch))
                .ForEach(l => (l.GetValue(null) as Patch).Inject());
        }
    }
}
