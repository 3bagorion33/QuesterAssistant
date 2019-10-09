using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace QuesterAssistant.UIEditors
{
    class ParagonSelectEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            SelectedParagons paragons = PlayerParagonSelectForm.GetParagons(value as SelectedParagons);

            if (paragons != null)
                return paragons;
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}