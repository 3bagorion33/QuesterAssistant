using QuesterAssistant.Panels;

namespace QuesterAssistant.UpgradeManager
{
    internal partial class UpgradeManagerForm : CoreForm
    {
        private UpgradeManagerCore Core => QuesterAssistant.Core.UpgradeManagerCore;

        public UpgradeManagerForm() : base(QuesterAssistant.Core.UpgradeManagerCore)
        {
            InitializeComponent();
        }

        private void btnUpgradeOnce_Click(object sender, System.EventArgs e)
        {
            Core.UpgradeOnce();
        }
    }
}
