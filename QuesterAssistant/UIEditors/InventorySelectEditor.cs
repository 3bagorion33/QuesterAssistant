using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Controls;
using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class InventorySelectEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            InventorySelectForm.Show(value as InventorySelect);
            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}