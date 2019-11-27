using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace QuesterAssistant.Panels
{
    public static class QMessageBox
    {
        private static void Init()
        {
            UserLookAndFeel.Default.SkinName = Core.SkinName;
            XtraMessageBox.AllowCustomLookAndFeel = true;
        }

        public static void ShowError(string text)
        {
            Init();
            XtraMessageBox.Show(owner: Form.ActiveForm, text: text, caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
        }

        public static DialogResult ShowDialog(string text, string caption)
        {
            Init();
            return XtraMessageBox.Show(owner: Form.ActiveForm, text: text, caption: caption, icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
        }
    }
}
