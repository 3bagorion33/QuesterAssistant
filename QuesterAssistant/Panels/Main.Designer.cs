namespace QuesterAssistant.Panels
{
    partial class Main
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
            this.mainTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.settingsTab = new DevExpress.XtraTab.XtraTabPage();
            this.upgradeTab = new DevExpress.XtraTab.XtraTabPage();
            this.pManagerTab = new DevExpress.XtraTab.XtraTabPage();
            this.pushTab = new DevExpress.XtraTab.XtraTabPage();
            this.aboutTab = new DevExpress.XtraTab.XtraTabPage();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainTabControl)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.sidePanel1.SuspendLayout();
            this.sidePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedTabPage = this.settingsTab;
            this.mainTabControl.Size = new System.Drawing.Size(370, 372);
            this.mainTabControl.TabIndex = 8;
            this.mainTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.settingsTab,
            this.upgradeTab,
            this.pManagerTab,
            this.pushTab,
            this.aboutTab});
            // 
            // settingsTab
            // 
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(368, 347);
            this.settingsTab.Text = "Settings";
            // 
            // upgradeTab
            // 
            this.upgradeTab.Name = "upgradeTab";
            this.upgradeTab.Size = new System.Drawing.Size(368, 347);
            this.upgradeTab.Text = "Upgrade Manager";
            // 
            // pManagerTab
            // 
            this.pManagerTab.Name = "pManagerTab";
            this.pManagerTab.Size = new System.Drawing.Size(368, 347);
            this.pManagerTab.Text = "Powers Manager";
            // 
            // pushTab
            // 
            this.pushTab.Name = "pushTab";
            this.pushTab.Size = new System.Drawing.Size(368, 347);
            this.pushTab.Text = "Push Notify";
            // 
            // aboutTab
            // 
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTab.Size = new System.Drawing.Size(368, 347);
            this.aboutTab.Text = "About";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(88, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTip = "Save settings";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(7, 9);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.ToolTip = "Load settings";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // sidePanel1
            // 
            this.sidePanel1.AllowResize = false;
            this.sidePanel1.AllowSnap = false;
            this.sidePanel1.BorderThickness = 0;
            this.sidePanel1.Controls.Add(this.mainTabControl);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel1.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(370, 372);
            this.sidePanel1.TabIndex = 9;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel2
            // 
            this.sidePanel2.AllowResize = false;
            this.sidePanel2.AllowSnap = false;
            this.sidePanel2.BorderThickness = 0;
            this.sidePanel2.Controls.Add(this.btnLoad);
            this.sidePanel2.Controls.Add(this.btnSave);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel2.Location = new System.Drawing.Point(0, 375);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(370, 41);
            this.sidePanel2.TabIndex = 10;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainTabControl)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl mainTabControl;
        private DevExpress.XtraTab.XtraTabPage pManagerTab;
        private DevExpress.XtraTab.XtraTabPage aboutTab;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTab.XtraTabPage settingsTab;
        private DevExpress.XtraTab.XtraTabPage pushTab;
        private DevExpress.XtraTab.XtraTabPage upgradeTab;
    }
}