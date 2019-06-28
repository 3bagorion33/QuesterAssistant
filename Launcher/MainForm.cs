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
        private BindingList<LogEvent> logEvents = new BindingList<LogEvent>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var instance = new Instance();
            logEvents.Add(new LogEvent($"Starting new Instance #{instance.Process.ProcessName}"));
            instances.Add(instance);
        }

        private void timerDelete_Tick(object sender, EventArgs e)
        {
            string killResult;

            if (!string.IsNullOrEmpty(killResult = Instance.KillSpy()))
            {
                logEvents.Add(new LogEvent($"'{killResult}' has been closed"));
            }

            if (chkHideTitle.Checked)
            {
                instances.ForEach(i => i.Rename());
            }
            instances.Remove(i =>
            {
                if (i.Process.HasExited)
                {
                    logEvents.Add(new LogEvent($"Instance #{i.Process.ProcessName} has been closed, cleaning file..."));
                    return true;
                }
                return false;
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            instances.ForEach(i => i.Close());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Instance.Clean();
            bsrcInstancesList.DataSource = instances;
            gctlLogEventList.DataSource = logEvents;

            logEvents.Add(new LogEvent("Launcher is started"));
        }

        private void btnClose_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var instance = bsrcInstancesList.Current as Instance;
            logEvents.Add(new LogEvent($"Closing Instance #{instance.Process.ProcessName}"));
            instance.Close();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            (bsrcInstancesList.Current as Instance).ForeGround();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            {
                if (e.SelectedControl != gctlProcessList)
                    return;
                ToolTipControlInfo info = null;
                if (!(gctlProcessList.GetViewAt(e.ControlMousePosition) is GridView view))
                    return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.Caption == @"Process")
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
