using System;
using System.Windows.Forms;
using Astral.Controllers;
using DevExpress.XtraEditors;

namespace QuesterAssistant.UIEditors.Forms
{
    public partial class TargetSelectForm : XtraForm
    {
        static TargetSelectForm selectForm = null;
        public TargetSelectForm()
        {
            InitializeComponent();
        }

        public static DialogResult TargetGuiRequest(string caption, Form form_0 = null)
        {
            if (selectForm == null)
                selectForm = new TargetSelectForm();

            try
            {
                selectForm.lblMessage.Text = caption;
                Binds.AddAction(Keys.F12, selectForm.btnOK.PerformClick);
                return selectForm.ShowDialog();
            }
            finally
            {
                Binds.RemoveAction(Keys.F12);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private SimpleButton btnOK;

        private LabelControl lblMessage;
    }
}
