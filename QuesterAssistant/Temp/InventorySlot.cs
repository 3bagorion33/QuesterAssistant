namespace MyNW.Classes
{
    //using ;
    //using ;
    using MyNW;
    using MyNW.Internals;
    using MyNW.Patchables.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class InventorySlot : NativeObject
    {
        // Fields
        [CompilerGenerated]
        private InvBagIDs ;
        [CompilerGenerated]
        private uint ;
        [NonSerialized]
        internal static  ;

        // Methods
        static InventorySlot()
        {
            ..(typeof(InventorySlot));
        }

        public InventorySlot(IntPtr pointer) : base(pointer)
        {
            base.Pointer = pointer;
            this.BagId = InvBagIDs.None;
            this.Slot = uint.MaxValue;
        }

        public InventorySlot(IntPtr pointer, InvBagIDs bagId, uint slot) : base(pointer)
        {
            base.Pointer = pointer;
            this.BagId = bagId;
            this.Slot = slot;
        }

        public void BindPet(string name)
        {
            this.BindPet(uint.MaxValue, name);
        }

        public void BindPet(uint petSlot, string name)
        {
            if (this.Item.ItemDef.Type == ItemType.SuperCritterPet)
            {
                Injection.(this, petSlot, name);
            }
        }

        public void Equip()
        {
            Injection.(this.BagId, this.Slot);
        }

        public void Evolve()
        {
            this.Evolve(null);
        }

        public void Evolve(InventorySlot seal)
        {
            Injection.(this, seal);
        }

        public void Exec()
        {
            object[] objArray1 = new object[] { (0x2dd0), this.BagId.ToString(), (0x267b), this.Slot };
            GameCommands.Execute(.(objArray1));
            object[] objArray2 = new object[] { (0x2de9), this.BagId.ToString(), (0x267b), this.Slot };
            GameCommands.Execute(.(objArray2));
        }

        public void Feed(int feedPoints)
        {
            Injection.(this, feedPoints);
        }

        public void Identify(MyNW.Classes.Item IdentifyScroll)
        {
            Injection.((uint) this.BagId, this.Slot, IdentifyScroll);
        }

        public void Move(InvBagIDs destinationBagId, uint count)
        {
            Injection.(this.BagId, this.Slot, destinationBagId, uint.MaxValue, count);
        }

        public void Move(InvBagIDs destinationBagId, uint destinationSlot, uint count)
        {
            Injection.(this.BagId, this.Slot, destinationBagId, destinationSlot, count);
        }

        public void MoveAll(InvBagIDs destinationBagId)
        {
            this.Move(destinationBagId, uint.MaxValue, this.Item.Count);
        }

        public void MoveAll(InvBagIDs destinationBagId, uint destinationSlot)
        {
            this.Move(destinationBagId, destinationSlot, this.Item.Count);
        }

        public void MoveAllFromSharedBank()
        {
            this.MoveFromSharedBank(uint.MaxValue);
        }

        public void MoveAllToSharedBank()
        {
            this.MoveToSharedBank(uint.MaxValue);
        }

        public void MoveBoundPet(InvBagIDs destinationBag)
        {
            Injection.((uint) destinationBag, uint.MaxValue, (uint) this.BagId, this.Slot);
        }

        public void MoveBoundPet(InvBagIDs destinationBag, uint destinationSlot)
        {
            Injection.((uint) destinationBag, destinationSlot, (uint) this.BagId, this.Slot);
        }

        public void MoveFromSharedBank(uint count)
        {
            if (EntityManager.SharedBank.IsValid)
            {
                Injection.(EntityManager.SharedBank, this.BagId, this.Slot, EntityManager.LocalPlayer, InvBagIDs.Inventory, uint.MaxValue, count);
            }
        }

        public void MoveToPet(uint petIndex, uint petSlotIndex, bool equip)
        {
            Injection.(this.BagId, this.Slot, petIndex, petSlotIndex, equip);
        }

        public void MoveToSharedBank(uint count)
        {
            if (EntityManager.SharedBank.IsValid)
            {
                Injection.(EntityManager.LocalPlayer, this.BagId, this.Slot, EntityManager.SharedBank, InvBagIDs.Inventory, uint.MaxValue, count);
            }
        }

        public void Remove(uint count)
        {
            if (!this.Item.ItemDef.CantDiscard)
            {
                Injection.(this.BagId, this.Slot, count);
            }
        }

        public void RemoveAll()
        {
            this.Remove(this.Item.Count);
        }

        public void StoreSellItem()
        {
            Injection.(this, this.Item.Count);
        }

        public void SummonPet(bool state)
        {
            if (this.BagId == InvBagIDs.SuperCritterPets)
            {
                Injection.(this.Slot, state);
            }
        }

        public override string ToString()
        {
            return this.Item.DisplayName;
        }

        // Properties
        public InvBagIDs BagId
        {
            [CompilerGenerated]
            get
            {
                return this.;
            }
            [CompilerGenerated]
            set
            {
                this. = value;
            }
        }

        public bool Filled
        {
            get
            {
                return (this.IsValid && this.Item.IsValid);
            }
        }

        public MyNW.Classes.Item Item
        {
            get
            {
                return new MyNW.Classes.Item(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 4)));
            }
        }

        public uint Slot
        {
            [CompilerGenerated]
            get
            {
                return this.;
            }
            [CompilerGenerated]
            set
            {
                this. = value;
            }
        }

        public InvBagIDs SlotType
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 0));
            }
        }
    }
}

