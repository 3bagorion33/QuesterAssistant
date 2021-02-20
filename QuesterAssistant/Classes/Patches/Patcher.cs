using System;
using System.Linq;
using System.Reflection;
using Astral.Classes.ItemFilter;
using QuesterAssistant.Classes.ItemFilter.Forms;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    internal class Patcher
    {
        private static BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        private static PatchMethod Astral_Logic_UCC_Forms_AddClass_Show =
            new PatchMethod(
                typeof(Astral.Logic.UCC.Forms.AddClass).GetMethod(nameof(Astral.Logic.UCC.Forms.AddClass.Show), binding),
                typeof(AddClass).GetMethod(nameof(AddClass.Show), binding),
                !Core.IsEToolsPresent);

        private static PatchMethod Astral_Classes_ItemFilter_ItemFilterForm_Show =
            new PatchMethod(
                Assembly.GetAssembly(typeof(Astral.Quester.Forms.Main)).GetType("\u000F.\u0006")
                    .GetStaticAction<ItemFilterCore, ItemFilterCoreType>().Method,
                typeof(ItemFilterForm).GetStaticAction<ItemFilterCore, ItemFilterCoreType>().Method
            );

        private static PatchMethod Astral_Classes_ItemFilter_ItemFilterUC_ctor =
            new PatchMethod(
                typeof(Astral.Classes.ItemFilter.ItemFilterUC).GetConstructor(new Type[0]),
                typeof(ItemFilter.Forms.ItemFilterUC).GetConstructor(new Type[0])
            );

        private static PatchMethod Astral_Quester_Forms_GetMissionId_Show =
            new PatchMethod(
                typeof(Astral.Quester.Forms.GetMissionId).GetMethod(nameof(Astral.Quester.Forms.GetMissionId.Show), BindingFlags.Public | BindingFlags.Static),
                typeof(UIEditors.Forms.GetMissionId).GetMethod(nameof(UIEditors.Forms.GetMissionId.Show), BindingFlags.Public | BindingFlags.Static)
            );

        public static void Apply()
        {
            typeof(Patcher).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .ToList()
                .FindAll(l => l.FieldType == typeof(PatchMethod) || l.FieldType.BaseType == typeof(PatchMethod))
                .ForEach(l => (l.GetValue(null) as PatchMethod).Inject());
        }
    }
}
