using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace QuesterAssistant.UIEditors.Forms
{
    partial class GetMissionId
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
            this.treeMissions = new System.Windows.Forms.TreeView();
            this.b_Select = new DevExpress.XtraEditors.SimpleButton();
            this.rgMissionType = new DevExpress.XtraEditors.RadioGroup();
            this.gctrlMissionType = new DevExpress.XtraEditors.GroupControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.b_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.rgSortBy = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rgMissionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlMissionType)).BeginInit();
            this.gctrlMissionType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgSortBy.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeMissions
            // 
            this.treeMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMissions.Location = new System.Drawing.Point(12, 12);
            this.treeMissions.Name = "treeMissions";
            this.treeMissions.Size = new System.Drawing.Size(470, 360);
            this.treeMissions.TabIndex = 0;
            // 
            // b_Select
            // 
            this.b_Select.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Select.Location = new System.Drawing.Point(12, 431);
            this.b_Select.Name = "b_Select";
            this.b_Select.Size = new System.Drawing.Size(433, 23);
            this.b_Select.TabIndex = 1;
            this.b_Select.Text = "Select";
            this.b_Select.Click += new System.EventHandler(this.b_Select_Click);
            // 
            // rgMissionType
            // 
            this.rgMissionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgMissionType.Location = new System.Drawing.Point(2, 21);
            this.rgMissionType.Name = "rgMissionType";
            this.rgMissionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rgMissionType.Properties.Appearance.Options.UseBackColor = true;
            this.rgMissionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgMissionType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Active"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Open"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Completed"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Repeatable")});
            this.rgMissionType.Size = new System.Drawing.Size(312, 24);
            this.rgMissionType.TabIndex = 3;
            this.rgMissionType.SelectedIndexChanged += new System.EventHandler(this.rgMissionType_SelectedIndexChanged);
            // 
            // gctrlMissionType
            // 
            this.gctrlMissionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gctrlMissionType.Controls.Add(this.rgMissionType);
            this.gctrlMissionType.Location = new System.Drawing.Point(12, 378);
            this.gctrlMissionType.Name = "gctrlMissionType";
            this.gctrlMissionType.Size = new System.Drawing.Size(316, 47);
            this.gctrlMissionType.TabIndex = 4;
            this.gctrlMissionType.Text = "Mission type";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // b_Refresh
            // 
            this.b_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Refresh.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniRefresh;
            this.b_Refresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.b_Refresh.Location = new System.Drawing.Point(451, 431);
            this.b_Refresh.Name = "b_Refresh";
            this.b_Refresh.Size = new System.Drawing.Size(31, 23);
            this.b_Refresh.TabIndex = 2;
            this.b_Refresh.Click += new System.EventHandler(this.b_Refresh_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.rgSortBy);
            this.groupControl1.Location = new System.Drawing.Point(334, 378);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(148, 47);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Sort by name";
            // 
            // rgSortBy
            // 
            this.rgSortBy.Location = new System.Drawing.Point(0, 21);
            this.rgSortBy.Name = "rgSortBy";
            this.rgSortBy.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rgSortBy.Properties.Appearance.Options.UseBackColor = true;
            this.rgSortBy.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgSortBy.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Localized"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Internal")});
            this.rgSortBy.Size = new System.Drawing.Size(148, 26);
            this.rgSortBy.TabIndex = 0;
            this.rgSortBy.SelectedIndexChanged += new System.EventHandler(this.rgSortBy_SelectedIndexChanged);
            // 
            // GetMissionId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 466);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gctrlMissionType);
            this.Controls.Add(this.b_Refresh);
            this.Controls.Add(this.b_Select);
            this.Controls.Add(this.treeMissions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GetMissionId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get a mission id";
            this.Load += new System.EventHandler(this.GetMissionId_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rgMissionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gctrlMissionType)).EndInit();
            this.gctrlMissionType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgSortBy.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private TreeView treeMissions;
        private SimpleButton b_Select;
        private SimpleButton b_Refresh;
        private RadioGroup rgMissionType;
        private GroupControl gctrlMissionType;

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private GroupControl groupControl1;
        private RadioGroup rgSortBy;
    }
}