using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Astral.Classes.ItemFilter;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    partial class ItemFilterUC
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.purchaseOptions = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStringType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilterType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bRemoveEntry = new DevExpress.XtraEditors.SimpleButton();
            this.bAddEntry = new DevExpress.XtraEditors.DropDownButton();
            this.addEntryPop = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.bReverse = new DevExpress.XtraEditors.CheckButton();
            this.bAddItem = new DevExpress.XtraEditors.SimpleButton();
            this.addTypeLabel = new DevExpress.XtraEditors.LabelControl();
            this.itemListCombo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManager_0 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl_0 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_3 = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.fastAddItemCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.bAddEntryAdvanced = new DevExpress.XtraEditors.SimpleButton();
            this.addEntryMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bShowItems = new DevExpress.XtraEditors.SimpleButton();
            this.bClear = new DevExpress.XtraEditors.SimpleButton();
            this.bExport = new DevExpress.XtraEditors.SimpleButton();
            this.bImport = new DevExpress.XtraEditors.SimpleButton();
            this.bExpand = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.purchaseOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEntryPop)).BeginInit();
            this.addEntryPop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemListCombo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastAddItemCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEntryMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // purchaseOptions
            // 
            this.purchaseOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.purchaseOptions.Cursor = System.Windows.Forms.Cursors.Default;
            this.purchaseOptions.DataSource = this.bindingSource;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.purchaseOptions.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.purchaseOptions.Location = new System.Drawing.Point(3, 3);
            this.purchaseOptions.MainView = this.gridView2;
            this.purchaseOptions.Name = "purchaseOptions";
            this.purchaseOptions.Size = new System.Drawing.Size(469, 338);
            this.purchaseOptions.TabIndex = 6;
            this.purchaseOptions.UseEmbeddedNavigator = true;
            this.purchaseOptions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStringType,
            this.colFilterType,
            this.colMode,
            this.colText});
            this.gridView2.GridControl = this.purchaseOptions;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // colStringType
            // 
            this.colStringType.Caption = "Text type";
            this.colStringType.FieldName = "StringType";
            this.colStringType.Name = "colStringType";
            this.colStringType.OptionsColumn.FixedWidth = true;
            this.colStringType.Visible = true;
            this.colStringType.VisibleIndex = 0;
            // 
            // colFilterType
            // 
            this.colFilterType.Caption = "Filter type";
            this.colFilterType.FieldName = "Type";
            this.colFilterType.Name = "colFilterType";
            this.colFilterType.OptionsColumn.FixedWidth = true;
            this.colFilterType.Visible = true;
            this.colFilterType.VisibleIndex = 1;
            // 
            // colMode
            // 
            this.colMode.Caption = "Inclusion";
            this.colMode.FieldName = "Mode";
            this.colMode.Name = "colMode";
            this.colMode.OptionsColumn.FixedWidth = true;
            this.colMode.Visible = true;
            this.colMode.VisibleIndex = 2;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 3;
            this.colText.Width = 100;
            // 
            // bRemoveEntry
            // 
            this.bRemoveEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoveEntry.Location = new System.Drawing.Point(373, 347);
            this.bRemoveEntry.Name = "bRemoveEntry";
            this.bRemoveEntry.Size = new System.Drawing.Size(99, 23);
            this.bRemoveEntry.TabIndex = 8;
            this.bRemoveEntry.Text = "Remove selected";
            this.bRemoveEntry.Click += new System.EventHandler(this.bRemoveEntry_Click);
            // 
            // bAddEntry
            // 
            this.bAddEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddEntry.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.bAddEntry.DropDownControl = this.addEntryPop;
            this.bAddEntry.Location = new System.Drawing.Point(3, 347);
            this.bAddEntry.Name = "bAddEntry";
            this.bAddEntry.Size = new System.Drawing.Size(178, 23);
            this.bAddEntry.TabIndex = 9;
            this.bAddEntry.Text = "Add entry to filter";
            this.bAddEntry.Click += new System.EventHandler(this.bAddItem_Click);
            // 
            // addEntryPop
            // 
            this.addEntryPop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.addEntryPop.Controls.Add(this.bReverse);
            this.addEntryPop.Controls.Add(this.bAddItem);
            this.addEntryPop.Controls.Add(this.addTypeLabel);
            this.addEntryPop.Controls.Add(this.itemListCombo);
            this.addEntryPop.Controls.Add(this.bAddEntryAdvanced);
            this.addEntryPop.Location = new System.Drawing.Point(19, 71);
            this.addEntryPop.Manager = this.barManager_0;
            this.addEntryPop.Name = "addEntryPop";
            this.addEntryPop.Size = new System.Drawing.Size(319, 90);
            this.addEntryPop.TabIndex = 14;
            this.addEntryPop.Visible = false;
            // 
            // bReverse
            // 
            this.bReverse.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.reverse;
            this.bReverse.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bReverse.Location = new System.Drawing.Point(278, 14);
            this.bReverse.Name = "bReverse";
            this.bReverse.Size = new System.Drawing.Size(24, 23);
            this.bReverse.TabIndex = 13;
            this.bReverse.ToolTip = "Enable to sort list by display name";
            // 
            // bAddItem
            // 
            this.bAddItem.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniAdd;
            this.bAddItem.Location = new System.Drawing.Point(173, 47);
            this.bAddItem.Name = "bAddItem";
            this.bAddItem.Size = new System.Drawing.Size(75, 23);
            this.bAddItem.TabIndex = 3;
            this.bAddItem.Text = "Add";
            // 
            // addTypeLabel
            // 
            this.addTypeLabel.Location = new System.Drawing.Point(15, 19);
            this.addTypeLabel.Name = "addTypeLabel";
            this.addTypeLabel.Size = new System.Drawing.Size(49, 13);
            this.addTypeLabel.TabIndex = 2;
            this.addTypeLabel.Text = "Add item :";
            // 
            // itemListCombo
            // 
            this.itemListCombo.Location = new System.Drawing.Point(70, 16);
            this.itemListCombo.MenuManager = this.barManager_0;
            this.itemListCombo.Name = "itemListCombo";
            this.itemListCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.itemListCombo.Properties.Sorted = true;
            this.itemListCombo.Size = new System.Drawing.Size(202, 20);
            this.itemListCombo.TabIndex = 1;
            this.itemListCombo.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.itemListCombo_QueryPopUp);
            this.itemListCombo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.itemListCombo_EditValueChanging);
            // 
            // barManager_0
            // 
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barEditItem1,
            this.barListItem1,
            this.barToolbarsListItem1});
            this.barManager_0.MaxItemId = 4;
            this.barManager_0.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.fastAddItemCombo});
            // 
            // barDockControl_0
            // 
            this.barDockControl_0.CausesValidation = false;
            this.barDockControl_0.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl_0.Location = new System.Drawing.Point(0, 0);
            this.barDockControl_0.Manager = this.barManager_0;
            this.barDockControl_0.Size = new System.Drawing.Size(475, 0);
            // 
            // barDockControl_1
            // 
            this.barDockControl_1.CausesValidation = false;
            this.barDockControl_1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl_1.Location = new System.Drawing.Point(0, 373);
            this.barDockControl_1.Manager = this.barManager_0;
            this.barDockControl_1.Size = new System.Drawing.Size(475, 0);
            // 
            // barDockControl_2
            // 
            this.barDockControl_2.CausesValidation = false;
            this.barDockControl_2.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl_2.Location = new System.Drawing.Point(0, 0);
            this.barDockControl_2.Manager = this.barManager_0;
            this.barDockControl_2.Size = new System.Drawing.Size(0, 373);
            // 
            // barDockControl_3
            // 
            this.barDockControl_3.CausesValidation = false;
            this.barDockControl_3.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl_3.Location = new System.Drawing.Point(475, 0);
            this.barDockControl_3.Manager = this.barManager_0;
            this.barDockControl_3.Size = new System.Drawing.Size(0, 373);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Advanced";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Add an item";
            this.barEditItem1.Edit = this.fastAddItemCombo;
            this.barEditItem1.Id = 1;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // fastAddItemCombo
            // 
            this.fastAddItemCombo.AutoHeight = false;
            this.fastAddItemCombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fastAddItemCombo.Items.AddRange(new object[] {
            "fgrte",
            "gfdg",
            "trterg"});
            this.fastAddItemCombo.Name = "fastAddItemCombo";
            this.fastAddItemCombo.Sorted = true;
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "Test";
            this.barListItem1.Id = 2;
            this.barListItem1.Name = "barListItem1";
            this.barListItem1.Strings.AddRange(new object[] {
            "uytru",
            "yturtyut",
            "ghjfghj",
            "turty",
            "jhgfitj"});
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "test";
            this.barToolbarsListItem1.Id = 3;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // bAddEntryAdvanced
            // 
            this.bAddEntryAdvanced.Location = new System.Drawing.Point(72, 47);
            this.bAddEntryAdvanced.Name = "bAddEntryAdvanced";
            this.bAddEntryAdvanced.Size = new System.Drawing.Size(75, 23);
            this.bAddEntryAdvanced.TabIndex = 0;
            this.bAddEntryAdvanced.Text = "Advanced...";
            this.bAddEntryAdvanced.Click += new System.EventHandler(this.bAddEntryAdvanced_Click);
            // 
            // addEntryMenu
            // 
            this.addEntryMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.addEntryMenu.Manager = this.barManager_0;
            this.addEntryMenu.MinWidth = 300;
            this.addEntryMenu.Name = "addEntryMenu";
            this.addEntryMenu.Popup += new System.EventHandler(this.addEntryMenu_Popup);
            this.addEntryMenu.BeforePopup += new System.ComponentModel.CancelEventHandler(this.addEntryMenu_BeforePopup);
            // 
            // bShowItems
            // 
            this.bShowItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bShowItems.Location = new System.Drawing.Point(187, 347);
            this.bShowItems.Name = "bShowItems";
            this.bShowItems.Size = new System.Drawing.Size(91, 23);
            this.bShowItems.TabIndex = 19;
            this.bShowItems.Text = "Show items";
            this.bShowItems.Click += new System.EventHandler(this.bShowItems_Click);
            // 
            // bClear
            // 
            this.bClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClear.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniDelete;
            this.bClear.Location = new System.Drawing.Point(344, 347);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(23, 23);
            this.bClear.TabIndex = 24;
            this.bClear.ToolTip = "Clear list";
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bExport
            // 
            this.bExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bExport.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniExport;
            this.bExport.Location = new System.Drawing.Point(315, 347);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(23, 23);
            this.bExport.TabIndex = 29;
            this.bExport.ToolTip = "Export";
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // bImport
            // 
            this.bImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bImport.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniImport;
            this.bImport.Location = new System.Drawing.Point(286, 347);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(23, 23);
            this.bImport.TabIndex = 30;
            this.bImport.ToolTip = "Import";
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bExpand
            // 
            this.bExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExpand.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniExpand;
            this.bExpand.Location = new System.Drawing.Point(449, 2);
            this.bExpand.Name = "bExpand";
            this.bExpand.Size = new System.Drawing.Size(23, 23);
            this.bExpand.TabIndex = 35;
            this.bExpand.ToolTip = "Expand";
            this.bExpand.Click += new System.EventHandler(this.bExpand_Click);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
            // 
            // ItemFilterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bExpand);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bShowItems);
            this.Controls.Add(this.addEntryPop);
            this.Controls.Add(this.bAddEntry);
            this.Controls.Add(this.bRemoveEntry);
            this.Controls.Add(this.purchaseOptions);
            this.Controls.Add(this.barDockControl_2);
            this.Controls.Add(this.barDockControl_3);
            this.Controls.Add(this.barDockControl_1);
            this.Controls.Add(this.barDockControl_0);
            this.Name = "ItemFilterUC";
            this.Size = new System.Drawing.Size(475, 373);
            ((System.ComponentModel.ISupportInitialize)(this.purchaseOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEntryPop)).EndInit();
            this.addEntryPop.ResumeLayout(false);
            this.addEntryPop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemListCombo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastAddItemCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEntryMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private GridControl purchaseOptions;
        private GridView gridView2;
        private BindingSource bindingSource;
        private GridColumn colStringType;
        private GridColumn colFilterType;
        private GridColumn colMode;
        private GridColumn colText;
        private SimpleButton bRemoveEntry;
        private DropDownButton bAddEntry;
        private PopupMenu addEntryMenu;
        private BarButtonItem barButtonItem1;
        private BarManager barManager_0;
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarEditItem barEditItem1;
        private RepositoryItemComboBox fastAddItemCombo;
        private BarListItem barListItem1;
        private PopupControlContainer addEntryPop;
        private LabelControl addTypeLabel;
        private ComboBoxEdit itemListCombo;
        private SimpleButton bAddEntryAdvanced;
        private BarToolbarsListItem barToolbarsListItem1;
        private SimpleButton bAddItem;
        private SimpleButton bShowItems;
        private SimpleButton bClear;
        private SimpleButton bImport;
        private SimpleButton bExport;
        private SimpleButton bExpand;
        private CheckButton bReverse;

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}
