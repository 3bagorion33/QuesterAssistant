using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using MyNW.Internals;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuesterAssistant.Panels;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Enums;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.PowersManager
{
    internal partial class PowersManagerForm : CoreForm
    {
        private PowersManagerCore Core => core as PowersManagerCore;
        private PowersManagerData Data => Core.Data;
        private Preset CurrentPreset => lcPresetsList.CurrentItem as Preset ?? new Preset();
        private ParagonCategory prevCharParagon = ParagonCategory.Unknown;

        public PowersManagerForm()
        {
            InitializeComponent();
            lcPresetsList.EditValueChanged += lcPresetsList_EditValueChanged;
        }

        private void PowersManagerForm_Load(object sender, EventArgs e)
        {
            Data.HashChanged += gridControlPowers_SafeUpdate;
            Data.HashChanged += bsrcGlobHotKey.ResetCurrentItem;

            bsrcGlobHotKey.DataSource = Data.HotKey;
            chkHotKeys.BindAdd(bsrcGlobHotKey, nameof(CheckEdit.Checked), nameof(PowersManagerData.HotKey.Enabled));
            tedGlobHotKey.BindAdd(bsrcGlobHotKey, nameof(TextEdit.Text), nameof(PowersManagerData.HotKey.String), DataSourceUpdateMode.OnValidation);

            timerCharCheck.Tick += CharCheck;
        }

        private void lcPresetsList_EditValueChanged(object sender, EventArgs e)
        {
            gridControlPowers_SafeUpdate();

            tedCurrHotKey.DataBindings.Clear();
            tedCurrHotKey.BindAdd(CurrentPreset.HotKey, nameof(TextEdit.Text), nameof(Preset.HotKey.String), DataSourceUpdateMode.Never);
        }

        private void gridControlPowers_SafeUpdate()
        {
            gridControlPowers.InvokeSafe(() =>
                gridControlPowers.DataSource = CurrentPreset?.PowersList.Select(x => x.ToDispName()).ToList() ?? new List<Power>());
        }

        private void lcPresetsList_SafeBindSource()
        {
            gridControlPowers.InvokeSafe(() =>
            {
                gCtrlPowersPresets.Enabled = Paragon.Category != ParagonCategory.Unknown;
                lcPresetsList.BindSource<Preset>(Data.ParagonPresets);
            });
        }

        private void CharCheck(object sender, EventArgs e)
        {
            labelCharacterName.Text = $@"Paragon: {Paragon.DisplayName}";
            labelCharacterClass.Text = $@"Class: {EntityManager.LocalPlayer.Character.Class.DisplayName}";

            if (Paragon.Category != prevCharParagon)
            {
                prevCharParagon = Paragon.Category;
                lcPresetsList_SafeBindSource();
            }
        }

        private void btnGetPowers_Click(object sender, EventArgs e)
        {
            if (Paragon.IsValid && Data.ParagonPresets.Any())
            {
                CurrentPreset.PowersList = Powers.GetSlottedPowers();
            }
        }

        private void btnSetPowers_Click(object sender, EventArgs e)
        {
            if (Paragon.IsValid && Data.ParagonPresets.Any())
            {
                Powers.ApplyPowers(CurrentPreset?.PowersList);
            }
        }

        private void tedCurrHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (Paragon.IsValid && Data.ParagonPresets.Any())
            {
                if (e.KeyCode.IsNotModifier())
                {
                    ActiveControl = null;
                    if (Data.ParagonPresets.Any(x => x.HotKey.Keys == e.KeyData && x.Name != CurrentPreset.Name))
                    {
                        ErrorBox.Show("This hotkey is already in use.");
                        tedCurrHotKey.DataBindings[0].ReadValue();
                        return;
                    }
                    CurrentPreset.HotKey.String = e.KeyData.IgnoreBack().ConvertToString();
                }
                tedCurrHotKey.Text = e.KeyData.ConvertToString();
            }
        }

        private void tedGlobHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            tedGlobHotKey.Text = e.KeyData.ConvertToString();
            if (e.KeyCode.IsNotModifier())
            {
                ActiveControl = null;
                Data.HotKey.String = e.KeyData.IgnoreBack().ConvertToString();
            }
        }
    }
}
