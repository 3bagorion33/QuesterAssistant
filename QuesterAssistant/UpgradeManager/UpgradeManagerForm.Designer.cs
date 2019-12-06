namespace QuesterAssistant.UpgradeManager
{
    partial class UpgradeManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpgradeManagerForm));
            this.toolTipController = new DevExpress.Utils.ToolTipController();
            this.gctrlProfile = new DevExpress.XtraEditors.GroupControl();
            this.lcProfilesList = new QuesterAssistant.Panels.ListControl();
            this.cbxRunCondition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.bmTasksList = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.miTaskRunCurrent = new DevExpress.XtraBars.BarButtonItem();
            this.miTaskDelete = new DevExpress.XtraBars.BarButtonItem();
            this.miTaskRunFrom = new DevExpress.XtraBars.BarButtonItem();
            this.miTaskRunTo = new DevExpress.XtraBars.BarButtonItem();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbxAlgorithm = new DevExpress.XtraEditors.ComboBoxEdit();
            this.numIterationCont = new DevExpress.XtraEditors.SpinEdit();
            this.btnTasksAction = new DevExpress.XtraEditors.SimpleButton();
            this.gctlTasks = new DevExpress.XtraGrid.GridControl();
            this.gridTasksList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolRank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDisplayName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcolCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolChance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolWard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pmTasksList = new DevExpress.XtraBars.PopupMenu();
            this.chkHotKey = new DevExpress.XtraEditors.CheckEdit();
            this.txtHotKey = new DevExpress.XtraEditors.TextEdit();
            this.bsrcHotKey = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlProfile)).BeginInit();
            this.gctrlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxRunCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmTasksList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAlgorithm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterationCont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasksList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmTasksList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHotKey)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTipController
            // 
            this.toolTipController.InitialDelay = 100;
            // 
            // gctrlProfile
            // 
            this.gctrlProfile.Controls.Add(this.lcProfilesList);
            this.gctrlProfile.Controls.Add(this.cbxRunCondition);
            this.gctrlProfile.Controls.Add(this.labelControl3);
            this.gctrlProfile.Controls.Add(this.labelControl2);
            this.gctrlProfile.Controls.Add(this.labelControl1);
            this.gctrlProfile.Controls.Add(this.cbxAlgorithm);
            this.gctrlProfile.Controls.Add(this.numIterationCont);
            this.gctrlProfile.Controls.Add(this.btnTasksAction);
            this.gctrlProfile.Controls.Add(this.gctlTasks);
            this.gctrlProfile.Location = new System.Drawing.Point(0, 29);
            this.gctrlProfile.Name = "gctrlProfile";
            this.gctrlProfile.Size = new System.Drawing.Size(370, 343);
            this.gctrlProfile.TabIndex = 1;
            this.gctrlProfile.Text = "Profile";
            // 
            // lcProfilesList
            // 
            this.lcProfilesList.Location = new System.Drawing.Point(54, 1);
            this.lcProfilesList.Margin = new System.Windows.Forms.Padding(0);
            this.lcProfilesList.Name = "lcProfilesList";
            this.lcProfilesList.Size = new System.Drawing.Size(307, 20);
            this.lcProfilesList.TabIndex = 15;
            // 
            // cbxRunCondition
            // 
            this.cbxRunCondition.EditValue = "";
            this.cbxRunCondition.Location = new System.Drawing.Point(123, 46);
            this.cbxRunCondition.MenuManager = this.bmTasksList;
            this.cbxRunCondition.Name = "cbxRunCondition";
            this.cbxRunCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxRunCondition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxRunCondition.Size = new System.Drawing.Size(112, 20);
            this.cbxRunCondition.TabIndex = 14;
            // 
            // bmTasksList
            // 
            this.bmTasksList.DockControls.Add(this.barDockControlTop);
            this.bmTasksList.DockControls.Add(this.barDockControlBottom);
            this.bmTasksList.DockControls.Add(this.barDockControlLeft);
            this.bmTasksList.DockControls.Add(this.barDockControlRight);
            this.bmTasksList.Form = this;
            this.bmTasksList.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.miTaskRunCurrent,
            this.miTaskDelete,
            this.miTaskRunFrom,
            this.miTaskRunTo});
            this.bmTasksList.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmTasksList;
            this.barDockControlTop.Size = new System.Drawing.Size(370, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 372);
            this.barDockControlBottom.Manager = this.bmTasksList;
            this.barDockControlBottom.Size = new System.Drawing.Size(370, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bmTasksList;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 372);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(370, 0);
            this.barDockControlRight.Manager = this.bmTasksList;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 372);
            // 
            // miTaskRunCurrent
            // 
            this.miTaskRunCurrent.Caption = "Run current task";
            this.miTaskRunCurrent.Id = 0;
            this.miTaskRunCurrent.Name = "miTaskRunCurrent";
            this.miTaskRunCurrent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miTaskRunCurrent_ItemClick);
            // 
            // miTaskDelete
            // 
            this.miTaskDelete.Caption = "Delete current task";
            this.miTaskDelete.Id = 1;
            this.miTaskDelete.Name = "miTaskDelete";
            this.miTaskDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miTaskDelete_ItemClick);
            // 
            // miTaskRunFrom
            // 
            this.miTaskRunFrom.Caption = "Run from this task";
            this.miTaskRunFrom.Id = 2;
            this.miTaskRunFrom.Name = "miTaskRunFrom";
            this.miTaskRunFrom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miTaskRunFrom_ItemClick);
            // 
            // miTaskRunTo
            // 
            this.miTaskRunTo.Caption = "Run to this task";
            this.miTaskRunTo.Id = 3;
            this.miTaskRunTo.Name = "miTaskRunTo";
            this.miTaskRunTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miTaskRunTo_ItemClick);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(124, 27);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Run condition :";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(241, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Count :";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(5, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Algorithm :";
            // 
            // cbxAlgorithm
            // 
            this.cbxAlgorithm.Location = new System.Drawing.Point(3, 46);
            this.cbxAlgorithm.MenuManager = this.bmTasksList;
            this.cbxAlgorithm.Name = "cbxAlgorithm";
            this.cbxAlgorithm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxAlgorithm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxAlgorithm.Size = new System.Drawing.Size(112, 20);
            this.cbxAlgorithm.TabIndex = 10;
            // 
            // numIterationCont
            // 
            this.numIterationCont.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numIterationCont.Location = new System.Drawing.Point(241, 46);
            this.numIterationCont.MenuManager = this.bmTasksList;
            this.numIterationCont.Name = "numIterationCont";
            this.numIterationCont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numIterationCont.Properties.IsFloatValue = false;
            this.numIterationCont.Properties.Mask.EditMask = "N00";
            this.numIterationCont.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numIterationCont.Size = new System.Drawing.Size(45, 20);
            this.numIterationCont.TabIndex = 7;
            // 
            // btnTasksAction
            // 
            this.btnTasksAction.Location = new System.Drawing.Point(292, 43);
            this.btnTasksAction.Name = "btnTasksAction";
            this.btnTasksAction.Size = new System.Drawing.Size(75, 23);
            this.btnTasksAction.TabIndex = 6;
            this.btnTasksAction.Text = "Run";
            this.btnTasksAction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnTasksAction_MouseClick);
            // 
            // gctlTasks
            // 
            this.gctlTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlTasks.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gctlTasks.EmbeddedNavigator.ToolTipController = this.toolTipController;
            this.gctlTasks.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gctlTasks.Location = new System.Drawing.Point(3, 71);
            this.gctlTasks.MainView = this.gridTasksList;
            this.gctlTasks.Margin = new System.Windows.Forms.Padding(1);
            this.gctlTasks.MenuManager = this.bmTasksList;
            this.gctlTasks.Name = "gctlTasks";
            this.gctlTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riButtonEdit});
            this.gctlTasks.Size = new System.Drawing.Size(364, 269);
            this.gctlTasks.TabIndex = 2;
            this.gctlTasks.ToolTipController = this.toolTipController;
            this.gctlTasks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridTasksList});
            // 
            // gridTasksList
            // 
            this.gridTasksList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolRank,
            this.gcolDisplayName,
            this.gcolCount,
            this.gcolChance,
            this.gcolWard});
            this.gridTasksList.GridControl = this.gctlTasks;
            this.gridTasksList.Name = "gridTasksList";
            this.gridTasksList.NewItemRowText = "NewItemRowText";
            this.gridTasksList.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridTasksList.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridTasksList.OptionsCustomization.AllowColumnMoving = false;
            this.gridTasksList.OptionsCustomization.AllowFilter = false;
            this.gridTasksList.OptionsCustomization.AllowGroup = false;
            this.gridTasksList.OptionsCustomization.AllowSort = false;
            this.gridTasksList.OptionsDetail.AllowZoomDetail = false;
            this.gridTasksList.OptionsDetail.EnableMasterViewMode = false;
            this.gridTasksList.OptionsDetail.ShowDetailTabs = false;
            this.gridTasksList.OptionsDetail.SmartDetailExpand = false;
            this.gridTasksList.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridTasksList.OptionsFilter.AllowFilterEditor = false;
            this.gridTasksList.OptionsFilter.AllowMRUFilterList = false;
            this.gridTasksList.OptionsFind.AllowFindPanel = false;
            this.gridTasksList.OptionsMenu.EnableColumnMenu = false;
            this.gridTasksList.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridTasksList.OptionsPrint.UsePrintStyles = false;
            this.gridTasksList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridTasksList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridTasksList.OptionsSelection.UseIndicatorForSelection = false;
            this.gridTasksList.OptionsView.ShowDetailButtons = false;
            this.gridTasksList.OptionsView.ShowGroupPanel = false;
            this.gridTasksList.OptionsView.ShowIndicator = false;
            this.gridTasksList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolRank, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridTasksList.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridTasksList_PopupMenuShowing);
            this.gridTasksList.DoubleClick += new System.EventHandler(this.gridTasksList_DoubleClick);
            // 
            // gcolRank
            // 
            this.gcolRank.Caption = "R";
            this.gcolRank.FieldName = "Rank";
            this.gcolRank.MaxWidth = 20;
            this.gcolRank.Name = "gcolRank";
            this.gcolRank.OptionsColumn.AllowEdit = false;
            this.gcolRank.OptionsColumn.FixedWidth = true;
            this.gcolRank.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gcolRank.Width = 10;
            // 
            // gcolDisplayName
            // 
            this.gcolDisplayName.Caption = "Item";
            this.gcolDisplayName.ColumnEdit = this.riButtonEdit;
            this.gcolDisplayName.FieldName = "FullName";
            this.gcolDisplayName.Name = "gcolDisplayName";
            this.gcolDisplayName.Visible = true;
            this.gcolDisplayName.VisibleIndex = 0;
            this.gcolDisplayName.Width = 231;
            // 
            // riButtonEdit
            // 
            this.riButtonEdit.AutoHeight = false;
            this.riButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riButtonEdit.Name = "riButtonEdit";
            this.riButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.riButtonEdit.Click += new System.EventHandler(this.riButtonEdit_Click);
            // 
            // gcolCount
            // 
            this.gcolCount.Caption = "Count";
            this.gcolCount.FieldName = "Count";
            this.gcolCount.MaxWidth = 40;
            this.gcolCount.Name = "gcolCount";
            this.gcolCount.OptionsColumn.AllowEdit = false;
            this.gcolCount.OptionsColumn.FixedWidth = true;
            this.gcolCount.Visible = true;
            this.gcolCount.VisibleIndex = 1;
            this.gcolCount.Width = 40;
            // 
            // gcolChance
            // 
            this.gcolChance.Caption = "Chance";
            this.gcolChance.DisplayFormat.FormatString = "{0}%";
            this.gcolChance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcolChance.FieldName = "Chance";
            this.gcolChance.MinWidth = 40;
            this.gcolChance.Name = "gcolChance";
            this.gcolChance.OptionsColumn.AllowEdit = false;
            this.gcolChance.Visible = true;
            this.gcolChance.VisibleIndex = 2;
            this.gcolChance.Width = 41;
            // 
            // gcolWard
            // 
            this.gcolWard.AppearanceCell.Options.UseTextOptions = true;
            this.gcolWard.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.gcolWard.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("gcolWard.AppearanceHeader.Image")));
            this.gcolWard.AppearanceHeader.Options.UseImage = true;
            this.gcolWard.AppearanceHeader.Options.UseTextOptions = true;
            this.gcolWard.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
            this.gcolWard.Caption = "Ward";
            this.gcolWard.FieldName = "UseWard";
            this.gcolWard.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.gcolWard.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gcolWard.ImageOptions.Image")));
            this.gcolWard.MaxWidth = 20;
            this.gcolWard.MinWidth = 19;
            this.gcolWard.Name = "gcolWard";
            this.gcolWard.Visible = true;
            this.gcolWard.VisibleIndex = 3;
            this.gcolWard.Width = 20;
            // 
            // pmTasksList
            // 
            this.pmTasksList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miTaskRunCurrent),
            new DevExpress.XtraBars.LinkPersistInfo(this.miTaskRunFrom),
            new DevExpress.XtraBars.LinkPersistInfo(this.miTaskRunTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.miTaskDelete)});
            this.pmTasksList.Manager = this.bmTasksList;
            this.pmTasksList.Name = "pmTasksList";
            // 
            // chkHotKey
            // 
            this.chkHotKey.Location = new System.Drawing.Point(-1, 3);
            this.chkHotKey.MenuManager = this.bmTasksList;
            this.chkHotKey.Name = "chkHotKey";
            this.chkHotKey.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkHotKey.Properties.Appearance.Options.UseForeColor = true;
            this.chkHotKey.Properties.Caption = "Enable Run/Abort Hotkey :";
            this.chkHotKey.Size = new System.Drawing.Size(157, 19);
            this.chkHotKey.TabIndex = 6;
            // 
            // txtHotKey
            // 
            this.txtHotKey.Location = new System.Drawing.Point(163, 3);
            this.txtHotKey.MenuManager = this.bmTasksList;
            this.txtHotKey.Name = "txtHotKey";
            this.txtHotKey.Size = new System.Drawing.Size(118, 20);
            this.txtHotKey.TabIndex = 7;
            this.txtHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotKey_KeyDown);
            // 
            // UpgradeManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtHotKey);
            this.Controls.Add(this.chkHotKey);
            this.Controls.Add(this.gctrlProfile);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UpgradeManagerForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.UpgradeManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gctrlProfile)).EndInit();
            this.gctrlProfile.ResumeLayout(false);
            this.gctrlProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxRunCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmTasksList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAlgorithm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterationCont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasksList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmTasksList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHotKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraEditors.GroupControl gctrlProfile;
        private DevExpress.XtraGrid.GridControl gctlTasks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridTasksList;
        private DevExpress.XtraGrid.Columns.GridColumn gcolRank;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDisplayName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCount;
        private DevExpress.XtraBars.BarManager bmTasksList;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu pmTasksList;
        private DevExpress.XtraBars.BarButtonItem miTaskRunCurrent;
        private DevExpress.XtraGrid.Columns.GridColumn gcolChance;
        private DevExpress.XtraGrid.Columns.GridColumn gcolWard;
        private DevExpress.XtraBars.BarButtonItem miTaskDelete;
        private DevExpress.XtraEditors.SimpleButton btnTasksAction;
        private DevExpress.XtraEditors.SpinEdit numIterationCont;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbxAlgorithm;
        private DevExpress.XtraBars.BarButtonItem miTaskRunFrom;
        private DevExpress.XtraBars.BarButtonItem miTaskRunTo;
        private DevExpress.XtraEditors.TextEdit txtHotKey;
        private DevExpress.XtraEditors.CheckEdit chkHotKey;
        private System.Windows.Forms.BindingSource bsrcHotKey;
        private DevExpress.XtraEditors.ComboBoxEdit cbxRunCondition;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riButtonEdit;
        private Panels.ListControl lcProfilesList;
    }
}