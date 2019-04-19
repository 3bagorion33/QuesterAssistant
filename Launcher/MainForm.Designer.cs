namespace Launcher
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.timerDelete = new System.Windows.Forms.Timer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bsrcInstancesList = new System.Windows.Forms.BindingSource();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.gridInstances = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolNewTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolOrigTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.dbtnWindowsFirewall = new DevExpress.XtraEditors.DropDownButton();
            this.menuFirewall = new DevExpress.XtraBars.PopupMenu();
            this.menuFirewallDenyApps = new DevExpress.XtraBars.BarButtonItem();
            this.menuFirewallDenyAddress = new DevExpress.XtraBars.BarButtonItem();
            this.menuFirewallDeleteRules = new DevExpress.XtraBars.BarButtonItem();
            this.barFirewall = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.chkHideTitle = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideTitle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(12, 327);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(360, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start new instance";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerDelete
            // 
            this.timerDelete.Enabled = true;
            this.timerDelete.Interval = 50;
            this.timerDelete.Tick += new System.EventHandler(this.timerDelete_Tick);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsrcInstancesList;
            this.gridControl1.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gridControl1.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gridControl1.Location = new System.Drawing.Point(12, 129);
            this.gridControl1.MainView = this.gridInstances;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnClose});
            this.gridControl1.Size = new System.Drawing.Size(360, 192);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ToolTipController = this.toolTipController1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridInstances});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // toolTipController1
            // 
            this.toolTipController1.InitialDelay = 100;
            this.toolTipController1.ShowBeak = true;
            this.toolTipController1.ShowShadow = false;
            this.toolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // gridInstances
            // 
            this.gridInstances.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolProcess,
            this.gcolNewTitle,
            this.gcolOrigTitle,
            this.gcolButton});
            this.gridInstances.GridControl = this.gridControl1;
            this.gridInstances.Name = "gridInstances";
            this.gridInstances.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridInstances.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridInstances.OptionsCustomization.AllowColumnMoving = false;
            this.gridInstances.OptionsCustomization.AllowFilter = false;
            this.gridInstances.OptionsCustomization.AllowGroup = false;
            this.gridInstances.OptionsDetail.AllowZoomDetail = false;
            this.gridInstances.OptionsDetail.EnableMasterViewMode = false;
            this.gridInstances.OptionsDetail.ShowDetailTabs = false;
            this.gridInstances.OptionsDetail.SmartDetailExpand = false;
            this.gridInstances.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridInstances.OptionsFilter.AllowFilterEditor = false;
            this.gridInstances.OptionsFilter.AllowMRUFilterList = false;
            this.gridInstances.OptionsFind.AllowFindPanel = false;
            this.gridInstances.OptionsMenu.EnableColumnMenu = false;
            this.gridInstances.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridInstances.OptionsPrint.UsePrintStyles = false;
            this.gridInstances.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridInstances.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridInstances.OptionsSelection.UseIndicatorForSelection = false;
            this.gridInstances.OptionsView.ShowDetailButtons = false;
            this.gridInstances.OptionsView.ShowGroupPanel = false;
            this.gridInstances.OptionsView.ShowIndicator = false;
            // 
            // gcolProcess
            // 
            this.gcolProcess.Caption = "Process";
            this.gcolProcess.FieldName = "Process.ProcessName";
            this.gcolProcess.MaxWidth = 70;
            this.gcolProcess.Name = "gcolProcess";
            this.gcolProcess.OptionsColumn.AllowEdit = false;
            this.gcolProcess.OptionsColumn.FixedWidth = true;
            this.gcolProcess.Visible = true;
            this.gcolProcess.VisibleIndex = 0;
            this.gcolProcess.Width = 70;
            // 
            // gcolNewTitle
            // 
            this.gcolNewTitle.Caption = "New Title";
            this.gcolNewTitle.FieldName = "NewTitle";
            this.gcolNewTitle.Name = "gcolNewTitle";
            this.gcolNewTitle.OptionsColumn.AllowEdit = false;
            this.gcolNewTitle.Visible = true;
            this.gcolNewTitle.VisibleIndex = 1;
            this.gcolNewTitle.Width = 100;
            // 
            // gcolOrigTitle
            // 
            this.gcolOrigTitle.Caption = "Original Title";
            this.gcolOrigTitle.FieldName = "OriginalTitle";
            this.gcolOrigTitle.Name = "gcolOrigTitle";
            this.gcolOrigTitle.OptionsColumn.AllowEdit = false;
            this.gcolOrigTitle.Visible = true;
            this.gcolOrigTitle.VisibleIndex = 2;
            this.gcolOrigTitle.Width = 124;
            // 
            // gcolButton
            // 
            this.gcolButton.ColumnEdit = this.btnClose;
            this.gcolButton.MaxWidth = 25;
            this.gcolButton.MinWidth = 25;
            this.gcolButton.Name = "gcolButton";
            this.gcolButton.OptionsColumn.FixedWidth = true;
            this.gcolButton.OptionsColumn.ReadOnly = true;
            this.gcolButton.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcolButton.Visible = true;
            this.gcolButton.VisibleIndex = 3;
            this.gcolButton.Width = 25;
            // 
            // btnClose
            // 
            this.btnClose.AutoHeight = false;
            this.btnClose.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Close this instance", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(3);
            this.btnClose.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnClose.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnClose_ButtonClick);
            // 
            // dbtnWindowsFirewall
            // 
            this.dbtnWindowsFirewall.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.dbtnWindowsFirewall.DropDownControl = this.menuFirewall;
            this.dbtnWindowsFirewall.Location = new System.Drawing.Point(12, 100);
            this.dbtnWindowsFirewall.MenuManager = this.barFirewall;
            this.dbtnWindowsFirewall.Name = "dbtnWindowsFirewall";
            this.dbtnWindowsFirewall.Size = new System.Drawing.Size(135, 23);
            this.dbtnWindowsFirewall.TabIndex = 2;
            this.dbtnWindowsFirewall.Text = "Windows Firewal ...";
            // 
            // menuFirewall
            // 
            this.menuFirewall.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFirewallDenyApps),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFirewallDenyAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFirewallDeleteRules)});
            this.menuFirewall.Manager = this.barFirewall;
            this.menuFirewall.Name = "menuFirewall";
            // 
            // menuFirewallDenyApps
            // 
            this.menuFirewallDenyApps.Caption = "Deny CrypticError apps";
            this.menuFirewallDenyApps.Id = 0;
            this.menuFirewallDenyApps.Name = "menuFirewallDenyApps";
            this.menuFirewallDenyApps.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFirewallDenyApps_ItemClick);
            // 
            // menuFirewallDenyAddress
            // 
            this.menuFirewallDenyAddress.Caption = "Deny CrypticError server";
            this.menuFirewallDenyAddress.Id = 1;
            this.menuFirewallDenyAddress.Name = "menuFirewallDenyAddress";
            this.menuFirewallDenyAddress.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFirewallDenyAddress_ItemClick);
            // 
            // menuFirewallDeleteRules
            // 
            this.menuFirewallDeleteRules.Caption = "Remove all rules";
            this.menuFirewallDeleteRules.Id = 2;
            this.menuFirewallDeleteRules.Name = "menuFirewallDeleteRules";
            this.menuFirewallDeleteRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFirewallDeleteRules_ItemClick);
            // 
            // barFirewall
            // 
            this.barFirewall.DockControls.Add(this.barDockControlTop);
            this.barFirewall.DockControls.Add(this.barDockControlBottom);
            this.barFirewall.DockControls.Add(this.barDockControlLeft);
            this.barFirewall.DockControls.Add(this.barDockControlRight);
            this.barFirewall.Form = this;
            this.barFirewall.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.menuFirewallDenyApps,
            this.menuFirewallDenyAddress,
            this.menuFirewallDeleteRules});
            this.barFirewall.MaxItemId = 3;
            this.barFirewall.PopupShowMode = DevExpress.XtraBars.PopupShowMode.Inplace;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barFirewall;
            this.barDockControlTop.Size = new System.Drawing.Size(384, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 362);
            this.barDockControlBottom.Manager = this.barFirewall;
            this.barDockControlBottom.Size = new System.Drawing.Size(384, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barFirewall;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 362);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(384, 0);
            this.barDockControlRight.Manager = this.barFirewall;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 362);
            // 
            // chkHideTitle
            // 
            this.chkHideTitle.EditValue = true;
            this.chkHideTitle.Location = new System.Drawing.Point(13, 75);
            this.chkHideTitle.MenuManager = this.barFirewall;
            this.chkHideTitle.Name = "chkHideTitle";
            this.chkHideTitle.Properties.Caption = "Hide Astral title";
            this.chkHideTitle.Size = new System.Drawing.Size(134, 19);
            this.chkHideTitle.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.chkHideTitle);
            this.Controls.Add(this.dbtnWindowsFirewall);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideTitle.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private System.Windows.Forms.Timer timerDelete;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridInstances;
        private System.Windows.Forms.BindingSource bsrcInstancesList;
        private DevExpress.XtraGrid.Columns.GridColumn gcolProcess;
        private DevExpress.XtraGrid.Columns.GridColumn gcolOrigTitle;
        private DevExpress.XtraGrid.Columns.GridColumn gcolNewTitle;
        private DevExpress.XtraGrid.Columns.GridColumn gcolButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnClose;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.DropDownButton dbtnWindowsFirewall;
        private DevExpress.XtraBars.PopupMenu menuFirewall;
        private DevExpress.XtraBars.BarButtonItem menuFirewallDenyApps;
        private DevExpress.XtraBars.BarManager barFirewall;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem menuFirewallDenyAddress;
        private DevExpress.XtraBars.BarButtonItem menuFirewallDeleteRules;
        private DevExpress.XtraEditors.CheckEdit chkHideTitle;
    }
}

