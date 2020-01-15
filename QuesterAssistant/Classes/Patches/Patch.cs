using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuesterAssistant.Classes.Patches
{
    internal class Patch
    {
        private readonly MethodInfo methodToReplace;
        private readonly MethodInfo methodToInject;
        private readonly bool active;

        public Patch(MethodInfo methodToReplace, MethodInfo methodToInject, bool active = true)
        {
            this.methodToReplace = methodToReplace;
            this.methodToInject = methodToInject;
            this.active = active;
        }

        public void Inject()
        {
            if (!active || methodToReplace is null || methodToInject is null) return;

            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);

            unsafe
            {
                long* inj = (long*)methodToInject.MethodHandle.Value.ToPointer() + 1;
                long* tar = (long*)methodToReplace.MethodHandle.Value.ToPointer() + 1;

                *tar = *inj;
            }
        }
    }
}
