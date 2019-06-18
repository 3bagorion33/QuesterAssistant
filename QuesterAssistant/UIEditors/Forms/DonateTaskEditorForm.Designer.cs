namespace QuesterAssistant.UIEditors.Forms
{
    partial class DonateTaskEditorForm
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
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.gctlCofferList = new DevExpress.XtraGrid.GridControl();
            this.bsrcCofferList = new System.Windows.Forms.BindingSource();
            this.gviewCofferList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gctlItemList = new DevExpress.XtraGrid.GridControl();
            this.bsrcItemList = new System.Windows.Forms.BindingSource();
            this.gviewItemList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.riNumEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pmItemList = new DevExpress.XtraBars.PopupMenu();
            this.miSetToAll = new DevExpress.XtraBars.BarButtonItem();
            this.bmItemList = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gctlCofferList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcCofferList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewCofferList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNumEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pmItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // gctlCofferList
            // 
            this.gctlCofferList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gctlCofferList.DataSource = this.bsrcCofferList;
            this.gctlCofferList.Location = new System.Drawing.Point(12, 14);
            this.gctlCofferList.MainView = this.gviewCofferList;
            this.gctlCofferList.Name = "gctlCofferList";
            this.gctlCofferList.Size = new System.Drawing.Size(240, 402);
            this.gctlCofferList.TabIndex = 1;
            this.gctlCofferList.TabStop = false;
            this.gctlCofferList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewCofferList});
            // 
            // gviewCofferList
            // 
            this.gviewCofferList.GridControl = this.gctlCofferList;
            this.gviewCofferList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gviewCofferList.Name = "gviewCofferList";
            this.gviewCofferList.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            this.gviewCofferList.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gviewCofferList.OptionsBehavior.Editable = false;
            this.gviewCofferList.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gviewCofferList.OptionsCustomization.AllowColumnMoving = false;
            this.gviewCofferList.OptionsCustomization.AllowColumnResizing = false;
            this.gviewCofferList.OptionsCustomization.AllowFilter = false;
            this.gviewCofferList.OptionsCustomization.AllowGroup = false;
            this.gviewCofferList.OptionsCustomization.AllowSort = false;
            this.gviewCofferList.OptionsDetail.AllowZoomDetail = false;
            this.gviewCofferList.OptionsDetail.EnableMasterViewMode = false;
            this.gviewCofferList.OptionsDetail.ShowDetailTabs = false;
            this.gviewCofferList.OptionsDetail.SmartDetailExpand = false;
            this.gviewCofferList.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gviewCofferList.OptionsFilter.AllowMRUFilterList = false;
            this.gviewCofferList.OptionsFind.AllowFindPanel = false;
            this.gviewCofferList.OptionsMenu.EnableColumnMenu = false;
            this.gviewCofferList.OptionsMenu.EnableFooterMenu = false;
            this.gviewCofferList.OptionsMenu.EnableGroupPanelMenu = false;
            this.gviewCofferList.OptionsPrint.UsePrintStyles = false;
            this.gviewCofferList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gviewCofferList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gviewCofferList.OptionsSelection.UseIndicatorForSelection = false;
            this.gviewCofferList.OptionsView.ShowDetailButtons = false;
            this.gviewCofferList.OptionsView.ShowGroupPanel = false;
            this.gviewCofferList.OptionsView.ShowIndicator = false;
            this.gviewCofferList.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gviewCofferList.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gviewCofferList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gviewCofferList_FocusedRowChanged);
            this.gviewCofferList.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.HandleMouseWheel);
            // 
            // gctlItemList
            // 
            this.gctlItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gctlItemList.DataSource = this.bsrcItemList;
            this.gctlItemList.Location = new System.Drawing.Point(258, 14);
            this.gctlItemList.MainView = this.gviewItemList;
            this.gctlItemList.Name = "gctlItemList";
            this.gctlItemList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riNumEdit});
            this.gctlItemList.Size = new System.Drawing.Size(400, 382);
            this.gctlItemList.TabIndex = 2;
            this.gctlItemList.TabStop = false;
            this.gctlItemList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewItemList});
            // 
            // gviewItemList
            // 
            this.gviewItemList.GridControl = this.gctlItemList;
            this.gviewItemList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gviewItemList.Name = "gviewItemList";
            this.gviewItemList.OptionsBehavior.AutoExpandAllGroups = true;
            this.gviewItemList.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gviewItemList.OptionsCustomization.AllowColumnMoving = false;
            this.gviewItemList.OptionsCustomization.AllowFilter = false;
            this.gviewItemList.OptionsCustomization.AllowGroup = false;
            this.gviewItemList.OptionsDetail.AllowZoomDetail = false;
            this.gviewItemList.OptionsDetail.EnableMasterViewMode = false;
            this.gviewItemList.OptionsDetail.ShowDetailTabs = false;
            this.gviewItemList.OptionsDetail.SmartDetailExpand = false;
            this.gviewItemList.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gviewItemList.OptionsFilter.AllowMRUFilterList = false;
            this.gviewItemList.OptionsFind.AllowFindPanel = false;
            this.gviewItemList.OptionsMenu.EnableColumnMenu = false;
            this.gviewItemList.OptionsMenu.EnableFooterMenu = false;
            this.gviewItemList.OptionsMenu.EnableGroupPanelMenu = false;
            this.gviewItemList.OptionsPrint.UsePrintStyles = false;
            this.gviewItemList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gviewItemList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gviewItemList.OptionsSelection.UseIndicatorForSelection = false;
            this.gviewItemList.OptionsView.ShowDetailButtons = false;
            this.gviewItemList.OptionsView.ShowGroupedColumns = true;
            this.gviewItemList.OptionsView.ShowGroupPanel = false;
            this.gviewItemList.OptionsView.ShowIndicator = false;
            this.gviewItemList.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gviewItemList.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gviewItemList_PopupMenuShowing);
            this.gviewItemList.ShownEditor += new System.EventHandler(this.gviewItemList_ShownEditor);
            // 
            // riNumEdit
            // 
            this.riNumEdit.AutoHeight = false;
            this.riNumEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riNumEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riNumEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riNumEdit.IsFloatValue = false;
            this.riNumEdit.Mask.EditMask = "d";
            this.riNumEdit.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.riNumEdit.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.riNumEdit.Name = "riNumEdit";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(583, 422);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(258, 384);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(400, 32);
            this.groupControl1.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Location = new System.Drawing.Point(6, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(349, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Use context menu for custom options. \"Donate = -1\" meas all as possible";
            // 
            // pmItemList
            // 
            this.pmItemList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miSetToAll)});
            this.pmItemList.Manager = this.bmItemList;
            this.pmItemList.Name = "pmItemList";
            // 
            // miSetToAll
            // 
            this.miSetToAll.Caption = "Set this count to all";
            this.miSetToAll.Id = 0;
            this.miSetToAll.Name = "miSetToAll";
            this.miSetToAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miSetToAll_ItemClick);
            // 
            // bmItemList
            // 
            this.bmItemList.DockControls.Add(this.barDockControlTop);
            this.bmItemList.DockControls.Add(this.barDockControlBottom);
            this.bmItemList.DockControls.Add(this.barDockControlLeft);
            this.bmItemList.DockControls.Add(this.barDockControlRight);
            this.bmItemList.Form = this;
            this.bmItemList.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.miSetToAll});
            this.bmItemList.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmItemList;
            this.barDockControlTop.Size = new System.Drawing.Size(670, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 460);
            this.barDockControlBottom.Manager = this.bmItemList;
            this.barDockControlBottom.Size = new System.Drawing.Size(670, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bmItemList;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 460);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(670, 0);
            this.barDockControlRight.Manager = this.bmItemList;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 460);
            // 
            // DonateTaskEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 460);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gctlItemList);
            this.Controls.Add(this.gctlCofferList);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.Name = "DonateTaskEditorForm";
            this.ShowIcon = false;
            this.Text = "Choose coffer and items to donate";
            this.Load += new System.EventHandler(this.DonateTaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gctlCofferList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcCofferList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewCofferList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNumEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pmItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gctlCofferList;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewCofferList;
        private DevExpress.XtraGrid.GridControl gctlItemList;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewItemList;
        private System.Windows.Forms.BindingSource bsrcCofferList;
        private System.Windows.Forms.BindingSource bsrcItemList;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riNumEdit;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.PopupMenu pmItemList;
        private DevExpress.XtraBars.BarButtonItem miSetToAll;
        private DevExpress.XtraBars.BarManager bmItemList;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}