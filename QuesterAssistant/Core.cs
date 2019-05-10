using Astral;
using Astral.Addons;
using MyNW;
using QuesterAssistant.Classes;
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
        public override Image Icon => null;
        public override string Name => "Quester Assistant";
        public override Astral.Forms.BasePanel Settings => new Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");
        internal static string Category => typeof(Core).Namespace;
        private static Process GameProcess => Process.GetProcessById((int)Memory.ProcessId);
        internal static IntPtr GameHandle => GameProcess.MainWindowHandle;

        /// <summary>
        /// ќбъект, отслеживающий изменение состо€ний бота
        /// и уведомл€ющий в случае отсутстви€ таковых
        /// </summary>
        private NotifyStatusMonitor statusMonitor = new NotifyStatusMonitor();

        internal static KeyboardHook KeyboardHook { get; private set; } = new KeyboardHook();
        private static List<Keys> keysMask = new List<Keys> {
            Keys.LWin, Keys.RWin,
            Keys.LShiftKey, Keys.RShiftKey,
            Keys.LControlKey, Keys.RControlKey,
            Keys.LMenu, Keys.RMenu,
            Keys.Apps, Keys.Back
        };

        internal static PowersManager.PowersManagerCore PowersManagerCore { get; private set; } = new PowersManager.PowersManagerCore();
        internal static Settings.SettingsCore SettingsCore { get; private set; } = new Settings.SettingsCore();
        internal static PushNotify.PushNotifyCore PushNotifyCore { get; private set; } = new PushNotify.PushNotifyCore();

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

            Astral.Quester.API.BeforeStartEngine += API_BeforeStartEngine;
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Task.Factory.StartNew(HooksLoader.SetHook);
        }

        private System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Ёта херн€ нужна дл€ избежани€ ошибок загрузки ресурсов и работы BinaryFormatter
            if (args.Name.Contains($"{Category}.resources"))
                return typeof(Astral.Forms.Main).Assembly;

            return typeof(Core).Assembly;
        }

        private void API_BeforeStartEngine(object sender, Astral.Logic.Classes.FSM.BeforeEngineStart e)
        {
            //Logger.WriteLine("Loading states");
            //Astral.Quester.API.Engine.AddState(new States.Identify());
            statusMonitor.Enabled = true;
            Logger.WriteLine("NotifyStatusMonitor Activated");
        }

        public override void OnUnload()
        {
            KeyboardHook.Dispose();
        }
    }
}

