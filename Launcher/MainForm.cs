using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Launcher.Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Launcher
{
    public partial class MainForm : XtraForm
    {
        private BindingList<Instance> instances = new BindingList<Instance>();

        public MainForm()
        {
            InitializeComponent();
            bsrcInstancesList.DataSource = instances;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            instances.Add(new Instance());
        }

        private void timerDelete_Tick(object sender, EventArgs e)
        {
            Instance.KillSpy();
            if (chkHideTitle.Checked)
            {
                instances.ForEach(i => i.Rename());
            }
            instances.Remove(i => i.Process.HasExited);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            instances.ForEach(i => i.Close());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Instance.Clean();
        }

        private void btnClose_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            (bsrcInstancesList.Current as Instance).Close();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            (bsrcInstancesList.Current as Instance).ForeGround();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            {
                if (e.SelectedControl != gridControl1)
                    return;
                ToolTipControlInfo info = null;
                if (!(gridControl1.GetViewAt(e.ControlMousePosition) is GridView view))
                    return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.Caption == "Process")
                {
                    object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
                    info = new ToolTipControlInfo(o, "Double click on row to foreground this instance");
                }
                if (info != null)
                    e.Info = info;
            }
        }

        private void menuFirewallDenyApps_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Firewall.CrypticErrorDenyApps();
        }

        private void menuFirewallDenyAddress_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Firewall.CrypticErrorDenyServer();
        }

        private void menuFirewallDeleteRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Firewall.RemoveAllRules();
        }
    }
}
