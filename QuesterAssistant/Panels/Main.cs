using System;
using System.ComponentModel;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    public partial class Main : Astral.Forms.BasePanel
    {
        internal static event EventHandler LoadSettings;
        internal static event EventHandler SaveSettings;

        public Main() : base("Quester Assistant")
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings(sideMain.Controls, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadSettings(sideMain.Controls, null);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //components = new Container();
            sideButtons.Visible = false;
            //OnPanelLeave += Dispose;

            // Init Tabs
            tileSettings.Tag = Core.SettingsCore.Panel;
            tileUpgradeManager.Tag = Core.UpgradeManagerCore.Panel;
            tilePowersManager.Tag = Core.PowersManagerCore.Panel;
            tilePushNotify.Tag = Core.PushNotifyCore.Panel;
            tileAbout.Tag = new About();
        }

        private void tile_ItemClick(object sender, TileItemEventArgs e)
        {
            void LoadPanel<T>(T form) where T : XtraUserControl
            {
                sideMain.InvokeSafe(() => sideMain.Controls.Remove(tileMain));
                sideMain.InvokeSafe(() => sideMain.Controls.Add(form));
                sideButtons.Visible = true;
            }
            LoadPanel((sender as TileItem)?.Tag as XtraUserControl);
        }
    }
}
