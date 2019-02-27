using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal partial class InputBox : XtraForm
    {
        public InputBox()
        {
            InitializeComponent();
        }
        
        internal static string MessageText(string message, string text = "", FormStartPosition startPosition = FormStartPosition.CenterParent)
        {
            InputBox inputBox = new InputBox();
            inputBox.labelMessage.Text = message;
            inputBox.textValue.Text = text;
            inputBox.StartPosition = startPosition;
            inputBox.ShowDialog();
            return inputBox.message;
        }

        private string message = string.Empty;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            message = textValue.Text;
            Close();
        }
    }
}