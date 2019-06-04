using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyNW.Classes
{
    internal static class InventorySlotEx
    {
        public static void Group(this InventorySlot slot)
        {
            bool Find(InventorySlot s)
            {
                return
                    s.Item.ItemDef.InternalName == slot.Item.ItemDef.InternalName &&
                    s.Item.ItemDef.StackLimit > 1 &&
                    s.Item.ProgressionLogic.CurrentRankXP == 0;
            }

            if (!slot.Item.IsValid) return;

            List<InventorySlot> slots = EntityManager.LocalPlayer.BagsItems.FindAll(Find);
            if (!slots.Any()) return;

            InventorySlot sFirst = slots.First(s => s.Item.Count < s.Item.ItemDef.StackLimit);
            InventorySlot sLast = slots.Last();
            if (sFirst == null || sFirst == sLast) return;

            uint count = Math.Min(sFirst.Item.ItemDef.StackLimit - sFirst.Item.Count, sLast.Item.Count);
            sLast.Move(sFirst.BagId, sFirst.Slot, count);
            Thread.Sleep(250);
            sFirst.Group();
        }
    }
}
