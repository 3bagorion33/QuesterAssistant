using Astral.Quester.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using QuesterAssistant.Classes.Common.Extensions;
using QuesterAssistant.Panels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static QuesterAssistant.UpgradeManager.UpgradeManagerData;

namespace QuesterAssistant.UpgradeManager
{
    internal partial class UpgradeManagerForm : CoreForm
    {
        private bool disableEvent = false;
        private UpgradeManagerCore Core => QuesterAssistant.Core.UpgradeManagerCore;
        private UpgradeManagerData Data => Core.Data;
        public Profile CurrentProfile => lkupProfilesList.EditValue as Profile;

        public UpgradeManagerForm()
        {
            InitializeComponent();
        }

        private void UpgradeManagerForm_Load(object sender, System.EventArgs e)
        {
            lkupProfilesList.ButtonClick += lkupProfilesList_ButtonClick;
            lkupProfilesList.ListChanged += lkupProfilesList_ListChanged;
            lkupProfilesList.EditValueChanged += lkupProfilesList_EditValueChanged;
            lkupProfilesList.Properties.DataSource = Data.Profiles;
            
            Data.HashChanged += gridTasksList.RefreshData;

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
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = "Abort"; });
        }

        private void TasksStopped()
        {
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = "Run"; });
        }

        private void lkupProfilesList_EditValueChanged(object sender, System.EventArgs e)
        {
            gctlTasks.InvokeSafe(() => gctlTasks.DataSource = CurrentProfile?.Tasks);

            numIterationCont.DataBindings.Clear();
            numIterationCont.BindAdd(CurrentProfile, nameof(SpinEdit.EditValue), nameof(Profile.IterationsCount));

            cbxAlgorithm.DataBindings.Clear();
            cbxAlgorithm.BindAdd(CurrentProfile, nameof(ComboBoxEdit.EditValue), nameof(Profile.Algorithm));

            cbxRunCondition.DataBindings.Clear();
            cbxRunCondition.BindAdd(CurrentProfile, nameof(ComboBoxEdit.EditValue), nameof(Profile.RunCondition));
        }

        private void lkupProfilesList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!disableEvent)
            {
                if (Data.Profiles.Any() && lkupProfilesList.ItemIndex == -1)
                {
                    lkupProfilesList.ItemIndex = 0;
                    gridTasksList.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                }
                if (!Data.Profiles.Any())
                {
                    lkupProfilesList.ItemIndex = -1;
                    gridTasksList.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                }
            }
            lkupProfilesList.Properties.DropDownRows = Data.Profiles.Count;
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

        private void lkupProfilesList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.Caption)
            {
                case "Add":
                    string addName = InputBox.MessageText("Enter a new profile name:");
                    if (addName.Any())
                    {
                        var profile = new Profile() { Name = addName };
                        Data.Profiles.AddOrReplace(x => x.Name == addName, profile);
                        lkupProfilesList.InvokeSafe(() => lkupProfilesList.EditValue = profile);
                    }
                    break;

                case "Delete":
                    if (Data.Profiles.Any() &&
                        (DialogBox.Show("Delete this profile?", "Confirm") == DialogResult.Yes))
                    {
                        Data.Profiles.Remove(CurrentProfile);
                        lkupProfilesList.ItemIndex = 0;
                    }
                    break;

                case "Sort":
                    if (Data.Profiles.Any())
                    {
                        disableEvent = true;
                        var selectedVal = lkupProfilesList.EditValue;
                        ChangeListOrder<Profile>.Show(Data.Profiles, CurrentProfile, "Change profiles order :");
                        lkupProfilesList.InvokeSafe(() => lkupProfilesList.EditValue = selectedVal);
                        disableEvent = false;
                    }
                    break;

                case "Rename":
                    if (Data.Profiles.Any())
                    {
                        string ren = InputBox.MessageText("Enter a new name for this profile:", CurrentProfile.Name);
                        if (ren.Any())
                            CurrentProfile.Name = ren;
                    }
                    break;

                default:
                    break;
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
