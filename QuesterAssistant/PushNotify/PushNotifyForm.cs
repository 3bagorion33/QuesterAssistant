﻿using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.PushBulletClient;
using QuesterAssistant.Classes.PushBulletClient.Models.Responses;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuesterAssistant.PushNotify
{
    internal partial class PushNotifyForm : CoreForm
    {
        private PushNotifyCore Core => core as PushNotifyCore;
        private PushNotifyData Data => Core.Data;

        public PushNotifyForm()
        {
            InitializeComponent();
        }

        private void SettingsLoaded()
        {
            cbxDevicesList.Properties.RefreshDataSource();
            cbxDevicesList.RefreshEditValue();
        }

        private void cbxDevicesList_Closed(object sender, ClosedEventArgs e)
        {
            ActiveControl = null;
            Data.Devices.Clear();
            var cbx = sender as CheckedComboBoxEdit;
            var srcList = cbx.Properties.Items.GetCheckedValues();
            try
            {
                srcList.ForEach(iden => bsrcUserDevices.Add((cbx.Properties.DataSource as List<Device>).Find(d => d.Iden == iden as string)));
            }
            catch (Exception ex)
            {
                QMessageBox.ShowError(ex.ToString());
            }
            cbx.Enabled = false;
            cbx.Properties.DataSource = bsrcUserDevices;
        }

        private void chbtHide_CheckedChanged(object sender, EventArgs e)
        {
            txtToken.Properties.UseSystemPasswordChar = ((CheckButton)sender).Checked;
        }

        private void bmiGetList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                cbxDevicesList.Properties.DataSource = Data.Client.CurrentUsersDevices().Devices;
                cbxDevicesList.Enabled = true;
            }
            catch (Exception ex) { QMessageBox.ShowError(ex.Message); }
        }

        private void bmiSendTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Data.Devices.Count != 0)
            {
                try
                {
                    Core.PushMessage("This is a test message.");
                }
                catch (Exception ex) { QMessageBox.ShowError(ex.Message); }
            }
        }

        private void PushNotifyForm_Load(object sender, EventArgs e)
        {
            Core.SettingsLoaded += SettingsLoaded;

            bsrcClient.DataSource = Data.Client;
            bsrcUserDevices.DataSource = Data.Devices;

            txtToken.BindAdd(bsrcClient, nameof(TextEdit.Text), nameof(PushBulletClientLite.AccessToken), DataSourceUpdateMode.OnValidation);

            cbxDevicesList.Properties.DataSource = bsrcUserDevices;
            cbxDevicesList.Properties.ValueMember = nameof(Device.Iden);
            cbxDevicesList.Properties.DisplayMember = nameof(Device.Nickname);
            cbxDevicesList.CheckAll();
        }
    }
}
