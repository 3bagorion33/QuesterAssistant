using Astral;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.PowersManager
{
    internal class PowersManagerCore : ACore<PowersManagerData, PowersManagerForm>
    {
        protected override bool IsValid => Data.CharClassesList.Count > 0;
        protected override bool HookEnableFlag => Data.HotKey.Enabled;

        protected override void KeyboardHook(KeyEventArgs e)
        {
            void FindAndApply()
            {
                Preset pres;
                if (e.KeyData == Data.HotKey.Keys)
                {
                    string name = InputBox.MessageText("Type partial name of preset and press Enter:", center: true);
                    pres = Data.CurrPresets?.Find(x => x.Name.CaseContains(name));
                }
                else
                {
                    pres = Data.CurrPresets?.Find(x => x.HotKey.Keys == e.KeyData);
                }
                if (pres != null)
                {
                    Logger.WriteLine($"Applying preset with name '{pres.Name}'...");
                    Powers.ApplyPowers(pres?.PowersList);
                }
            }
            var flag = EntityManager.LocalPlayer.IsValid && !Game.IsCursorModeEnabled &&
                Core.GameHandle == WinAPI.GetForegroundWindow();
            if (flag)
            {
                Task.Factory.StartNew(FindAndApply);
            }
        }
    }
}
