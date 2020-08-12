using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Astral.Classes.ItemFilter;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    partial class ItemFilterForm : XtraForm
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.itemFilterUC = new ItemFilterUC();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.SuspendLayout();
            // 
            // itemFilterUC
            // 
            this.itemFilterUC.Dock = DockStyle.Fill;
            this.itemFilterUC.Filter = null;
            this.itemFilterUC.Location = new Point(0, 0);
            this.itemFilterUC.Name = "itemFilterUC";
            this.itemFilterUC.ShowExpand = false;
            this.itemFilterUC.Size = new Size(620, 407);
            this.itemFilterUC.TabIndex = 0;
            this.itemFilterUC.Type = ItemFilterCoreType.Items;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // ItemFilterForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(620, 407);
            this.Controls.Add(this.itemFilterUC);
            this.Name = "ItemFilterForm";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Filter editor";
            this.ResumeLayout(false);

        }

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private IContainer components;
        private ItemFilterUC itemFilterUC;
    }
}
