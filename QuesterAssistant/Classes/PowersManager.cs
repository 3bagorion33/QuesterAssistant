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
using System.Windows.Forms;

namespace QuesterAssistant.Classes
{
    public class PowerManagerData
    {
        public List<CharClass> CharClassesList { get; set; }
        //public bool hkeys = false;

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
        }
    }

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

    public class Preset
    {
        public string Name { get; set; }
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
            return new Power(this.TraySlot, Powers.GetPowerByInternalName(this.InternalName).PowerDef.DisplayName);
        }
    }

    internal static class PManager
    {
        //internal static bool CanUpdate => EntityManager.LocalPlayer.IsValid && !EntityManager.LocalPlayer.IsLoading;
        internal static bool CanUpdate => EntityManager.LocalPlayer.IsValid;
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

        internal static bool ApplyPower(TraySlot slot, string newPowerInternalName)
        {
            MyNW.Classes.Power newPower = Powers.GetPowerByInternalName(newPowerInternalName);
            if (!newPower.IsValid)
            {
                Core.DebugWriteLine(string.Format("{0} not valid", newPower.PowerDef.InternalName));
                return false;
            }

            if (newPower.TraySlot == (uint)slot)
            {
                Core.DebugWriteLine(string.Format("{0} => {1}", newPowerInternalName, newPower.PowerDef.InternalName));
                return true;
            }

            var currPower = Powers.GetPowerBySlot((int)slot);
            while (currPower.IsOnCooldown())
            {
                Thread.Sleep(200);
            }
            Thread.Sleep(400);
            var playerPower = EntityManager.LocalPlayer.Character.Powers.Find(x => x.PowerDef.InternalName == newPowerInternalName);
            Injection.cmdwrapper_PowerTray_Slot(playerPower, slot);
            Core.DebugWriteLine(string.Format("Slot power => {0}", newPower.PowerDef.InternalName));
            return true;
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
                    pManager = new PowerManagerData(true);
                }
                if (pManager.CharClassesList == null)
                {
                    pManager = new PowerManagerData(true);
                }
                return pManager;
            }
            return new PowerManagerData(true);
        }

        internal static void SaveSettings(PowerManagerData pManager)
        {
            var idxCharClass = pManager.CharClassesList.FindIndex(x => x.CharClassCategory == CurrCharClass.Category);
            Core.DebugWriteLine(string.Format("Class: {0}[{1}] ", CurrCharClass, idxCharClass));
            if (idxCharClass < 0)
            {
                XtraMessageBox.AllowCustomLookAndFeel = true;
                XtraMessageBox.Show("Invalid character class!");
                return;
            }

            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"), pManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
}
