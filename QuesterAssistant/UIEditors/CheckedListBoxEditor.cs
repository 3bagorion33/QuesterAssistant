using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QuesterAssistant.UIEditors
{
    internal class CheckedListBoxEditor<T> : UITypeEditor
    {
        private bool isListLoaded = false;
        private CheckedListBox cbx = new CheckedListBox();
        private object value;
        private IWindowsFormsEditorService es;
        public override bool IsDropDownResizable => true;

        internal CheckedListBoxEditor()
        {
            cbx.Leave += bx_Leave;
            cbx.KeyDown += cbx_KeyDown;
        }

        private void cbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, true);
                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, false);
                }
            }
            if (e.Control && e.KeyCode == Keys.I)
            {
                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    cbx.SetItemChecked(i, !cbx.GetItemChecked(i));
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
                cbx.Sorted = true;
                es.DropDownControl(cbx);
            }
            return this.value;
        }

        private void LoadListBoxItems(object value)
        {
            if (!isListLoaded)
            {
                int idx = 0;
                foreach (var item in value as Dictionary<T, bool>)
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
            value = dict;
        }
    }
}
