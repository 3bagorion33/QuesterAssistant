using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    internal partial class MessageBoxSpc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleButton1 = new SimpleButton();
            this.message = new LabelControl();
            base.SuspendLayout();
            this.simpleButton1.Location = new Point(105, 56);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "OK (F12)";
            this.simpleButton1.Click += this.simpleButton1_Click;
            this.message.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.message.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            this.message.AutoSizeMode = LabelAutoSizeMode.None;
            this.message.Location = new Point(7, 7);
            this.message.Name = "message";
            this.message.Size = new Size(269, 41);
            this.message.TabIndex = 1;
            this.message.Text = "-";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(284, 83);
            base.Controls.Add(this.message);
            base.Controls.Add(this.simpleButton1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "MessageBoxSpc";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Message";
            base.TopMost = true;
            base.ResumeLayout(false);
        }

        #endregion

        private SimpleButton simpleButton1;
        private LabelControl message;
    }
}