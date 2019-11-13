using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuesterAssistant.Classes
{
    internal class Patcher
    {
        private static void Astral_Logic_UCC_Forms_AddClass_Show()
        {
            MethodInfo methodToReplace =
                typeof(Astral.Logic.UCC.Forms.AddClass).GetMethod(
                    nameof(Astral.Logic.UCC.Forms.AddClass.Show),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo methodToInject =
                typeof(Patches.AddClass).GetMethod(
                    nameof(Patches.AddClass.Show),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            Inject(methodToReplace, methodToInject);
        }

        private static void Inject(MethodInfo methodToReplace, MethodInfo methodToInject)
        {
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);

            unsafe
            {
                long* inj = (long*)methodToInject.MethodHandle.Value.ToPointer() + 1;
                long* tar = (long*)methodToReplace.MethodHandle.Value.ToPointer() + 1;

                *tar = *inj;
            }
        }

        public static void Apply()
        {
            Astral_Logic_UCC_Forms_AddClass_Show();
        }
    }
}
