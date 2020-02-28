using System.Linq;
using System.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    internal class Patcher
    {
        private static BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        private static PatchMethod Astral_Logic_UCC_Forms_AddClass_Show =
            new PatchMethod(
                typeof(Astral.Logic.UCC.Forms.AddClass).GetMethod(nameof(Astral.Logic.UCC.Forms.AddClass.Show), binding),
                typeof(AddClass).GetMethod(nameof(AddClass.Show), binding));

        public static void Apply()
        {
            typeof(Patcher).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .ToList()
                .FindAll(l => l.FieldType == typeof(PatchMethod) || l.FieldType.BaseType == typeof(PatchMethod))
                .ForEach(l => (l.GetValue(null) as PatchMethod).Inject());
        }
    }
}
