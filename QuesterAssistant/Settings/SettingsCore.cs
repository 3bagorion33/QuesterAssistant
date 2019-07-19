using System;
using Astral;
using QuesterAssistant.Classes;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Settings
{
    internal class SettingsCore : ACore<SettingsData, SettingsForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => Data.RoleToggleHotKey.Enabled || Data.HideGameHotKey.Enabled;

        private IntPtr handle;

        private void HideGameWindow()
        {
            if (Core.GameProcess.HasExited) return;

            if (WinAPI.IsWindowVisible(Core.GameWindowHandle))
            {
                handle = Core.GameWindowHandle;
                WinAPI.HideWindow(handle);
                return;
            }
            if (handle != IntPtr.Zero)
            {
                WinAPI.RestoreWindow(handle);
                Pause.Sleep(50);
                WinAPI.SetForegroundWindow(handle);
            }
        }

        protected override void KeyboardHook(KeyEventArgs e)
        {
            if (Data.RoleToggleHotKey.Keys == e.KeyData)
            {
                Task.Factory.StartNew(API.ToogleRole); 
            }

            if (Data.HideGameHotKey.Keys == e.KeyData)
            {
                HideGameWindow();
            }
        }
    }
}
