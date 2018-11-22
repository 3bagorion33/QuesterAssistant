using MyNW.Patchables.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
