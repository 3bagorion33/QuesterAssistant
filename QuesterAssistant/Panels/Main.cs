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
        PManager pManager = new PManager();

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
            if (SlottedPower.CanUpdate)
            {
                if (currCharClass != prevCharClass)
                {
                    OnCharClassChanged();
                    prevCharClass = currCharClass;
                }
            }
        }

        internal Dictionary<TraySlot, Power> slottedPowers;

        private void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("Name:  {0}",
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("Class:  {0}",
                EntityManager.LocalPlayer.Character.Class.DisplayName);

            slottedPowers = SlottedPower.GetSlottedPowers();
            this.gridControlPowers.DataSource = slottedPowers;
        }

        public Main() : base ("Quester Assistant")
        {
            InitializeComponent();
            Initialize();
            OnPanelLeave += this.Dispose;
        }

        private void buttonGetPowers_Click(object sender, EventArgs e)
        {
            if (SlottedPower.CanUpdate)
            {
                OnCharClassChanged();
            }
        }

        private void buttonSetPowers_Click(object sender, EventArgs e)
        {
            if (SlottedPower.CanUpdate)
            {
                foreach (var pwr in slottedPowers)
                {
                    Task.Factory.StartNew(() => SlottedPower.ApplyPower(pwr.Key, pwr.Value.PowerDef.InternalName));
                }
            }
        }

        private void setsList_ButtonClick(object sender, ButtonPressedEventArgs e)
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
                    setsList.Properties.Items.Add(str);
                    setsList.Refresh();
                    setsList.SelectedItem = str;
                    break;
                case "Delete":
                    if (XtraMessageBox.Show(Form.ActiveForm, "Caption", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        setsList.Properties.Items.Remove(setsList.SelectedItem);
                        setsList.Refresh();
                        setsList.SelectedIndex = setsList.Properties.Items.Count - 1;
                    }
                    break;
                default:
                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            PList pList = new PList
            {
                Powers = SlottedPower.GetSlottedPowersNames()
            };

            pManager.CharClasses[currCharClass].PLists.Add("Test Preset", pList);
            pManager.CharClasses[currCharClass].PLists.Add("Test Preset2", pList);

            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"), pManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                pManager = Astral.Functions.XmlSerializer.Deserialize<PManager>(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager.xml"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
    }
}
