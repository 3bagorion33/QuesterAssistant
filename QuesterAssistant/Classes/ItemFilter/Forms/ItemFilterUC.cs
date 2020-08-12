using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Astral.Classes.ItemFilter;
using Astral.Controllers;
using Astral.Functions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    public partial class ItemFilterUC : UserControl
    {
        private ItemFilterCoreType itemFilterCoreType;

        private ItemFilterCore itemFilterCore_0;

        public ItemFilterCoreType Type
        {
            get => itemFilterCoreType;
            set
            {
                this.itemFilterCoreType = value;
                bReverse.Visible = value == ItemFilterCoreType.ItemsID;
                ItemFilterCoreType itemFilterCoreType = this.itemFilterCoreType;
                if (itemFilterCoreType == ItemFilterCoreType.Items)
                {
                    addTypeLabel.Text = "Add item :";
                    return;
                }
                if (itemFilterCoreType != ItemFilterCoreType.Loots)
                {
                    return;
                }
                addTypeLabel.Text = "Add loot :";
            }
        }

        [Browsable(false)]
        public ItemFilterCore Filter
        {
            get => itemFilterCore_0;
            set
            {
                itemFilterCore_0 = value;
                if (value != null)
                {
                    bindingSource_0.DataSource = value.Entries;
                    return;
                }
                bindingSource_0.DataSource = null;
            }
        }

        public bool ShowExpand
        {
            get => bExpand.Visible;
            set => bExpand.Visible = value;
        }

        public ItemFilterUC()
        {
            InitializeComponent();
            RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();
            repositoryItemComboBox.QueryPopUp += this.method_0;
            repositoryItemComboBox.TextEditStyle = TextEditStyles.DisableTextEditor;
            bReverse.Visible = false;
            colFilterType.ColumnEdit = repositoryItemComboBox;
        }

        private void method_0(object sender, CancelEventArgs e)
        {
            ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
            comboBoxEdit.Properties.Items.Clear();
            foreach (ItemFilterType itemFilterType in ItemFilterCoreEx.GetFilterTypes(Type))
            {
                comboBoxEdit.Properties.Items.Add(itemFilterType);
            }
        }

        private void bRemoveEntry_Click(object sender, EventArgs e)
        {
            if (bindingSource_0.Current != null && QMessageBox.ShowDialog("Are you sure to delete this entry ?") == DialogResult.OK)
            {
                bindingSource_0.RemoveCurrent();
            }
        }

        private void bAddItem_Click(object sender, EventArgs e)
        {
            string text = itemListCombo.Text;
            if (text.Length > 0)
            {
                ItemFilterEntry itemFilterEntry = new ItemFilterEntry
                {
                    Text = text
                };
                switch (Type)
                {
                    case ItemFilterCoreType.Items:
                        itemFilterEntry.Type = ItemFilterType.ItemName;
                        break;
                    case ItemFilterCoreType.Loots:
                        itemFilterEntry.Type = ItemFilterType.Loot;
                        break;
                    case ItemFilterCoreType.ItemsID:
                        itemFilterEntry.Type = ItemFilterType.ItemID;
                        break;
                }
                this.method_2(itemFilterEntry);
                return;
            }
            XtraMessageBox.Show("Item name is empty !");
        }

        private void method_2(ItemFilterEntry itemFilterEntry)
        {
            if (itemFilterEntry != null)
            {
                bindingSource_0.Add(itemFilterEntry);
                bindingSource_0.Position = bindingSource_0.IndexOf(itemFilterEntry);
            }
        }

        private void itemListCombo_QueryPopUp(object sender, CancelEventArgs e)
        {
            itemListCombo.Properties.Items.Clear();
            switch (itemFilterCoreType)
            {
                case ItemFilterCoreType.Items:
                    itemListCombo.Properties.Items.AddRange(ItemFilterCoreEx.GetFilterCollection(ItemFilterType.ItemName).Values);
                    return;
                case ItemFilterCoreType.Loots:
                    itemListCombo.Properties.Items.AddRange(ItemFilterCoreEx.GetFilterCollection(ItemFilterType.Loot).Values);
                    return;
                case ItemFilterCoreType.ItemsID:
                    itemListCombo.Properties.Items.AddRange(ItemFilterCoreEx.GetFilterCollection(ItemFilterType.ItemID, bReverse.Checked).Values);
                    return;
                default:
                    return;
            }
        }

        private void itemListCombo_EditValueChanging(object sender, ChangingEventArgs e)
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

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) { }

        private void bAddEntryAdvanced_Click(object sender, EventArgs e)
        {
            addEntryPop.HidePopup();
            ItemFilterEntry itemFilterEntry = ItemFilterAddEntry.Show(itemFilterCoreType);
            if (itemFilterEntry != null)
            {
                bindingSource_0.Add(itemFilterEntry);
                bindingSource_0.Position = bindingSource_0.IndexOf(itemFilterEntry);
            }
        }

        private void addEntryMenu_Popup(object sender, EventArgs e) { }
        private void addEntryMenu_BeforePopup(object sender, CancelEventArgs e) { }

        private void bShowItems_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            List<string> list = new List<string>();
            foreach (InventorySlot inventorySlot in EntityManager.LocalPlayer.AllItems)
            {
                if (!list.Contains(inventorySlot.Item.ItemDef.DisplayName) && Filter.IsMatch(inventorySlot.Item))
                {
                    list.Add(inventorySlot.Item.ItemDef.DisplayName);
                    text +=
                        $"{inventorySlot.Item.ItemDef.DisplayName} [{inventorySlot.Item.ItemDef.InternalName}]{Environment.NewLine}";
                }
            }
            QMessageBox.ShowInfo(text);
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            if (QMessageBox.ShowDialog("Are you sure to clear the filter list ?") == DialogResult.OK)
            {
                method_3();
            }
        }

        private void method_3()
        {
            bindingSource_0.Clear();
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directories.SettingsPath;
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = itemFilterCoreType + " filter profile (*.xml)|*.xml";
            saveFileDialog.FileName = itemFilterCoreType + "Filters.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer.Serialize(saveFileDialog.FileName, Filter);
            }
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directories.SettingsPath;
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = itemFilterCoreType + " filter profile (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ItemFilterCore itemFilterCore = XmlSerializer.Deserialize<ItemFilterCore>(openFileDialog.FileName);
                if (itemFilterCore.Entries.Count > 0)
                {
                    DialogResult dialogResult = DialogResult.Abort;
                    if (Filter.Entries.Count > 0)
                        dialogResult = QMessageBox.ShowDialog("Add to current list ? (Else, clear it before)", "Import filters");

                    if (dialogResult == DialogResult.Cancel)
                        return;

                    if (Filter.Entries.Count != 0)
                    {
                        if (dialogResult != DialogResult.Yes)
                        {
                            method_3();
                            using (List<ItemFilterEntry>.Enumerator enumerator = itemFilterCore.Entries.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    ItemFilterEntry itemFilterEntry = enumerator.Current;
                                    method_2(itemFilterEntry);
                                }
                                return;
                            }
                        }
                    }
                    using (List<ItemFilterEntry>.Enumerator enumerator = itemFilterCore.Entries.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ItemFilterEntry itemFilterEntry = enumerator.Current;
                            method_2(itemFilterEntry);
                        }
                        return;
                    }
                }
                QMessageBox.ShowError("Empty or file opening error.");
            }
        }

        private void bExpand_Click(object sender, EventArgs e)
        {
            ItemFilterForm.Show(Filter, itemFilterCoreType);
            bindingSource_0.ResetBindings(true);
        }

        private void ItemFilterUC_Load(object sender, EventArgs e) { }
    }
}
