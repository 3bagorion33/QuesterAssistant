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
            this.bsrcHotKey = new System.Windows.Forms.BindingSource();
            this.bsrcData = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCommonSettings)).BeginInit();
            this.gctrlCommonSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHotKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcData)).BeginInit();
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
            this.txtRoleToggleString.Location = new System.Drawing.Point(153, 27);
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
            this.gctrlCommonSettings.Controls.Add(this.chkRoleToggleEnabled);
            this.gctrlCommonSettings.Controls.Add(this.txtRoleToggleString);
            this.gctrlCommonSettings.Location = new System.Drawing.Point(11, 11);
            this.gctrlCommonSettings.Name = "gctrlCommonSettings";
            this.gctrlCommonSettings.Padding = new System.Windows.Forms.Padding(3);
            this.gctrlCommonSettings.Size = new System.Drawing.Size(348, 58);
            this.gctrlCommonSettings.TabIndex = 2;
            this.gctrlCommonSettings.Text = "Common";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gctrlCommonSettings);
            this.MinimumSize = new System.Drawing.Size(370, 348);
            this.Name = "SettingsForm";
            this.Size = new System.Drawing.Size(370, 348);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleToggleEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleToggleString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlCommonSettings)).EndInit();
            this.gctrlCommonSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsrcHotKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkRoleToggleEnabled;
        private DevExpress.XtraEditors.TextEdit txtRoleToggleString;
        private DevExpress.XtraEditors.GroupControl gctrlCommonSettings;
        private System.Windows.Forms.BindingSource bsrcHotKey;
        private System.Windows.Forms.BindingSource bsrcData;
    }
}