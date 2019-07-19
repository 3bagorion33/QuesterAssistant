using Astral;
using Astral.Addons;
using MyNW;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Hooks;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        public override string Author => "DartKotik & MichaelProg";
        public override Image Icon => Properties.Resources.Icon;
        public override string Name => "Quester Assistant";
        public override Astral.Forms.BasePanel Settings => new Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");
        internal static string Category => typeof(Core).Namespace;
        internal static Process GameProcess => Process.GetProcessById((int)Memory.ProcessId);
        internal static IntPtr GameWindowHandle => GameProcess.MainWindowHandle;
        internal static IntPtr AstralHandle => Process.GetCurrentProcess().MainWindowHandle;
        internal static bool IsGameForeground => GameWindowHandle == WinAPI.GetForegroundWindow();
        internal static bool IsAstralForeground => AstralHandle == WinAPI.GetForegroundWindow();

        /// <summary>
        /// ќбъект, отслеживающий изменение состо€ний бота
        /// и уведомл€ющий в случае отсутстви€ таковых
        /// </summary>
        //private NotifyStatusMonitor statusMonitor = new NotifyStatusMonitor();

        internal static KeyboardHook KeyboardHook { get; } = new KeyboardHook();
        private static List<Keys> keysMask = new List<Keys> {
            Keys.LWin, Keys.RWin,
            Keys.LShiftKey, Keys.RShiftKey,
            Keys.LControlKey, Keys.RControlKey,
            Keys.LMenu, Keys.RMenu,
            Keys.Apps, Keys.Back
        };

        internal static PowersManager.PowersManagerCore PowersManagerCore { get; } = new PowersManager.PowersManagerCore();
        internal static Settings.SettingsCore SettingsCore { get; } = new Settings.SettingsCore();
        internal static PushNotify.PushNotifyCore PushNotifyCore { get; } = new PushNotify.PushNotifyCore();
        internal static UpgradeManager.UpgradeManagerCore UpgradeManagerCore { get; } = new UpgradeManager.UpgradeManagerCore();

        public override void OnBotStart() { }
        public override void OnBotStop() { }

        public override void OnLoad()
        {
            if (!Directory.Exists(SettingsPath))
            {
                try
                {
                    Directory.CreateDirectory(SettingsPath);
                }
                catch (Exception ex)
                {
                    ErrorBox.Show(ex.ToString());
                    Logger.WriteLine(ex.ToString());
                }
            }

            DevExpress.Utils.Drawing.Helpers.Win32SubclasserException.Allow = false;

            Astral.Quester.API.BeforeStartEngine += API_BeforeStartEngine;
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Task.Factory.StartNew(HooksLoader.SetHook);
        }

        private System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Ёта херн€ нужна дл€ избежани€ ошибок загрузки ресурсов и работы BinaryFormatter
            return args.Name.Contains($"{Category}.resources") ? typeof(Astral.Forms.Main).Assembly : typeof(Core).Assembly;
        }

        private void API_BeforeStartEngine(object sender, Astral.Logic.Classes.FSM.BeforeEngineStart e)
        {
            //Logger.WriteLine("Loading states");
            //Astral.Quester.API.Engine.AddState(new States.Identify());
            //statusMonitor.Enabled = true;
            //Logger.WriteLine("NotifyStatusMonitor Activated");
        }

        public override void OnUnload()
        {
            KeyboardHook.Dispose();
        }
    }
}

