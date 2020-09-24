using System;
using System.Linq;
using System.Reflection;
using Astral.Classes.ItemFilter;
using Astral.Quester.Forms;
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
                Assembly.GetAssembly(typeof(Main)).GetType("\u001B.\u0003")
                    .GetStaticAction<ItemFilterCore, ItemFilterCoreType>().Method,
                typeof(ItemFilterForm).GetStaticAction<ItemFilterCore, ItemFilterCoreType>().Method
            );

        private static PatchMethod Astral_Classes_ItemFilter_ItemFilterUC_ctor =
            new PatchMethod(
                typeof(Astral.Classes.ItemFilter.ItemFilterUC).GetConstructor(new Type[0]),
                typeof(ItemFilter.Forms.ItemFilterUC).GetConstructor(new Type[0])
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
