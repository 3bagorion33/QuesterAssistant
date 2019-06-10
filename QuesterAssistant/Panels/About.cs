using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public partial class About : UserControl
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
            lblVersion.Text = $"v {System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion}";
        }
    }
}
