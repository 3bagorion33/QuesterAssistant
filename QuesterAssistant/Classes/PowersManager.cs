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

namespace QuesterAssistant.Classes
{
    [Serializable]
    public class PowerManagerData
    {
        public List<CharClass> CharClassesList { get; set; }
        public bool HotKeysEnabled { get; set; }

        public PowerManagerData() { }
        public PowerManagerData(bool b)
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

    internal static class PManager
    {
        internal static bool CanUpdate => EntityManager.LocalPlayer.IsValid; //&& !EntityManager.LocalPlayer.IsLoading;
        internal static CharacterClass CurrCharClass => EntityManager.LocalPlayer.Character.Class;

        internal static List<Power> GetSlottedPowers()
        {
            List<Power> slottedPowers = new List<Power>();
            slottedPowers.Add(new Power(TraySlot.Mechanic, Powers.GetPowerBySlot((int)TraySlot.Mechanic).PowerDef.InternalName));

            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add(new Power((TraySlot)i, Powers.GetPowerBySlot(i).PowerDef.InternalName));
            }

            return slottedPowers;
        }

        internal static void ApplyPowers(List<Power> powers)
        {
            foreach (var pwr in powers)
            {
                Task.Factory.StartNew(() => PManager.ApplyPower(pwr));
            }
        }

        internal static void ApplyPower(Power pwr)
        {
            MyNW.Classes.Power newPower = Powers.GetPowerByInternalName(pwr.InternalName);
            if (!newPower.IsValid)
            {
                Core.DebugWriteLine(string.Format("{0} not valid", newPower.PowerDef.InternalName));
                return;
            }

            if (newPower.TraySlot == (uint)pwr.TraySlot)
            {
                Core.DebugWriteLine(string.Format("{0} => \n{1}", pwr.InternalName, newPower.PowerDef.InternalName));
                return;
            }

            var currPower = Powers.GetPowerBySlot((int)pwr.TraySlot);

            while (currPower.RechargeTime > 0 || currPower.SubCombatStatePowers.Exists(x => x.RechargeTime > 0))
            {
                Core.DebugWriteLine(currPower.PowerDef.DisplayName);
                Thread.Sleep(200);
            }
            Core.DebugWriteLine(currPower.PowerDef.DisplayName);
            Thread.Sleep(400);
            Injection.cmdwrapper_PowerTray_Slot(newPower, pwr.TraySlot);
            Core.DebugWriteLine(string.Format("Slot power => \n{0}", newPower.PowerDef.InternalName));
            return;
        }

        internal static PowerManagerData LoadSettings()
        {
            var path = Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml");

            if (File.Exists(path))
            {
                PowerManagerData pManager;
                try
                {
                    pManager = Astral.Functions.XmlSerializer.Deserialize<PowerManagerData>(path);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Logger.WriteLine("Powers Manager settings is wrong, using default...");
                    pManager = new PowerManagerData(true);
                }
                
                if (pManager.CharClassesList == null)
                {
                    Logger.WriteLine("Powers Manager settings is wrong, using default...");
                    pManager = new PowerManagerData(true);
                }
                else
                {
                    Logger.WriteLine("Powers Manager settings has been loaded...");
                }

                return pManager;
            }
            Logger.WriteLine("Powers Manager settings not found, using default...");
            return new PowerManagerData(true);
        }

        internal static void SaveSettings(PowerManagerData pManager)
        {
            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"), pManager);
                Logger.WriteLine("Powers Manager settings has been saved...");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
}
