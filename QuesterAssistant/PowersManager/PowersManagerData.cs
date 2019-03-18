using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuesterAssistant.PowersManager
{
    [Serializable]
    public class PowersManagerData : NotifyChanged
    {
        public bool HotKeysEnabled { get; set; }
        public List<CharClass> CharClassesList { get; set; }
        public string HotKeys
        {
            get
            {
                var kc = new KeysConverter();
                return kc.ConvertToString(Keys);
            }
            set
            {
                var kc = new KeysConverter();
                Keys = (Keys)kc.ConvertFromString(value);
            }
        }
        internal Keys Keys;

        public PowersManagerData() { }
        protected internal List<Preset> CurrPresets
        {
            get
            {
                if (EntityManager.LocalPlayer.IsValid)
                {
                    return this?.CharClassesList?.Find(x => x.ParagonCategory == Paragon.Category)?.PresetsList ?? new List<Preset>();
                }
                return new List<Preset>();
            }
        }
        public override int GetHashCode()
        {
            return HotKeysEnabled.GetHashCode() ^ CharClassesList.GetHashCodeExt() ^ Keys.GetHashCode();
        }
    }

    [Serializable]
    public class CharClass
    {
        public ParagonCategory ParagonCategory { get; set; }
        public List<Preset> PresetsList { get; set; }

        public CharClass() { }
        public CharClass(ParagonCategory charClass)
        {
            ParagonCategory = charClass;
            PresetsList = new List<Preset>();
        }
        public override int GetHashCode()
        {
            return PresetsList.GetHashCodeExt() ^ ParagonCategory.GetHashCode();
        }
    }

    [Serializable]
    public class Preset
    {
        public string Name { get; set; }
        public string HotKeys
        {
            get
            {
                var kc = new KeysConverter();
                return kc.ConvertToString(Keys);
            }
            set
            {
                var kc = new KeysConverter();
                Keys = (Keys)kc.ConvertFromString(value);
            }
        }
        internal Keys Keys;
        public List<Power> PowersList { get; set; }

        public Preset() { }
        public Preset(string name)
        {
            Name = name;
            PowersList = new List<Power>()
            {
                { new Power(TraySlot.AtWill1, string.Empty) },
                { new Power(TraySlot.AtWill2, string.Empty) },
                { new Power(TraySlot.ClassFeature1, string.Empty) },
                { new Power(TraySlot.ClassFeature2, string.Empty) },
                { new Power(TraySlot.Daily1, string.Empty) },
                { new Power(TraySlot.Daily2, string.Empty) },
                { new Power(TraySlot.Encounter1, string.Empty) },
                { new Power(TraySlot.Encounter2, string.Empty) },
                { new Power(TraySlot.Encounter3, string.Empty) },
                { new Power(TraySlot.Mechanic, string.Empty) },
            };
        }
        public Preset(string name, List<Power> powers)
        {
            Name = name;
            PowersList = powers;
        }
        public override string ToString()
        {
            return Name;
        }
        public override int GetHashCode()
        {
            return PowersList.GetHashCodeExt() ^ Keys.GetHashCode() ^ Name.GetHashCode();
        }
    }

    [Serializable]
    public class Power
    {
        public TraySlot TraySlot { get; set; }
        public string InternalName { get; set; }

        public Power() { }
        public Power(TraySlot traySlot, string iName)
        {
            TraySlot = traySlot;
            InternalName = iName;
        }
        internal Power ToDispName()
        {
            var pwr = Powers.GetPowerByInternalName(InternalName);
            if (!pwr.IsValid) return new Power(TraySlot, "Unknown spell");
            return new Power(TraySlot, (pwr.IsInTray ? "[Slotted] " : "") + pwr.PowerDef.DisplayName);
        }
        public override int GetHashCode()
        {
            return TraySlot.GetHashCode() ^ InternalName.GetHashCode();
        }
    }
}
