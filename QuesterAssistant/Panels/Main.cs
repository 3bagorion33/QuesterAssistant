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
        private Timer timerIsConnecting;
        PowerManagerData pManager = new PowerManagerData(true);
        private CharClassCategory prevCharClass;
        private List<Preset> CurrPresets => pManager.CharClassesList.Find(x => x.CharClassCategory == PManager.CurrCharClass.Category).PresetsList;

        private void Initialize()
        {
            this.components = new Container();
            this.timerIsConnecting = new Timer(this.components)
            {
                Enabled = true,
                Interval = 1000
            };
            this.timerIsConnecting.Tick += new EventHandler(this.IsConnectingTick);
            this.CharClassChanging += new EventHandler(this.FormUpdate);

            pManager = PManager.LoadSettings();
#if DEBUG
            Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager_deb.xml"), pManager);
#endif
        }

        public Main() : base("Quester Assistant")
        {
            InitializeComponent();
            Initialize();
            OnPanelLeave += this.Dispose;
        }

        private void Dispose(object s, EventArgs e) { this.Dispose(true); }

        private event EventHandler CharClassChanging;
        private void OnCharClassChanged()
        {
            CharClassChanging?.Invoke(this, EventArgs.Empty);
        }

        private void IsConnectingTick(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                if (PManager.CurrCharClass.Category != prevCharClass)
                {
                    OnCharClassChanged();
                    prevCharClass = PManager.CurrCharClass.Category;
                }
                GuiActive(true);
            }
            else
            {
                GuiActive(false);
                gridControlPowers_Update(true);
            }
        }

        private void GuiActive(bool v)
        {
            cmbPresetsList.Enabled = v;
            btnGetPowers.Enabled = v;
            btnSetPowers.Enabled = v;
            btnSave.Enabled = v;
            btnLoad.Enabled = v;
        }

        private void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("Name:  {0}",
                //EntityManager.LocalPlayer.Character.Class.GetPaths().Find(x => x.PowerTree.TreeTypeDef.Name == "Paragon").DisplayName);
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("Class:  {0}",
                PManager.CurrCharClass.DisplayName);

            gridControlPowers.DataSource = null;
            cmbPresetsList.Properties.Items.Clear();
            cmbPresetsList.SelectedIndex = -1;

            if (CurrPresets.Any())
            {
                cmbPresetsList.Properties.Items.BeginUpdate();
                try
                {
                    foreach (var item in CurrPresets)
                    {
                        cmbPresetsList.Properties.Items.Add(item.Name);
                    }
                }
                finally
                {
                    cmbPresetsList.Properties.Items.EndUpdate();
                    cmbPresetsList.SelectedIndex = 0;
                }
            }
        }

        private void btnGetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                CurrPresets[cmbPresetsList.SelectedIndex].PowersList = PManager.GetSlottedPowers();
                gridControlPowers_Update();
            }
        }

        private void btnSetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                foreach (var pwr in CurrPresets[cmbPresetsList.SelectedIndex].PowersList)
                {
                    Task.Factory.StartNew(() => PManager.ApplyPower(pwr.TraySlot, pwr.InternalName));
                }
            }
        }

        private void cmbPresetsList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Core.DebugWriteLine(string.Format("Pressed button: {0}", e.Button.Tag));

            switch (e.Button.Tag.ToString())
            {
                case "Select":
                    //setsList.Properties.Items.Add("New item");
                    
                    break;
                case "Add":
                    string str = InputBox.MessageText("Enter a new profile name:");
                    CurrPresets.AddOrReplace(x => x.Name == str, new Preset(str, PManager.GetSlottedPowers()));
                    OnCharClassChanged();
                    cmbPresetsList.SelectedItem = cmbPresetsList.Properties.Items.Count - 1;
                    break;
                case "Delete":
                    if (CurrPresets.Any() && 
                        (XtraMessageBox.Show(Form.ActiveForm, "Delete this preset?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        CurrPresets.RemoveAt(cmbPresetsList.SelectedIndex);
                        OnCharClassChanged();
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PManager.SaveSettings(pManager);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            pManager = PManager.LoadSettings();
            OnCharClassChanged();
        }

        private void cmbPresetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControlPowers_Update();
        }

        private void gridControlPowers_Update(bool b = false)
        {
            List<Power> pList = new List<Power>();
            if (b) { this.gridControlPowers.DataSource = pList; return; }
            if (CurrPresets.Any())
            {
                pList = CurrPresets[cmbPresetsList.SelectedIndex].PowersList.Select(delegate (Power x) { return x.ToDispName(); }).ToList();
            }
            this.gridControlPowers.DataSource = pList;
        }
    }
}
