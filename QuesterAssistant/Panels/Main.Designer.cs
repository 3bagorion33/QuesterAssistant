﻿namespace QuesterAssistant.Panels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPowersSwitcher = new DevExpress.XtraTab.XtraTabPage();
            this.groupControlPowersList = new DevExpress.XtraEditors.GroupControl();
            this.groupControlCharInfo = new DevExpress.XtraEditors.GroupControl();
            this.labelCharacterName = new DevExpress.XtraEditors.LabelControl();
            this.labelCharacterClass = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabAbout = new DevExpress.XtraTab.XtraTabPage();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelAuthor = new DevExpress.XtraEditors.LabelControl();
            this.labelVersion = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPowersSwitcher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPowersList)).BeginInit();
            this.groupControlPowersList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCharInfo)).BeginInit();
            this.groupControlCharInfo.SuspendLayout();
            this.xtraTabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPowersSwitcher;
            this.xtraTabControl1.Size = new System.Drawing.Size(370, 416);
            this.xtraTabControl1.TabIndex = 8;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPowersSwitcher,
            this.xtraTabAbout});
            // 
            // xtraTabPowersSwitcher
            // 
            this.xtraTabPowersSwitcher.Controls.Add(this.groupControlPowersList);
            this.xtraTabPowersSwitcher.Controls.Add(this.groupControlCharInfo);
            this.xtraTabPowersSwitcher.Name = "xtraTabPowersSwitcher";
            this.xtraTabPowersSwitcher.Size = new System.Drawing.Size(368, 391);
            this.xtraTabPowersSwitcher.Text = "Powers Switcher";
            // 
            // groupControlPowersList
            // 
            this.groupControlPowersList.AutoSize = true;
            this.groupControlPowersList.Controls.Add(this.gridControl1);
            this.groupControlPowersList.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControlPowersList.Location = new System.Drawing.Point(10, 51);
            this.groupControlPowersList.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.groupControlPowersList.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControlPowersList.Name = "groupControlPowersList";
            this.groupControlPowersList.Size = new System.Drawing.Size(348, 329);
            this.groupControlPowersList.TabIndex = 1;
            this.groupControlPowersList.Text = "Powers List";
            // 
            // groupControlCharInfo
            // 
            this.groupControlCharInfo.AutoSize = true;
            this.groupControlCharInfo.Controls.Add(this.labelCharacterName);
            this.groupControlCharInfo.Controls.Add(this.labelCharacterClass);
            this.groupControlCharInfo.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControlCharInfo.Location = new System.Drawing.Point(10, 3);
            this.groupControlCharInfo.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.groupControlCharInfo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControlCharInfo.Name = "groupControlCharInfo";
            this.groupControlCharInfo.Size = new System.Drawing.Size(348, 42);
            this.groupControlCharInfo.TabIndex = 1;
            this.groupControlCharInfo.Text = "Current Character Info";
            // 
            // labelCharacterName
            // 
            this.labelCharacterName.Location = new System.Drawing.Point(9, 20);
            this.labelCharacterName.Name = "labelCharacterName";
            this.labelCharacterName.Padding = new System.Windows.Forms.Padding(3);
            this.labelCharacterName.Size = new System.Drawing.Size(40, 19);
            this.labelCharacterName.TabIndex = 0;
            this.labelCharacterName.Text = "Name: ";
            // 
            // labelCharacterClass
            // 
            this.labelCharacterClass.Location = new System.Drawing.Point(163, 20);
            this.labelCharacterClass.Name = "labelCharacterClass";
            this.labelCharacterClass.Padding = new System.Windows.Forms.Padding(3);
            this.labelCharacterClass.Size = new System.Drawing.Size(38, 19);
            this.labelCharacterClass.TabIndex = 0;
            this.labelCharacterClass.Text = "Class: ";
            // 
            // xtraTabAbout
            // 
            this.xtraTabAbout.Controls.Add(this.textBoxDesc);
            this.xtraTabAbout.Controls.Add(this.labelAuthor);
            this.xtraTabAbout.Controls.Add(this.labelVersion);
            this.xtraTabAbout.Name = "xtraTabAbout";
            this.xtraTabAbout.Size = new System.Drawing.Size(368, 391);
            this.xtraTabAbout.Text = "About";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDesc.Location = new System.Drawing.Point(3, 28);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ReadOnly = true;
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDesc.Size = new System.Drawing.Size(362, 360);
            this.textBoxDesc.TabIndex = 4;
            this.textBoxDesc.Text = resources.GetString("textBoxDesc.Text");
            // 
            // labelAuthor
            // 
            this.labelAuthor.Location = new System.Drawing.Point(306, 3);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Padding = new System.Windows.Forms.Padding(3);
            this.labelAuthor.Size = new System.Drawing.Size(59, 19);
            this.labelAuthor.TabIndex = 6;
            this.labelAuthor.Text = "by Orion33";
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(3, 3);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Padding = new System.Windows.Forms.Padding(3);
            this.labelVersion.Size = new System.Drawing.Size(31, 19);
            this.labelVersion.TabIndex = 5;
            this.labelVersion.Text = "v 1.0";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(11, 29);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(326, 200);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPowersSwitcher.ResumeLayout(false);
            this.xtraTabPowersSwitcher.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPowersList)).EndInit();
            this.groupControlPowersList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCharInfo)).EndInit();
            this.groupControlCharInfo.ResumeLayout(false);
            this.groupControlCharInfo.PerformLayout();
            this.xtraTabAbout.ResumeLayout(false);
            this.xtraTabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPowersSwitcher;
        private DevExpress.XtraEditors.GroupControl groupControlPowersList;
        private DevExpress.XtraEditors.GroupControl groupControlCharInfo;
        private DevExpress.XtraEditors.LabelControl labelCharacterName;
        private DevExpress.XtraEditors.LabelControl labelCharacterClass;
        private DevExpress.XtraTab.XtraTabPage xtraTabAbout;
        private System.Windows.Forms.TextBox textBoxDesc;
        private DevExpress.XtraEditors.LabelControl labelAuthor;
        private DevExpress.XtraEditors.LabelControl labelVersion;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}