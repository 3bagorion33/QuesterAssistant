namespace QuesterAssistant.Settings
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.chkRoleToggleEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.txtRoleToggleString = new DevExpress.XtraEditors.TextEdit();
            this.gctrlCommonSettings = new DevExpress.XtraEditors.GroupControl();
            this.cbxHideMinimize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkHideGameEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.txtHideGameString = new DevExpress.XtraEditors.TextEdit();
            this.bsrcRoleToggleHotKey = new System.Windows.Forms.BindingSource(this.components);
            this.bsrcHideGameHotKey = new System.Windows.Forms.BindingSource(this.components);
            this.bsrcHideMode = new System.Windows.Forms.BindingSource(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCommonSettings)).BeginInit();
            this.gctrlCommonSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHideMinimize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideGameEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRoleToggleEnabled
            // 
            this.chkRoleToggleEnabled.Location = new System.Drawing.Point(8, 27);
            this.chkRoleToggleEnabled.Name = "chkRoleToggleEnabled";
            this.chkRoleToggleEnabled.Properties.Caption = "Enable Role Toggle by :";
            this.chkRoleToggleEnabled.Size = new System.Drawing.Size(139, 19);
            this.chkRoleToggleEnabled.TabIndex = 0;
            this.chkRoleToggleEnabled.TabStop = false;
            // 
            // txtRoleToggleString
            // 
            this.txtRoleToggleString.Location = new System.Drawing.Point(262, 27);
            this.txtRoleToggleString.Name = "txtRoleToggleString";
            this.txtRoleToggleString.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.txtRoleToggleString.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtRoleToggleString.Properties.ReadOnly = true;
            this.txtRoleToggleString.Size = new System.Drawing.Size(100, 20);
            this.txtRoleToggleString.TabIndex = 1;
            this.txtRoleToggleString.TabStop = false;
            this.txtRoleToggleString.ToolTip = "Click here to bind hotkey";
            this.txtRoleToggleString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoleToggleString_KeyDown);
            // 
            // gctrlCommonSettings
            // 
            this.gctrlCommonSettings.Controls.Add(this.cbxHideMinimize);
            this.gctrlCommonSettings.Controls.Add(this.labelControl1);
            this.gctrlCommonSettings.Controls.Add(this.chkHideGameEnabled);
            this.gctrlCommonSettings.Controls.Add(this.chkRoleToggleEnabled);
            this.gctrlCommonSettings.Controls.Add(this.txtHideGameString);
            this.gctrlCommonSettings.Controls.Add(this.txtRoleToggleString);
            this.gctrlCommonSettings.Location = new System.Drawing.Point(0, 3);
            this.gctrlCommonSettings.Name = "gctrlCommonSettings";
            this.gctrlCommonSettings.Padding = new System.Windows.Forms.Padding(3);
            this.gctrlCommonSettings.Size = new System.Drawing.Size(370, 98);
            this.gctrlCommonSettings.TabIndex = 2;
            this.gctrlCommonSettings.Text = "Common";
            // 
            // cbxHideMinimize
            // 
            this.cbxHideMinimize.Location = new System.Drawing.Point(65, 53);
            this.cbxHideMinimize.Name = "cbxHideMinimize";
            this.cbxHideMinimize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHideMinimize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHideMinimize.Size = new System.Drawing.Size(111, 20);
            this.cbxHideMinimize.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(182, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "GameClient by :";
            // 
            // chkHideGameEnabled
            // 
            this.chkHideGameEnabled.Location = new System.Drawing.Point(8, 53);
            this.chkHideGameEnabled.Name = "chkHideGameEnabled";
            this.chkHideGameEnabled.Properties.Caption = "Enable";
            this.chkHideGameEnabled.Size = new System.Drawing.Size(57, 19);
            this.chkHideGameEnabled.TabIndex = 0;
            this.chkHideGameEnabled.TabStop = false;
            // 
            // txtHideGameString
            // 
            this.txtHideGameString.Location = new System.Drawing.Point(262, 53);
            this.txtHideGameString.Name = "txtHideGameString";
            this.txtHideGameString.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.txtHideGameString.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtHideGameString.Properties.ReadOnly = true;
            this.txtHideGameString.Size = new System.Drawing.Size(100, 20);
            this.txtHideGameString.TabIndex = 1;
            this.txtHideGameString.TabStop = false;
            this.txtHideGameString.ToolTip = "Click here to bind hotkey";
            this.txtHideGameString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoleToggleString_KeyDown);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(0, 107);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Get Key";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(0, 136);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(370, 20);
            this.textEdit1.TabIndex = 4;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(81, 107);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Set Key";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gctrlCommonSettings);
            this.MinimumSize = new System.Drawing.Size(370, 372);
            this.Name = "SettingsForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCommonSettings)).EndInit();
            this.gctrlCommonSettings.ResumeLayout(false);
            this.gctrlCommonSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHideMinimize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideGameEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkRoleToggleEnabled;
        private DevExpress.XtraEditors.TextEdit txtRoleToggleString;
        private DevExpress.XtraEditors.GroupControl gctrlCommonSettings;
        private System.Windows.Forms.BindingSource bsrcRoleToggleHotKey;
        private System.Windows.Forms.BindingSource bsrcHideGameHotKey;
        private DevExpress.XtraEditors.CheckEdit chkHideGameEnabled;
        private DevExpress.XtraEditors.TextEdit txtHideGameString;
        private DevExpress.XtraEditors.ComboBoxEdit cbxHideMinimize;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.BindingSource bsrcHideMode;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}