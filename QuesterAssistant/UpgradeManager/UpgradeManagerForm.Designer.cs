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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnUpgradeOnce = new DevExpress.XtraEditors.SimpleButton();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.gctrlProfile = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridStones = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolRank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDisplayName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolInternalName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlProfile)).BeginInit();
            this.gctrlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpgradeOnce
            // 
            this.btnUpgradeOnce.Location = new System.Drawing.Point(5, 294);
            this.btnUpgradeOnce.Name = "btnUpgradeOnce";
            this.btnUpgradeOnce.Size = new System.Drawing.Size(80, 23);
            this.btnUpgradeOnce.TabIndex = 0;
            this.btnUpgradeOnce.Text = "Upgrade Once";
            this.btnUpgradeOnce.Click += new System.EventHandler(this.btnUpgradeOnce_Click);
            // 
            // toolTipController1
            // 
            this.toolTipController1.InitialDelay = 100;
            this.toolTipController1.ShowBeak = true;
            this.toolTipController1.ShowShadow = false;
            this.toolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // gctrlProfile
            // 
            this.gctrlProfile.Controls.Add(this.gridControl1);
            this.gctrlProfile.Controls.Add(this.btnUpgradeOnce);
            this.gctrlProfile.Location = new System.Drawing.Point(13, 13);
            this.gctrlProfile.Name = "gctrlProfile";
            this.gctrlProfile.Size = new System.Drawing.Size(344, 322);
            this.gctrlProfile.TabIndex = 1;
            this.gctrlProfile.Text = "Profile";
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gridControl1.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gridControl1.Location = new System.Drawing.Point(5, 96);
            this.gridControl1.MainView = this.gridStones;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnClose,
            this.repositoryItemComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(334, 192);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ToolTipController = this.toolTipController1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridStones});
            // 
            // gridStones
            // 
            this.gridStones.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolRank,
            this.gcolDisplayName,
            this.gcolInternalName,
            this.gcolCount});
            this.gridStones.GridControl = this.gridControl1;
            this.gridStones.Name = "gridStones";
            this.gridStones.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridStones.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridStones.OptionsCustomization.AllowColumnMoving = false;
            this.gridStones.OptionsCustomization.AllowFilter = false;
            this.gridStones.OptionsCustomization.AllowGroup = false;
            this.gridStones.OptionsDetail.AllowZoomDetail = false;
            this.gridStones.OptionsDetail.EnableMasterViewMode = false;
            this.gridStones.OptionsDetail.ShowDetailTabs = false;
            this.gridStones.OptionsDetail.SmartDetailExpand = false;
            this.gridStones.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridStones.OptionsFilter.AllowFilterEditor = false;
            this.gridStones.OptionsFilter.AllowMRUFilterList = false;
            this.gridStones.OptionsFind.AllowFindPanel = false;
            this.gridStones.OptionsMenu.EnableColumnMenu = false;
            this.gridStones.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridStones.OptionsPrint.UsePrintStyles = false;
            this.gridStones.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridStones.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridStones.OptionsSelection.UseIndicatorForSelection = false;
            this.gridStones.OptionsView.ShowDetailButtons = false;
            this.gridStones.OptionsView.ShowGroupPanel = false;
            this.gridStones.OptionsView.ShowIndicator = false;
            // 
            // gcolRank
            // 
            this.gcolRank.Caption = "R";
            this.gcolRank.FieldName = "Process.ProcessName";
            this.gcolRank.MaxWidth = 20;
            this.gcolRank.Name = "gcolRank";
            this.gcolRank.OptionsColumn.AllowEdit = false;
            this.gcolRank.OptionsColumn.FixedWidth = true;
            this.gcolRank.Visible = true;
            this.gcolRank.VisibleIndex = 0;
            this.gcolRank.Width = 10;
            // 
            // gcolDisplayName
            // 
            this.gcolDisplayName.Caption = "Display Name";
            this.gcolDisplayName.FieldName = "NewTitle";
            this.gcolDisplayName.Name = "gcolDisplayName";
            this.gcolDisplayName.OptionsColumn.AllowEdit = false;
            this.gcolDisplayName.Visible = true;
            this.gcolDisplayName.VisibleIndex = 1;
            this.gcolDisplayName.Width = 132;
            // 
            // gcolInternalName
            // 
            this.gcolInternalName.Caption = "InternalName";
            this.gcolInternalName.ColumnEdit = this.repositoryItemComboBox1;
            this.gcolInternalName.FieldName = "OriginalTitle";
            this.gcolInternalName.Name = "gcolInternalName";
            this.gcolInternalName.OptionsColumn.AllowEdit = false;
            this.gcolInternalName.Visible = true;
            this.gcolInternalName.VisibleIndex = 2;
            this.gcolInternalName.Width = 160;
            // 
            // gcolCount
            // 
            this.gcolCount.Caption = "Count";
            this.gcolCount.MaxWidth = 30;
            this.gcolCount.Name = "gcolCount";
            this.gcolCount.OptionsColumn.FixedWidth = true;
            this.gcolCount.OptionsColumn.ReadOnly = true;
            this.gcolCount.Visible = true;
            this.gcolCount.VisibleIndex = 3;
            this.gcolCount.Width = 30;
            // 
            // btnClose
            // 
            this.btnClose.AutoHeight = false;
            this.btnClose.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "Close this instance", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(3);
            this.btnClose.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // UpgradeManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gctrlProfile);
            this.MinimumSize = new System.Drawing.Size(370, 348);
            this.Name = "UpgradeManagerForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(370, 348);
            ((System.ComponentModel.ISupportInitialize)(this.gctrlProfile)).EndInit();
            this.gctrlProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUpgradeOnce;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.GroupControl gctrlProfile;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridStones;
        private DevExpress.XtraGrid.Columns.GridColumn gcolRank;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDisplayName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolInternalName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}