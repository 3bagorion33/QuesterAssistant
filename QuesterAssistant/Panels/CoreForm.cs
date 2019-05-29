using QuesterAssistant.Classes;
using System;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    internal class CoreForm : UserControl
    {
        protected ICore core;

        public CoreForm() { }
        public CoreForm(ICore core)
        {
            this.core = core;
            Dock = DockStyle.Fill;
            Main.LoadSettings += LoadSettings;
            Main.SaveSettings += SaveSettings;
        }
        // protected из-за атавизма Power Manager
        protected void SaveSettings(object sender, EventArgs e)
        {
            if (sender == Parent)
            {
                core.SaveSettings();
            }
        }
        protected void LoadSettings(object sender, EventArgs e)
        {
            if (sender == Parent)
            {
                ActiveControl = null;
                core.LoadSettings();
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CoreForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            MinimumSize = new System.Drawing.Size(370, 348);
            Name = "CoreForm";
            Size = new System.Drawing.Size(370, 348);
            ResumeLayout(false);
        }
    }
}
