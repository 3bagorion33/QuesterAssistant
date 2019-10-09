using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Classes.Hooks
{
    internal partial class HooksLoader : XtraForm
    {
        public HooksLoader()
        {
            InitializeComponent();
        }

        public static void SetHook()
        {
            var hookLoad = new HooksLoader();
            hookLoad.Show();
        }

        private void HookLoad_Load(object sender, EventArgs e)
        {
            Core.KeyboardHook.Start();
            SetVisibleCore(false);
            Application.Run();
        }
    }
}