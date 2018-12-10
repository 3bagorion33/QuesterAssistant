using Astral;
using Astral.Forms;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QuesterAssistant.Panels
{
    public partial class Main : BasePanel
    {
        public Main() : base("Quester Assistant")
        {
            InitializeComponent();
            InitializePManager();
            components = new Container();
            components.Add(timerCharCheck);
            OnPanelLeave += this.Dispose;
        }

        private void Dispose(object s, EventArgs e) { this.Dispose(true); }

        #region Power Manager Tab
        PowerManagerData pManager = new PowerManagerData(true);
        private CharClassCategory prevCharClass;
        private List<Preset> CurrPresets
        {
            get
            {
                if (PManager.CanUpdate)
                {
                    return pManager.CharClassesList.Find(x => x.CharClassCategory == PManager.CurrCharClass.Category).PresetsList;
                }
                return new List<Preset>();
            }
        }

        private void InitializePManager()
        {
            this.CharClassChanging += new EventHandler(this.FormUpdate);

            pManager = PManager.LoadSettings();
#if DEBUG
            Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager_deb.xml"), pManager);
#endif
        }

        private event EventHandler CharClassChanging;

        private void CharCheck(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                if (PManager.CurrCharClass.Category != prevCharClass)
                {
                    CharClassChanging?.Invoke(this, EventArgs.Empty);
                    prevCharClass = PManager.CurrCharClass.Category;
                }
            }
        }

        private void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("Name:  {0}",
                //EntityManager.LocalPlayer.Character.Class.GetPaths().Find(x => x.PowerTree.TreeTypeDef.Name == "Paragon").DisplayName);
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("Class:  {0}",
                PManager.CurrCharClass.DisplayName);

            cmbPresetsList_Update();
            foreach (var treeBuild in MyNW.Internals.EntityManager.LocalPlayer.Character.PowerTreeBuilds)
            {
                foreach (var secondaryPath in treeBuild.SecondaryPaths)
                {
                    Astral.Logger.WriteLine("Paragon Path : " + secondaryPath.Path.DisplayName);
                    return;
                }
            }
        }

        private void btnGetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate && (cmbPresetsList.SelectedIndex != -1))
            {
                CurrPresets[cmbPresetsList.SelectedIndex].PowersList = PManager.GetSlottedPowers();
                gridControlPowers_Update();
            }
        }

        private void btnSetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate && (cmbPresetsList.SelectedIndex != -1))
            {
                foreach (var pwr in CurrPresets[cmbPresetsList.SelectedIndex].PowersList)
                {
                    Task.Factory.StartNew(() => PManager.ApplyPower(pwr.TraySlot, pwr.InternalName));
                }
            }
        }

        private void cmbPresetsList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (PManager.CanUpdate)
            {
                Core.DebugWriteLine(string.Format("Pressed button: {0}", e.Button.Tag));

                switch (e.Button.Tag.ToString())
                {
                    case "Select":
                        //setsList.Properties.Items.Add("New item");

                        break;

                    case "Add":
                        string str = InputBox.MessageText("Enter a new profile name:");
                        if (str.Any())
                        {
                            CurrPresets.AddOrReplace(x => x.Name == str, new Preset(str, PManager.GetSlottedPowers()));
                            cmbPresetsList_Update(cmbPresetsList.Properties.Items.Count);
                        }
                        break;

                    case "Delete":
                        if (CurrPresets.Any() &&
                            (XtraMessageBox.Show(Form.ActiveForm, "Delete this preset?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            CurrPresets.RemoveAt(cmbPresetsList.SelectedIndex);
                            cmbPresetsList_Update();
                        }
                        break;

                    case "Sort":
                        break;

                    default:
                        break;
                }
            }
        }

        private void cmbPresetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.DebugWriteLine(string.Format("SelectedIndexChanged => {0}", cmbPresetsList.SelectedIndex));
            gridControlPowers_Update();
            tedHotKey_Update();
        }

        private void gridControlPowers_Update()
        {
            gridControlPowers.BeginUpdate();
            try
            {
                gridControlPowers.DataSource = null;
                Core.DebugWriteLine(string.Format("gridControlPowers_Update => \ncmbPresetsList.SelectedIndex: {0}", cmbPresetsList.SelectedIndex));
                List<Power> pList = new List<Power>();
                if (cmbPresetsList.SelectedIndex < 0) { gridControlPowers.DataSource = pList; return; }
                if (CurrPresets.Any())
                {
                    pList = CurrPresets[cmbPresetsList.SelectedIndex].PowersList.Select(x => x.ToDispName()).ToList();
                }
                gridControlPowers.DataSource = pList;
            }
            finally
            {
                gridControlPowers.EndUpdate();
            }
        }

        private void cmbPresetsList_Update(int selIdx = -1)
        {
            cmbPresetsList.Properties.Items.Clear();
            cmbPresetsList.Properties.Items.BeginUpdate();
            try
            {
                if (CurrPresets.Any())
                {
                    foreach (var item in CurrPresets)
                    {
                        cmbPresetsList.Properties.Items.Add(item.Name);
                    }
                    if (selIdx == -1) selIdx = 0;
                }
            }
            finally
            {
                cmbPresetsList.Properties.Items.EndUpdate();
            }
            cmbPresetsList.SelectedIndex = selIdx;
        }

        private void tedHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            //var keymask = Keys.LWin | Keys.RWin | Keys.ShiftKey | Keys.ControlKey | Keys.Menu | Keys.Apps;
            // KeyCode - последняя нажатая клавиша
            // KeyData - все нажатые клавиши
            if (PManager.CanUpdate)
            {
                KeysConverter kc = new KeysConverter();
                if (e.Shift || e.Control || e.Alt)
                {
                    string str = kc.ConvertToString(e.Modifiers);
                    tedHotKey.Text = str.Remove(str.Length - 4);
                }

                if (e.KeyCode != Keys.LWin && e.KeyCode != Keys.RWin && e.KeyCode != Keys.ShiftKey &&
                    e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.Apps)
                {
                    base.ActiveControl = null;
                    CurrPresets[cmbPresetsList.SelectedIndex].Keys = e.KeyData;
                    tedHotKey_Update();
                }
            }
        }

        private void tedHotKey_Update()
        {
            if (PManager.CanUpdate && (cmbPresetsList.SelectedIndex != -1))
            {
                tedHotKey.Text = CurrPresets[cmbPresetsList.SelectedIndex].HotKeys;
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTabPage.Equals(pManagerTab) && PManager.CanUpdate)
            {
                PManager.SaveSettings(pManager);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTabPage.Equals(pManagerTab))
            {
                pManager = PManager.LoadSettings();
                cmbPresetsList_Update();
            }
        }
    }
}
