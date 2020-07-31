﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Astral.Classes;
using DevExpress.Utils;
using DevExpress.Utils.Helpers;
using DevExpress.XtraEditors;
using MyNW;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.CodeReader;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Patches;
using QuesterAssistant.Classes.Reflection;
using QuesterAssistant.Enums;
using QuesterAssistant.Panels;
using ChatManager = QuesterAssistant.Classes.ChatManager;

namespace QuesterAssistant.Settings
{
    internal partial class SettingsForm : CoreForm
    {
        private SettingsCore Core => core as SettingsCore;
        private SettingsData Data => Core.Data;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void txtRoleToggleString_KeyDown(object sender, KeyEventArgs e)
        {
            var k = e.KeyData;
            (sender as TextEdit).Text = k.IgnoreBack().ConvertToString();

            if (k.IsNotModifier())
            {
                ActiveControl = null;
            }
        }

        private void txtPauseBotHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            (sender as TextEdit).Text = e.KeyData.IgnoreBack().ConvertToString();
            ActiveControl = null;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            bsrcData.DataSource = Data;
            bsrcRoleToggleHotKey.DataSource = Data.RoleToggleHotKey;
            bsrcHideGameHotKey.DataSource = Data.HideClient.HotKey;
            bsrcHideMode.DataSource = Data.HideClient;
            bsrcPauseBotHotKey.DataSource = Data.PauseBot.HotKey;
            bsrcPauseBot.DataSource = Data.PauseBot;
            bsrcPatches.DataSource = Data.Patches;
            cbxHideMinimize.Properties.Items.AddRange(typeof(SettingsData.HideClientClass.Mode).GetEnumValues());

            chkRoleToggleEnabled.BindAdd(bsrcRoleToggleHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtRoleToggleString.BindAdd(bsrcRoleToggleHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
            chkHideGameEnabled.BindAdd(bsrcHideGameHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtHideGameString.BindAdd(bsrcHideGameHotKey, nameof(TextEdit.Text), nameof(HotKey.String), DataSourceUpdateMode.OnValidation);
            cbxHideMinimize.BindAdd(bsrcHideMode, nameof(ComboBoxEdit.EditValue), nameof(SettingsData.HideClientClass.HideMode));
            chkPauseBotHotKey.BindAdd(bsrcPauseBotHotKey, nameof(CheckEdit.Checked), nameof(HotKey.Enabled));
            txtPauseBotHotKey.BindAdd(bsrcPauseBotHotKey, nameof(TextEdit.Text), nameof(HotKey.String));
            numPauseDelay.BindAdd(bsrcPauseBot, nameof(SpinEdit.EditValue), nameof(SettingsData.PauseBot.Delay));
            chkGameCursorMoving.BindAdd(bsrcData, nameof(ComboBoxEdit.EditValue), nameof(Data.GameCursorMoving));
            numStackLifeTime.BindAdd(bsrcData, nameof(SpinEdit.EditValue), nameof(Data.StackLifeTime));

            chkWayPointPatch.BindAdd(bsrcPatches, nameof(CheckEdit.Checked), nameof(SettingsData.Patches.WayPointFilterPatch));
            chkProfessionPatch.BindAdd(bsrcPatches, nameof(CheckEdit.Checked), nameof(SettingsData.PatchesDef.ProfessionPatch));
            numReadyTasksCount.BindAdd(bsrcPatches, nameof(SpinEdit.EditValue), nameof(SettingsData.PatchesDef.ProfessionPatchFreeTasksSlots));

            Data.HashChanged += bsrcRoleToggleHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcHideMode.ResetCurrentItem;
            Data.HashChanged += bsrcHideGameHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcPauseBotHotKey.ResetCurrentItem;
            Data.HashChanged += bsrcPauseBot.ResetCurrentItem;
            Data.HashChanged += bsrcData.ResetCurrentItem;
            Data.HashChanged += bsrcPatches.ResetCurrentItem;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // x32
            //string[] mnemonics = {
            //    "push 0",
            //    "call " + (Memory.BaseAdress + 0x5c9fd0),
            //    "add esp, 0x4",
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "UIGen_AuctionRequestAuctionsBidByPlayer");
            //string[] mnemonics = {
            //    "call " + (Memory.BaseAdress + 0x5CC110),
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "gslGarrison_TendStructureOnCurrentPlot");

            //x64

            //string[] mnemonics = {
            //    "sub rsp, 0x20",
            //    "mov rax, " + (Memory.BaseAdress + 0x6DF7A0),
            //    "call rax",
            //    "add rsp, 0x20",
            //    "retn"
            //};
            //Hooks.Executor.Execute<IntPtr>(mnemonics, "gslGarrison_TendStructureOnCurrentPlot");
            //var machineid = Memory.MMemory.ReadBytes(Memory.BaseAdress + 0x2640BD0, 64);
            //textEdit1.Text = System.Text.Encoding.UTF8.GetString(machineid, 0, machineid.Length);

            var machineid = Memory.MMemory.ReadString(Memory.BaseAdress + 0x2640BD0, Encoding.UTF8, 64);
            textEdit1.Text = machineid;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var machineid = textEdit1.Text;
            Memory.MMemory.WriteString(Memory.BaseAdress + 0x2640BD0, Encoding.UTF8, machineid);
        }

