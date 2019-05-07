using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public class ErrorBox
    {
        public static void Show(string text)
        {
            XtraMessageBox.Show(text: text, caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
        }
    }
}
