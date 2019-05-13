﻿using Astral.Forms;
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
            components = new Container();
            OnPanelLeave += Dispose;
            lblVersion.Text = $"v {System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion}";

            // Init Tabs
            settingsTab.Controls.Add(Core.SettingsCore.Panel);
            upgradeTab.Controls.Add(Core.UpgradeManagerCore.Panel);
            pManagerTab.Controls.Add(Core.PowersManagerCore.Panel);
            pushTab.Controls.Add(Core.PushNotifyCore.Panel);
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

        private void hlkQAForumThread_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://neverwinter-bot.com/forums/viewtopic.php?f=155&t=9712");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/aorion33/10");
        }
    }
}
