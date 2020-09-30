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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.timerDelete = new System.Windows.Forms.Timer();
            this.gctlProcessList = new DevExpress.XtraGrid.GridControl();
            this.bsrcInstancesList = new System.Windows.Forms.BindingSource();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.gridInstances = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolPID = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.spinOffsetX = new DevExpress.XtraEditors.SpinEdit();
            this.spinOffsetY = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbxPosotion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbxPriority = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkClose = new DevExpress.XtraEditors.CheckEdit();
            this.chkKill = new DevExpress.XtraEditors.CheckEdit();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.gctlLogEventList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabNavigationPage3 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.gctlPatchesList = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.notifyTray = new System.Windows.Forms.NotifyIcon();
            this.barInstances = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.miCloseProcess = new DevExpress.XtraBars.BarButtonItem();
            this.menuInstances = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.gctlProcessList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOffsetX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOffsetY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPosotion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKill.Properties)).BeginInit();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gctlLogEventList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tabNavigationPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gctlPatchesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuInstances)).BeginInit();
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
            this.btnStart.Location = new System.Drawing.Point(12, 350);
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
            this.gctlProcessList.Location = new System.Drawing.Point(12, 112);
            this.gctlProcessList.MainView = this.gridInstances;
            this.gctlProcessList.Name = "gctlProcessList";
            this.gctlProcessList.Size = new System.Drawing.Size(360, 197);
            this.gctlProcessList.TabIndex = 1;
            this.gctlProcessList.ToolTipController = this.toolTipController1;
            this.gctlProcessList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridInstances});
            this.gctlProcessList.DoubleClick += new System.EventHandler(this.gctlProcessList_DoubleClick);
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
            this.gcolTitle,
            this.gcolPID});
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
            this.gridInstances.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridInstances_PopupMenuShowing);
            // 
            // gcolProcess
            // 
            this.gcolProcess.Caption = "Process";
            this.gcolProcess.FieldName = "Process.ProcessName";
            this.gcolProcess.MaxWidth = 65;
            this.gcolProcess.Name = "gcolProcess";
            this.gcolProcess.OptionsColumn.AllowEdit = false;
            this.gcolProcess.OptionsColumn.FixedWidth = true;
            this.gcolProcess.Visible = true;
            this.gcolProcess.VisibleIndex = 0;
            this.gcolProcess.Width = 65;
            // 
            // gcolTitle
            // 
            this.gcolTitle.Caption = "Title";
            this.gcolTitle.FieldName = "Title";
            this.gcolTitle.Name = "gcolTitle";
            this.gcolTitle.OptionsColumn.AllowEdit = false;
            this.gcolTitle.Visible = true;
            this.gcolTitle.VisibleIndex = 1;
            this.gcolTitle.Width = 119;
            // 
            // gcolPID
            // 
            this.gcolPID.Caption = "PID";
            this.gcolPID.FieldName = "PID";
            this.gcolPID.MaxWidth = 65;
            this.gcolPID.Name = "gcolPID";
            this.gcolPID.OptionsColumn.AllowEdit = false;
            this.gcolPID.Visible = true;
            this.gcolPID.VisibleIndex = 2;
            this.gcolPID.Width = 65;
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
            this.barDockControlTop.Location = new System.Drawing.Point(0, 46);
            this.barDockControlTop.Manager = this.barFirewall;
            this.barDockControlTop.Size = new System.Drawing.Size(384, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 408);
            this.barDockControlBottom.Manager = this.barFirewall;
            this.barDockControlBottom.Size = new System.Drawing.Size(384, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Manager = this.barFirewall;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 362);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(384, 46);
            this.barDockControlRight.Manager = this.barFirewall;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 362);
            // 
            // tabPane1
            // 
            this.tabPane1.AllowCollapse = DevExpress.Utils.DefaultBoolean.Default;
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Controls.Add(this.tabNavigationPage3);
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage3,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(384, 336);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(384, 336);
            this.tabPane1.TabIndex = 12;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "Main";
            this.tabNavigationPage1.Controls.Add(this.groupControl2);
            this.tabNavigationPage1.Controls.Add(this.groupControl1);
            this.tabNavigationPage1.Controls.Add(this.gctlProcessList);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(384, 309);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.spinOffsetX);
            this.groupControl2.Controls.Add(this.spinOffsetY);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.cbxPosotion);
            this.groupControl2.Controls.Add(this.cbxPriority);
            this.groupControl2.Location = new System.Drawing.Point(12, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(177, 103);
            this.groupControl2.TabIndex = 9;
            this.groupControl2.Text = "Astral\'s Instances";
            // 
            // spinOffsetX
            // 
            this.spinOffsetX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOffsetX.Location = new System.Drawing.Point(55, 78);
            this.spinOffsetX.Name = "spinOffsetX";
            this.spinOffsetX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOffsetX.Properties.IsFloatValue = false;
            this.spinOffsetX.Properties.Mask.EditMask = "N00";
            this.spinOffsetX.Size = new System.Drawing.Size(46, 20);
            this.spinOffsetX.TabIndex = 9;
            // 
            // spinOffsetY
            // 
            this.spinOffsetY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOffsetY.Location = new System.Drawing.Point(126, 78);
            this.spinOffsetY.Name = "spinOffsetY";
            this.spinOffsetY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOffsetY.Properties.IsFloatValue = false;
            this.spinOffsetY.Properties.Mask.EditMask = "N00";
            this.spinOffsetY.Size = new System.Drawing.Size(46, 20);
            this.spinOffsetY.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(107, 81);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(13, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Y :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Offset X :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Position :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Priority :";
            // 
            // cbxPosotion
            // 
            this.cbxPosotion.Location = new System.Drawing.Point(55, 52);
            this.cbxPosotion.Name = "cbxPosotion";
            this.cbxPosotion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPosotion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxPosotion.Size = new System.Drawing.Size(117, 20);
            this.cbxPosotion.TabIndex = 8;
            this.cbxPosotion.SelectedIndexChanged += new System.EventHandler(this.cbxPriority_SelectedIndexChanged);
            // 
            // cbxPriority
            // 
            this.cbxPriority.Location = new System.Drawing.Point(55, 26);
            this.cbxPriority.MenuManager = this.barFirewall;
            this.cbxPriority.Name = "cbxPriority";
            this.cbxPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPriority.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxPriority.Size = new System.Drawing.Size(117, 20);
            this.cbxPriority.TabIndex = 8;
            this.cbxPriority.SelectedIndexChanged += new System.EventHandler(this.cbxPriority_SelectedIndexChanged);
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
            this.groupControl1.Size = new System.Drawing.Size(177, 103);
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
            this.tabNavigationPage2.Size = new System.Drawing.Size(384, 309);
            // 
            // gctlLogEventList
            // 
            this.gctlLogEventList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlLogEventList.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gctlLogEventList.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gctlLogEventList.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gctlLogEventList.Location = new System.Drawing.Point(12, 13);
            this.gctlLogEventList.MainView = this.gridView1;
            this.gctlLogEventList.Name = "gctlLogEventList";
            this.gctlLogEventList.Size = new System.Drawing.Size(360, 296);
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
            // tabNavigationPage3
            // 
            this.tabNavigationPage3.Caption = "Patches";
            this.tabNavigationPage3.Controls.Add(this.gctlPatchesList);
            this.tabNavigationPage3.Name = "tabNavigationPage3";
            this.tabNavigationPage3.Size = new System.Drawing.Size(384, 309);
            // 
            // gctlPatchesList
            // 
            this.gctlPatchesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlPatchesList.EmbeddedNavigator.ToolTip = "Tooltip";
            this.gctlPatchesList.EmbeddedNavigator.ToolTipController = this.toolTipController1;
            this.gctlPatchesList.EmbeddedNavigator.ToolTipTitle = "TooltipTitle";
            this.gctlPatchesList.Location = new System.Drawing.Point(12, 13);
            this.gctlPatchesList.MainView = this.gridView2;
            this.gctlPatchesList.Name = "gctlPatchesList";
            this.gctlPatchesList.Size = new System.Drawing.Size(360, 296);
            this.gctlPatchesList.TabIndex = 3;
            this.gctlPatchesList.ToolTipController = this.toolTipController1;
            this.gctlPatchesList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView2.GridControl = this.gctlPatchesList;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView2.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsDetail.AllowZoomDetail = false;
            this.gridView2.OptionsDetail.EnableMasterViewMode = false;
            this.gridView2.OptionsDetail.ShowDetailTabs = false;
            this.gridView2.OptionsDetail.SmartDetailExpand = false;
            this.gridView2.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView2.OptionsFilter.AllowFilterEditor = false;
            this.gridView2.OptionsFilter.AllowMRUFilterList = false;
            this.gridView2.OptionsFind.AllowFindPanel = false;
            this.gridView2.OptionsMenu.EnableColumnMenu = false;
            this.gridView2.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView2.OptionsPrint.UsePrintStyles = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView2.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Name";
            this.gridColumn3.FieldName = "Name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Description";
            this.gridColumn4.FieldName = "Desc";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 258;
            // 
            // gridColumn5
            // 
            this.gridColumn5.FieldName = "Active";
            this.gridColumn5.MaxWidth = 20;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.OptionsColumn.ShowCaption = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 20;
            // 
            // notifyTray
            // 
            this.notifyTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyTray.Icon")));
            this.notifyTray.Text = "Launcher";
            this.notifyTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyTray_MouseClick);
            // 
            // barInstances
            // 
            this.barInstances.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barInstances.DockControls.Add(this.barDockControl1);
            this.barInstances.DockControls.Add(this.barDockControl2);
            this.barInstances.DockControls.Add(this.barDockControl3);
            this.barInstances.DockControls.Add(this.barDockControl4);
            this.barInstances.Form = this;
            this.barInstances.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.miCloseProcess});
            this.barInstances.MainMenu = this.bar2;
            this.barInstances.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Сервис";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Сервис";
            // 
            // bar2
            // 
            this.bar2.BarName = "Главное меню";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Главное меню";
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barInstances;
            this.barDockControl1.Size = new System.Drawing.Size(384, 46);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 408);
            this.barDockControl2.Manager = this.barInstances;
            this.barDockControl2.Size = new System.Drawing.Size(384, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 46);
            this.barDockControl3.Manager = this.barInstances;
            this.barDockControl3.Size = new System.Drawing.Size(0, 362);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(384, 46);
            this.barDockControl4.Manager = this.barInstances;
            this.barDockControl4.Size = new System.Drawing.Size(0, 362);
            // 
            // miCloseProcess
            // 
            this.miCloseProcess.Caption = "Close Process";
            this.miCloseProcess.Id = 1;
            this.miCloseProcess.Name = "miCloseProcess";
            this.miCloseProcess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miCloseProcess_ItemClick);
            // 
            // menuInstances
            // 
            this.menuInstances.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miCloseProcess)});
            this.menuInstances.Manager = this.barInstances;
            this.menuInstances.Name = "menuInstances";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 408);
            this.Controls.Add(this.tabPane1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Launcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gctlProcessList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcInstancesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barFirewall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOffsetX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOffsetY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPosotion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKill.Properties)).EndInit();
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gctlLogEventList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tabNavigationPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gctlPatchesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuInstances)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gcolPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolTitle;
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
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage3;
        private DevExpress.XtraGrid.GridControl gctlPatchesList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager barInstances;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.PopupMenu menuInstances;
        private DevExpress.XtraBars.BarButtonItem miCloseProcess;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbxPosotion;
        private DevExpress.XtraEditors.SpinEdit spinOffsetY;
        private DevExpress.XtraEditors.SpinEdit spinOffsetX;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}

