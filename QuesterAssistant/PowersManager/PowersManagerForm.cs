using Astral.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Enums;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuesterAssistant.Panels;

namespace QuesterAssistant.PowersManager
{
    public partial class PowersManagerForm : BasePanel
    {
        private PowersManagerData pManager = Core.PowersManager.Data;
        private ParagonCategory prevCharParagon;

        public PowersManagerForm() : base("Powers Manager")
        {
            InitializeComponent();
#if DEBUG
            Astral.Functions.XmlSerializer.Serialize(Path.Combine(Core.SettingsPath, "PowersManager_deb.xml"), pManager);
#endif
            chkHotKeys_Update();
            tedGlobHotKey_Update();

            Panels.Main.OnLoadSettings += LoadSettings;
            Panels.Main.OnSaveSettings += SaveSettings;
        }

        private void SaveSettings(string tabName)
        {
            if (tabName == "pManagerTab")
            {
                Core.PowersManager.SaveSettings();
            }
        }

        private void LoadSettings(string tabName)
        {
            if (tabName == "pManagerTab")
            {
                Core.PowersManager.LoadSettings();
                cmbPresetsList_Update();
                chkHotKeys_Update();
            }
        }

        private void CharCheck(object sender, EventArgs e)
        {
            labelCharacterName.Text = $"Paragon: {Paragon.DisplayName}";
            labelCharacterClass.Text = $"Class: {EntityManager.LocalPlayer.Character.Class.DisplayName}";

            if (Paragon.Category != prevCharParagon)
            {
                cmbPresetsList_Update();
                prevCharParagon = Paragon.Category;
            }
            powerListSource_Update();
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
                switch (e.Button.Caption)
                {
                    case "Add":
                        string _add = InputBox.MessageText("Enter a new preset name:");
                        if (_add.Any())
                        {
                            pManager.CurrPresets.AddOrReplace(x => x.Name == _add, new Preset(_add, Powers.GetSlottedPowers()));
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
                        if (pManager.CurrPresets.Any())
                        {
                            var selected = cmbPresetsList.SelectedItem;
                            Astral.Professions.Forms.ChangeItemsOrder<Preset>.Show(pManager.CurrPresets, "Change presets order :");
                            cmbPresetsList_Update();
                            cmbPresetsList.SelectedItem = selected;
                        }
                        break;

                    case "Rename":
                        if (pManager.CurrPresets.Any())
                        {
                            var _preset = cmbPresetsList.SelectedItem as Preset;
                            string _ren = InputBox.MessageText("Enter a new name for this preset:", _preset.Name);
                            if (_ren.Any())
                            {
                                _preset.Name = _ren;
                                var _selIdx = cmbPresetsList.SelectedIndex;
                                cmbPresetsList_Update(_selIdx);
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void cmbPresetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            powerListSource_Update();
            tedCurrHotKey_Update();
        }

        private void tedCurrHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            // KeyCode - последняя нажатая клавиша
            // KeyData - все нажатые клавиши
            if (Paragon.IsValid && pManager.CurrPresets.Any())
            {
                KeysConverter kc = new KeysConverter();
                if (e.Shift || e.Control || e.Alt)
                {
                    string str = kc.ConvertToString(e.Modifiers);
                    tedCurrHotKey.Text = str.Remove(str.Length - 4);
                }

                if (e.KeyCode != Keys.LWin && e.KeyCode != Keys.RWin && e.KeyCode != Keys.ShiftKey &&
                    e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.Apps)
                {
                    base.ActiveControl = null;
                    if (pManager.CurrPresets.Exists(x => x.Keys == e.KeyData))
                    {
                        XtraMessageBox.Show("This hot keys is already in use.");
                        return;
                    }
                    pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex).Keys = (e.KeyCode != Keys.Back) ? e.KeyData : Keys.None;
                    tedCurrHotKey_Update();
                }
            }
        }

        private void tedGlobHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            if (e.Shift || e.Control || e.Alt)
            {
                string str = kc.ConvertToString(e.Modifiers);
                tedGlobHotKey.Text = str.Remove(str.Length - 4);
            }

            if (e.KeyCode != Keys.LWin && e.KeyCode != Keys.RWin && e.KeyCode != Keys.ShiftKey &&
                e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.Apps)
            {
                base.ActiveControl = null;
                pManager.Keys = (e.KeyCode != Keys.Back) ? e.KeyData : Keys.None;
                tedGlobHotKey_Update();
            }
        }

        private void cmbPresetsList_Update(int selIdx = -1)
        {
            cmbPresetsList.Properties.Items.Clear();
            cmbPresetsList.Properties.Items.BeginUpdate();
            try
            {
                if (pManager.CurrPresets.Any())
                {
                    cmbPresetsList.Properties.Items.AddRange(pManager.CurrPresets);
                    if (selIdx == -1) selIdx = 0;
                }
            }
            finally
            {
                cmbPresetsList.Properties.Items.EndUpdate();
            }
            cmbPresetsList.SelectedIndex = selIdx;
        }

        private void powerListSource_Update()
        {
            powerListSource.DataSource = pManager.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.PowersList.Select(x => x.ToDispName()).ToList();
        }

        private void tedGlobHotKey_Update()
        {
            tedGlobHotKey.Text = pManager?.HotKeys ?? null;
        }

        private void tedCurrHotKey_Update()
        {
            tedCurrHotKey.Text = pManager.CurrPresets?.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.HotKeys ?? null;
        }

        private void chkHotKeys_Update()
        {
            chkHotKeys.Checked = pManager.HotKeysEnabled;
        }

        private void chkHotKeys_CheckedChanged(object sender, EventArgs e)
        {
            pManager.HotKeysEnabled = chkHotKeys.Checked;
        }
    }
}
