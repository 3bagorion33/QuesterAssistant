using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes;
using QuesterAssistant.Panels;
using static QuesterAssistant.InsigniaManager.InsigniaManagerData;

namespace QuesterAssistant.InsigniaManager
{
    internal partial class InsigniaManagerForm : CoreForm
    {
        private InsigniaManagerCore Core => core as InsigniaManagerCore;
        private InsigniaManagerData Data => Core.Data;
        public Preset CurrentPreset => lcProfilesList.CurrentItem as Preset ?? new Preset();

        public InsigniaManagerForm()
        {
            InitializeComponent();
            lcProfilesList.EditValueChanged += lcProfilesList_EditValueChanged;
            lcProfilesList.ListChanged += lcProfilesList_ListChanged;
        }

        private void InsigniaManagerForm_Load(object sender, EventArgs e)
        {
            lcProfilesList.BindSource<Preset>(Data.Presets);
            Data.HashChanged += gctlMounts_SafeUpdate;
        }

        private void gctlMounts_SafeUpdate()
        {
            gctlMounts.InvokeSafe(() =>
                //gctlMounts.DataSource = CurrentPreset?.MountSlots.Select(x => x.ToDispName()).ToList() ?? new List<Power>());
                gctlMounts.DataSource = CurrentPreset?.MountSlots ?? new List<MountDef>());
        }

        private void lcProfilesList_EditValueChanged(object sender, EventArgs e)
        {
            gctlMounts.InvokeSafe(() => gctlMounts.DataSource = CurrentPreset?.MountSlots ?? new List<MountDef>());

            //numIterationCont.DataBindings.Clear();
            //numIterationCont.BindAdd(CurrentPreset, nameof(SpinEdit.EditValue), nameof(Preset.IterationsCount));

            //cbxAlgorithm.DataBindings.Clear();
            //cbxAlgorithm.BindAdd(CurrentPreset, nameof(ComboBoxEdit.EditValue), nameof(Preset.Algorithm));

            //cbxRunCondition.DataBindings.Clear();
            //cbxRunCondition.BindAdd(CurrentPreset, nameof(ComboBoxEdit.EditValue), nameof(Preset.RunCondition));
        }

        private void lcProfilesList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //gridMounts.OptionsView.NewItemRowPosition =
            //    ((LookUpEdit)sender).ItemIndex == -1 ? NewItemRowPosition.None : NewItemRowPosition.Bottom;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            CurrentPreset.GetMounts();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            Insignia.ExtractFromEquippedMounts();
            if (ModifierKeys.HasFlag(Keys.Control))
                Insignia.ExtractFromPassiveMounts();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            CurrentPreset.Apply();
        }
    }
}
