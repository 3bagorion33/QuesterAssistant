using Astral;
using Astral.Addons;
using Astral.Forms;
using Astral.Logic.NW;
using QuesterAssistant.Classes;
using QuesterAssistant.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Timers;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        // Properties
        public override string Author => "Orion33";
        public override Image Icon => null;
        public override string Name => "Quester Assistant";
        public override BasePanel Settings => new Panels.Main();
        internal static string SettingsPath => Path.Combine(Astral.Controllers.Directories.SettingsPath, "QuesterAssistant");

        public override void OnBotStart() { }
        public override void OnBotStop() { }

        public override void OnLoad()
        {
            Astral.Quester.API.BeforeStartEngine += API_BeforeStartEngine;
        }

        private void API_BeforeStartEngine(object sender, Astral.Logic.Classes.FSM.BeforeEngineStart e)
        {
            Astral.Quester.API.Engine.AddState(new States.Identify());
            Astral.Quester.API.Engine.States.Sort();
        }

        public override void OnUnload() { }
    }
}

