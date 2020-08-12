using System;
using System.ComponentModel;
using System.Linq;
using Astral.Classes.ItemFilter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    public partial class ItemFilterAddEntry : XtraForm
    {
        private ItemFilterEntry itemFilterEntry;

        private ItemFilterCoreType itemFilterCoreType;

        public ItemFilterAddEntry()
        {
            InitializeComponent();
            FillFilterTypes();
            bReverse.Visible = false;
            filterMode.Properties.Items.AddEnum<ItemFilterMode>();
            filterMode.SelectedIndex = 0;
            filterStringType.Properties.Items.AddEnum<ItemFilterStringType>();
            filterStringType.SelectedIndex = 0;
        }

        public static ItemFilterEntry Show(ItemFilterCoreType itemFilterCoreType)
        {
            ItemFilterAddEntry itemFilterAddEntry = new ItemFilterAddEntry();
            itemFilterAddEntry.itemFilterCoreType = itemFilterCoreType;
            itemFilterAddEntry.Text = $@"Add entry to {itemFilterCoreType} filter - Advanced";
            itemFilterAddEntry.FillFilterTypes();
            itemFilterAddEntry.ShowDialog();
            return itemFilterAddEntry.itemFilterEntry;
        }

        private void FillFilterTypes()
        {
            filterType.Properties.Items.Clear();
            foreach (ItemFilterType itemFilterType in ItemFilterCoreEx.GetFilterTypes(itemFilterCoreType))
            {
                filterType.Properties.Items.Add(itemFilterType);
            }
            filterType.SelectedIndex = 0;
        }

        private void filterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bReverse.Visible = false;
            filterTypeInfos.Text = ItemFilterCoreEx.GetFilterCollection((ItemFilterType)filterType.EditValue).Description;
            if ((ItemFilterType)filterType.EditValue == ItemFilterType.ItemID)
            {
                bReverse.Visible = true;
            }
            textName.Text = filterType.Text + " :";
        }

        private void filterText_SelectedIndexChanged(object sender, EventArgs e) { }
        private void filterText_QueryCloseUp(object sender, CancelEventArgs e) { }

        private void filterText_QueryPopUp(object sender, CancelEventArgs e)
        {
            filterText.Properties.Items.Clear();
            foreach (string item in ItemFilterCoreEx.GetFilterCollection((ItemFilterType)filterType.EditValue, bReverse.Checked).Values)
            {
                filterText.Properties.Items.Add(item);
            }
        }

        private void filterText_EditValueChanging(object sender, ChangingEventArgs e)
        {
            string text = e.NewValue.ToString();
            if (text.Contains('['))
            {
                if (bReverse.Checked)
                {
                    int num = text.IndexOf(']');
                    if (num > 1)
                    {
                        string newValue = text.Remove(0, num + 2);
                        e.NewValue = newValue;
                    }
                }
                else
                {
                    int num2 = text.IndexOf('[');
                    if (num2 > 1)
                    {
                        string newValue2 = text.Remove(num2 - 1);
                        e.NewValue = newValue2;
                    }
                }
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (filterText.Text.Length == 0)
            {
                XtraMessageBox.Show("Text field is empty !");
                filterText.Focus();
                return;
            }
            itemFilterEntry = new ItemFilterEntry
            {
                Text = filterText.Text,
                Mode = (ItemFilterMode)filterMode.EditValue,
                StringType = (ItemFilterStringType)filterStringType.EditValue,
                Type = (ItemFilterType)filterType.EditValue
            };
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QMessageBox.ShowInfo("Simple : you can use * char at the start or the end of the text\r\nRegex: for advanced users");
        }

        private void bReverse_CheckedChanged(object sender, EventArgs e) { }
        private void ItemFilterAddEntry_Load(object sender, EventArgs e) { }
    }
}
