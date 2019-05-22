using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public class DialogBox
    {
        public static DialogResult Show(string text, string caption)
        {
            return XtraMessageBox.Show(owner: Form.ActiveForm, text: text, caption: caption, icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
        }
    }
}
