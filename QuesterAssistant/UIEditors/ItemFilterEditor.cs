using Astral.Classes.ItemFilter;
using QuesterAssistant.UIEditors.Forms;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace QuesterAssistant.UIEditors
{
    internal class ItemFilterEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ItemFilterForm.Show(value as ItemFilterCore, ItemFilterCoreType.Items);
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
