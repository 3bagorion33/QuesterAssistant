using System.Linq;
using Astral;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuesterAssistant.Classes.Monitoring;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.PowersManager
{
    internal class PowersManagerCore : ACore<PowersManagerData, PowersManagerForm>
    {
        protected override bool IsValid => Data.CharClassesList.Count > 0;
        protected override bool HookEnableFlag => Data.HotKey.Enabled;

        protected override void KeyboardHookDown(KeyEventArgs e)
        {
            void FindAndApply()
            {
                Preset pres;
                if (e.KeyData == Data.HotKey.Keys)
                {
                    string name = InputBox.MessageText("Type partial name of preset and press Enter:", center: true);
                    pres = Data.ParagonPresets.ToList().Find(x => x.Name.CaseContains(name));
                }
                else
                {
                    pres = Data.ParagonPresets.ToList().Find(x => x.HotKey.Keys == e.KeyData);
                }
                if (pres != null)
                {
                    Logger.WriteLine($"Applying preset with name '{pres.Name}'...");
                    Powers.ApplyPowers(pres.PowersList);
                }
            }
            if (EntityManager.LocalPlayer.IsValid && !Game.IsCursorModeEnabled &&
                GameClient.IsForeground)
            {
                Task.Factory.StartNew(FindAndApply);
            }
        }
    }
}
