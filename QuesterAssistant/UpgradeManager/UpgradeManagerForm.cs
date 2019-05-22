﻿using Astral.Quester.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
        private Profile CurrentProfile => lkupProfilesList.EditValue as Profile;

        private const string Abort = "Abort";
        private const string Start = "Start";

        public UpgradeManagerForm() : base(QuesterAssistant.Core.UpgradeManagerCore)
        {
            InitializeComponent();

            lkupProfilesList.ButtonClick += lkupProfilesList_ButtonClick;
            lkupProfilesList.ListChanged += lkupProfilesList_ListChanged;
            lkupProfilesList.EditValueChanged += lkupProfilesList_EditValueChanged;

            Data.HashChanged += gridTasksList.RefreshData;

            Core.TasksStarted += Core_TasksStarted;
            Core.TasksStopped += Core_TasksStopped;
        }

        private void Core_TasksStarted()
        {
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = "Abort"; });
        }

        private void Core_TasksStopped()
        {
            btnTasksAction.InvokeSafe(() => { btnTasksAction.Text = "Run"; });
        }

        private void lkupProfilesList_EditValueChanged(object sender, System.EventArgs e)
        {
            gctlTasks.DataSource = CurrentProfile?.Tasks;

            numIterationCont.DataBindings.Clear();
            numIterationCont.DataBindings.Add(nameof(SpinEdit.EditValue), CurrentProfile, nameof(Profile.IterationsCount), false, DataSourceUpdateMode.OnValidation);

            cbxAlgorithm.DataBindings.Clear();
            cbxAlgorithm.DataBindings.Add(nameof(ComboBoxEdit.EditValue), CurrentProfile, nameof(Profile.Algorithm), false, DataSourceUpdateMode.OnValidation);
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

        private void UpgradeManagerForm_Load(object sender, System.EventArgs e)
        {
            lkupProfilesList.Properties.DataSource = Data.Profiles;
            cbxAlgorithm.Properties.Items.Add(Profile.AlgorithmDirection.UpToDown);
            cbxAlgorithm.Properties.Items.Add(Profile.AlgorithmDirection.DownToUp);
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
            Core.StartTasks(CurrentProfile, taskStartIdx: gridTasksList.FocusedRowHandle, taskStopIdx: gridTasksList.FocusedRowHandle);
        }

        private void miTaskRunFrom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0) return;
            Core.StartTasks(CurrentProfile, taskStartIdx: gridTasksList.FocusedRowHandle);
        }

        private void miTaskRunTo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 1) return;
            Core.StartTasks(CurrentProfile, taskStopIdx: gridTasksList.FocusedRowHandle - 1);
        }

        private void miTaskDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridTasksList.DeleteSelectedRows();
            gctlTasks.RefreshDataSource();
        }

        private void gridTasksList_DoubleClick(object sender, System.EventArgs e)
        {
            if (gridTasksList.FocusedRowHandle < 0) return;
            Core.StartTasks(CurrentProfile, taskStartIdx: gridTasksList.FocusedRowHandle, taskStopIdx: gridTasksList.FocusedRowHandle, count: 1);
        }

        private void btnTasksAction_MouseClick(object sender, MouseEventArgs e)
        {
            switch (Core.TasksIsRunning)
            {
                case false:
                    Core.StartTasks(CurrentProfile, taskStartIdx: 0);
                    break;
                case true:
                    Core.StopTasks();
                    break;
                default:
                    break;
            }
        }
    }
}