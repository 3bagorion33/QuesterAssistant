using Astral;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.PowersManager;
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
    internal class Powers : Astral.Logic.NW.Powers
    {
        internal static List<Power> GetSlottedPowers()
        {
            List<Power> slottedPowers = new List<Power>();
            slottedPowers.Add(new Power(TraySlot.Mechanic, GetPowerBySlot((int)TraySlot.Mechanic).PowerDef.InternalName));

            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add(new Power((TraySlot)i, GetPowerBySlot(i).PowerDef.InternalName));
            }

            return slottedPowers;
        }

        internal static void ApplyPowers(List<Power> powers)
        {
            foreach (var pwr in powers)
            {
                Task.Factory.StartNew(() => ApplyPower(pwr));
            }
        }

        internal static void ApplyPower(Power pwr)
        {
            MyNW.Classes.Power newPower = GetPowerByInternalName(pwr.InternalName);
            if (!newPower.IsValid)
            {
                Debug.WriteLine(string.Format("{0} not valid", newPower.PowerDef.InternalName));
                return;
            }

            if (newPower.TraySlot == (uint)pwr.TraySlot)
            {
                Debug.WriteLine(string.Format("{0} => \n{1}", pwr.InternalName, newPower.PowerDef.InternalName));
                return;
            }

            var currPower = GetPowerBySlot((int)pwr.TraySlot);

            while (currPower.RechargeTime > 0 || currPower.SubCombatStatePowers.Exists(x => x.RechargeTime > 0))
            {
                Debug.WriteLine(currPower.PowerDef.DisplayName);
                Thread.Sleep(200);
            }
            Debug.WriteLine(currPower.PowerDef.DisplayName);
            Thread.Sleep(400);
            Injection.cmdwrapper_PowerTray_Slot(newPower, pwr.TraySlot);
            Debug.WriteLine(string.Format("Slot power => \n{0}", newPower.PowerDef.InternalName));
            return;
        }
    }
}
