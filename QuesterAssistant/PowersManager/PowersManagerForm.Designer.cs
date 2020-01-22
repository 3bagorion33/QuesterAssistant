namespace QuesterAssistant.PowersManager
{
    partial class PowersManagerForm
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.tedGlobHotKey = new DevExpress.XtraEditors.TextEdit();
            this.chkHotKeys = new DevExpress.XtraEditors.CheckEdit();
            this.gCtrlPowersPresets = new DevExpress.XtraEditors.GroupControl();
            this.lcPresetsList = new QuesterAssistant.Panels.ListControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tedCurrHotKey = new DevExpress.XtraEditors.TextEdit();
            this.btnGetPowers = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetPowers = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlPowers = new DevExpress.XtraGrid.GridControl();
            this.powerListSource = new System.Windows.Forms.BindingSource();
            this.gridViewPowers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gctrlCharInfo = new DevExpress.XtraEditors.GroupControl();
            this.labelCharacterName = new DevExpress.XtraEditors.LabelControl();
            this.labelCharacterClass = new DevExpress.XtraEditors.LabelControl();
            this.bsrcGlobHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcCurrHotKey = new System.Windows.Forms.BindingSource();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            ((System.ComponentModel.ISupportInitialize)(this.tedGlobHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHotKeys.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gCtrlPowersPresets)).BeginInit();
            this.gCtrlPowersPresets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedCurrHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPowers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerListSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPowers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCharInfo)).BeginInit();
            this.gctrlCharInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcGlobHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcCurrHotKey)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(161, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.labelControl5.Size = new System.Drawing.Size(111, 19);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "Presets loader window";
            this.labelControl5.ToolTip = "PopUp window over game client";
            // 
            // tedGlobHotKey
            // 
            this.tedGlobHotKey.EditValue = "";
            this.tedGlobHotKey.Location = new System.Drawing.Point(287, 5);
            this.tedGlobHotKey.Name = "tedGlobHotKey";
            this.tedGlobHotKey.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.tedGlobHotKey.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.tedGlobHotKey.Properties.ReadOnly = true;
            this.tedGlobHotKey.Size = new System.Drawing.Size(83, 20);
            this.tedGlobHotKey.TabIndex = 12;
            this.tedGlobHotKey.TabStop = false;
            this.tedGlobHotKey.ToolTip = "Click here to bind hotkey";
            this.tedGlobHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tedGlobHotKey_KeyDown);
            // 
            // chkHotKeys
            // 
            this.chkHotKeys.Location = new System.Drawing.Point(-1, 5);
            this.chkHotKeys.Name = "chkHotKeys";
            this.chkHotKeys.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkHotKeys.Properties.Appearance.Options.UseForeColor = true;
            this.chkHotKeys.Properties.Caption = "Enable hotkeys";
            this.chkHotKeys.Size = new System.Drawing.Size(106, 19);
            this.chkHotKeys.TabIndex = 14;
            this.chkHotKeys.TabStop = false;
            // 
            // gCtrlPowersPresets
            // 
            this.gCtrlPowersPresets.AutoSize = true;
            this.gCtrlPowersPresets.Controls.Add(this.lcPresetsList);
            this.gCtrlPowersPresets.Controls.Add(this.labelControl1);
            this.gCtrlPowersPresets.Controls.Add(this.tedCurrHotKey);
            this.gCtrlPowersPresets.Controls.Add(this.btnGetPowers);
            this.gCtrlPowersPresets.Controls.Add(this.btnSetPowers);
            this.gCtrlPowersPresets.Controls.Add(this.gridControlPowers);
            this.gCtrlPowersPresets.Enabled = false;
            this.gCtrlPowersPresets.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gCtrlPowersPresets.Location = new System.Drawing.Point(0, 80);
            this.gCtrlPowersPresets.Name = "gCtrlPowersPresets";
            this.gCtrlPowersPresets.Size = new System.Drawing.Size(370, 292);
            this.gCtrlPowersPresets.TabIndex = 10;
            this.gCtrlPowersPresets.Text = "Powers Preset";
            this.gCtrlPowersPresets.UseDisabledStatePainter = false;
            // 
            // lcPresetsList
            // 
            this.lcPresetsList.Location = new System.Drawing.Point(86, 1);
            this.lcPresetsList.Margin = new System.Windows.Forms.Padding(0);
            this.lcPresetsList.Name = "lcPresetsList";
            this.lcPresetsList.Size = new System.Drawing.Size(275, 20);
            this.lcPresetsList.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.labelControl1.Size = new System.Drawing.Size(74, 19);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Preset hotkey:";
            // 
            // tedCurrHotKey
            // 
            this.tedCurrHotKey.EditValue = "";
            this.tedCurrHotKey.Location = new System.Drawing.Point(86, 29);
            this.tedCurrHotKey.Name = "tedCurrHotKey";
            this.tedCurrHotKey.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.tedCurrHotKey.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.tedCurrHotKey.Properties.ReadOnly = true;
            this.tedCurrHotKey.Size = new System.Drawing.Size(119, 20);
            this.tedCurrHotKey.TabIndex = 7;
            this.tedCurrHotKey.TabStop = false;
            this.tedCurrHotKey.ToolTip = "Click here to bind hotkey";
            this.tedCurrHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tedCurrHotKey_KeyDown);
            // 
            // btnGetPowers
            // 
            this.btnGetPowers.Location = new System.Drawing.Point(211, 27);
            this.btnGetPowers.Name = "btnGetPowers";
            this.btnGetPowers.Size = new System.Drawing.Size(75, 23);
            this.btnGetPowers.TabIndex = 1;
            this.btnGetPowers.Text = "Get Powers";
            this.btnGetPowers.ToolTip = "Get powers list from the game";
            this.btnGetPowers.Click += new System.EventHandler(this.btnGetPowers_Click);
            // 
            // btnSetPowers
            // 
            this.btnSetPowers.Location = new System.Drawing.Point(292, 27);
            this.btnSetPowers.Name = "btnSetPowers";
            this.btnSetPowers.Size = new System.Drawing.Size(75, 23);
            this.btnSetPowers.TabIndex = 1;
            this.btnSetPowers.Text = "Set Powers";
            this.btnSetPowers.ToolTip = "Send powers list to the game";
            this.btnSetPowers.Click += new System.EventHandler(this.btnSetPowers_Click);
            // 
            // gridControlPowers
            // 
            this.gridControlPowers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlPowers.DataSource = this.powerListSource;
            this.gridControlPowers.Location = new System.Drawing.Point(3, 56);
            this.gridControlPowers.MainView = this.gridViewPowers;
            this.gridControlPowers.Name = "gridControlPowers";
            this.gridControlPowers.Size = new System.Drawing.Size(364, 233);
            this.gridControlPowers.TabIndex = 0;
            this.gridControlPowers.TabStop = false;
            this.gridControlPowers.UseDisabledStatePainter = false;
            this.gridControlPowers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPowers});
            // 
            // gridViewPowers
            // 
            this.gridViewPowers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridViewPowers.GridControl = this.gridControlPowers;
            this.gridViewPowers.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gridViewPowers.Name = "gridViewPowers";
            this.gridViewPowers.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridViewPowers.OptionsBehavior.Editable = false;
            this.gridViewPowers.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridViewPowers.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewPowers.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewPowers.OptionsCustomization.AllowFilter = false;
            this.gridViewPowers.OptionsCustomization.AllowGroup = false;
            this.gridViewPowers.OptionsCustomization.AllowSort = false;
            this.gridViewPowers.OptionsDetail.AllowZoomDetail = false;
            this.gridViewPowers.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewPowers.OptionsDetail.ShowDetailTabs = false;
            this.gridViewPowers.OptionsDetail.SmartDetailExpand = false;
            this.gridViewPowers.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewPowers.OptionsFilter.AllowMRUFilterList = false;
            this.gridViewPowers.OptionsFind.AllowFindPanel = false;
            this.gridViewPowers.OptionsMenu.EnableColumnMenu = false;
            this.gridViewPowers.OptionsMenu.EnableFooterMenu = false;
            this.gridViewPowers.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewPowers.OptionsPrint.UsePrintStyles = false;
            this.gridViewPowers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPowers.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewPowers.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewPowers.OptionsView.ShowDetailButtons = false;
            this.gridViewPowers.OptionsView.ShowGroupPanel = false;
            this.gridViewPowers.OptionsView.ShowIndicator = false;
            this.gridViewPowers.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gridViewPowers.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Slot";
            this.gridColumn1.FieldName = "TraySlot";
            this.gridColumn1.MaxWidth = 83;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 83;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Power";
            this.gridColumn2.FieldName = "InternalName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gctrlCharInfo
            // 
            this.gctrlCharInfo.AutoSize = true;
            this.gctrlCharInfo.Controls.Add(this.labelCharacterName);
            this.gctrlCharInfo.Controls.Add(this.labelCharacterClass);
            this.gctrlCharInfo.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gctrlCharInfo.Location = new System.Drawing.Point(0, 28);
            this.gctrlCharInfo.Name = "gctrlCharInfo";
            this.gctrlCharInfo.Size = new System.Drawing.Size(370, 46);
            this.gctrlCharInfo.TabIndex = 11;
            this.gctrlCharInfo.Text = "Current Character Info";
            // 
            // labelCharacterName
            // 
            this.labelCharacterName.Location = new System.Drawing.Point(166, 20);
            this.labelCharacterName.Name = "labelCharacterName";
            this.labelCharacterName.Padding = new System.Windows.Forms.Padding(3);
            this.labelCharacterName.Size = new System.Drawing.Size(53, 19);
            this.labelCharacterName.TabIndex = 0;
            this.labelCharacterName.Text = "Paragon: ";
            // 
            // labelCharacterClass
            // 
            this.labelCharacterClass.Location = new System.Drawing.Point(9, 20);
            this.labelCharacterClass.Name = "labelCharacterClass";
            this.labelCharacterClass.Padding = new System.Windows.Forms.Padding(3);
            this.labelCharacterClass.Size = new System.Drawing.Size(38, 19);
            this.labelCharacterClass.TabIndex = 0;
            this.labelCharacterClass.Text = "Class: ";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // PowersManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.tedGlobHotKey);
            this.Controls.Add(this.chkHotKeys);
            this.Controls.Add(this.gCtrlPowersPresets);
            this.Controls.Add(this.gctrlCharInfo);
            this.Name = "PowersManagerForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.PowersManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tedGlobHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHotKeys.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gCtrlPowersPresets)).EndInit();
            this.gCtrlPowersPresets.ResumeLayout(false);
            this.gCtrlPowersPresets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedCurrHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPowers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerListSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPowers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCharInfo)).EndInit();
            this.gctrlCharInfo.ResumeLayout(false);
            this.gctrlCharInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcGlobHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcCurrHotKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit tedGlobHotKey;
        private DevExpress.XtraEditors.CheckEdit chkHotKeys;
        private DevExpress.XtraEditors.GroupControl gCtrlPowersPresets;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tedCurrHotKey;
        private DevExpress.XtraEditors.SimpleButton btnGetPowers;
        private DevExpress.XtraEditors.SimpleButton btnSetPowers;
        private DevExpress.XtraGrid.GridControl gridControlPowers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPowers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl gctrlCharInfo;
        private DevExpress.XtraEditors.LabelControl labelCharacterName;
        private DevExpress.XtraEditors.LabelControl labelCharacterClass;
        private System.Windows.Forms.BindingSource powerListSource;
        private Panels.ListControl lcPresetsList;
        private System.Windows.Forms.BindingSource bsrcGlobHotKey;
        private System.Windows.Forms.BindingSource bsrcCurrHotKey;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}