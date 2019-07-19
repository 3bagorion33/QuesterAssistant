using DevExpress.XtraEditors;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System.Windows.Forms;

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

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            bsrcRoleToggleHotKey.DataSource = Core.Data.RoleToggleHotKey;
            bsrcHideGameHotKey.DataSource = Core.Data.HideGameHotKey;

            chkRoleToggleEnabled.BindAdd(bsrcRoleToggleHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.BindAdd(bsrcRoleToggleHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);

            chkHideGameEnabled.BindAdd(bsrcHideGameHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtHideGameString.BindAdd(bsrcHideGameHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
        }
    }
}
