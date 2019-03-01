﻿using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MyNW;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Panels
{
    internal partial class InputBox : XtraForm
    {
        private static bool isLoaded = false;

        public InputBox()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                isLoaded = true;
            }
        }
        
        internal static string MessageText(string message, string text = "", FormStartPosition startPosition = FormStartPosition.CenterParent)
        {
            InputBox inputBox = new InputBox();
            inputBox.labelMessage.Text = message;
            inputBox.textValue.Text = text;
            inputBox.StartPosition = startPosition;
            inputBox.TopMost = true;
            Thread.Sleep(300);
            inputBox.Show();
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
            if (Handle != Win32.User32.GetForegroundWindow())
            {
                Win32.User32.SetForegroundWindow(System.Diagnostics.Process.GetProcessById((int)Memory.ProcessId).MainWindowHandle);
                Win32.User32.SetForegroundWindow(Handle);
            }
        }
    }
}