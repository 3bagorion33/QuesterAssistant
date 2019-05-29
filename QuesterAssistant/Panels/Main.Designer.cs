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
            this.mainTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.settingsTab = new DevExpress.XtraTab.XtraTabPage();
            this.upgradeTab = new DevExpress.XtraTab.XtraTabPage();
            this.pManagerTab = new DevExpress.XtraTab.XtraTabPage();
            this.pushTab = new DevExpress.XtraTab.XtraTabPage();
            this.aboutTab = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.hlkQAForumThread = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelAuthor = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            ((System.ComponentModel.ISupportInitialize)(this.mainTabControl)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.aboutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.aboutTab.Controls.Add(this.groupControl3);
            this.aboutTab.Controls.Add(this.groupControl2);
            this.aboutTab.Controls.Add(this.groupControl1);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTab.Size = new System.Drawing.Size(368, 347);
            this.aboutTab.Text = "About";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.pictureBox1);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Location = new System.Drawing.Point(6, 164);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Padding = new System.Windows.Forms.Padding(4);
            this.groupControl3.Size = new System.Drawing.Size(356, 177);
            this.groupControl3.TabIndex = 11;
            this.groupControl3.Text = "Donate";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::QuesterAssistant.Properties.Resources.Donate;
            this.pictureBox1.Location = new System.Drawing.Point(100, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox1.Size = new System.Drawing.Size(156, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(62, 28);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Padding = new System.Windows.Forms.Padding(3);
            this.labelControl4.Size = new System.Drawing.Size(228, 19);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Do you like it? You can gift us a cup of beer =)";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.hlkQAForumThread);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Location = new System.Drawing.Point(6, 57);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Padding = new System.Windows.Forms.Padding(4);
            this.groupControl2.Size = new System.Drawing.Size(356, 100);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "Info";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.labelControl2.Size = new System.Drawing.Size(318, 19);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "This plugin adds some functionality and actions into Quester role.";
            // 
            // hlkQAForumThread
            // 
            this.hlkQAForumThread.Location = new System.Drawing.Point(68, 53);
            this.hlkQAForumThread.Name = "hlkQAForumThread";
            this.hlkQAForumThread.Padding = new System.Windows.Forms.Padding(3);
            this.hlkQAForumThread.Size = new System.Drawing.Size(204, 19);
            this.hlkQAForumThread.TabIndex = 7;
            this.hlkQAForumThread.Text = "Quester Assistant thread on Astral forum";
            this.hlkQAForumThread.Click += new System.EventHandler(this.hlkQAForumThread_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(3);
            this.labelControl3.Size = new System.Drawing.Size(239, 32);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Please visit                                                                     " +
    " \r\nfor more info, support and to check for updates.";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblVersion);
            this.groupControl1.Controls.Add(this.labelAuthor);
            this.groupControl1.Location = new System.Drawing.Point(6, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(4);
            this.groupControl1.Size = new System.Drawing.Size(356, 45);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Version";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(9, 19);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Padding = new System.Windows.Forms.Padding(3);
            this.lblVersion.Size = new System.Drawing.Size(31, 19);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "v 1.1";
            // 
            // labelAuthor
            // 
            this.labelAuthor.Location = new System.Drawing.Point(212, 19);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Padding = new System.Windows.Forms.Padding(3);
            this.labelAuthor.Size = new System.Drawing.Size(135, 19);
            this.labelAuthor.TabIndex = 6;
            this.labelAuthor.Text = "by DartKotik && MichaelProg";
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
            this.aboutTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl mainTabControl;
        private DevExpress.XtraTab.XtraTabPage pManagerTab;
        private DevExpress.XtraTab.XtraTabPage aboutTab;
        private DevExpress.XtraEditors.LabelControl labelAuthor;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlkQAForumThread;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraTab.XtraTabPage settingsTab;
        private DevExpress.XtraTab.XtraTabPage pushTab;
        private DevExpress.XtraTab.XtraTabPage upgradeTab;
    }
}