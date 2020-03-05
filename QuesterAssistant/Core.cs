using Astral;
using Astral.Addons;
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
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Classes.Patches;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        public override string Author => "DartKotik & MichaelProg";
        public override Image Icon => Properties.Resources.Icon;
        public override string Name => "Quester Assistant";
        public override Astral.Forms.BasePanel Settings => new Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");
        internal static string ProfilesPath => Astral.Controllers.Directories.ProfilesPath;
        internal static string Category => typeof(Core).Namespace;
        internal static string Deprecated => "Deprecated";
        internal static Process GameProcess => API.AttachedGameProcess;
        internal static IntPtr GameWindowHandle => GameProcess?.MainWindowHandle ?? new IntPtr();
        internal static IntPtr AstralHandle => Process.GetCurrentProcess().MainWindowHandle;
        internal static bool IsGameForeground => GameWindowHandle == WinAPI.GetForegroundWindow();
        internal static bool IsAstralForeground => AstralHandle == WinAPI.GetForegroundWindow();
        internal static string SkinName => "Office 2013 Light Gray";

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
        internal static InsigniaManager.InsigniaManagerCore InsigniaManagerCore { get; } = new InsigniaManager.InsigniaManagerCore();

        public override void OnBotStart()
        {
            ProfessionsPatch.RunAtPlay();
        }

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
                    QMessageBox.ShowError(ex.ToString());
                    Logger.WriteLine(ex.ToString());
                }
            }

            DevExpress.Utils.Drawing.Helpers.Win32SubclasserException.Allow = false;

            Astral.Quester.API.BeforeStartEngine += API_BeforeStartEngine;
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Task.Factory.StartNew(HooksLoader.SetHook, TaskCreationOptions.LongRunning);
            Patcher.Apply();
            if (SettingsCore.Data.Patches.ProfessionPatch)
                ProfessionsPatch.RunOnce();
            Interact.Patch();

            Monitor.Start();
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
            //Astral.Quester.API.Engine.AddState(new States.WayPointFilter());
            //Astral.Quester.API.Engine.States.Sort();
            //statusMonitor.Enabled = true;
            //Logger.WriteLine("NotifyStatusMonitor Activated");

            if (SettingsCore.Data.Patches.WayPointFilterPatch)
                new WayPointFilter().Run();
        }

        public override void OnUnload()
        {
            KeyboardHook.Dispose();
            Monitor.Stop();
        }
    }
}

