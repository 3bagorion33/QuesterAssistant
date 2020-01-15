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
            this.numPauseDelay = new DevExpress.XtraEditors.SpinEdit();
            this.cbxHideMinimize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkHideGameEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.chkPauseBotHotKey = new DevExpress.XtraEditors.CheckEdit();
            this.txtHideGameString = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPauseBotHotKey = new DevExpress.XtraEditors.TextEdit();
            this.bsrcRoleToggleHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcHideGameHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcHideMode = new System.Windows.Forms.BindingSource();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.bsrcPauseBotHotKey = new System.Windows.Forms.BindingSource();
            this.btnClearStack = new DevExpress.XtraEditors.SimpleButton();
            this.btnShowStack = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.bsrcPauseBot = new System.Windows.Forms.BindingSource();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabCommon = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabProfilesStack = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabPatches = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numReadyTasksCount = new DevExpress.XtraEditors.SpinEdit();
            this.chkProfessionPatch = new DevExpress.XtraEditors.CheckEdit();
            this.chkWayPointPatch = new DevExpress.XtraEditors.CheckEdit();
            this.tabTest = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.bsrcPatches = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPauseDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHideMinimize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideGameEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPauseBotHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPauseBotHotKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBotHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabCommon.SuspendLayout();
            this.tabProfilesStack.SuspendLayout();
            this.tabPatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadyTasksCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProfessionPatch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWayPointPatch.Properties)).BeginInit();
            this.tabTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPatches)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRoleToggleEnabled
            // 
            this.chkRoleToggleEnabled.Location = new System.Drawing.Point(6, 6);
            this.chkRoleToggleEnabled.Name = "chkRoleToggleEnabled";
            this.chkRoleToggleEnabled.Properties.Caption = "Enable Role Toggle by :";
            this.chkRoleToggleEnabled.Size = new System.Drawing.Size(139, 19);
            this.chkRoleToggleEnabled.TabIndex = 0;
            this.chkRoleToggleEnabled.TabStop = false;
            // 
            // txtRoleToggleString
            // 
            this.txtRoleToggleString.Location = new System.Drawing.Point(264, 6);
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
            // numPauseDelay
            // 
            this.numPauseDelay.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPauseDelay.Location = new System.Drawing.Point(319, 58);
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
            // cbxHideMinimize
            // 
            this.cbxHideMinimize.Location = new System.Drawing.Point(67, 32);
            this.cbxHideMinimize.Name = "cbxHideMinimize";
            this.cbxHideMinimize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHideMinimize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHideMinimize.Size = new System.Drawing.Size(111, 20);
            this.cbxHideMinimize.TabIndex = 3;
            // 
            // chkHideGameEnabled
            // 
            this.chkHideGameEnabled.Location = new System.Drawing.Point(6, 32);
            this.chkHideGameEnabled.Name = "chkHideGameEnabled";
            this.chkHideGameEnabled.Properties.Caption = "Enable";
            this.chkHideGameEnabled.Size = new System.Drawing.Size(57, 19);
            this.chkHideGameEnabled.TabIndex = 0;
            this.chkHideGameEnabled.TabStop = false;
            // 
            // chkPauseBotHotKey
            // 
            this.chkPauseBotHotKey.Location = new System.Drawing.Point(6, 58);
            this.chkPauseBotHotKey.Name = "chkPauseBotHotKey";
            this.chkPauseBotHotKey.Properties.Caption = "Pause bot while W is pressed, second(s)";
            this.chkPauseBotHotKey.Size = new System.Drawing.Size(220, 19);
            this.chkPauseBotHotKey.TabIndex = 0;
            this.chkPauseBotHotKey.TabStop = false;
            // 
            // txtHideGameString
            // 
            this.txtHideGameString.Location = new System.Drawing.Point(264, 32);
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(237, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(103, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "are pressed with, sec";
            this.labelControl1.Visible = false;
            // 
            // txtPauseBotHotKey
            // 
            this.txtPauseBotHotKey.Location = new System.Drawing.Point(165, 6);
            this.txtPauseBotHotKey.Name = "txtPauseBotHotKey";
            this.txtPauseBotHotKey.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.txtPauseBotHotKey.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtPauseBotHotKey.Properties.ReadOnly = true;
            this.txtPauseBotHotKey.Size = new System.Drawing.Size(66, 20);
            this.txtPauseBotHotKey.TabIndex = 1;
            this.txtPauseBotHotKey.TabStop = false;
            this.txtPauseBotHotKey.ToolTip = "Click here to bind hotkey";
            this.txtPauseBotHotKey.Visible = false;
            this.txtPauseBotHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPauseBotHotKey_KeyDown);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(3, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Get Key";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(3, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(364, 20);
            this.textEdit1.TabIndex = 4;
            this.textEdit1.Visible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(84, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Set Key";
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnClearStack
            // 
            this.btnClearStack.Location = new System.Drawing.Point(86, 6);
            this.btnClearStack.Name = "btnClearStack";
            this.btnClearStack.Size = new System.Drawing.Size(75, 23);
            this.btnClearStack.TabIndex = 0;
            this.btnClearStack.Text = "Clear";
            this.btnClearStack.Click += new System.EventHandler(this.btnClearStack_Click);
            // 
            // btnShowStack
            // 
            this.btnShowStack.Location = new System.Drawing.Point(5, 6);
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
            // tabPane1
            // 
            this.tabPane1.AllowCollapse = DevExpress.Utils.DefaultBoolean.Default;
            this.tabPane1.Controls.Add(this.tabCommon);
            this.tabPane1.Controls.Add(this.tabProfilesStack);
            this.tabPane1.Controls.Add(this.tabPatches);
            this.tabPane1.Controls.Add(this.tabTest);
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabCommon,
            this.tabProfilesStack,
            this.tabPatches,
            this.tabTest});
            this.tabPane1.RegularSize = new System.Drawing.Size(370, 372);
            this.tabPane1.SelectedPage = this.tabCommon;
            this.tabPane1.Size = new System.Drawing.Size(370, 372);
            this.tabPane1.TabIndex = 7;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabCommon
            // 
            this.tabCommon.Caption = "Common";
            this.tabCommon.Controls.Add(this.numPauseDelay);
            this.tabCommon.Controls.Add(this.chkRoleToggleEnabled);
            this.tabCommon.Controls.Add(this.cbxHideMinimize);
            this.tabCommon.Controls.Add(this.txtRoleToggleString);
            this.tabCommon.Controls.Add(this.chkHideGameEnabled);
            this.tabCommon.Controls.Add(this.txtHideGameString);
            this.tabCommon.Controls.Add(this.chkPauseBotHotKey);
            this.tabCommon.Margin = new System.Windows.Forms.Padding(0);
            this.tabCommon.Name = "tabCommon";
            this.tabCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommon.Size = new System.Drawing.Size(370, 345);
            // 
            // tabProfilesStack
            // 
            this.tabProfilesStack.Caption = "Profiles Stack";
            this.tabProfilesStack.Controls.Add(this.btnClearStack);
            this.tabProfilesStack.Controls.Add(this.btnShowStack);
            this.tabProfilesStack.Margin = new System.Windows.Forms.Padding(0);
            this.tabProfilesStack.Name = "tabProfilesStack";
            this.tabProfilesStack.Padding = new System.Windows.Forms.Padding(3);
            this.tabProfilesStack.Size = new System.Drawing.Size(370, 345);
            // 
            // tabPatches
            // 
            this.tabPatches.Caption = "Astral Patches";
            this.tabPatches.Controls.Add(this.labelControl2);
            this.tabPatches.Controls.Add(this.numReadyTasksCount);
            this.tabPatches.Controls.Add(this.chkProfessionPatch);
            this.tabPatches.Controls.Add(this.chkWayPointPatch);
            this.tabPatches.Margin = new System.Windows.Forms.Padding(0);
            this.tabPatches.Name = "tabPatches";
            this.tabPatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatches.Size = new System.Drawing.Size(370, 345);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(240, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Free tasks slots:";
            // 
            // numReadyTasksCount
            // 
            this.numReadyTasksCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReadyTasksCount.Location = new System.Drawing.Point(325, 31);
            this.numReadyTasksCount.Name = "numReadyTasksCount";
            this.numReadyTasksCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numReadyTasksCount.Properties.Mask.EditMask = "N00";
            this.numReadyTasksCount.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numReadyTasksCount.Size = new System.Drawing.Size(39, 20);
            this.numReadyTasksCount.TabIndex = 2;
            this.numReadyTasksCount.ToolTip = "Try to keep free slots in Delivery Box";
            // 
            // chkProfessionPatch
            // 
            this.chkProfessionPatch.Location = new System.Drawing.Point(6, 31);
            this.chkProfessionPatch.Name = "chkProfessionPatch";
            this.chkProfessionPatch.Properties.Caption = "Enable profession patch";
            this.chkProfessionPatch.Size = new System.Drawing.Size(137, 19);
            this.chkProfessionPatch.TabIndex = 1;
            this.chkProfessionPatch.TabStop = false;
            this.chkProfessionPatch.ToolTip = "Need relaunch";
            // 
            // chkWayPointPatch
            // 
            this.chkWayPointPatch.Location = new System.Drawing.Point(6, 6);
            this.chkWayPointPatch.Name = "chkWayPointPatch";
            this.chkWayPointPatch.Properties.Caption = "Enable waypoint filter";
            this.chkWayPointPatch.Size = new System.Drawing.Size(128, 19);
            this.chkWayPointPatch.TabIndex = 1;
            this.chkWayPointPatch.TabStop = false;
            this.chkWayPointPatch.ToolTip = "Probably need relaunsh";
            // 
            // tabTest
            // 
            this.tabTest.Caption = "Test";
            this.tabTest.Controls.Add(this.labelControl1);
            this.tabTest.Controls.Add(this.txtPauseBotHotKey);
            this.tabTest.Controls.Add(this.simpleButton2);
            this.tabTest.Controls.Add(this.simpleButton1);
            this.tabTest.Controls.Add(this.textEdit1);
            this.tabTest.Name = "tabTest";
            this.tabTest.Size = new System.Drawing.Size(370, 345);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tabPane1);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(370, 372);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "groupControl1";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.MinimumSize = new System.Drawing.Size(370, 372);
            this.Name = "SettingsForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPauseDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHideMinimize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideGameEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPauseBotHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideGameString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPauseBotHotKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcRoleToggleHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideGameHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHideMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBotHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPauseBot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabCommon.ResumeLayout(false);
            this.tabProfilesStack.ResumeLayout(false);
            this.tabPatches.ResumeLayout(false);
            this.tabPatches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadyTasksCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProfessionPatch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWayPointPatch.Properties)).EndInit();
            this.tabTest.ResumeLayout(false);
            this.tabTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsrcPatches)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkRoleToggleEnabled;
        private DevExpress.XtraEditors.TextEdit txtRoleToggleString;
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
        private DevExpress.XtraEditors.SimpleButton btnClearStack;
        private DevExpress.XtraEditors.SimpleButton btnShowStack;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.SpinEdit numPauseDelay;
        private System.Windows.Forms.BindingSource bsrcPauseBot;
        private DevExpress.XtraEditors.TextEdit txtPauseBotHotKey;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabCommon;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabProfilesStack;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPatches;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabTest;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chkWayPointPatch;
        private System.Windows.Forms.BindingSource bsrcPatches;
        private DevExpress.XtraEditors.CheckEdit chkProfessionPatch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit numReadyTasksCount;
    }
}