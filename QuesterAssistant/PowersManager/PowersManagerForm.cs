using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Enums;
using System;
using System.Linq;
using System.Windows.Forms;
using QuesterAssistant.Panels;
using QuesterAssistant.Classes.Common.Extensions;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.PowersManager
{
    internal partial class PowersManagerForm : CoreForm
    {
        private PowersManagerCore Core => core as PowersManagerCore;
        private PowersManagerData Data => Core.Data;
        private ParagonCategory prevCharParagon;

        public PowersManagerForm()
        {
            InitializeComponent();
        }

        private void SettingsLoaded()
        {
            cmbPresetsList_Update();
            chkHotKeys_Update();
            tedGlobHotKey_Update();
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
            if (Paragon.IsValid && Data.CurrPresets.Any())
            {
                Data.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex).PowersList = Powers.GetSlottedPowers();
                powerListSource_Update();
            }
        }

        private void btnSetPowers_Click(object sender, EventArgs e)
        {
            if (Paragon.IsValid && Data.CurrPresets.Any())
            {
                Powers.ApplyPowers(Data.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.PowersList);
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
                            Data.CurrPresets.AddOrReplace(x => x.Name == _add, new Preset(_add, Powers.GetSlottedPowers()));
                            cmbPresetsList_Update(cmbPresetsList.Properties.Items.Count);
                        }
                        break;

                    case "Delete":
                        if (Data.CurrPresets.Any() &&
                            (XtraMessageBox.Show(Form.ActiveForm, "Delete this preset?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            Data.CurrPresets.RemoveAt(cmbPresetsList.SelectedIndex);
                            cmbPresetsList_Update();
                        }
                        break;

                    case "Sort":
                        if (Data.CurrPresets.Any())
                        {
                            var selected = cmbPresetsList.SelectedItem;
                            Astral.Professions.Forms.ChangeItemsOrder<Preset>.Show(Data.CurrPresets, "Change presets order :");
                            cmbPresetsList_Update();
                            cmbPresetsList.SelectedItem = selected;
                        }
                        break;

                    case "Rename":
                        if (Data.CurrPresets.Any())
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
            if (Paragon.IsValid && Data.CurrPresets.Any())
            {
                tedCurrHotKey.Text = e.KeyData.ConvertToString();
                if (e.KeyCode.IsNotModifier())
                {
                    ActiveControl = null;
                    if (Data.CurrPresets.Exists(x => x.HotKey.Keys == e.KeyData && x.Name != (cmbPresetsList.SelectedItem as Preset).Name))
                    {
                        XtraMessageBox.Show("This hotkey is already in use.");
                        return;
                    }
                    Data.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex).HotKey.String = e.KeyData.IgnoreBack().ConvertToString();
                    tedCurrHotKey_Update();
                }
            }
        }

        private void tedGlobHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            tedGlobHotKey.Text = e.KeyData.ConvertToString();

            if (e.KeyCode.IsNotModifier())
            {
                ActiveControl = null;
                Data.HotKey.String = e.KeyData.IgnoreBack().ConvertToString();
                tedGlobHotKey_Update();
            }
        }

        private void cmbPresetsList_Update(int selIdx = -1)
        {
            cmbPresetsList.Properties.Items.Clear();
            cmbPresetsList.Properties.Items.BeginUpdate();
            try
            {
                if (Data.CurrPresets.Any())
                {
                    cmbPresetsList.Properties.Items.AddRange(Data.CurrPresets);
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
            gridControlPowers.InvokeSafe(() =>
            powerListSource.DataSource = Data.CurrPresets.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.PowersList.Select(x => x.ToDispName()).ToList()
            );
        }

        private void tedGlobHotKey_Update()
        {
            tedGlobHotKey.Text = Data?.HotKey.String ?? null;
        }

        private void tedCurrHotKey_Update()
        {
            tedCurrHotKey.Text = Data.CurrPresets?.ElementAtOrDefault(cmbPresetsList.SelectedIndex)?.HotKey.String ?? null;
        }

        private void chkHotKeys_Update()
        {
            chkHotKeys.Checked = Data.HotKey.Enabled;
        }

        private void chkHotKeys_CheckedChanged(object sender, EventArgs e)
        {
            Data.HotKey.Enabled = chkHotKeys.Checked;
        }

        private void PowersManagerForm_Load(object sender, EventArgs e)
        {
            Core.SettingsLoaded += SettingsLoaded;

            chkHotKeys_Update();
            tedGlobHotKey_Update();
        }
    }
}
