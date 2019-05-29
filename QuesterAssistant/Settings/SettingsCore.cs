using Astral;
using QuesterAssistant.Classes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant.Settings
{
    internal class SettingsCore : ACore<SettingsData, SettingsForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => Data.RoleToggleHotKey.Enabled;

        protected override void KeyboardHook(KeyEventArgs e)
        {
            if (Data.RoleToggleHotKey.Keys == e.KeyData)
            {
                Task.Factory.StartNew(API.ToogleRole); 
            }
        }
    }
}
