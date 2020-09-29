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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.purchaseOptions = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStringType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilterType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riFilterTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bRemoveEntry = new DevExpress.XtraEditors.SimpleButton();
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.barDockControl_0 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl_3 = new DevExpress.XtraBars.BarDockControl();
            this.fastAddItemCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bTest = new DevExpress.XtraEditors.SimpleButton();
            this.bClear = new DevExpress.XtraEditors.SimpleButton();
            this.bExport = new DevExpress.XtraEditors.SimpleButton();
            this.bImport = new DevExpress.XtraEditors.SimpleButton();
            this.bExpand = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFilterTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastAddItemCombo)).BeginInit();
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
            this.purchaseOptions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riFilterTextEdit});
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
            this.gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
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
            this.colText.ColumnEdit = this.riFilterTextEdit;
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 3;
            this.colText.Width = 100;
            // 
            // riFilterTextEdit
            // 
            this.riFilterTextEdit.AutoHeight = false;
            this.riFilterTextEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riFilterTextEdit.Name = "riFilterTextEdit";
            this.riFilterTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riFilterTextEdit_ButtonClick);
            // 
            // bRemoveEntry
            // 
            this.bRemoveEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoveEntry.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniCancel;
            this.bRemoveEntry.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.bRemoveEntry.Location = new System.Drawing.Point(402, 347);
            this.bRemoveEntry.Name = "bRemoveEntry";
            this.bRemoveEntry.Size = new System.Drawing.Size(70, 23);
            this.bRemoveEntry.TabIndex = 8;
            this.bRemoveEntry.Text = "Delete";
            this.bRemoveEntry.ToolTip = "Delete selected row";
            this.bRemoveEntry.Click += new System.EventHandler(this.bRemoveEntry_Click);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControl_0);
            this.barManager.DockControls.Add(this.barDockControl_1);
            this.barManager.DockControls.Add(this.barDockControl_2);
            this.barManager.DockControls.Add(this.barDockControl_3);
            this.barManager.Form = this;
            this.barManager.MaxItemId = 4;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.fastAddItemCombo});
            // 
            // barDockControl_0
            // 
            this.barDockControl_0.CausesValidation = false;
            this.barDockControl_0.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl_0.Location = new System.Drawing.Point(0, 0);
            this.barDockControl_0.Manager = this.barManager;
            this.barDockControl_0.Size = new System.Drawing.Size(475, 0);
            // 
            // barDockControl_1
            // 
            this.barDockControl_1.CausesValidation = false;
            this.barDockControl_1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl_1.Location = new System.Drawing.Point(0, 373);
            this.barDockControl_1.Manager = this.barManager;
            this.barDockControl_1.Size = new System.Drawing.Size(475, 0);
            // 
            // barDockControl_2
            // 
            this.barDockControl_2.CausesValidation = false;
            this.barDockControl_2.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl_2.Location = new System.Drawing.Point(0, 0);
            this.barDockControl_2.Manager = this.barManager;
            this.barDockControl_2.Size = new System.Drawing.Size(0, 373);
            // 
            // barDockControl_3
            // 
            this.barDockControl_3.CausesValidation = false;
            this.barDockControl_3.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl_3.Location = new System.Drawing.Point(475, 0);
            this.barDockControl_3.Manager = this.barManager;
            this.barDockControl_3.Size = new System.Drawing.Size(0, 373);
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
            // bTest
            // 
            this.bTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bTest.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniPlay;
            this.bTest.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.bTest.Location = new System.Drawing.Point(244, 347);
            this.bTest.Name = "bTest";
            this.bTest.Size = new System.Drawing.Size(65, 23);
            this.bTest.TabIndex = 19;
            this.bTest.Text = "Test";
            this.bTest.Click += new System.EventHandler(this.bTest_Click);
            // 
            // bClear
            // 
            this.bClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClear.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniDelete;
            this.bClear.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bClear.Location = new System.Drawing.Point(373, 347);
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
            this.bExport.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bExport.Location = new System.Drawing.Point(344, 347);
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
            this.bImport.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bImport.Location = new System.Drawing.Point(315, 347);
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
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniAdd;
            this.btnAddNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAddNew.Location = new System.Drawing.Point(3, 347);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(80, 23);
            this.btnAddNew.TabIndex = 40;
            this.btnAddNew.Text = "Add new";
            this.btnAddNew.ToolTip = "Add a new entry";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // ItemFilterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.bExpand);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bTest);
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
            ((System.ComponentModel.ISupportInitialize)(this.riFilterTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastAddItemCombo)).EndInit();
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
        private BarManager barManager;
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private RepositoryItemComboBox fastAddItemCombo;
        private SimpleButton bTest;
        private SimpleButton bClear;
        private SimpleButton bImport;
        private SimpleButton bExport;
        private SimpleButton bExpand;

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private RepositoryItemButtonEdit riFilterTextEdit;
        private SimpleButton btnAddNew;
    }
}
