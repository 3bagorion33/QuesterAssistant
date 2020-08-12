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
using DevExpress.XtraGrid;
using Launcher.Properties;
using QuesterAssistant.Classes.Common;

namespace Launcher
{
    public partial class MainForm : XtraForm
    {
        private readonly BindingList<Instance> instances = new BindingList<Instance>();
        private readonly BindingList<LogEvent> logEvents = new BindingList<LogEvent>();
        private readonly Patches patches = new Patches();
        private readonly Settings settings = new Settings();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var instance = new Instance(patches.Items);
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
                    string crashTitle = "GameClient.exe";
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

            instances.ForEach(i => i.Rename());
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
            Closing += MainForm_Closing;
            settings.Load();

            if (settings.Patches != null)
            {
                for (int i = 0; i < MathTools.Min(patches.Items.Count, settings.Patches.Count); i++)
                {
                    patches.Items[i].Active = settings.Patches[i];
                }
            }

            Instance.Clean();
            bsrcInstancesList.DataSource = instances;
            gctlLogEventList.DataSource = logEvents;
            gctlPatchesList.DataSource = patches.Items;

            cbxPriority.Properties.Items.Add(ProcessPriorityClass.RealTime);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.High);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.AboveNormal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.Normal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.BelowNormal);
            cbxPriority.Properties.Items.Add(ProcessPriorityClass.Idle);

            cbxPriority.DataBindings.Add(nameof(ComboBoxEdit.EditValue), settings, nameof(Settings.Priority));
            chkKill.DataBindings.Add(nameof(CheckEdit.EditValue), settings, nameof(Settings.KillCrypticError));
            chkClose.DataBindings.Add(nameof(CheckEdit.EditValue), settings, nameof(Settings.CloseCrashError));

            logEvents.Add(new LogEvent("Launcher is started"));
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            settings.Patches = patches.Items.Select(p => p.Active).ToList();
            settings.Save();
        }

        private void gctlProcessList_DoubleClick(object sender, EventArgs e)
        {
            if (((sender as GridControl).FocusedView as GridView).FocusedColumn.AbsoluteIndex == 2)
            {
                (bsrcInstancesList.Current as Instance).ForeGroundAttach();
                return;
            }
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

        private void gridInstances_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = hitInfo.RowHandle;
                menuInstances.ShowPopup(barInstances, view.GridControl.PointToScreen(e.Point));
            }
        }

        private void miCloseProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var instance = bsrcInstancesList.Current as Instance;

            if (gridInstances.FocusedColumn.AbsoluteIndex == 2)
            {
                logEvents.Add(new LogEvent($"Closing attached GameClient #{instance.PID}"));
                instance.CloseAttach();
                return;
            }
            logEvents.Add(new LogEvent($"Closing Instance #{instance.Process.ProcessName}"));
            instance.Close();
        }
    }
}
