using System;
using System.Text;
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
            // x32
            //string[] mnemonics = {
            //    "push 0",
            //    "call " + (Memory.BaseAdress + 0x5c9fd0),
            //    "add esp, 0x4",
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "UIGen_AuctionRequestAuctionsBidByPlayer");
            //string[] mnemonics = {
            //    "call " + (Memory.BaseAdress + 0x5CC110),
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "gslGarrison_TendStructureOnCurrentPlot");

            //x64

            //string[] mnemonics = {
            //    "sub rsp, 0x20",
            //    "mov rax, " + (Memory.BaseAdress + 0x6DF7A0),
            //    "call rax",
            //    "add rsp, 0x20",
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "gslGarrison_TendStructureOnCurrentPlot");
            //var machineid = Memory.MMemory.ReadBytes(Memory.BaseAdress + 0x2640BD0, 64);
            //textEdit1.Text = System.Text.Encoding.UTF8.GetString(machineid, 0, machineid.Length);

            var machineid = Memory.MMemory.ReadString(Memory.BaseAdress + 0x2640BD0, Encoding.UTF8, 64);
            textEdit1.Text = machineid;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var machineid = textEdit1.Text;
            Memory.MMemory.WriteString(Memory.BaseAdress + 0x2640BD0, Encoding.UTF8, machineid);
        }
    }
}
