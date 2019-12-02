using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;

namespace QuesterAssistant.Panels
{
    public static class QMessageBox
    {
        private static void Init()
        {
            UserLookAndFeel.Default.SkinName = Core.SkinName;
            XtraMessageBox.AllowCustomLookAndFeel = true;
        }

        public static void ShowError(string text, DefaultBoolean allowHtml = DefaultBoolean.Default)
        {
            Init();
            XtraMessageBox.Show(
                owner: Form.ActiveForm,
                text: text,
                caption: "Error",
                icon: MessageBoxIcon.Error,
                buttons: MessageBoxButtons.OK,
                allowHtmlText: allowHtml);
        }

        public static DialogResult ShowDialog(string text, string caption = "Confirm", DefaultBoolean allowHtml = DefaultBoolean.Default)
        {
            Init();
            return XtraMessageBox.Show(
                owner: Form.ActiveForm,
                text: text,
                caption: caption,
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.YesNo,
                allowHtmlText: allowHtml);
        }

        public static void ShowInfo(string text, string caption = "Info", DefaultBoolean allowHtml = DefaultBoolean.Default)
        {
            Init();
            XtraMessageBox.Show(
                owner: Form.ActiveForm,
                text: text,
                caption: caption,
                icon: MessageBoxIcon.Information,
                buttons: MessageBoxButtons.OK,
                allowHtmlText: allowHtml);
        }
    }
}
