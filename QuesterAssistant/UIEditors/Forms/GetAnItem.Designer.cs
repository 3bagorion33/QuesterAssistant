using DevExpress.XtraEditors;

namespace QuesterAssistant.UIEditors.Forms
{
    partial class GetAnItem
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
            this.lbItemsSource = new DevExpress.XtraEditors.ListBoxControl();
            this.b_Select = new DevExpress.XtraEditors.SimpleButton();
            this.itemListChoice = new DevExpress.XtraEditors.RadioGroup();
            this.b_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lbItemsSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemListChoice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbItemsSource
            // 
            this.lbItemsSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbItemsSource.Location = new System.Drawing.Point(12, 12);
            this.lbItemsSource.Name = "lbItemsSource";
            this.lbItemsSource.Size = new System.Drawing.Size(537, 231);
            this.lbItemsSource.TabIndex = 0;
            // 
            // b_Select
            // 
            this.b_Select.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Select.Location = new System.Drawing.Point(12, 297);
            this.b_Select.Name = "b_Select";
            this.b_Select.Size = new System.Drawing.Size(500, 23);
            this.b_Select.TabIndex = 1;
            this.b_Select.Text = "Select";
            this.b_Select.Click += new System.EventHandler(this.b_Select_Click);
            // 
            // itemListChoice
            // 
            this.itemListChoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemListChoice.Location = new System.Drawing.Point(12, 249);
            this.itemListChoice.Name = "itemListChoice";
            this.itemListChoice.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Inventory"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Vendor"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Consumables"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Equiped"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Rewards"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All bags"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mail"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Auction")});
            this.itemListChoice.Size = new System.Drawing.Size(537, 42);
            this.itemListChoice.TabIndex = 3;
            this.itemListChoice.Visible = false;
            this.itemListChoice.SelectedIndexChanged += new System.EventHandler(this.itemListChoice_SelectedIndexChanged);
            // 
            // b_Refresh
            // 
            this.b_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Refresh.ImageOptions.Image = global::QuesterAssistant.Properties.Resources.miniRefresh;
            this.b_Refresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.b_Refresh.Location = new System.Drawing.Point(518, 297);
            this.b_Refresh.Name = "b_Refresh";
            this.b_Refresh.Size = new System.Drawing.Size(30, 23);
            this.b_Refresh.TabIndex = 2;
            this.b_Refresh.Click += new System.EventHandler(this.b_Refresh_Click);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // GetAnItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 332);
            this.Controls.Add(this.itemListChoice);
            this.Controls.Add(this.b_Refresh);
            this.Controls.Add(this.b_Select);
            this.Controls.Add(this.lbItemsSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GetAnItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get an item id";
            this.Load += new System.EventHandler(this.GetAnId_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbItemsSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemListChoice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private ListBoxControl lbItemsSource;

        private SimpleButton b_Select;

        private SimpleButton b_Refresh;

        private RadioGroup itemListChoice;

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}