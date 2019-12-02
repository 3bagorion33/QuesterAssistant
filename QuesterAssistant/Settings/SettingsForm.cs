using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
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
        private SettingsData Data => Core.Data;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void txtRoleToggleString_KeyDown(object sender, KeyEventArgs e)
        {
            var k = e.KeyData;
            (sender as TextEdit).Text = k.IgnoreBack().ConvertToString();

            if (k.IsNotModifier())
            {
                ActiveControl = null;
            }
        }

        private void txtPauseBotHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            (sender as TextEdit).Text = e.KeyData.IgnoreBack().ConvertToString();
            ActiveControl = null;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            bsrcRoleToggleHotKey.DataSource = Data.RoleToggleHotKey;
            bsrcHideGameHotKey.DataSource = Data.HideClient.HotKey;
            bsrcHideMode.DataSource = Data.HideClient;
            bsrcPauseBotHotKey.DataSource = Data.PauseBot.HotKey;
            bsrcPauseBot.DataSource = Data.PauseBot;
            cbxHideMinimize.Properties.Items.AddRange(typeof(SettingsData.HideClientClass.Mode).GetEnumValues());

            chkRoleToggleEnabled.BindAdd(bsrcRoleToggleHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.BindAdd(bsrcRoleToggleHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
            chkHideGameEnabled.BindAdd(bsrcHideGameHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtHideGameString.BindAdd(bsrcHideGameHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
            cbxHideMinimize.BindAdd(bsrcHideMode, nameof(ComboBoxEdit.EditValue), nameof(SettingsData.HideClientClass.HideMode));
            chkPauseBotHotKey.BindAdd(bsrcPauseBotHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtPauseBotHotKey.BindAdd(bsrcPauseBotHotKey, nameof(TextEdit.Text), nameof(HotKey.String));
            numPauseDelay.BindAdd(bsrcPauseBot, nameof(SpinEdit.EditValue), nameof(SettingsData.PauseBot.Delay));

            Data.HashChanged += bsrcRoleToggleHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcHideMode.ResetCurrentItem;
            Data.HashChanged += bsrcHideGameHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcPauseBotHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcPauseBot.ResetCurrentItem;
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

        private void btnClearStack_Click(object sender, EventArgs e)
        {
            ProfilesStack.Clear();
        }

        private void btnShowStack_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => XtraMessageBox.Show(ProfilesStack.Show(), DefaultBoolean.True));
        }
    }
}
