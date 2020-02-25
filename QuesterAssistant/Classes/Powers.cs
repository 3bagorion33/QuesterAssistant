using MyNW.Internals;
using MyNW.Patchables.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.Classes
{
    internal class Powers : Astral.Logic.NW.Powers
    {
        public static List<Power> GetSlottedPowers()
        {
            List<Power> slottedPowers = new List<Power>();
            slottedPowers.Add(new Power(TraySlot.Mechanic, GetPowerBySlot((int)TraySlot.Mechanic).PowerDef.InternalName));
            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add(new Power((TraySlot)i, GetPowerBySlot(i).PowerDef.InternalName));
            }
            return slottedPowers;
        }

        public static void ApplyPowers(List<Power> powers)
        {
            foreach (var pwr in powers)
            {
                Task.Factory.StartNew(() => ApplyPower(pwr));
            }
        }

        private static void ApplyPower(Power pwr)
        {
            MyNW.Classes.Power newPower = GetPowerByInternalName(pwr.InternalName);
            if (!newPower.IsValid)
                return;

            if (newPower.TraySlot == (uint)pwr.TraySlot)
                return;

            var currPower = GetPowerBySlot((int) pwr.TraySlot);
            while (currPower.TraySlot != (uint) TraySlot.ClassFeature1 &&
                   currPower.TraySlot != (uint) TraySlot.ClassFeature2 &&
                   (currPower.IsActive || currPower.RechargeTime > 0 || 
                    currPower.SubCombatStatePowers.Exists(x => x.IsActive || x.RechargeTime > 0)))
                Pause.Sleep(200);
            Pause.Sleep(200);
            Injection.cmdwrapper_PowerTray_Slot(newPower, pwr.TraySlot);
        }
    }
}
