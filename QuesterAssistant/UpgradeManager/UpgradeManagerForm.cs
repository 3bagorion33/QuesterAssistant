using System.Collections.Generic;
using Astral.Quester.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System.ComponentModel;
using System.Windows.Forms;
using static QuesterAssistant.UpgradeManager.UpgradeManagerData;

namespace QuesterAssistant.UpgradeManager
{
    internal partial class UpgradeManagerForm : CoreForm
    {
        private UpgradeManagerCore Core => core as UpgradeManagerCore;
        private UpgradeManagerData Data => Core.Data;
        public Profile CurrentProfile => lcProfilesList.CurrentItem as Profile ?? new Profile();

        public UpgradeManagerForm()
        {
            InitializeComponent();
            lcProfilesList.EditValueChanged += lcProfilesList_EditValueChanged;
            lcProfilesList.ListChanged += lcProfilesList_ListChanged;
        }

        private void UpgradeManagerForm_Load(object sender, System.EventArgs e)
        {
            lcProfilesList.BindSource<Profile>(Data.Profiles);

            Data.HashChanged += gridTasksList.RefreshData;
            Data.HashChanged += bsrcHotKey.ResetCurrentItem;

            Core.TasksStarted += TasksStarted;
            Core.TasksStopped += TasksStopped;

            cbxAlgorithm.Properties.Items.Add(Profile.AlgorithmDirection.UpToDown);
            cbxAlgorithm.Properties.Items.Add(Profile.AlgorithmDirection.DownToUp);

            cbxRunCondition.Properties.Items.Add(Profile.ErrorBehavior.WhilePossible);
            cbxRunCondition.Properties.Items.Add(Profile.ErrorBehavior.StopOnError);

            bsrcHotKey.DataSource = Data.ToggleHotKey;
            chkHotKey.BindAdd(bsrcHotKey, nameof(CheckEdit.Checked), nameof(UpgradeManagerData.ToggleHotKey.Enabled));
            txtHotKey.BindAdd(bsrcHotKey, nameof(TextEdit.Text), nameof(UpgradeManagerData.ToggleHotKey.String), DataSourceUpdateMode.OnValidation);
        }

        private void TasksStarted()
        {
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = @"Abort"; });
        }

        private void TasksStopped()
        {
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = @"Run"; });
        }

        private void lcProfilesList_EditValueChanged(object sender, System.EventArgs e)
        {
            gctlTasks.InvokeSafe(() => gctlTasks.DataSource = CurrentProfile?.Tasks ?? new List<Task>());

            numIterationCont.DataBindings.Clear();
            numIterationCont.BindAdd(CurrentProfile, nameof(SpinEdit.EditValue), nameof(Profile.IterationsCount));

            cbxAlgorithm.DataBindings.Clear();
            cbxAlgorithm.BindAdd(CurrentProfile, nameof(ComboBoxEdit.EditValue), nameof(Profile.Algorithm));

            cbxRunCondition.DataBindings.Clear();
            cbxRunCondition.BindAdd(CurrentProfile, nameof(ComboBoxEdit.EditValue), nameof(Profile.RunCondition));
        }

        private void lcProfilesList_ListChanged(object sender, ListChangedEventArgs e)
        {
            gridTasksList.OptionsView.NewItemRowPosition =
                ((LookUpEdit) sender).ItemIndex == -1 ? NewItemRowPosition.None : NewItemRowPosition.Bottom;
        }

        private void gridTasksList_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = hitInfo.RowHandle;
                pmTasksList.ShowPopup(bmTasksList, view.GridControl.PointToScreen(e.Point));
            }
        }

        private void riButtonEdit_Click(object sender, System.EventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0)
            {
                CurrentProfile.AddTask(GetAnItem.Show());
            }
        }

        private void miTaskRunCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0) return;
            Core.StartTasks(taskStartIdx: gridTasksList.FocusedRowHandle, taskStopIdx: gridTasksList.FocusedRowHandle);
        }

        private void miTaskRunFrom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0) return;
            Core.StartTasks(taskStartIdx: gridTasksList.FocusedRowHandle);
        }

        private void miTaskRunTo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 1) return;
            Core.StartTasks(taskStopIdx: gridTasksList.FocusedRowHandle - 1);
        }

        private void miTaskDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridTasksList.DeleteSelectedRows();
            gctlTasks.RefreshDataSource();
        }

        private void gridTasksList_DoubleClick(object sender, System.EventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0) return;
            Core.StartTasks(taskStartIdx: gridTasksList.FocusedRowHandle, taskStopIdx: gridTasksList.FocusedRowHandle, count: 1);
        }

        private void btnTasksAction_MouseClick(object sender, MouseEventArgs e)
        {
            Core.ToggleTasks();
        }

        private void txtHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            var txtEdit = sender as TextEdit;
            var k = e.KeyData;
            txtEdit.Text = k.IgnoreBack().ConvertToString();

            if (k.IsNotModifier())
            {
                ActiveControl = null;
            }
        }
    }
}
