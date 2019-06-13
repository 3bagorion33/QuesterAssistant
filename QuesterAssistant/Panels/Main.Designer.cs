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
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sideButtons = new DevExpress.XtraEditors.SidePanel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.tctlMain = new DevExpress.XtraEditors.TileControl();
            this.sideMain = new DevExpress.XtraEditors.SidePanel();
            this.tileMain = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileSettings = new DevExpress.XtraEditors.TileItem();
            this.tileUpgradeManager = new DevExpress.XtraEditors.TileItem();
            this.tilePushNotify = new DevExpress.XtraEditors.TileItem();
            this.tileAbout = new DevExpress.XtraEditors.TileItem();
            this.tilePowersManager = new DevExpress.XtraEditors.TileItem();
            this.sideButtons.SuspendLayout();
            this.sideMain.SuspendLayout();
            this.SuspendLayout();
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
            // sideButtons
            // 
            this.sideButtons.AllowResize = false;
            this.sideButtons.AllowSnap = false;
            this.sideButtons.BorderThickness = 0;
            this.sideButtons.Controls.Add(this.btnLoad);
            this.sideButtons.Controls.Add(this.btnSave);
            this.sideButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sideButtons.Location = new System.Drawing.Point(0, 375);
            this.sideButtons.Name = "sideButtons";
            this.sideButtons.Size = new System.Drawing.Size(370, 41);
            this.sideButtons.TabIndex = 10;
            this.sideButtons.Text = "sidePanel2";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // tctlMain
            // 
            this.tctlMain.ColumnCount = 3;
            this.tctlMain.Location = new System.Drawing.Point(0, 0);
            this.tctlMain.Name = "tctlMain";
            this.tctlMain.Size = new System.Drawing.Size(240, 150);
            this.tctlMain.TabIndex = 0;
            // 
            // sideMain
            // 
            this.sideMain.Controls.Add(this.tileMain);
            this.sideMain.Location = new System.Drawing.Point(0, 0);
            this.sideMain.Name = "sideMain";
            this.sideMain.Size = new System.Drawing.Size(370, 372);
            this.sideMain.TabIndex = 11;
            this.sideMain.Text = "sidePanel1";
            // 
            // tileMain
            // 
            this.tileMain.AllowDrag = false;
            this.tileMain.AllowDragTilesBetweenGroups = false;
            this.tileMain.AllowSelectedItemBorder = false;
            this.tileMain.AllowSmoothScrolling = false;
            this.tileMain.AppearanceItem.Normal.BackColor = System.Drawing.Color.RoyalBlue;
            this.tileMain.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.CornflowerBlue;
            this.tileMain.AppearanceItem.Normal.BorderColor = System.Drawing.Color.RoyalBlue;
            this.tileMain.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileMain.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileMain.ColumnCount = 3;
            this.tileMain.Groups.Add(this.tileGroup2);
            this.tileMain.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tileMain.IndentBetweenGroups = 0;
            this.tileMain.IndentBetweenItems = 10;
            this.tileMain.ItemSize = 110;
            this.tileMain.Location = new System.Drawing.Point(0, 0);
            this.tileMain.Margin = new System.Windows.Forms.Padding(0);
            this.tileMain.MaxId = 6;
            this.tileMain.Name = "tileMain";
            this.tileMain.RowCount = 2;
            this.tileMain.Size = new System.Drawing.Size(370, 372);
            this.tileMain.TabIndex = 13;
            this.tileMain.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileSettings);
            this.tileGroup2.Items.Add(this.tileUpgradeManager);
            this.tileGroup2.Items.Add(this.tilePushNotify);
            this.tileGroup2.Items.Add(this.tileAbout);
            this.tileGroup2.Items.Add(this.tilePowersManager);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileSettings
            // 
            tileItemElement1.Text = "Settings";
            this.tileSettings.Elements.Add(tileItemElement1);
            this.tileSettings.Id = 1;
            this.tileSettings.Name = "tileSettings";
            this.tileSettings.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tile_ItemClick);
            // 
            // tileUpgradeManager
            // 
            tileItemElement2.Text = "Upgrade Manager";
            this.tileUpgradeManager.Elements.Add(tileItemElement2);
            this.tileUpgradeManager.Id = 2;
            this.tileUpgradeManager.Name = "tileUpgradeManager";
            this.tileUpgradeManager.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tile_ItemClick);
            // 
            // tilePushNotify
            // 
            tileItemElement3.Text = "Push Notify";
            this.tilePushNotify.Elements.Add(tileItemElement3);
            this.tilePushNotify.Id = 4;
            this.tilePushNotify.Name = "tilePushNotify";
            this.tilePushNotify.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tile_ItemClick);
            // 
            // tileAbout
            // 
            tileItemElement4.Text = "About";
            this.tileAbout.Elements.Add(tileItemElement4);
            this.tileAbout.Id = 5;
            this.tileAbout.Name = "tileAbout";
            this.tileAbout.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tile_ItemClick);
            // 
            // tilePowersManager
            // 
            tileItemElement5.Text = "Powers Manager";
            this.tilePowersManager.Elements.Add(tileItemElement5);
            this.tilePowersManager.Id = 3;
            this.tilePowersManager.Name = "tilePowersManager";
            this.tilePowersManager.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tile_ItemClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sideMain);
            this.Controls.Add(this.sideButtons);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.sideButtons.ResumeLayout(false);
            this.sideMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SidePanel sideButtons;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.TileControl tctlMain;
        private DevExpress.XtraEditors.SidePanel sideMain;
        private DevExpress.XtraEditors.TileControl tileMain;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileSettings;
        private DevExpress.XtraEditors.TileItem tileUpgradeManager;
        private DevExpress.XtraEditors.TileItem tilePushNotify;
        private DevExpress.XtraEditors.TileItem tileAbout;
        private DevExpress.XtraEditors.TileItem tilePowersManager;
    }
}