using Astral;
using Astral.Addons;
using Astral.Forms;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        public override string Author => "Orion33";
        public override Image Icon => null;
        public override string Name => "Quester Assistant";
        public override BasePanel Settings => new Panels.Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");
        internal static string Category = typeof(Core).Namespace;

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
        }

        private System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return typeof(Core).Assembly;
        }

        private void API_BeforeStartEngine(object sender, Astral.Logic.Classes.FSM.BeforeEngineStart e)
        {
            Logger.WriteLine("Loading states");
            Astral.Quester.API.Engine.AddState(new States.Identify());
        }

        public override void OnUnload() { }
    }
}

