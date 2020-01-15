using System;
using System.ComponentModel;
using System.Drawing.Design;
using Astral.Classes.ItemFilter;
using QuesterAssistant.Conditions;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class ItemIdFilterEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context.Instance is AuctionItemCount checkNode && checkNode.GetItemsFromParentAction(out var items))
                return items;
            ItemFilterForm.Show(value as ItemFilterCore, ItemFilterCoreType.ItemsID);
            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}