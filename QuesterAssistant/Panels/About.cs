using System;

namespace QuesterAssistant.Panels
{
    internal partial class About : CoreForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void hlkQAForumThread_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://neverwinter-bot.com/forums/viewtopic.php?f=155&t=9712");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/aorion33/10");
        }

        private void About_Load(object sender, EventArgs e)
        {
            lblVersion.Text = $@"v {System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion}";
        }
    }
}
