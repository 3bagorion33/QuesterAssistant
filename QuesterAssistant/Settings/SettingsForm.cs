using DevExpress.XtraEditors;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common.Extensions;
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
            bsrcHotKey.DataSource = Core.Data.RoleToggleHotKey;

            chkRoleToggleEnabled.BindAdd(bsrcHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.BindAdd(bsrcHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
        }
    }
}
