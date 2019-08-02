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
        protected override bool HookEnableFlag => Data.RoleToggleHotKey.Enabled || Data.HideClient.HotKey.Enabled;

        private IntPtr handle;

        private void HideGameWindow()
        {
            if (Core.GameProcess.HasExited) return;

            if (WinAPI.IsWindowVisible(Core.GameWindowHandle) && !WinAPI.IsWindowMinimize(Core.GameWindowHandle))
            {
                handle = Core.GameWindowHandle;
                WinAPI.MinimizeWindow(handle);
                if (Data.HideClient.HideMode == SettingsData.HideGameClient.Mode.Hide)
                    WinAPI.HideWindow(handle);
                return;
            }
            if (handle != IntPtr.Zero)
            {
                if (Data.HideClient.HideMode == SettingsData.HideGameClient.Mode.Hide)
                    WinAPI.UnhideWindow(handle);
                WinAPI.RestoreWindow(handle);
                WinAPI.SetForegroundWindow(handle);
            }
        }

        protected override void KeyboardHook(KeyEventArgs e)
        {
            if (Data.RoleToggleHotKey.Keys == e.KeyData)
            {
                Task.Factory.StartNew(API.ToogleRole); 
            }

            if (Data.HideClient.HotKey.Keys == e.KeyData)
            {
                HideGameWindow();
                e.SuppressKeyPress = false;
            }
        }
    }
}
