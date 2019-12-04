using QuesterAssistant.Classes;
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal class CoreForm : XtraUserControl
    {
        protected ICore core;
        public event Action OnPanelLoad;
        public event Action OnPanelLeave;

        public void Init(ICore core)
        {
            this.core = core;
            Dock = DockStyle.Fill;
            Main.LoadSettings += LoadSettings;
            Main.SaveSettings += SaveSettings;
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            if ((sender as ControlCollection).Contains(this))
            {
                core.SaveSettings();
            }
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            if ((sender as ControlCollection).Contains(this))
            {
                ActiveControl = null;
                core.LoadSettings();
            }
        }

        public void PanelLoad() => OnPanelLoad?.Invoke();
        public void PanelLeave() => OnPanelLeave?.Invoke();

        //private void InitializeComponent()
        //{
        //    SuspendLayout();
        //    AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //    MinimumSize = new System.Drawing.Size(370, 348);
        //    Name = "CoreForm";
        //    Size = new System.Drawing.Size(370, 348);
        //    ResumeLayout(false);
        //}
    }
}
