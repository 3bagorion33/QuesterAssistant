using Astral.Quester.Classes;
using Astral.Quester.Forms;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes;
using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Panels;
using ConditionList = System.Collections.Generic.List<Astral.Quester.Classes.Condition>;

namespace QuesterAssistant.UIEditors.Forms
{
    public partial class ConditionListForm : XtraForm
    {
        private Condition conditionCopy;

        public ConditionListForm()
        {
            InitializeComponent();
        }

        private void bntAdd_Click(object sender, EventArgs e)
        {
            if (AddAction.Show(typeof(Condition)) is Condition condition)
            {
                Conditions.Items.Add(condition);
                Conditions.SelectedItem = condition;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (Conditions.SelectedIndex >= 0
                && QMessageBox.ShowDialog("Are you sure to remove selected condition ?", "Remove Condition ?") == DialogResult.Yes)
            {
                Conditions.Items.RemoveAt(Conditions.SelectedIndex);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (Conditions.SelectedItem is Condition cond && SetConditionCopy(cond))
                QMessageBox.ShowInfo($"Condition {cond} copied!");
            else QMessageBox.ShowError($"Error while copying of the condition!");
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            Condition cond = GetConditionCopy();
            if (cond != null)
            {
                int ind = Conditions.Items.Add(CopyHelper.CreateDeepCopy(cond));
                Conditions.SetItemChecked(ind, cond.Locked);
                Conditions.SelectedIndex = ind;
                Conditions.Refresh();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Conditions.SelectedItem is Condition cond)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(cond.TestInfos).AppendLine();
                sb.Append("Result: ").Append(cond.IsValid);

                QMessageBox.ShowInfo(sb.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bntCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Conditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.SelectedObject = Conditions.SelectedItem;
        }

        private void Conditions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Conditions.SelectedItem is Condition condition)
            {
                condition.Locked = (e.NewValue == CheckState.Checked);
                Properties.Refresh();
            }
        }

        private void Properties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Locked")
            {
                Conditions.SetItemChecked(Conditions.SelectedIndex, e.ChangedItem.Value.Equals(true));
                Conditions.Refresh();
            }
        }

        public ConditionList GetConditionList(ConditionList conditions = null)
        {
            Conditions.Items.Clear();
            if (conditions != null)
            {
                // Отображаем список условий
                foreach (Condition condition in conditions)
                {
                    int ind = Conditions.Items.Add(CopyHelper.CreateDeepCopy(condition));
                    Conditions.SetItemChecked(ind, condition.Locked);
                }
            }

            ShowDialog();

            if (DialogResult == DialogResult.OK)
            {
                // Формируем новый список условий
                ConditionList newConditions = new ConditionList();
                foreach (object item in Conditions.Items)
                {
                    if (item is Condition condition)
                        newConditions.Add(condition);
                }
                return newConditions;
            }
            return conditions;
        }

        /// <summary>
        /// Получении копии Condition из приватного поля copiedCondition в QuesterEditor'е
        /// </summary>
        /// <returns></returns>
        protected Condition GetConditionCopy()
        {
            Editor qEditor = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form is Editor editor)
                {
                    qEditor = editor;
                    break;
                }
            }

            if (qEditor == null) return null;
            var cond = qEditor.GetFieldValue("copiedCondition") as Condition;
            return CopyHelper.CreateDeepCopy(cond);
        }

        /// <summary>
        /// Сохранение копии Condition в приватное поле copiedCondition в QuesterEditor
        /// </summary>
        /// <returns></returns>
        protected bool SetConditionCopy(Condition cond)
        {
            Editor qEditor = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form is Editor editor)
                {
                    qEditor = editor;
                    break;
                }
            }

            if (qEditor == null) return false;
            // помещаем копию условия в буфер редактора QuesterEditor
            if (qEditor.SetFieldValue("copiedCondition", CopyHelper.CreateDeepCopy(cond)))
            {
                // Включаем кнопку "Вставки условия"
                var btnPaste = qEditor.GetFieldValue("b_PasteCond");
                if (btnPaste != null)
                    return btnPaste.SetPropertyValue("Enabled", true, BindingFlags.Instance | BindingFlags.Public, true);
            }
            return false;
        }
    }
}