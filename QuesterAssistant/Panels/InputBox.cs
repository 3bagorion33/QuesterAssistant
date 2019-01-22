using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal partial class InputBox : DevExpress.XtraEditors.XtraForm
    {
        public InputBox()
        {
            InitializeComponent();
        }
        
        internal static string MessageText(string message)
        {
            InputBox inputBox = new InputBox();
            inputBox.labelMessage.Text = message;
            inputBox.ShowDialog();
            return inputBox.message;
        }

        private string message = string.Empty;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.message = this.textValue.Text;
            base.Close();
        }
    }
}