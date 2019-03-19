using Astral.Forms;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QuesterAssistant.Settings
{
    internal partial class SettingsForm : BasePanel
    {
        private SettingsData Data => Core.SettingsCore.Data;
        public SettingsForm() : base ("Settings")
        {
            InitializeComponent();

            bsrcHotKey.DataSource = Data.RoleToggleHotKey;

            chkRoleToggleEnabled.BindAdd(bsrcHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.BindAdd(bsrcHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);

            Panels.Main.LoadSettings += LoadSettings;
            Panels.Main.SaveSettings += SaveSettings;
        }

        private void SaveSettings(string tabName)
        {
            if (tabName == "settingsTab")
            {
                Core.SettingsCore.SaveSettings();
            }
        }

        private void LoadSettings(string tabName)
        {
            if (tabName == "settingsTab")
            {
                Core.SettingsCore.LoadSettings();
            }
        }

        private void txtRoleToggleString_KeyDown(object sender, KeyEventArgs e)
        {
            var txtEdit = sender as TextEdit;
            var k = e.KeyData;
            txtEdit.Text = k.IgnoreBack().ConvertToString();

            if (k.IsNotModifier())
            {
                ActiveControl = null;
            }
        }
    }
}
