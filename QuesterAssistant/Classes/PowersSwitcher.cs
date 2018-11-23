using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QuesterAssistant.Classes
{
    class PowerData
    {
        public PowerData() { }

        public PowerData(TraySlot slot, string name, string gamename)
        {
            this.Slot = slot;
            this.Name = name;
            this.Gamename = gamename;
        }

        public TraySlot Slot { get; set; }
        public string Name { get; set; }
        public string Gamename { get; set; }
    }

    class PowerTab
    {
        public Keys Hotkey { get; set; }
        public List<PowerData> PowerData { get; set; }
        public CharClassCategory TabCharClass { get; set; }
        public PowerTab() { }

        public PowerTab(Keys Hotkey, CharClassCategory TabCharClass, List<PowerData> PowerData)
        {
            this.Hotkey = Hotkey;
            this.TabCharClass = TabCharClass;
            this.PowerData = PowerData;
        }
    }

    internal class SlottedPower
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

        internal static bool ApplyPower(TraySlot slot, Power newPower)
        {
            var currPower = Powers.GetPowerBySlot((int)slot);
            if (currPower.PowerId == newPower.PowerId) return true;

            while (currPower.IsOnCooldown())
            {
                Thread.Sleep(200);
            }
            Thread.Sleep(400);
            var playerPower = EntityManager.LocalPlayer.Character.Powers.Find(x => x.PowerId == newPower.PowerId);
            Injection.cmdwrapper_PowerTray_Slot(playerPower, slot);
            return true;
        }
    }
}
