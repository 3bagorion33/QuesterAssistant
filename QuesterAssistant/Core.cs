using Astral;
using Astral.Addons;
using Astral.Forms;
using Astral.Logic.NW;
using QuesterAssistant.Properties;
using System;
using System.Drawing;
using System.Timers;

namespace QuesterAssistant
{
    public class Core : Plugin
    {
        // Fields
        private Timer AutoIdent;

        // Properties
        public override string Author => "Orion33";
        public override Image Icon => null;
        public override string Name => "QuesterAssistant";
        public override BasePanel Settings => new Main();

        // Methods
        private void Identification(object sender, EventArgs e)
        {
            if (API.CurrentSettings.RefineArtifact)
            {
                Interact.IdentifyItems();
            }
        }

        public override void OnBotStart() { }
        public override void OnBotStop() { }

        public override void OnLoad()
        {
            this.AutoIdent = new Timer(2000);
            this.AutoIdent.Elapsed += new ElapsedEventHandler(this.Identification);
            this.AutoIdent.Enabled = true;
        }

        public override void OnUnload() { }

        internal static void DebugWriteLine(string text)
        {
#if DEBUG
            Logger.WriteLine(text);
#endif
        }

        internal static string DeprecatedMessage (string actionLabel, string actionInstead)
        {
            return string.Format("{0} is an obsolete action, use {1} instead.", actionLabel, actionInstead);
        }

        internal static string DeprecatedWriteLine (string actionLabel)
        {
            return string.Format("{0} is an obsolete action, profile need update, stop bot", actionLabel);
        }
    }
}

