﻿using Astral.Classes.ItemFilter;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Classes.ItemFilter.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class ItemFilterEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ItemFilterForm.Show(value as ItemFilterCore, ItemFilterCoreType.Items);
            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}
