using Astral;
using Astral.Forms;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace QuesterAssistant
{
    public class Main : BasePanel
    {
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private TextBox textBox1;
        private DevExpress.XtraEditors.LabelControl labelCharacterClass;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;

        // Methods
        public Main() : base("Quester Assistant")
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.labelCharacterClass = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.labelControl2.Size = new System.Drawing.Size(31, 19);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "v 1.0";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(306, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(3);
            this.labelControl3.Size = new System.Drawing.Size(59, 19);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "by Orion33";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(370, 416);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.labelCharacterClass);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(368, 391);
            this.xtraTabPage1.Text = "Power Switcher";
            // 
            // labelCharacterClass
            // 
            this.labelCharacterClass.Location = new System.Drawing.Point(3, 3);
            this.labelCharacterClass.Name = "labelCharacterClass";
            this.labelCharacterClass.Padding = new System.Windows.Forms.Padding(3);
            this.labelCharacterClass.Size = new System.Drawing.Size(125, 19);
            this.labelCharacterClass.TabIndex = 0;
            this.labelCharacterClass.Text = "Current character class: ";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.textBox1);
            this.xtraTabPage2.Controls.Add(this.labelControl3);
            this.xtraTabPage2.Controls.Add(this.labelControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(368, 391);
            this.xtraTabPage2.Text = "About";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(3, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(362, 360);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = Properties.Resources.Description;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}

