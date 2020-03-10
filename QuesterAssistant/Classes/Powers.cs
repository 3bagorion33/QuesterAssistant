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
            var slottedPowers = new List<Power>
            {
                new Power(TraySlot.Mechanic, GetPowerBySlot((int) TraySlot.Mechanic).PowerDef.InternalName)
            };
            for (int i = 0; i < 9; i++)
            {
                slottedPowers.Add(new Power((TraySlot)i, GetPowerBySlot(i).PowerDef.InternalName));
            }
            return slottedPowers;
        }

        public static void ApplyPowers(List<Power> powers)
        {
            foreach (var pwr in powers) Task.Factory.StartNew(() => ApplyPower(pwr));
        }

        private static void ApplyPower(Power pwr)
        {
            var newPower = GetPowerByInternalName(pwr.InternalName);
            if (!newPower.IsValid || newPower.TraySlot == (uint)pwr.TraySlot)
                return;

            var cPower = GetPowerBySlot((int) pwr.TraySlot);
            if (cPower.TraySlot != (uint) TraySlot.ClassFeature1 &&
                cPower.TraySlot != (uint) TraySlot.ClassFeature2)
            {
                do
                {
                    do Pause.Sleep(200);
                    while (cPower.IsActive || cPower.RechargeTime > 0 ||
                           cPower.SubCombatStatePowers.Exists(x => x.IsActive || x.RechargeTime > 0));
                }
                while (EntityManager.LocalPlayer.IsLoading);
            }
            Injection.cmdwrapper_PowerTray_Slot(newPower, pwr.TraySlot);
        }
    }
}
