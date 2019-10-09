using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Panels
{
    internal partial class InputBox : XtraForm
    {
        private static bool isLoaded = false;
        protected bool center = false;

        public InputBox()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                isLoaded = true;
            }
        }
        
        internal static string MessageText(string message, string text = "", bool center = false)
        {
            InputBox inputBox = new InputBox();
            inputBox.labelMessage.Text = message;
            inputBox.textValue.Text = text;
            inputBox.center = center;
            inputBox.StartPosition = FormStartPosition.CenterParent;
            if (center)
            {
                inputBox.StartPosition = FormStartPosition.CenterScreen;
                inputBox.TopMost = true;
                Thread.Sleep(300);
            }
            inputBox.ShowDialog();
            return inputBox.message;
        }

        private string message = string.Empty;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            message = textValue.Text;
            Close();
        }

        private void textValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void InputBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            isLoaded = false;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            if (center && (Handle != WinAPI.GetForegroundWindow()))
            {
                WinAPI.SetForegroundWindow(Core.GameWindowHandle);
                WinAPI.SetForegroundWindow(Handle);
            }
        }
    }
}