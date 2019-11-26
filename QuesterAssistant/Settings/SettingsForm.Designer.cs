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
            this.chkRoleToggleEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.txtRoleToggleString = new DevExpress.XtraEditors.TextEdit();
            this.gctrlCommonSettings = new DevExpress.XtraEditors.GroupControl();
            this.cbxHideMinimize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkHideGameEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.chkPauseBotHotKey = new DevExpress.XtraEditors.CheckEdit();
            this.txtHideGameString = new DevExpress.XtraEditors.TextEdit();
            this.bsrcRoleToggleHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcHideGameHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcHideMode = new System.Windows.Forms.BindingSource();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.bsrcPauseBotHotKey = new System.Windows.Forms.BindingSource();
            this.gctlProfilesStack = new DevExpress.XtraEditors.GroupControl();
            this.btnClearStack = new DevExpress.XtraEditors.SimpleButton();
            this.btnShowStack = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.numPauseDelay = new DevExpress.XtraEditors.SpinEdit();
            this.bsrcPauseBot = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCommonSettings)).BeginInit();
            this.gctrlCommonSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHideMinimize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideGameEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPauseBotHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBotHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlProfilesStack)).BeginInit();
            this.gctlProfilesStack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPauseDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBot)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRoleToggleEnabled
            // 
            this.chkRoleToggleEnabled.Location = new System.Drawing.Point(8, 21);
            this.chkRoleToggleEnabled.Name = "chkRoleToggleEnabled";
            this.chkRoleToggleEnabled.Properties.Caption = "Enable Role Toggle by :";
            this.chkRoleToggleEnabled.Size = new System.Drawing.Size(139, 19);
            this.chkRoleToggleEnabled.TabIndex = 0;
            this.chkRoleToggleEnabled.TabStop = false;
            // 
            // txtRoleToggleString
            // 
            this.txtRoleToggleString.Location = new System.Drawing.Point(262, 21);
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
            this.gctrlCommonSettings.Controls.Add(this.numPauseDelay);
            this.gctrlCommonSettings.Controls.Add(this.cbxHideMinimize);
            this.gctrlCommonSettings.Controls.Add(this.labelControl1);
            this.gctrlCommonSettings.Controls.Add(this.chkHideGameEnabled);
            this.gctrlCommonSettings.Controls.Add(this.chkPauseBotHotKey);
            this.gctrlCommonSettings.Controls.Add(this.chkRoleToggleEnabled);
            this.gctrlCommonSettings.Controls.Add(this.txtHideGameString);
            this.gctrlCommonSettings.Controls.Add(this.txtRoleToggleString);
            this.gctrlCommonSettings.Location = new System.Drawing.Point(0, 0);
            this.gctrlCommonSettings.Name = "gctrlCommonSettings";
            this.gctrlCommonSettings.Padding = new System.Windows.Forms.Padding(3);
            this.gctrlCommonSettings.Size = new System.Drawing.Size(370, 97);
            this.gctrlCommonSettings.TabIndex = 2;
            this.gctrlCommonSettings.Text = "Common";
            // 
            // cbxHideMinimize
            // 
            this.cbxHideMinimize.Location = new System.Drawing.Point(65, 47);
            this.cbxHideMinimize.Name = "cbxHideMinimize";
            this.cbxHideMinimize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHideMinimize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHideMinimize.Size = new System.Drawing.Size(111, 20);
            this.cbxHideMinimize.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(182, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "GameClient by :";
            // 
            // chkHideGameEnabled
            // 
            this.chkHideGameEnabled.Location = new System.Drawing.Point(8, 47);
            this.chkHideGameEnabled.Name = "chkHideGameEnabled";
            this.chkHideGameEnabled.Properties.Caption = "Enable";
            this.chkHideGameEnabled.Size = new System.Drawing.Size(57, 19);
            this.chkHideGameEnabled.TabIndex = 0;
            this.chkHideGameEnabled.TabStop = false;
            // 
            // chkPauseBotHotKey
            // 
            this.chkPauseBotHotKey.Location = new System.Drawing.Point(8, 72);
            this.chkPauseBotHotKey.Name = "chkPauseBotHotKey";
            this.chkPauseBotHotKey.Properties.Caption = "Pause bot while WASD are pressed with                                second(s)";
            this.chkPauseBotHotKey.Size = new System.Drawing.Size(354, 19);
            this.chkPauseBotHotKey.TabIndex = 0;
            this.chkPauseBotHotKey.TabStop = false;
            // 
            // txtHideGameString
            // 
            this.txtHideGameString.Location = new System.Drawing.Point(262, 47);
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
            this.simpleButton1.Location = new System.Drawing.Point(0, 204);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Get Key";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(0, 233);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(370, 20);
            this.textEdit1.TabIndex = 4;
            this.textEdit1.Visible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(81, 204);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Set Key";
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // gctlProfilesStack
            // 
            this.gctlProfilesStack.Controls.Add(this.btnClearStack);
            this.gctlProfilesStack.Controls.Add(this.btnShowStack);
            this.gctlProfilesStack.Location = new System.Drawing.Point(0, 104);
            this.gctlProfilesStack.Name = "gctlProfilesStack";
            this.gctlProfilesStack.Padding = new System.Windows.Forms.Padding(3);
            this.gctlProfilesStack.Size = new System.Drawing.Size(370, 53);
            this.gctlProfilesStack.TabIndex = 6;
            this.gctlProfilesStack.Text = "Profiles Stack";
            // 
            // btnClearStack
            // 
            this.btnClearStack.Location = new System.Drawing.Point(89, 21);
            this.btnClearStack.Name = "btnClearStack";
            this.btnClearStack.Size = new System.Drawing.Size(75, 23);
            this.btnClearStack.TabIndex = 0;
            this.btnClearStack.Text = "Clear";
            this.btnClearStack.Click += new System.EventHandler(this.btnClearStack_Click);
            // 
            // btnShowStack
            // 
            this.btnShowStack.Location = new System.Drawing.Point(8, 21);
            this.btnShowStack.Name = "btnShowStack";
            this.btnShowStack.Size = new System.Drawing.Size(75, 23);
            this.btnShowStack.TabIndex = 0;
            this.btnShowStack.Text = "Show";
            this.btnShowStack.Click += new System.EventHandler(this.btnShowStack_Click);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // numPauseDelay
            // 
            this.numPauseDelay.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPauseDelay.Location = new System.Drawing.Point(262, 72);
            this.numPauseDelay.Name = "numPauseDelay";
            this.numPauseDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numPauseDelay.Properties.IsFloatValue = false;
            this.numPauseDelay.Properties.Mask.EditMask = "N00";
            this.numPauseDelay.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numPauseDelay.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPauseDelay.Size = new System.Drawing.Size(45, 20);
            this.numPauseDelay.TabIndex = 8;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gctlProfilesStack);
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
            ((System.ComponentModel.ISupportInitialize)(this.chkPauseBotHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBotHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctlProfilesStack)).EndInit();
            this.gctlProfilesStack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPauseDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBot)).EndInit();
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
        private DevExpress.XtraEditors.CheckEdit chkPauseBotHotKey;
        private System.Windows.Forms.BindingSource bsrcPauseBotHotKey;
        private DevExpress.XtraEditors.GroupControl gctlProfilesStack;
        private DevExpress.XtraEditors.SimpleButton btnClearStack;
        private DevExpress.XtraEditors.SimpleButton btnShowStack;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.SpinEdit numPauseDelay;
        private System.Windows.Forms.BindingSource bsrcPauseBot;
    }
}