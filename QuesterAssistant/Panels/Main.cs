using Astral;
using Astral.Forms;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MyNW.Classes;
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
        }

        private void Dispose(object s, EventArgs e) { this.Dispose(true); }

        private event EventHandler CharClassChanging;
        private void OnCharClassChanged()
        {
            CharClassChanging?.Invoke(this, EventArgs.Empty);
        }

        private CharClassCategory currCharClass;
        private CharClassCategory prevCharClass;
        private void IsConnectingTick(object sender, EventArgs e)
        {
            currCharClass = EntityManager.LocalPlayer.Character.Class.Category;
            if (PManager.CanUpdate)
            {
                if (currCharClass != prevCharClass)
                {
                    OnCharClassChanged();
                    prevCharClass = currCharClass;
                }
            }
        }

        internal Dictionary<TraySlot, MyNW.Classes.Power> slottedPowers;

        private void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("Name:  {0}",
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("Class:  {0}",
                EntityManager.LocalPlayer.Character.Class.DisplayName);

            //slottedPowers = PManager.GetSlottedPowers();
            //this.gridControlPowers.DataSource = slottedPowers;

            pManager = PManager.LoadSettings();
            //PowerManagerData pManager = new PowerManagerData(true);
            Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager_deb.xml"), pManager);
            
            cmbPresetsList.Properties.Items.Clear();
            gridControlPowers.RefreshDataSource();

            var charClass = pManager.CharClassesList.Find(x => x.CharClassCategory == currCharClass);
            var presList = charClass.PresetsList;

            if (presList.Count == 0)
            {
                cmbPresetsList.Properties.Items.Add("Create a new preset");
            }
            else
            {
                cmbPresetsList.Properties.Items.BeginUpdate();
                try { cmbPresetsList.Properties.Items.AddRange(presList); }
                finally { cmbPresetsList.Properties.Items.EndUpdate(); }
            }

            cmbPresetsList.SelectedIndex = 0;
        }

        public Main() : base ("Quester Assistant")
        {
            InitializeComponent();
            Initialize();
            OnPanelLeave += this.Dispose;
        }

        private void buttonGetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                OnCharClassChanged();
            }
        }

        private void buttonSetPowers_Click(object sender, EventArgs e)
        {
            if (PManager.CanUpdate)
            {
                foreach (var pwr in slottedPowers)
                {
                    Task.Factory.StartNew(() => PManager.ApplyPower(pwr.Key, pwr.Value.PowerDef.InternalName));
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
                    Core.DebugWriteLine("Switch to Add");
                    string str = InputBox.MessageText("Enter a new profile name:");
                    cmbPresetsList.Properties.Items.Add(str);
                    cmbPresetsList.Refresh();
                    cmbPresetsList.SelectedItem = str;
                    break;
                case "Delete":
                    if (XtraMessageBox.Show(Form.ActiveForm, "Caption", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmbPresetsList.Properties.Items.Remove(cmbPresetsList.SelectedItem);
                        cmbPresetsList.Refresh();
                        cmbPresetsList.SelectedIndex = cmbPresetsList.Properties.Items.Count - 1;
                    }
                    break;
                default:
                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //PManager.SaveSettings(pManager, currCharClass);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //pManager = PManager.LoadSettings();
        }

        private void cmbPresetsList_SelectItem(object sender, EventArgs e)
        {
            /*
            var s = (KeyValuePair<string, PList>)presetsList.Properties.Items[presetsList.SelectedIndex];
            Core.DebugWriteLine(string.Format("Key = {0}", s.Key));

            try
            {
                this.gridControlPowers.DataSource = s.Value.Powers;
            }
            catch (Exception)
            {

                throw;
            }
            */
        }
    }
}
