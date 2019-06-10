using Astral.Forms;
using System;
using System.ComponentModel;

namespace QuesterAssistant.Panels
{
    public partial class Main : BasePanel
    {
        internal static event EventHandler LoadSettings;
        internal static event EventHandler SaveSettings;

        public Main() : base("Quester Assistant")
        {
            InitializeComponent();
        }

        private void Dispose(object s, EventArgs e)
        {
            Dispose(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings(mainTabControl.SelectedTabPage, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadSettings(mainTabControl.SelectedTabPage, null);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            components = new Container();
            //OnPanelLeave += Dispose;

            // Init Tabs
            settingsTab.Controls.Add(Core.SettingsCore.Panel);
            upgradeTab.Controls.Add(Core.UpgradeManagerCore.Panel);
            pManagerTab.Controls.Add(Core.PowersManagerCore.Panel);
            pushTab.Controls.Add(Core.PushNotifyCore.Panel);
            aboutTab.Controls.Add(new About());
        }
    }
}
