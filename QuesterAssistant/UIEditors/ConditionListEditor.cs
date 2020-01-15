﻿using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using QuesterAssistant.UIEditors.Forms;
using ConditionList = System.Collections.Generic.List<Astral.Quester.Classes.Condition>;


namespace QuesterAssistant.UIEditors
{
    class ConditionListEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ConditionListForm listEditor = new ConditionListForm();
            ConditionList newConditions = listEditor.GetConditionList(value as ConditionList);
            return listEditor.DialogResult == DialogResult.OK ? newConditions : value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}
