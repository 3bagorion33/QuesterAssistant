﻿using Astral.Forms;
using QuesterAssistant.PowersManager;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public partial class Main : BasePanel
    {
        internal static event Action<string> OnLoadSettings;
        internal static event Action<string> OnSaveSettings;

        public Main() : base("Quester Assistant")
        {
            InitializeComponent();
            components = new Container();
            OnPanelLeave += Dispose;
            lblVersion.Text = $"v {System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion}";

            // Init Powers Manager
            var powersManagerForm = new PowersManagerForm();
            powersManagerForm.Dock = DockStyle.Fill;
            pManagerTab.Controls.Add(powersManagerForm);

            // Init Mini Quester
            Astral.Quester.Forms.Main miniQuesterForm = new Astral.Quester.Forms.Main();
            miniQuesterForm.MinimanistMode();
            miniQuesterForm.Dock = DockStyle.Fill;
            mQuesterTab.Controls.Add(miniQuesterForm);
        }

        private void Dispose(object s, EventArgs e)
        {
            Dispose(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSaveSettings(mainTabControl.SelectedTabPage.Name);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OnLoadSettings(mainTabControl.SelectedTabPage.Name);
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
