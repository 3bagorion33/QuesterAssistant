using System.Diagnostics.CodeAnalysis;
using MyNW;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes.CodeReader;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class Offsets
    {
        public static readonly int itemProgression_EvoItem =
            (int) new MethodBodyReader(typeof(Injection).GetStaticAction<InventorySlot, InventorySlot>().Method)
                .GetInstructionByIndex(110).Operand;

        public static readonly int GameCursorMoving =
            (int) new MethodBodyReader(typeof(Memory).GetMethod(nameof(Memory.Initialize),
                Helper.DefaultFlags)).GetInstructionByIndex(33).Operand;
    }
}