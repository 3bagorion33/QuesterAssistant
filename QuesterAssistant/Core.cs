using Astral;
using Astral.Addons;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes.Hooks;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        public override string Author => "Orion33 & MichaelProg";
        public override Image Icon => null;
        public override string Name => "Quester Assistant";
        public override Astral.Forms.BasePanel Settings => new Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");
        internal static string Category => typeof(Core).Namespace;

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
                    XtraMessageBox.Show(ex.ToString());
                    Logger.WriteLine(ex.ToString());
                }
            }

            Astral.Quester.API.BeforeStartEngine += API_BeforeStartEngine;
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Task.Factory.StartNew(HooksLoader.SetHook);
        }

        private System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Эта херня нужна для избежания ошибок загрузки ресурсов и работы BinaryFormatter
            if (args.Name.Contains($"{Category}.resources"))
                return typeof(Astral.Forms.Main).Assembly;

            return typeof(Core).Assembly;
        }

        private void API_BeforeStartEngine(object sender, Astral.Logic.Classes.FSM.BeforeEngineStart e)
        {
            Logger.WriteLine("Loading states");
            Astral.Quester.API.Engine.AddState(new States.Identify());
        }

        public override void OnUnload()
        {
            KeyboardHook.Dispose();
        }
    }
}

