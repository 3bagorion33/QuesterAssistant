﻿using Astral.Classes.ItemFilter;
using QuesterAssistant.UIEditors.Forms;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Conditions;

namespace QuesterAssistant.UIEditors
{
    internal class ItemFilterEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context.Instance is AuctionItemCount checkNode && checkNode.GetItemsFromParentAction(out var items))
                return items;
            ItemFilterForm.Show(value as ItemFilterCore, ItemFilterCoreType.Items);
            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}
