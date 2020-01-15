using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes;

namespace QuesterAssistant.UIEditors.Forms
{
    internal partial class InventorySelectForm : XtraForm
    {
        private InventorySelect InventorySelector { get; set; }

        private InventorySelectForm()
        {
            InitializeComponent();
        }

        public static void Show(InventorySelect toEdit)
        {
            var form = new InventorySelectForm
            {
                StartPosition = FormStartPosition.CenterParent,
                InventorySelector = toEdit ?? new InventorySelect()
            };
            form.propertyGridControl1.SelectedObject = form.InventorySelector;
            var result = form.ShowDialog();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}