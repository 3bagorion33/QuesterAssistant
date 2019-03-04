﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace QuesterAssistant.UIEditors
{
    internal class CheckedListBoxEditor<T> : UITypeEditor
    {
        private bool isListLoaded = false;
        private CheckedListBox cbx = new CheckedListBox();
        private object value;
        private IWindowsFormsEditorService es;
        public override bool IsDropDownResizable => true;
        private ToolTip toolTip;
        private int toolTipIndex;

        internal CheckedListBoxEditor()
        {
            cbx.Leave += bx_Leave;
            cbx.KeyDown += cbx_KeyDown;
            cbx.MouseHover += cbx_ShowTooltip;
            toolTip = new ToolTip() { ToolTipTitle = "Ctrl+A to select all, Ctrl+D to deselect, Ctrl+I to inverse, Ctrl+S to sort" };
        }

        private void cbx_ShowTooltip(object sender, EventArgs e)
        {
            toolTipIndex = cbx.IndexFromPoint(cbx.PointToClient(Control.MousePosition));
            if (toolTipIndex > -1)
            {
                toolTip.SetToolTip(cbx, cbx.Items[toolTipIndex].ToString());
            }
        }

        private void cbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.SuppressKeyPress = true;
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, true);
                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, false);
                }
            }
            if (e.Control && e.KeyCode == Keys.I)
            {
                e.SuppressKeyPress = true;
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, !cbx.GetItemChecked(i));
                }
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.Sorted = true;
                }
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            es = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (es != null)
            {
                LoadListBoxItems(value);
                //cbx.Sorted = true;
                es.DropDownControl(cbx);
            }
            return this.value;
        }

        private void LoadListBoxItems(object value)
        {
            if (!isListLoaded)
            {
                int idx = 0;
                var dict = value as CheckedListBoxSelector<T>;
                foreach (var item in dict.Dictionary)
                {
                    cbx.Items.Add(item.Key);
                    cbx.SetItemChecked(idx, item.Value);
                    idx++;
                }
                isListLoaded = true;
                this.value = value;
            }
        }

        private void bx_Leave(object sender, EventArgs e)
        {
            var dict = new Dictionary<T, bool>();
            for (int i = 0; i < cbx.Items.Count; i++)
            {
                var item = (T)cbx.Items[i];
                dict.Add(item, cbx.GetItemChecked(i));
            }
            var listSelector = value as CheckedListBoxSelector<T>;
            listSelector.Dictionary = dict;
        }
    }

    [Serializable]
    public class CheckedListBoxSelector<TEnum>
    {
        public List<TEnum> Items = new List<TEnum>();
        [XmlIgnore]
        public Dictionary<TEnum, bool> Dictionary
        {
            get
            {
                var value = new Dictionary<TEnum, bool>();
                foreach (var s in Enum.GetNames(typeof(TEnum)))
                {
                    if (s != "None")
                    {
                        var item = (TEnum)Enum.Parse(typeof(TEnum), s);
                        value.Add(item, Items.Contains(item));
                    }
                }
                return value;
            }
            set
            {
                Items.Clear();
                foreach (var item in value)
                {
                    if (item.Value)
                    {
                        Items.Add(item.Key);
                    }
                }
            }
        }
        public override string ToString()
        {
            return typeof(TEnum).Name;
        }
    }
}
