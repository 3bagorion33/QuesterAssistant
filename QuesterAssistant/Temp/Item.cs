namespace MyNW.Classes
{
    using ;
    using ;
    using MyNW;
    using MyNW.Classes.ItemProgression;
    using MyNW.Internals;
    using MyNW.Patchables.Enums;
    using System;
    using System.Collections.Generic;

    public class Item : NativeObject
    {
        // Fields
        [NonSerialized]
        internal static  ;

        // Methods
        static Item()
        {
            .(typeof(Item));
        }

        public Item(IntPtr pointer) : base(pointer)
        {
        }

        public bool CanExecute()
        {
            using (List<Power>.Enumerator enumerator = this.Powers.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (!enumerator.Current.CanExec())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void GemThisItem(Item usedGem, uint slot)
        {
            Injection.(usedGem, this, slot);
        }

        public bool IsItemFlagActive(ItemFlags flag)
        {
            return ((this.Flags & flag) > 0);
        }

        public bool IsRecommended()
        {
            return this.IsRecommended(new Item(IntPtr.Zero));
        }

        public bool IsRecommended(Item otherItem)
        {
            return Injection.(this, otherItem);
        }

        public void Salvage(int mode, uint count)
        {
            Injection.(this, mode, count);
        }

        public void SalvageAsAstralDiamonds()
        {
            int mode = 0;
            foreach (ItemSalvageModeDef def in Game.ItemSalvageModesDef)
            {
                if (.(def.SalvageNumeric, (0x2db6)))
                {
                    mode = def.SalvageMode;
                    break;
                }
            }
            this.Salvage(mode, 1);
        }

        public void SalvageAsRefinementCurrency()
        {
            this.SalvageAsRefinementCurrency(this.Count);
        }

        public void SalvageAsRefinementCurrency(uint count)
        {
            int mode = -1;
            foreach (ItemSalvageModeDef def in Game.ItemSalvageModesDef)
            {
                if (.(def.SalvageNumeric, (0x19e5)))
                {
                    mode = def.SalvageMode;
                    break;
                }
            }
            if (mode >= 0)
            {
                this.Salvage(mode, count);
            }
        }

        // Properties
        public List<ItemFlags> ActiveFlags
        {
            get
            {
                List<ItemFlags> list = new List<ItemFlags>();
                foreach (ItemFlags flags in Enum.GetValues(typeof(ItemFlags)))
                {
                    if (this.IsItemFlagActive(flags))
                    {
                        list.Add(flags);
                    }
                }
                return list;
            }
        }

        public MyNW.Classes.AlgoItemProps AlgoItemProps
        {
            get
            {
                return new MyNW.Classes.AlgoItemProps(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 0x10)));
            }
        }

        public uint Count
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 0x20));
            }
        }

        public string DisplayName
        {
            get
            {
                IntPtr ptr = Memory.MMemory.Read<IntPtr>(.(base.Pointer, 12));
                if (.(ptr, IntPtr.Zero))
                {
                    string str = .~(Memory.MMemory, ptr, .(), 0x80);
                    if (.~(str) > 0)
                    {
                        return str;
                    }
                }
                if (this.ItemDef.IsValid)
                {
                    return this.ItemDef.DisplayName;
                }
                return string.Empty;
            }
        }

        public uint Flags
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 40));
            }
        }

        public ulong Id
        {
            get
            {
                return Memory.MMemory.Read<ulong>(.(base.Pointer, 0));
            }
        }

        public uint Id1
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 0));
            }
        }

        public uint Id2
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(.(base.Pointer, 0), 4));
            }
        }

        public bool IsBound
        {
            get
            {
                return this.IsItemFlagActive(ItemFlags.Bound);
            }
        }

        public MyNW.Classes.ItemDef ItemDef
        {
            get
            {
                return new MyNW.Classes.ItemDef(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 8)));
            }
        }

        public IntPtr pItemDef
        {
            get
            {
                return Memory.MMemory.Read<IntPtr>(.(base.Pointer, 8));
            }
        }

        public uint PowerMagnitude
        {
            get
            {
                using (List<Power>.Enumerator enumerator = this.Powers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        foreach (AttribModDef def in enumerator.Current.PowerDef.AttribModsDef)
                        {
                            if (def.UnkMagnitude > 0)
                            {
                                return def.UnkMagnitude;
                            }
                        }
                    }
                }
                return 0;
            }
        }

        public List<Power> Powers
        {
            get
            {
                return NWList.Get<Power>(Memory.MMemory.Read<IntPtr>(base.Pointer + 0x18));
            }
        }

        public ItemProgressionLogic ProgressionLogic
        {
            get
            {
                return new ItemProgressionLogic(this, this.ItemDef.ProgressionDef);
            }
        }

        public uint RewardData
        {
            get
            {
                return Memory.MMemory.Read<uint>(.(base.Pointer, 0x1c));
            }
        }

        public SpecialItemProps SpecialProps
        {
            get
            {
                return new SpecialItemProps(Memory.MMemory.Read<IntPtr>(.(base.Pointer, 20)));
            }
        }
    }
}

