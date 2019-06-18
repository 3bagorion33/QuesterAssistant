using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class DonateTaskEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Coffer coffer = value as Coffer;
            Coffer cofferNew = DonateTaskEditorForm.Show(coffer);
            if (cofferNew != null)
            {
                return cofferNew;
            }
            return coffer;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
