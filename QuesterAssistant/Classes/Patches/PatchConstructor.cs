using System;

namespace QuesterAssistant.Classes.Patches
{
    internal class PatchConstructor<TReplace, TInject> : PatchMethod where TInject : TReplace
    {
        public PatchConstructor(Type[] argumentTypes) :
            base(typeof(TReplace).GetConstructor(argumentTypes),
                typeof(TInject).GetConstructor(argumentTypes)) { }
    }
}