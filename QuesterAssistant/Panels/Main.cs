using Astral;
using Astral.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Hooks;
using QuesterAssistant.Classes.PowersManager;
using QuesterAssistant.Enums;
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

        private void Dispose(object s, EventArgs e)
        {
            Core.DebugWriteLine("Dispose event");
            this.Dispose(true);
        }

        #region Power Manager Tab

        PowersManagerData pManager = new PowersManagerData(true);
        private ParagonCategory prevCharParagon;

        private void InitializePManager()
        {
            pManager.LoadSettings();
#if DEBUG
            Astral.Functions.XmlSerializer.Serialize(Path.Combine(Astral.Controllers.Directories.SettingsPath, "PowersManager_deb.xml"), pManager);
#endif
            keyboardHook.KeysMask.AddRange(keysMask);
            keyboardHook.KeyDown += keyboardHook_KeyDown;

            chkHotKeys.Checked = pManager.HotKeysEnabled;
        }

        private void CharCheck(object sender, EventArgs e)
        {
            this.labelCharacterName.Text = "Paragon:  " +
                Paragon.DisplayName;

            this.labelCharacterClass.Text = "Class:  " +
                EntityManager.LocalPlayer.Character.Class.DisplayName;

            if (Paragon.Category != prevCharParagon)
            {
                cmbPresetsList_Update();
                prevCharParagon = Paragon.Category;
            }
            powerListSource_Update();
            //Core.DebugWriteLine(EntityManager.LocalPlayer.Character.CurrentPowerTreeBuild.SecondaryPaths.FirstOrDefault()?.Path.PowerTree.Name + " => \n" +
            //    EntityManager.LocalPlayer.Character.CurrentPowerTreeBuild.SecondaryPaths.FirstOrDefault()?.Path.PowerTree.DisplayName);
        }

        private void FormUpdate(object sender, EventArgs e)
        {
        }

        private void powerListSource_Update()
        {
            powerListSource.DataSource = pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.PowersList.Select(x => x.ToDispName()).ToList();
        }

        private void btnGetPowers_Click(object sender, EventArgs e)
        {
            if (Paragon.IsValid && pManager.CurrPresets.Any())
            {
                pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex).PowersList = Powers.GetSlottedPowers();
                powerListSource_Update();
            }
        }

        private void btnSetPowers_Click(object sender, EventArgs e)
        {
            if (Paragon.IsValid && pManager.CurrPresets.Any())
            {
                Powers.ApplyPowers(pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.PowersList);
            }
        }

        private void cmbPresetsList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (Paragon.IsValid)
            {
                Core.DebugWriteLine(string.Format("Pressed button: {0}", e.Button.Caption));

                switch (e.Button.Caption)
                {
                    case "Add":
                        string str = InputBox.MessageText("Enter a new profile name:");
                        if (str.Any())
                        {
                            pManager.CurrPresets.AddOrReplace(x => x.Name == str, new Preset(str, Powers.GetSlottedPowers()));
                            cmbPresetsList_Update(cmbPresetsList.Properties.Items.Count);
                        }
                        break;

                    case "Delete":
                        if (pManager.CurrPresets.Any() &&
                            (XtraMessageBox.Show(Form.ActiveForm, "Delete this preset?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            pManager.CurrPresets.RemoveAt(cmbPresetsList.SelectedIndex);
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
            powerListSource_Update();
            tedHotKey_Update();
        }

        private void cmbPresetsList_Update(int selIdx = -1)
        {
            cmbPresetsList.Properties.Items.Clear();
            cmbPresetsList.Properties.Items.BeginUpdate();
            try
            {
                if (pManager.CurrPresets.Any())
                {
                    foreach (var item in pManager.CurrPresets)
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

        private static List<Keys> keysMask = new List<Keys> {
            Keys.LWin, Keys.RWin,
            Keys.LShiftKey, Keys.RShiftKey,
            Keys.LControlKey, Keys.RControlKey,
            Keys.LMenu, Keys.RMenu,
            Keys.Apps, Keys.Back
        };

        private void tedHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            // KeyCode - последняя нажатая клавиша
            // KeyData - все нажатые клавиши
            if (Paragon.IsValid && pManager.CurrPresets.Any())
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
                    pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex).Keys = (e.KeyCode != Keys.Back) ? e.KeyData : Keys.None;
                    tedHotKey_Update();
                }
            }
        }

        private void tedHotKey_Update()
        {
            tedHotKey.Text = pManager.CurrPresets?.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.HotKeys ?? null;
        }

        private KeyboardHook keyboardHook = new KeyboardHook();

        private void chkHotKeys_CheckedChanged(object sender, EventArgs e)
        {
            keyboardHook_Toggle();
            pManager.HotKeysEnabled = chkHotKeys.Checked;
        }

        private void keyboardHook_Toggle()
        {
            if (chkHotKeys.Checked)
            {
                Core.DebugWriteLine("Hook is starting");
                keyboardHook.Start();
            }
            else
            {
                Core.DebugWriteLine("Hook is stopping");
                keyboardHook.Stop();
            }
        }

        private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            Core.DebugWriteLine("Hooked " + pManager.CurrPresets?.Find(x => x.Keys == e.KeyData)?.Name);
            var _pres = pManager.CurrPresets?.Find(x => x.Keys == e.KeyData);
            if (_pres != null)
            {
                Logger.WriteLine("Applying preset with name '" + _pres.Name + "'...");
                Powers.ApplyPowers(_pres?.PowersList);
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTabPage.Equals(pManagerTab))
            {
                pManager.SaveSettings();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTabPage.Equals(pManagerTab))
            {
                pManager.LoadSettings();
                cmbPresetsList_Update();
            }
        }
    }
}
