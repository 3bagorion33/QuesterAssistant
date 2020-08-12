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
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.components = new Container();
            GridLevelNode gridLevelNode = new GridLevelNode();
            GridLevelNode gridLevelNode2 = new GridLevelNode();
            this.purchaseOptions = new GridControl();
            this.bindingSource_0 = new BindingSource(this.components);
            this.gridView2 = new GridView();
            this.colStringType = new GridColumn();
            this.colFilterType = new GridColumn();
            this.colMode = new GridColumn();
            this.colText = new GridColumn();
            this.bRemoveEntry = new SimpleButton();
            this.bAddEntry = new DropDownButton();
            this.addEntryPop = new PopupControlContainer(this.components);
            this.bReverse = new CheckButton();
            this.bAddItem = new SimpleButton();
            this.addTypeLabel = new LabelControl();
            this.itemListCombo = new ComboBoxEdit();
            this.barManager_0 = new BarManager(this.components);
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.barButtonItem1 = new BarButtonItem();
            this.barEditItem1 = new BarEditItem();
            this.fastAddItemCombo = new RepositoryItemComboBox();
            this.barListItem1 = new BarListItem();
            this.barToolbarsListItem1 = new BarToolbarsListItem();
            this.bAddEntryAdvanced = new SimpleButton();
            this.addEntryMenu = new PopupMenu(this.components);
            this.bShowItems = new SimpleButton();
            this.bClear = new SimpleButton();
            this.bExport = new SimpleButton();
            this.bImport = new SimpleButton();
            this.bExpand = new SimpleButton();
            this.purchaseOptions.BeginInit();
            ((ISupportInitialize)this.bindingSource_0).BeginInit();
            this.gridView2.BeginInit();
            this.addEntryPop.BeginInit();
            this.addEntryPop.SuspendLayout();
            ((ISupportInitialize)this.itemListCombo.Properties).BeginInit();
            this.barManager_0.BeginInit();
            ((ISupportInitialize)this.fastAddItemCombo).BeginInit();
            this.addEntryMenu.BeginInit();
            base.SuspendLayout();
            this.purchaseOptions.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.purchaseOptions.Cursor = Cursors.Default;
            this.purchaseOptions.DataSource = this.bindingSource_0;
            gridLevelNode.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.purchaseOptions.LevelTree.Nodes.AddRange(new GridLevelNode[]
            {
                gridLevelNode,
                gridLevelNode2
            });
            this.purchaseOptions.Location = new Point(3, 3);
            this.purchaseOptions.MainView = this.gridView2;
            this.purchaseOptions.Name = "purchaseOptions";
            this.purchaseOptions.Size = new Size(469, 338);
            this.purchaseOptions.TabIndex = 6;
            this.purchaseOptions.UseEmbeddedNavigator = true;
            this.purchaseOptions.ViewCollection.AddRange(new BaseView[]
            {
                this.gridView2
            });
            this.bindingSource_0.DataSource = typeof(ItemFilterEntry);
            this.gridView2.Columns.AddRange(new GridColumn[]
            {
                this.colStringType,
                this.colFilterType,
                this.colMode,
                this.colText
            });
            this.gridView2.GridControl = this.purchaseOptions;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.colStringType.Caption = "Text type";
            this.colStringType.FieldName = "StringType";
            this.colStringType.Name = "colStringType";
            this.colStringType.OptionsColumn.FixedWidth = true;
            this.colStringType.Visible = true;
            this.colStringType.VisibleIndex = 0;
            this.colFilterType.Caption = "Filter type";
            this.colFilterType.FieldName = "Type";
            this.colFilterType.Name = "colFilterType";
            this.colFilterType.OptionsColumn.FixedWidth = true;
            this.colFilterType.Visible = true;
            this.colFilterType.VisibleIndex = 1;
            this.colMode.Caption = "Inclusion";
            this.colMode.FieldName = "Mode";
            this.colMode.Name = "colMode";
            this.colMode.OptionsColumn.FixedWidth = true;
            this.colMode.Visible = true;
            this.colMode.VisibleIndex = 2;
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 3;
            this.colText.Width = 100;
            this.bRemoveEntry.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.bRemoveEntry.Location = new Point(373, 347);
            this.bRemoveEntry.Name = "bRemoveEntry";
            this.bRemoveEntry.Size = new Size(99, 23);
            this.bRemoveEntry.TabIndex = 8;
            this.bRemoveEntry.Text = "Remove selected";
            this.bRemoveEntry.Click += this.bRemoveEntry_Click;
            this.bAddEntry.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.bAddEntry.DropDownArrowStyle = DropDownArrowStyle.Show;
            this.bAddEntry.DropDownControl = this.addEntryPop;
            this.bAddEntry.Location = new Point(3, 347);
            this.bAddEntry.Name = "bAddEntry";
            this.bAddEntry.Size = new Size(178, 23);
            this.bAddEntry.TabIndex = 9;
            this.bAddEntry.Text = "Add entry to filter";
            this.addEntryPop.BorderStyle = 0;
            this.addEntryPop.Controls.Add(this.bReverse);
            this.addEntryPop.Controls.Add(this.bAddItem);
            this.addEntryPop.Controls.Add(this.addTypeLabel);
            this.addEntryPop.Controls.Add(this.itemListCombo);
            this.addEntryPop.Controls.Add(this.bAddEntryAdvanced);
            this.addEntryPop.Location = new Point(19, 71);
            this.addEntryPop.Manager = this.barManager_0;
            this.addEntryPop.Name = "addEntryPop";
            this.addEntryPop.Size = new Size(319, 90);
            this.addEntryPop.TabIndex = 14;
            this.addEntryPop.Visible = false;
            //this.bReverse.Image = Class2.reverse;
            this.bReverse.ImageLocation = ImageLocation.MiddleCenter;
            this.bReverse.Location = new Point(278, 14);
            this.bReverse.Name = "bReverse";
            this.bReverse.Size = new Size(24, 23);
            this.bReverse.TabIndex = 13;
            this.bReverse.ToolTip = "Enable to sort list by display name";
            //this.bAddItem.Image = Class2.miniAdd;
            this.bAddItem.Location = new Point(173, 47);
            this.bAddItem.Name = "bAddItem";
            this.bAddItem.Size = new Size(75, 23);
            this.bAddItem.TabIndex = 3;
            this.bAddItem.Text = "Add";
            this.bAddItem.Click += this.bAddItem_Click;
            this.addTypeLabel.Location = new Point(15, 19);
            this.addTypeLabel.Name = "addTypeLabel";
            this.addTypeLabel.Size = new Size(49, 13);
            this.addTypeLabel.TabIndex = 2;
            this.addTypeLabel.Text = "Add item :";
            this.itemListCombo.Location = new Point(70, 16);
            this.itemListCombo.MenuManager = this.barManager_0;
            this.itemListCombo.Name = "itemListCombo";
            this.itemListCombo.Properties.Buttons.AddRange(new EditorButton[]
            {
                new EditorButton(ButtonPredefines.Combo)
            });
            this.itemListCombo.Properties.Sorted = true;
            this.itemListCombo.Size = new Size(202, 20);
            this.itemListCombo.TabIndex = 1;
            this.itemListCombo.QueryPopUp += this.itemListCombo_QueryPopUp;
            this.itemListCombo.EditValueChanging += this.itemListCombo_EditValueChanging;
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[]
            {
                this.barButtonItem1,
                this.barEditItem1,
                this.barListItem1,
                this.barToolbarsListItem1
            });
            this.barManager_0.MaxItemId = 4;
            this.barManager_0.RepositoryItems.AddRange(new RepositoryItem[]
            {
                this.fastAddItemCombo
            });
            this.barDockControl_0.CausesValidation = false;
            this.barDockControl_0.Dock = DockStyle.Top;
            this.barDockControl_0.Location = new Point(0, 0);
            this.barDockControl_0.Manager = this.barManager_0;
            this.barDockControl_0.Size = new Size(475, 0);
            this.barDockControl_1.CausesValidation = false;
            this.barDockControl_1.Dock = DockStyle.Bottom;
            this.barDockControl_1.Location = new Point(0, 373);
            this.barDockControl_1.Manager = this.barManager_0;
            this.barDockControl_1.Size = new Size(475, 0);
            this.barDockControl_2.CausesValidation = false;
            this.barDockControl_2.Dock = DockStyle.Left;
            this.barDockControl_2.Location = new Point(0, 0);
            this.barDockControl_2.Manager = this.barManager_0;
            this.barDockControl_2.Size = new Size(0, 373);
            this.barDockControl_3.CausesValidation = false;
            this.barDockControl_3.Dock = DockStyle.Right;
            this.barDockControl_3.Location = new Point(475, 0);
            this.barDockControl_3.Manager = this.barManager_0;
            this.barDockControl_3.Size = new Size(0, 373);
            this.barButtonItem1.Caption = "Advanced";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new ItemClickEventHandler(this.barButtonItem1_ItemClick);
            this.barEditItem1.Caption = "Add an item";
            this.barEditItem1.Edit = this.fastAddItemCombo;
            this.barEditItem1.Id = 1;
            this.barEditItem1.Name = "barEditItem1";
            this.fastAddItemCombo.AutoHeight = false;
            this.fastAddItemCombo.Buttons.AddRange(new EditorButton[]
            {
                new EditorButton(ButtonPredefines.Combo), 
            });
            this.fastAddItemCombo.Items.AddRange(new object[]
            {
                "fgrte",
                "gfdg",
                "trterg"
            });
            this.fastAddItemCombo.Name = "fastAddItemCombo";
            this.fastAddItemCombo.Sorted = true;
            this.barListItem1.Caption = "Test";
            this.barListItem1.Id = 2;
            this.barListItem1.Name = "barListItem1";
            this.barListItem1.Strings.AddRange(new object[]
            {
                "uytru",
                "yturtyut",
                "ghjfghj",
                "turty",
                "jhgfitj"
            });
            this.barToolbarsListItem1.Caption = "test";
            this.barToolbarsListItem1.Id = 3;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            this.bAddEntryAdvanced.Location = new Point(72, 47);
            this.bAddEntryAdvanced.Name = "bAddEntryAdvanced";
            this.bAddEntryAdvanced.Size = new Size(75, 23);
            this.bAddEntryAdvanced.TabIndex = 0;
            this.bAddEntryAdvanced.Text = "Advanced...";
            this.bAddEntryAdvanced.Click += this.bAddEntryAdvanced_Click;
            this.addEntryMenu.LinksPersistInfo.AddRange(new LinkPersistInfo[]
            {
                new LinkPersistInfo(this.barButtonItem1)
            });
            this.addEntryMenu.Manager = this.barManager_0;
            this.addEntryMenu.MinWidth = 300;
            this.addEntryMenu.Name = "addEntryMenu";
            this.addEntryMenu.Popup += this.addEntryMenu_Popup;
            this.addEntryMenu.BeforePopup += this.addEntryMenu_BeforePopup;
            this.bShowItems.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.bShowItems.Location = new Point(187, 347);
            this.bShowItems.Name = "bShowItems";
            this.bShowItems.Size = new Size(91, 23);
            this.bShowItems.TabIndex = 19;
            this.bShowItems.Text = "Show items";
            this.bShowItems.Click += this.bShowItems_Click;
            this.bClear.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            //this.bClear.Image = Class2.miniDelete;
            this.bClear.Location = new Point(344, 347);
            this.bClear.Name = "bClear";
            this.bClear.Size = new Size(23, 23);
            this.bClear.TabIndex = 24;
            this.bClear.ToolTip = "Clear list";
            this.bClear.Click += this.bClear_Click;
            this.bExport.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            //this.bExport.Image = Class2.miniExport;
            this.bExport.Location = new Point(315, 347);
            this.bExport.Name = "bExport";
            this.bExport.Size = new Size(23, 23);
            this.bExport.TabIndex = 29;
            this.bExport.ToolTip = "Export";
            this.bExport.Click += this.bExport_Click;
            this.bImport.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            //this.bImport.Image = Class2.miniImport;
            this.bImport.Location = new Point(286, 347);
            this.bImport.Name = "bImport";
            this.bImport.Size = new Size(23, 23);
            this.bImport.TabIndex = 30;
            this.bImport.ToolTip = "Import";
            this.bImport.Click += this.bImport_Click;
            this.bExpand.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //this.bExpand.Image = Class2.miniExpand;
            this.bExpand.Location = new Point(449, 2);
            this.bExpand.Name = "bExpand";
            this.bExpand.Size = new Size(23, 23);
            this.bExpand.TabIndex = 35;
            this.bExpand.ToolTip = "Expand";
            this.bExpand.Click += this.bExpand_Click;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.bExpand);
            base.Controls.Add(this.bImport);
            base.Controls.Add(this.bExport);
            base.Controls.Add(this.bClear);
            base.Controls.Add(this.bShowItems);
            base.Controls.Add(this.addEntryPop);
            base.Controls.Add(this.bAddEntry);
            base.Controls.Add(this.bRemoveEntry);
            base.Controls.Add(this.purchaseOptions);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "ItemFilterUC";
            base.Size = new Size(475, 373);
            base.Load += this.ItemFilterUC_Load;
            this.purchaseOptions.EndInit();
            ((ISupportInitialize)this.bindingSource_0).EndInit();
            this.gridView2.EndInit();
            this.addEntryPop.EndInit();
            this.addEntryPop.ResumeLayout(false);
            this.addEntryPop.PerformLayout();
            ((ISupportInitialize)this.itemListCombo.Properties).EndInit();
            this.barManager_0.EndInit();
            ((ISupportInitialize)this.fastAddItemCombo).EndInit();
            this.addEntryMenu.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private GridControl purchaseOptions;
        private GridView gridView2;
        private BindingSource bindingSource_0;
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
    }
}
