namespace QuesterAssistant.PushNotify
{
    partial class PushNotifyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PushNotifyForm));
            this.txtToken = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bsrcClient = new System.Windows.Forms.BindingSource();
            this.cbxDevicesList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.bsrcUserDevices = new System.Windows.Forms.BindingSource();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chbtHide = new DevExpress.XtraEditors.CheckButton();
            this.pmDevicesActions = new DevExpress.XtraBars.PopupMenu();
            this.bmiGetList = new DevExpress.XtraBars.BarButtonItem();
            this.bmiSendTest = new DevExpress.XtraBars.BarButtonItem();
            this.bmDevicesActions = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnDevicesActions = new DevExpress.XtraEditors.DropDownButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtToken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDevicesList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcUserDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmDevicesActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmDevicesActions)).BeginInit();
            this.SuspendLayout();
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(63, 4);
            this.txtToken.Name = "txtToken";
            this.txtToken.Properties.AutoHeight = false;
            this.txtToken.Properties.UseSystemPasswordChar = true;
            this.txtToken.Size = new System.Drawing.Size(260, 23);
            this.txtToken.TabIndex = 0;
            this.txtToken.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "API key :";
            // 
            // cbxDevicesList
            // 
            this.cbxDevicesList.EditValue = ((object)(resources.GetObject("cbxDevicesList.EditValue")));
            this.cbxDevicesList.Enabled = false;
            this.cbxDevicesList.Location = new System.Drawing.Point(63, 33);
            this.cbxDevicesList.Name = "cbxDevicesList";
            this.cbxDevicesList.Properties.AllowMultiSelect = true;
            this.cbxDevicesList.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.cbxDevicesList.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.cbxDevicesList.Properties.AutoHeight = false;
            this.cbxDevicesList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDevicesList.Properties.EditValueType = DevExpress.XtraEditors.Repository.EditValueTypeCollection.List;
            this.cbxDevicesList.Properties.FocusPopupControl = false;
            this.cbxDevicesList.Properties.ForceUpdateEditValue = DevExpress.Utils.DefaultBoolean.True;
            this.cbxDevicesList.Properties.NullText = "Devices not found";
            this.cbxDevicesList.Properties.NullValuePrompt = "Select devices to send";
            this.cbxDevicesList.Properties.NullValuePromptShowForEmptyValue = true;
            this.cbxDevicesList.Properties.PopupSizeable = false;
            this.cbxDevicesList.Properties.SelectAllItemVisible = false;
            this.cbxDevicesList.Properties.ShowNullValuePromptWhenFocused = true;
            this.cbxDevicesList.Size = new System.Drawing.Size(223, 23);
            this.cbxDevicesList.TabIndex = 3;
            this.cbxDevicesList.TabStop = false;
            this.cbxDevicesList.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cbxDevicesList_Closed);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Devices :";
            // 
            // chbtHide
            // 
            this.chbtHide.Checked = true;
            this.chbtHide.Location = new System.Drawing.Point(329, 3);
            this.chbtHide.Name = "chbtHide";
            this.chbtHide.Size = new System.Drawing.Size(38, 23);
            this.chbtHide.TabIndex = 5;
            this.chbtHide.TabStop = false;
            this.chbtHide.Text = "Hide";
            this.chbtHide.CheckedChanged += new System.EventHandler(this.chbtHide_CheckedChanged);
            // 
            // pmDevicesActions
            // 
            this.pmDevicesActions.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiGetList),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiSendTest)});
            this.pmDevicesActions.Manager = this.bmDevicesActions;
            this.pmDevicesActions.Name = "pmDevicesActions";
            // 
            // bmiGetList
            // 
            this.bmiGetList.Caption = "Get list of devices";
            this.bmiGetList.Id = 4;
            this.bmiGetList.Name = "bmiGetList";
            this.bmiGetList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bmiGetList_ItemClick);
            // 
            // bmiSendTest
            // 
            this.bmiSendTest.Caption = "Send test message";
            this.bmiSendTest.Id = 5;
            this.bmiSendTest.Name = "bmiSendTest";
            this.bmiSendTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bmiSendTest_ItemClick);
            // 
            // bmDevicesActions
            // 
            this.bmDevicesActions.DockControls.Add(this.barDockControlTop);
            this.bmDevicesActions.DockControls.Add(this.barDockControlBottom);
            this.bmDevicesActions.DockControls.Add(this.barDockControlLeft);
            this.bmDevicesActions.DockControls.Add(this.barDockControlRight);
            this.bmDevicesActions.Form = this;
            this.bmDevicesActions.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bmiGetList,
            this.bmiSendTest});
            this.bmDevicesActions.MaxItemId = 6;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmDevicesActions;
            this.barDockControlTop.Size = new System.Drawing.Size(370, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 372);
            this.barDockControlBottom.Manager = this.bmDevicesActions;
            this.barDockControlBottom.Size = new System.Drawing.Size(370, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bmDevicesActions;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 372);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(370, 0);
            this.barDockControlRight.Manager = this.bmDevicesActions;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 372);
            // 
            // btnDevicesActions
            // 
            this.btnDevicesActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide;
            this.btnDevicesActions.DropDownControl = this.pmDevicesActions;
            this.btnDevicesActions.Location = new System.Drawing.Point(292, 32);
            this.btnDevicesActions.MenuManager = this.bmDevicesActions;
            this.btnDevicesActions.Name = "btnDevicesActions";
            this.btnDevicesActions.Size = new System.Drawing.Size(75, 23);
            this.btnDevicesActions.TabIndex = 2;
            this.btnDevicesActions.TabStop = false;
            this.btnDevicesActions.Text = "Actions";
            // 
            // PushNotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDevicesActions);
            this.Controls.Add(this.chbtHide);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cbxDevicesList);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(370, 372);
            this.Name = "PushNotifyForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.PushNotifyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtToken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDevicesList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsrcUserDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmDevicesActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmDevicesActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtToken;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.BindingSource bsrcClient;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbxDevicesList;
        private System.Windows.Forms.BindingSource bsrcUserDevices;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckButton chbtHide;
        private DevExpress.XtraBars.BarManager bmDevicesActions;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu pmDevicesActions;
        private DevExpress.XtraEditors.DropDownButton btnDevicesActions;
        private DevExpress.XtraBars.BarButtonItem bmiGetList;
        private DevExpress.XtraBars.BarButtonItem bmiSendTest;
    }
}