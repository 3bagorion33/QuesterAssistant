using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MyNW;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.Patches;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class InventorySlotEx
    {
        public static void Group(this InventorySlot slot)
        {
            bool Find(InventorySlot s)
            {
                return
                    s.Item.ItemDef.StackLimit > 1 &&
                    (int) s.Item.ProgressionLogic.CurrentRankXP == 0 &&
                    s.Item.Flags == slot.Item.Flags &&
                    s.Item.ItemDef.InternalName == slot.Item.ItemDef.InternalName;
            }

            if (!slot.Filled || !slot.Item.IsValid) return;

            List<InventorySlot> slots = EntityManager.LocalPlayer.GetBagsForSale().FindAll(Find);
            if (!slots.Any()) return;

            InventorySlot sFirst = slots.FirstOrDefault(s => s.Item.Count < s.Item.ItemDef.StackLimit);
            InventorySlot sLast = slots.Last();
            if (sFirst == null || sFirst == sLast) return;

            uint count = Math.Min(sFirst.Item.ItemDef.StackLimit - sFirst.Item.Count, sLast.Item.Count);
            sLast.Move(sFirst.BagId, sFirst.Slot, count);
            Thread.Sleep(250);
            sFirst.Group();
        }

        public static void EvolveBatch(this InventorySlot gemSlot, InventorySlot wardSlot = null)
        {
            if (!gemSlot.IsValid || !gemSlot.Filled) return;

            // STRUCT(0) 0     0     1    | 2 0 1 | 0 1 1
            // 0x20      0x28  0x30  0x38
            // 0x28 =
            //      0 -> w/o sort
            //      1 -> 100% fail
            //      2 -> group item
            //      3 -> 100% fail
            // 0x30 -> ?
            // 0x38 -> count of iteration. если кончаются каталики, жрет ресурсы. надо рассчитывать

            string[] mnemonics =
            {
                "sub rsp, 0x40",
                "mov rax, 1",   // + (wardSlot != null && wardSlot.Filled ? wardSlot.Item.Count : 150),
                "mov [rsp+0x38], rax",
                "mov rax, 2",
                "mov [rsp+0x28], rax",
                "mov rax, 0",
                //"mov [rsp+0x30], rax",
                "mov [rsp+0x20], rax",
                "mov r9d, " + (wardSlot?.Slot ?? uint.MaxValue),
                "mov r8d, " + (uint) (wardSlot?.BagId ?? (InvBagIDs) uint.MaxValue),
                "mov edx, " + gemSlot.Slot,
                "mov ecx, " + (uint) gemSlot.BagId,
                "mov rax, " + (Memory.BaseAdress + Offsets.itemProgression_EvoItem),
                "call rax",
                "add rsp, 0x40",
                "retn"
            };
            MyNW.Hooks.Executor.Execute<IntPtr>(mnemonics, "cmdwrapper_itemProgression_EvoItem");
        }
    }
}
