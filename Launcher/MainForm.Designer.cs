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
            this.gctlProcessList = new DevExpress.XtraGrid.GridControl();
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
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkClose = new DevExpress.XtraEditors.CheckEdit();
            this.chkKill = new DevExpress.XtraEditors.CheckEdit();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.gctlLogEventList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.notifyTray = new System.Windows.Forms.NotifyIcon();
            this.cbxPriority = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gctlProcessList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKill.Properties)).BeginInit();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gctlLogEventList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPriority.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnStart.Appearance.Options.UseFont = true;
            this.btnStart.Location = new System.Drawing.Point(12, 277);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(360, 46);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start new instance";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerDelete
            // 
            this.timerDelete.Enabled = true;
            this.timerDelete.Interval = 250;
            this.timerDelete.Tick += new System.EventHandler(this.timerDelete_Tick);
            // 
            // gctlProcessList
            // 
            this.gctlProcessList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlProcessList.DataSource = this.bsrcInstancesList;
            this.gctlProcessList.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gctlProcessList.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gctlProcessList.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gctlProcessList.Location = new System.Drawing.Point(12, 109);
            this.gctlProcessList.MainView = this.gridInstances;
            this.gctlProcessList.Name = "gctlProcessList";
            this.gctlProcessList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnClose});
            this.gctlProcessList.Size = new System.Drawing.Size(360, 162);
            this.gctlProcessList.TabIndex = 1;
            this.gctlProcessList.ToolTipController = this.toolTipController1;
            this.gctlProcessList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridInstances});
            this.gctlProcessList.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
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
            this.gridInstances.GridControl = this.gctlProcessList;
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
            this.dbtnWindowsFirewall.Location = new System.Drawing.Point(5, 24);
            this.dbtnWindowsFirewall.MenuManager = this.barFirewall;
            this.dbtnWindowsFirewall.Name = "dbtnWindowsFirewall";
            this.dbtnWindowsFirewall.Size = new System.Drawing.Size(167, 23);
            this.dbtnWindowsFirewall.TabIndex = 2;
            this.dbtnWindowsFirewall.Text = "Windows Firewall ...";
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
            this.chkHideTitle.Location = new System.Drawing.Point(4, 53);
            this.chkHideTitle.MenuManager = this.barFirewall;
            this.chkHideTitle.Name = "chkHideTitle";
            this.chkHideTitle.Properties.Caption = "Hide title";
            this.chkHideTitle.Size = new System.Drawing.Size(134, 19);
            this.chkHideTitle.TabIndex = 7;
            // 
            // tabPane1
            // 
            this.tabPane1.AllowCollapse = DevExpress.Utils.DefaultBoolean.Default;
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(384, 362);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(384, 362);
            this.tabPane1.TabIndex = 12;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "Main";
            this.tabNavigationPage1.Controls.Add(this.groupControl2);
            this.tabNavigationPage1.Controls.Add(this.groupControl1);
            this.tabNavigationPage1.Controls.Add(this.btnStart);
            this.tabNavigationPage1.Controls.Add(this.gctlProcessList);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(384, 335);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.cbxPriority);
            this.groupControl2.Controls.Add(this.chkHideTitle);
            this.groupControl2.Location = new System.Drawing.Point(12, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(177, 100);
            this.groupControl2.TabIndex = 9;
            this.groupControl2.Text = "Astral\'s Instances";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.chkClose);
            this.groupControl1.Controls.Add(this.chkKill);
            this.groupControl1.Controls.Add(this.dbtnWindowsFirewall);
            this.groupControl1.Location = new System.Drawing.Point(195, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(177, 100);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "CrypticError";
            // 
            // chkClose
            // 
            this.chkClose.EditValue = true;
            this.chkClose.Location = new System.Drawing.Point(4, 78);
            this.chkClose.Name = "chkClose";
            this.chkClose.Properties.Caption = "Close NWO crash frame";
            this.chkClose.Size = new System.Drawing.Size(152, 19);
            this.chkClose.TabIndex = 3;
            // 
            // chkKill
            // 
            this.chkKill.EditValue = true;
            this.chkKill.Location = new System.Drawing.Point(4, 53);
            this.chkKill.MenuManager = this.barFirewall;
            this.chkKill.Name = "chkKill";
            this.chkKill.Properties.Caption = "Kill CrypticError processes";
            this.chkKill.Size = new System.Drawing.Size(152, 19);
            this.chkKill.TabIndex = 3;
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "Log";
            this.tabNavigationPage2.Controls.Add(this.gctlLogEventList);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(384, 335);
            // 
            // gctlLogEventList
            // 
            this.gctlLogEventList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlLogEventList.DataSource = this.bsrcInstancesList;
            this.gctlLogEventList.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gctlLogEventList.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gctlLogEventList.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gctlLogEventList.Location = new System.Drawing.Point(12, 13);
            this.gctlLogEventList.MainView = this.gridView1;
            this.gctlLogEventList.Name = "gctlLogEventList";
            this.gctlLogEventList.Size = new System.Drawing.Size(360, 310);
            this.gctlLogEventList.TabIndex = 2;
            this.gctlLogEventList.ToolTipController = this.toolTipController1;
            this.gctlLogEventList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gctlLogEventList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsDetail.AllowZoomDetail = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsDetail.SmartDetailExpand = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsFind.AllowFindPanel = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsPrint.UsePrintStyles = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "DateTime";
            this.gridColumn1.DisplayFormat.FormatString = "G";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "DateTime";
            this.gridColumn1.MaxWidth = 120;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Event";
            this.gridColumn2.FieldName = "Event";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 238;
            // 
            // notifyTray
            // 
            this.notifyTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyTray.Icon")));
            this.notifyTray.Text = "Launcher";
            this.notifyTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyTray_MouseClick);
            // 
            // cbxPriority
            // 
            this.cbxPriority.Location = new System.Drawing.Point(52, 26);
            this.cbxPriority.MenuManager = this.barFirewall;
            this.cbxPriority.Name = "cbxPriority";
            this.cbxPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPriority.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxPriority.Size = new System.Drawing.Size(120, 20);
            this.cbxPriority.TabIndex = 8;
            this.cbxPriority.SelectedIndexChanged += new System.EventHandler(this.cbxPriority_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Priority :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.tabPane1);
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
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gctlProcessList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKill.Properties)).EndInit();
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gctlLogEventList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPriority.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private System.Windows.Forms.Timer timerDelete;
        private DevExpress.XtraGrid.GridControl gctlProcessList;
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
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private DevExpress.XtraGrid.GridControl gctlLogEventList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chkClose;
        private DevExpress.XtraEditors.CheckEdit chkKill;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.NotifyIcon notifyTray;
        private DevExpress.XtraEditors.ComboBoxEdit cbxPriority;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