        private void btnClearStack_Click(object sender, EventArgs e)
        {
            ProfilesStack.Clear();
        }

        private void btnShowStack_Click(object sender, EventArgs e)
        {
            ProfilesStack.Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ChatManager.Load();
            ChatManager.OnChatMessage += OnChatMessage;
            simpleButton3.Enabled = false;
            simpleButton4.Enabled = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ChatManager.OnChatMessage -= OnChatMessage;
            ChatManager.UnLoad();
            simpleButton4.Enabled = false;
            simpleButton3.Enabled = true;
        }

        private void OnChatMessage(ChatManager.ChatLogEntryType chatLogEntryType, List<string> messages)
        {
            richTextBox1.AppendText($"[{chatLogEntryType}]: {string.Join(" => ", messages)}\n\r");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ////CostumeRef costumeRef = new CostumeRef((IntPtr) (EntityManager.LocalPlayer.CostumeRef.pMountCostume + 8));
            //var costumeDbg = new List<CostumeDbg>();
            ////var idx = new[] {0, 8, 16, 24};
            ////foreach (var i in idx)
            ////for (int i = 0; i < 256; i++)
            ////{
            ////    costumeDbg.Add(new CostumeDbg(i));
            ////}
            //costumeDbg.Add(new CostumeDbg(32));
            //var costume = EntityManager.LocalPlayer.GetMountCostume();
            var items = Email.Mails[3].Message.GetAttachedItems();
        }

        private class MailSlot : NativeObject
        {
            public MailSlot(IntPtr pointer) : base(pointer)
            {
            }

            public Item Item => new MyItem(Pointer);
        }

        private class MyItem : Item
        {
            public MyItem(IntPtr pointer) : base(pointer) { }

            public override string ToString()
            {
                return base.DisplayName;
            }
        }

        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
        private struct CostumeDbg
        {
            //private static IntPtr baseAddr = (IntPtr) EntityManager.LocalPlayer.CostumeRef.pMountCostume;
            private static IntPtr baseAddr = Email.Mails[3].Message.Pointer;

            private int num;
            private string p0_string;
            private int p0_int;
            private IntPtr p1;
            private List<MailSlot> p1_mSlots;
            private List<Item> p1_Items;
            //private List<InventorySlot> p1_iSlots;
            private string p1_string;
            private int p1_int;

            //private List<string> p1_strings;

            public CostumeDbg(int i)
            {
                num = i;
                //p0_string = Memory.MMemory.ReadString(Memory.MMemory.Read<IntPtr>(baseAddr + i),Encoding.UTF8, 256);
                //p0_int = Memory.MMemory.Read<int>(Memory.MMemory.Read<IntPtr>(baseAddr + i));
                //p1 = Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(baseAddr + i));
                //p1_string = Memory.MMemory.ReadString(p1, Encoding.UTF8, 256);
                //p1_int = Memory.MMemory.Read<int>(p1);
                //p1_strings = new List<string>();
                //for (int j = 0; j < 256; j++)
                //{
                //    p1_strings.Add(Memory.MMemory.ReadString(p1 + j, Encoding.UTF8, 256));
                //}

                p0_string = Memory.MMemory.ReadString(baseAddr + i, Encoding.UTF8, 256);
                p0_int = Memory.MMemory.Read<int>(baseAddr + i);
                p1 = Memory.MMemory.Read<IntPtr>(baseAddr + i);
                p1_mSlots = NWList.Get<MailSlot>(p1);
                p1_Items = NWList.Get<Item>(p1);
                //p1_iSlots = NWList.Get<InventorySlot>(p1);
                p1_string = (p1_mSlots.Count > 0) ? "" : "<Empty>"; // Memory.MMemory.ReadString(p1, Encoding.UTF8, 256);
                p1_int = p1_mSlots.Count; //Memory.MMemory.Read<int>(p1);

            }

            public override string ToString() =>
                //$"{num}|{p0_int}:{p0_string}|{p1_int}:{p1_string}";
                $"{num}|{p1_int}:{p1_string}";
        }
    }
}
