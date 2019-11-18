using System.Linq;
using System.Reflection;

namespace QuesterAssistant.Classes
{
    internal class Patcher
    {
        private static Patch Astral_Logic_UCC_Forms_AddClass_Show =
            new Patch(
                typeof(Astral.Logic.UCC.Forms.AddClass).GetMethod(nameof(Astral.Logic.UCC.Forms.AddClass.Show),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic),
                typeof(Patches.AddClass).GetMethod(nameof(Patches.AddClass.Show),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic));

        private static Patch Astral_Professions_FSM_States_Main_RandomPause = 
            new Patch(
                typeof(Astral.Professions.FSM.States.Main).GetMethod("RandomPause",
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic),
                typeof(Patches.Methods).GetMethod(nameof(Patches.Methods.Astral_Professions_FSM_States_Main_RandomPause),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic));

        public static void Apply()
        {
            typeof(Patcher).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .ToList()
                .FindAll(l => l.FieldType == typeof(Patch))
                .ForEach(l => (l.GetValue(null) as Patch).Inject());
        }
    }
}
