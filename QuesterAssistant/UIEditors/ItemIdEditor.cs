using System;
using System.ComponentModel;
using System.Drawing.Design;
using Astral.Classes.ItemFilter;
using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class ItemIdEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            GetAnItem.ListItem listItem = GetAnItem.Show(ItemFilterType.ItemID, context.Instance is IMailCollectAction ? 6 : 0);
            if (listItem != null)
                return listItem.ItemId;
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}