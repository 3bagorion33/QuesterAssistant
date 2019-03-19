using Astral;
using QuesterAssistant.Classes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant.Settings
{
    internal class SettingsCore : ACore<SettingsData>
    {
        public override SettingsData Data { get; set; } = new SettingsData();
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => Data.RoleToggleHotKey.Enabled;

        protected override void KeyboardHook(object sender, KeyEventArgs e)
        {
            if (Data.RoleToggleHotKey.Enabled && (Data.RoleToggleHotKey.Keys == e.KeyData))
            {
                Task.Factory.StartNew(API.ToogleRole); 
            }
        }
    }
}
