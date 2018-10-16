namespace MyNW.Classes
{
    using MyNW;
    using MyNW.Patchables.Enums;
    using System;
    using System.Collections.Generic;

    public class InventoryBag : NativeObject
    {
        // Methods
        public InventoryBag(IntPtr pointer) : base(pointer)
        {
        }

        public InventorySlot GetSlotByIndex(uint index)
        {
            IntPtr ptr = Memory.MMemory.Read<IntPtr>(.(base.Pointer, 12));
            if (.(ptr, IntPtr.Zero))
            {
                uint num = Memory.MMemory.Read<uint>(.(ptr, 12));
                if (num > 0x3e8)
                {
                    num = 0x3e8;
                }
                if (index < num)
                {
                    return new InventorySlot(Memory.MMemory.Read<IntPtr>(.(ptr, (int) (index * 4))), this.BagId, index);
                }
            }
            return new InventorySlot(IntPtr.Zero);
        }

        // Properties
        public InvBagIDs BagId
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 0));
            }
        }

        public InvBagDef Def
        {
            get
            {
                return new InvBagDef(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 4)));
            }
        }

        public uint FilledSlots
        {
            get
            {
                return Hooks.PlayerBagsCallback.GetInventoryBagFilledSlotsById(this.BagId);
            }
        }

        public List<InventorySlot> GetItems
        {
            get
            {
                List<InventorySlot> list = new List<InventorySlot>();
                foreach (InventorySlot slot in this.Slots)
                {
                    if (slot.Filled)
                    {
                        list.Add(slot);
                    }
                }
                return list;
            }
        }

        public uint MaxSlots
        {
            get
            {
                return Hooks.PlayerBagsCallback.GetInventoryBagMaxSlotsById(this.BagId);
            }
        }

        public MyNW.Classes.RewardBagInfo RewardBagInfo
        {
            get
            {
                return new MyNW.Classes.RewardBagInfo(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 0x10)));
            }
        }

        public List<InventorySlot> Slots
        {
            get
            {
                List<InventorySlot> list = new List<InventorySlot>();
                IntPtr ptr = Memory.MMemory.Read<IntPtr>(base.Pointer + 12);
                if (ptr != IntPtr.Zero)
                {
                    uint num = Memory.MMemory.Read<uint>(ptr - 12);
                    if (num > 0x3e8)
                    {
                        num = 0x3e8;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        IntPtr pointer = Memory.MMemory.Read<IntPtr>(ptr + (i * 4));
                        if (pointer != IntPtr.Zero)
                        {
                            InventorySlot item = new InventorySlot(pointer, this.BagId, (uint) i);
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
        }
    }
}

