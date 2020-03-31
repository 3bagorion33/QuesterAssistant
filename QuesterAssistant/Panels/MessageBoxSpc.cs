using System;
using System.Windows.Forms;
using Astral.Controllers;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Panels
{
    internal partial class MessageBoxSpc : XtraForm
    {
        private bool result;

        private MessageBoxSpc()
        {
            InitializeComponent();
        }

        public static DialogResult ShowDialog(string message, Form form = null)
        {
            if (form == null)
                form = typeof(Forms).GetStaticPropertyValue("Main") as Astral.Forms.Main;
            MessageBoxSpc messageBoxSpc = new MessageBoxSpc { message = { Text = message } };
            Binds.AddAction(Keys.F12, messageBoxSpc.simpleButton1.PerformClick);
            messageBoxSpc.ShowDialog();
            Binds.RemoveAction(Keys.F12);
            if (messageBoxSpc.result)
                return DialogResult.OK;
            return DialogResult.Cancel;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            result = true;
            Close();
        }
    }
}
