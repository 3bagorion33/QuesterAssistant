using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Astral.Classes.ItemFilter;
using Astral.Controllers;
using Astral.Functions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Panels;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    public partial class ItemFilterUC : UserControl
    {
        private ItemFilterCoreType itemFilterCoreType;
        private ItemFilterCore itemFilterCore;

        public ItemFilterCoreType Type
        {
            get => itemFilterCoreType;
            set => itemFilterCoreType = value;
            //{
            //    this.itemFilterCoreType = value;
            //    bReverse.Visible = value == ItemFilterCoreType.ItemsID;
            //    ItemFilterCoreType itemFilterCoreType = this.itemFilterCoreType;
            //    if (itemFilterCoreType == ItemFilterCoreType.Items)
            //    {
            //        addTypeLabel.Text = "Add item :";
            //        return;
            //    }
            //    if (itemFilterCoreType != ItemFilterCoreType.Loots)
            //    {
            //        return;
            //    }
            //    addTypeLabel.Text = "Add loot :";
            //}
        }

        [Browsable(false)]
        public ItemFilterCore Filter
        {
            get => itemFilterCore;
            set
            {
                itemFilterCore = value;
                if (value != null)
                {
                    bindingSource.DataSource = value.Entries;
                    return;
                }
                bindingSource.DataSource = null;
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
            repositoryItemComboBox.QueryPopUp += FillItems;
            repositoryItemComboBox.TextEditStyle = TextEditStyles.DisableTextEditor;
            colFilterType.ColumnEdit = repositoryItemComboBox;
        }

        private void FillItems(object sender, CancelEventArgs e)
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
            if (bindingSource.Current != null && QMessageBox.ShowDialog("Are you sure to delete this entry ?") == DialogResult.Yes)
            {
                bindingSource.RemoveCurrent();
            }
        }

        private void AddFilterEntry(ItemFilterEntry itemFilterEntry)
        {
            if (itemFilterEntry != null)
            {
                bindingSource.Add(itemFilterEntry);
                bindingSource.Position = bindingSource.IndexOf(itemFilterEntry);
            }
        }

        private void AddNewFilterEntry(bool showGetAnItemForm = false)
        {
            ItemFilterType entryFilterType;
            switch (itemFilterCoreType)
            {
                case ItemFilterCoreType.Items:
                    entryFilterType = ItemFilterType.ItemName;
                    break;
                case ItemFilterCoreType.Loots:
                    entryFilterType = ItemFilterType.Loot;
                    break;
                default:
                    entryFilterType = ItemFilterType.ItemID;
                    break;
            }
            var text = string.Empty;
            if (showGetAnItemForm)
            {
                var item = GetAnItem.Show(entryFilterType);
                if (item is null) return;
                text = GetEntryText(item, entryFilterType);
            }
            AddFilterEntry(new ItemFilterEntry {Type = entryFilterType, Text = text});
        }

        private string GetEntryText(GetAnItem.ListItem item, ItemFilterType filter) => 
            filter == ItemFilterType.ItemName ? item.DisplayName : item.ItemId;

        private void bTest_Click(object sender, EventArgs e)
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
            if (QMessageBox.ShowDialog("Are you sure to clear the filter list ?") == DialogResult.Yes)
            {
                ClearSource();
            }
        }

        private void ClearSource()
        {
            bindingSource.Clear();
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
                            ClearSource();
                            using (List<ItemFilterEntry>.Enumerator enumerator = itemFilterCore.Entries.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    ItemFilterEntry itemFilterEntry = enumerator.Current;
                                    AddFilterEntry(itemFilterEntry);
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
                            AddFilterEntry(itemFilterEntry);
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
            bindingSource.ResetBindings(true);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewFilterEntry();
        }

        private void riFilterTextEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0)
            {
                AddNewFilterEntry(true);
            }
            else
            {
                var entry = bindingSource.Current as ItemFilterEntry;
                if (entry is null) return;
                var item = GetAnItem.Show(entry.Type);
                if (item is null) return;
                entry.Text = entry.Type == ItemFilterType.ItemName ? item.DisplayName : item.ItemId;
            }
            gridView2.FocusedRowHandle = -1;
        }
    }
}
