using Astral.Logic.NW;
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
    public class PManagerData
    {
        public SerializableDictionary<CharClassCategory, PSet> CharClasses { get; set; }
        //public bool hkeys = false;

        public PManagerData()
        {
            CharClasses = new SerializableDictionary<CharClassCategory, PSet>
            {
                { CharClassCategory.ControlWizard, new PSet() },
                { CharClassCategory.DevotedCleric, new PSet() },
                { CharClassCategory.GreatWeaponFigher, new PSet() },
                { CharClassCategory.GuardianFighter, new PSet() },
                { CharClassCategory.HunterRanger, new PSet() },
                { CharClassCategory.OathboundPaladin, new PSet() },
                { CharClassCategory.SourgeWarlock, new PSet() },
                { CharClassCategory.TricksterRogue, new PSet() }
            };
        }
    }

    public class PSet
    {
        public SerializableDictionary<string, PList> PLists { get; set; }

        public PSet()
        {
            PLists = new SerializableDictionary<string, PList>();
        }
    }

    public class PList
    {
        public SerializableDictionary<TraySlot, string> Powers { get; set; }
        public PList()
        {
            Powers = new SerializableDictionary<TraySlot, string>
            {
                { TraySlot.AtWill1, string.Empty }, { TraySlot.AtWill2, string.Empty },
                { TraySlot.ClassFeature1, string.Empty }, { TraySlot.ClassFeature2, string.Empty },
                { TraySlot.Daily1, string.Empty }, { TraySlot.Daily2, string.Empty },
                { TraySlot.Encounter1, string.Empty },{ TraySlot.Encounter2, string.Empty },{ TraySlot.Encounter3, string.Empty },
                { TraySlot.Mechanic, string.Empty },
            };
        }
    }



    internal static class PManager
    {
        internal static bool CanUpdate => EntityManager.LocalPlayer.IsValid && !EntityManager.LocalPlayer.IsLoading;

        internal static Dictionary<TraySlot, Power> GetSlottedPowers()
        {
            Dictionary<TraySlot, Power> slottedPowers = new Dictionary<TraySlot, Power>();
            slottedPowers.Add(TraySlot.Mechanic, Powers.GetPowerBySlot((int)TraySlot.Mechanic));

            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add((TraySlot)i, Powers.GetPowerBySlot(i));
            }

            return slottedPowers;
        }

        internal static SerializableDictionary<TraySlot, string> GetSlottedPowersNames()
        {
            SerializableDictionary<TraySlot, string> slottedPowers = new SerializableDictionary<TraySlot, string>();
            slottedPowers.Add(TraySlot.Mechanic, Powers.GetPowerBySlot((int)TraySlot.Mechanic).PowerDef.InternalName);

            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add((TraySlot)i, Powers.GetPowerBySlot(i).PowerDef.InternalName);
            }

            return slottedPowers;
        }

        internal static bool ApplyPower(TraySlot slot, string newPowerInternalName)
        {
            Power newPower = Powers.GetPowerByInternalName(newPowerInternalName);
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

        internal static PManagerData LoadSettings()
        {
            try
            {
                var pManager = Astral.Functions.XmlSerializer.Deserialize<PManagerData>(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"));
                return pManager;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                return new PManagerData();
            }
        }

        internal static void SaveSettings(PManagerData pManager, CharClassCategory currCharClass)
        {
            PList pList = new PList
            {
                Powers = PManager.GetSlottedPowersNames()
            };

            //pManager.CharClasses[currCharClass].PLists.Add("Test Preset", pList);
            //pManager.CharClasses[currCharClass].PLists.Add("Test Preset2", pList);

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
