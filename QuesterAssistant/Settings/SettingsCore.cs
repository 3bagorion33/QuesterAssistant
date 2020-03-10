using System;
using Astral;
using QuesterAssistant.Classes;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Monitoring;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace QuesterAssistant.Settings
{
    internal class SettingsCore : ACore<SettingsData, SettingsForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => Data.RoleToggleHotKey.Enabled || Data.HideClient.HotKey.Enabled;

        private void HideGameWindow()
        {
            GameClient.ToggleVisibility(Data.HideClient.HideMode == SettingsData.HideClientClass.Mode.Hide);
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
                       GameClient.IsForeground;

            if (Data.PauseBot.HotKey.Enabled && flag && (e.KeyCode & Keys.W) != 0)
            {
                //e.SuppressKeyPress = false;
                Task.Factory.StartNew(PauseBot);
            }
        }
    }
}
