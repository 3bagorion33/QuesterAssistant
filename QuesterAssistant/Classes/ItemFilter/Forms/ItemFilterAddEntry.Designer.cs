using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    partial class ItemFilterAddEntry
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
            this.filterType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.filterText = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textName = new DevExpress.XtraEditors.LabelControl();
            this.filterMode = new DevExpress.XtraEditors.RadioGroup();
            this.filterStringType = new DevExpress.XtraEditors.RadioGroup();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.filterTypeInfos = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.bReverse = new DevExpress.XtraEditors.CheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.filterType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterStringType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // filterType
            // 
            this.filterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterType.Location = new System.Drawing.Point(109, 29);
            this.filterType.Name = "filterType";
            this.filterType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.filterType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.filterType.Size = new System.Drawing.Size(357, 20);
            this.filterType.TabIndex = 0;
            this.filterType.SelectedIndexChanged += new System.EventHandler(this.filterType_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(40, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Filter type :";
            // 
            // filterText
            // 
            this.filterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterText.Location = new System.Drawing.Point(109, 87);
            this.filterText.Name = "filterText";
            this.filterText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.filterText.Properties.Sorted = true;
            this.filterText.Size = new System.Drawing.Size(327, 20);
            this.filterText.TabIndex = 2;
            this.filterText.SelectedIndexChanged += new System.EventHandler(this.filterText_SelectedIndexChanged);
            this.filterText.QueryCloseUp += new System.ComponentModel.CancelEventHandler(this.filterText_QueryCloseUp);
            this.filterText.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.filterText_QueryPopUp);
            this.filterText.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.filterText_EditValueChanging);
            // 
            // textName
            // 
            this.textName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textName.Appearance.Options.UseTextOptions = true;
            this.textName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.textName.Location = new System.Drawing.Point(12, 90);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(84, 13);
            this.textName.TabIndex = 3;
            this.textName.Text = "Text :";
            // 
            // filterMode
            // 
            this.filterMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterMode.Location = new System.Drawing.Point(109, 147);
            this.filterMode.Name = "filterMode";
            this.filterMode.Size = new System.Drawing.Size(357, 25);
            this.filterMode.TabIndex = 4;
            // 
            // filterStringType
            // 
            this.filterStringType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterStringType.Location = new System.Drawing.Point(109, 117);
            this.filterStringType.Name = "filterStringType";
            this.filterStringType.Size = new System.Drawing.Size(327, 25);
            this.filterStringType.TabIndex = 5;
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.Location = new System.Drawing.Point(12, 190);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(454, 23);
            this.bAdd.TabIndex = 6;
            this.bAdd.Text = "Add";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(42, 123);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Text type :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(63, 153);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Mode :";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Location = new System.Drawing.Point(121, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(122, 147);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(50, 20);
            // 
            // filterTypeInfos
            // 
            this.filterTypeInfos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTypeInfos.Appearance.Options.UseTextOptions = true;
            this.filterTypeInfos.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.filterTypeInfos.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.filterTypeInfos.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.filterTypeInfos.Location = new System.Drawing.Point(24, 51);
            this.filterTypeInfos.Name = "filterTypeInfos";
            this.filterTypeInfos.Size = new System.Drawing.Size(431, 30);
            this.filterTypeInfos.TabIndex = 9;
            this.filterTypeInfos.Text = "-";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(442, 117);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(24, 25);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.ToolTip = "Simple : you can use * char at the start or the end of the text\r\n\r\nRegex : for ad" +
    "vanced users";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // bReverse
            // 
            this.bReverse.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bReverse.Location = new System.Drawing.Point(442, 85);
            this.bReverse.Name = "bReverse";
            this.bReverse.Size = new System.Drawing.Size(24, 23);
            this.bReverse.TabIndex = 12;
            this.bReverse.ToolTip = "Enable to sort list by display name";
            this.bReverse.CheckedChanged += new System.EventHandler(this.bReverse_CheckedChanged);
            // 
            // ItemFilterAddEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 225);
            this.Controls.Add(this.bReverse);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.filterTypeInfos);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.filterMode);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.filterStringType);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.filterText);
            this.Controls.Add(this.filterType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(494, 264);
            this.MinimumSize = new System.Drawing.Size(494, 259);
            this.Name = "ItemFilterAddEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add entry to item filter - Advanced";
            this.Load += new System.EventHandler(this.ItemFilterAddEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filterType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterStringType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ComboBoxEdit filterType;
        private LabelControl labelControl1;
        private ComboBoxEdit filterText;
        private LabelControl textName;
        private RadioGroup filterMode;
        private RadioGroup filterStringType;
        private SimpleButton bAdd;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private LayoutControlItem layoutControlItem2;
        private LabelControl filterTypeInfos;
        private SimpleButton simpleButton1;
        private CheckButton bReverse;

        #endregion
    }
}