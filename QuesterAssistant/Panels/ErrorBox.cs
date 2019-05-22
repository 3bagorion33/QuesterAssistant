using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public class ErrorBox
    {
        public static void Show(string text)
        {
            XtraMessageBox.Show(owner: Form.ActiveForm, text: text, caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
        }
    }
}
