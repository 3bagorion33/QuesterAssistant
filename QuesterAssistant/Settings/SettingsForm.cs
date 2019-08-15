using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MyNW;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Settings
{
    internal partial class SettingsForm : CoreForm
    {
        private SettingsCore Core => core as SettingsCore;

        public SettingsForm()
        {
            InitializeComponent();
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

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            bsrcRoleToggleHotKey.DataSource = Core.Data.RoleToggleHotKey;
            bsrcHideGameHotKey.DataSource = Core.Data.HideClient.HotKey;
            bsrcHideMode.DataSource = Core.Data.HideClient.HideMode;
            cbxHideMinimize.Properties.Items.AddRange(typeof(SettingsData.HideGameClient.Mode).GetEnumValues());

            Core.SettingsLoaded += Rebind;

            Rebind();
        }

        private void Rebind()
        {
            chkRoleToggleEnabled.DataBindings.Clear();
            chkRoleToggleEnabled.BindAdd(bsrcRoleToggleHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.DataBindings.Clear();
            txtRoleToggleString.BindAdd(bsrcRoleToggleHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);

            chkHideGameEnabled.DataBindings.Clear();
            chkHideGameEnabled.BindAdd(bsrcHideGameHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtHideGameString.DataBindings.Clear();
            txtHideGameString.BindAdd(bsrcHideGameHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);

            cbxHideMinimize.DataBindings.Clear();
            cbxHideMinimize.BindAdd(Core.Data.HideClient, nameof(ComboBoxEdit.EditValue), nameof(SettingsData.HideGameClient.HideMode));
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string[] mnemonics = {
            //    "push 0",
            //    "call " + (Memory.BaseAdress + 0x5c9fd0),
            //    "add esp, 0x4",
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "UIGen_AuctionRequestAuctionsBidByPlayer");
            string[] mnemonics = {
                "call " + (Memory.BaseAdress + 0x5CC110),
                "retn"
            };
            Hooks.Executor.Execute<IntPtr>(mnemonics, "gslGarrison_TendStructureOnCurrentPlot");
        }
    }
}
