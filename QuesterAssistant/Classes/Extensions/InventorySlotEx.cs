using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
                    s.Item.ProgressionLogic.CurrentRankXP == 0 &&
                    s.Item.Flags == slot.Item.Flags &&
                    s.Item.ItemDef.InternalName == slot.Item.ItemDef.InternalName;
            }

            if (!slot.Filled || !slot.Item.IsValid) return;

            List<InventorySlot> slots = EntityManager.LocalPlayer.BagsItems.FindAll(Find);
            if (!slots.Any()) return;

            InventorySlot sFirst = slots.FirstOrDefault(s => s.Item.Count < s.Item.ItemDef.StackLimit);
            InventorySlot sLast = slots.Last();
            if (sFirst == null || sFirst == sLast) return;

            uint count = Math.Min(sFirst.Item.ItemDef.StackLimit - sFirst.Item.Count, sLast.Item.Count);
            sLast.Move(sFirst.BagId, sFirst.Slot, count);
            Thread.Sleep(250);
            sFirst.Group();
        }
    }
}
