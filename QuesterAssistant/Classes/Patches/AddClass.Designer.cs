using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuesterAssistant.Classes.Patches
{
    partial class AddClass
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_add = new DevExpress.XtraEditors.SimpleButton();
            this.typesList = new DevExpress.XtraEditors.ListBoxControl();
            this.valuesList = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.typesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valuesList)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "1. Choose type :";
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_add.Location = new System.Drawing.Point(222, 270);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(116, 23);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // typesList
            // 
            this.typesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.typesList.Location = new System.Drawing.Point(25, 34);
            this.typesList.Name = "typesList";
            this.typesList.Size = new System.Drawing.Size(120, 213);
            this.typesList.TabIndex = 3;
            this.typesList.SelectedIndexChanged += new System.EventHandler(this.typesList_SelectedIndexChanged);
            // 
            // valuesList
            // 
            this.valuesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valuesList.Location = new System.Drawing.Point(171, 34);
            this.valuesList.Name = "valuesList";
            this.valuesList.Size = new System.Drawing.Size(367, 213);
            this.valuesList.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.valuesList.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(171, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "2. Choose value :";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // AddClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 308);
            this.Controls.Add(this.valuesList);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.typesList);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "AddClass";
            this.Text = "Add new action";
            ((System.ComponentModel.ISupportInitialize)(this.typesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valuesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private bool valid;
        private Dictionary<string, Type> actions = new Dictionary<string, Type>();
        private string selecteType = string.Empty;
        private LabelControl labelControl1;
        private SimpleButton btn_add;
        private ListBoxControl typesList;
        private ListBoxControl valuesList;
        private LabelControl labelControl2;
        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}