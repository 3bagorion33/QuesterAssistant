using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Launcher.Classes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using QuesterAssistant.Classes.Common;

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
            instance.Process.PriorityClass = (ProcessPriorityClass) cbxPriority.SelectedItem;
            logEvents.Add(new LogEvent($"Starting new Instance #{instance.Process.ProcessName}"));
            instances.Add(instance);
        }

        [SecurityCritical]
        private string KillSpy()
        {
            string message = string.Empty;
            try
            {
                if (chkKill.Checked)
                {
                    var process = Process.GetProcesses().ToList()
                        .Find(p => Regex.IsMatch(p.ProcessName, @"^(CrashReporter|CrypticError)",
                            RegexOptions.IgnoreCase));
                    if (process != null)
                    {
                        process.Kill();
                        message = process.ProcessName;
                    }
                }

                if (chkClose.Checked)
                {
                    string crashTitle = "Невервинтер Crash";
                    var handle = WinAPI.FindWindow(null, crashTitle);
                    if (handle != IntPtr.Zero)
                    {
                        WinAPI.CloseWindow(handle);
                        message = crashTitle;
                    }
                }
            }
            catch { }

            return message;
        }

        private void timerDelete_Tick(object sender, EventArgs e)
        {
            string killResult;

            if (!string.IsNullOrEmpty(killResult = KillSpy()))
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

            cbxPriority.Properties.Items.Add(ProcessPriorityClass.RealTime);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.High);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.AboveNormal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.Normal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.BelowNormal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.Idle);
            cbxPriority.SelectedIndex = 2;

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
            Firewall.DenyCrypticErrorApps();
        }

        private void menuFirewallDenyAddress_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Firewall.DenyCrypticErrorServer();
        }

        private void menuFirewallDeleteRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Firewall.RemoveAllRules();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Thread.Sleep(250);
                ShowInTaskbar = false;
                notifyTray.Visible = true;
            }
        }

        private void notifyTray_MouseClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            notifyTray.Visible = false;
        }

        private void cbxPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            instances.ForEach(i => i.Process.PriorityClass = (ProcessPriorityClass) cbxPriority.SelectedItem);
        }
    }
}
