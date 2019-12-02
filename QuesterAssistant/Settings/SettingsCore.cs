using System;
using Astral;
using QuesterAssistant.Classes;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

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
                if (Data.HideClient.HideMode == SettingsData.HideClientClass.Mode.Hide)
                    WinAPI.HideWindow(handle);
                return;
            }
            if (handle != IntPtr.Zero)
            {
                if (Data.HideClient.HideMode == SettingsData.HideClientClass.Mode.Hide)
                    WinAPI.UnhideWindow(handle);
                WinAPI.RestoreWindow(handle);
                WinAPI.SetForegroundWindow(handle);
            }
        }

        private void PauseBot()
        {
            new Astral.Logic.UCC.Actions.AbordCombat {IgnoreCombatTime = 1, IgnoreCombatMinHP = Data.PauseBot.Delay}.Run();
        }

        protected override void KeyboardHookDown(KeyEventArgs e)
        {
            if (Data.RoleToggleHotKey.Keys == e.KeyData)
            {
                Task.Factory.StartNew(API.ToogleRole); 
            }

            if (Data.HideClient.HotKey.Keys == e.KeyData)
            {
                e.SuppressKeyPress = false;
                Task.Factory.StartNew(HideGameWindow);
            }

            var flag = EntityManager.LocalPlayer.IsValid && !Game.IsCursorModeEnabled &&
                       Core.GameWindowHandle == WinAPI.GetForegroundWindow();

            if (Data.PauseBot.HotKey.Enabled && flag && (e.KeyCode & Keys.W) != 0)
            {
                e.SuppressKeyPress = false;
                Task.Factory.StartNew(PauseBot);
            }
        }
    }
}
