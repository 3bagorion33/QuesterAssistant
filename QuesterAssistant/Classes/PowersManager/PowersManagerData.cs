using Astral;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant.Classes.PowersManager
{
    [Serializable]
    public class PowersManagerData
    {
        public bool HotKeysEnabled { get; set; }
        public List<CharClass> CharClassesList { get; set; }

        public PowersManagerData() { }
        public PowersManagerData(bool b)
        {
            CharClassesList = new List<CharClass>
            {
                { new CharClass(CharClassCategory.ControlWizard) },
                { new CharClass(CharClassCategory.DevotedCleric) },
                { new CharClass(CharClassCategory.GreatWeaponFigher) },
                { new CharClass(CharClassCategory.GuardianFighter) },
                { new CharClass(CharClassCategory.HunterRanger) },
                { new CharClass(CharClassCategory.OathboundPaladin) },
                { new CharClass(CharClassCategory.SourgeWarlock) },
                { new CharClass(CharClassCategory.TricksterRogue) }
            };
            HotKeysEnabled = false;
        }

        protected internal List<Preset> CurrPresets
        {
            get
            {
                if (EntityManager.LocalPlayer.IsValid)
                {
                    return this?.CharClassesList?.Find(x => x.CharClassCategory == EntityManager.LocalPlayer.Character.Class.Category).PresetsList ?? new List<Preset>();
                }
                return new List<Preset>();
            }
        }

        protected internal void LoadSettings()
        {
            var path = Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml");

            if (File.Exists(path))
            {
                PowersManagerData pManager;
                try
                {
                    pManager = Astral.Functions.XmlSerializer.Deserialize<PowersManagerData>(path);
                    if (pManager.CharClassesList == null)
                    {
                        Logger.WriteLine("Powers Manager settings is wrong, using default...");
                    }
                    else
                    {
                        Logger.WriteLine("Powers Manager settings has been loaded...");
                        this.CharClassesList = pManager.CharClassesList;
                        this.HotKeysEnabled = pManager.HotKeysEnabled;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Logger.WriteLine("Powers Manager settings is wrong, using default...");
                }
                return;
            }
            Logger.WriteLine("Powers Manager settings not found, using default...");
        }

        protected internal void SaveSettings()
        {
            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"), this);
                Logger.WriteLine("Powers Manager settings has been saved...");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }

    [Serializable]
    public class CharClass
    {
        public CharClassCategory CharClassCategory { get; set; }
        public List<Preset> PresetsList { get; set; }

        public CharClass() { }
        public CharClass(CharClassCategory charClass)
        {
            CharClassCategory = charClass;
            PresetsList = new List<Preset>();
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
    }

    [Serializable]
    public class Power
    {
        public TraySlot TraySlot { get; set; }
        public string InternalName { get; set; }

        public Power() { }
        public Power(TraySlot traySlot, string iName)
        {
            this.TraySlot = traySlot;
            this.InternalName = iName;
        }

        internal Power ToDispName()
        {
            var pwr = Powers.GetPowerByInternalName(this.InternalName);
            if (!pwr.IsValid) return new Power(this.TraySlot, "Unknown spell");
            return new Power(this.TraySlot, (pwr.IsInTray ? "[Slotted] " : "") + pwr.PowerDef.DisplayName);
        }
    }
}
