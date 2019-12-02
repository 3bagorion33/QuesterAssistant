using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Panels
{
    internal partial class ListControl : XtraUserControl
    {
        private IBindingList DataSource => lkupList.Properties.DataSource as IBindingList;
        private Type dataType;
        private string dataTypeName;
        private bool disableEvent;

        public event EventHandler EditValueChanged;
        public event ListChangedEventHandler ListChanged;

        public IListControlSource CurrentItem => lkupList.EditValue as IListControlSource;

        public ListControl()
        {
            InitializeComponent();
        }

        public void BindSource<T>(IBindingList data) where T : IListControlSource
        {
            lkupList.Properties.DataSource = data as BindingList<T>;
            dataType = typeof(T);
            dataTypeName = dataType.Name;
            lkupList.Properties.NullValuePrompt = $@"Create a new {dataTypeName}";
            lkupList.Properties.Buttons[0].ToolTip = $@"Select a {dataTypeName}";
            lkupList.Properties.Buttons[1].ToolTip = $"Add a new {dataTypeName}\nHold Ctrl to copy";
            lkupList.Properties.Buttons[2].ToolTip = $@"Rename current {dataTypeName}";
            lkupList.Properties.Buttons[3].ToolTip = $@"Change {dataTypeName}s order";
            lkupList.Properties.Buttons[4].ToolTip = $@"Delete current {dataTypeName}";
        }

        private void ListControl_Load(object sender, EventArgs e)
        {
            lkupList.ButtonClick += lkupList_ButtonClick;
            lkupList.ListChanged += lkupList_ListChanged;
            lkupList.EditValueChanged += lkupList_EditValueChanged;
        }

        private void lkupList_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged?.Invoke(sender, e);
        }

        private void lkupList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!disableEvent)
            {
                if (DataSource.Count != 0 && lkupList.ItemIndex == -1)
                    lkupList.ItemIndex = 0;
                if (DataSource.Count == 0)
                {
                    lkupList.ItemIndex = -1;
                    lkupList.EditValue = null;
                }
                ListChanged?.Invoke(sender, e);
            }
            lkupList.Properties.DropDownRows = DataSource.Count;
        }

        private void lkupList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            bool FindByName(string name)
            {
                bool result = false;
                foreach (var item in DataSource)
                {
                    result = ((IListControlSource)item).Name == name;
                    if (result)
                    {
                        QMessageBox.ShowError($"{dataTypeName} with this name already exist!");
                        break;
                    }
                }
                return result;
            }

            switch (e.Button.Caption)
            {
                case "Add":
                    var mod = ModifierKeys;
                    string addName = InputBox.MessageText($"Enter a new {dataTypeName} name:");
                    if (!string.IsNullOrEmpty(addName) && !FindByName(addName))
                    {
                        var newItem = mod == Keys.Control
                            ? lkupList.EditValue.MemberwiseClone() as IListControlSource
                            : Activator.CreateInstance(dataType) as IListControlSource;

                        newItem.Name = addName;
                        DataSource.Add(newItem);
                        lkupList.InvokeSafe(() => lkupList.EditValue = newItem);
                    }
                    break;

                case "Delete":
                    if (DataSource.Count != 0 &&
                        QMessageBox.ShowDialog($"Delete this {dataTypeName}?") == DialogResult.Yes)
                    {
                        DataSource.Remove(CurrentItem);
                        lkupList.ItemIndex = 0;
                    }
                    break;

                case "Sort":
                    if (DataSource.Count != 0)
                    {
                        disableEvent = true;
                        var selectedVal = lkupList.EditValue;
                        ChangeListOrder.Show(DataSource, CurrentItem, $"Change {dataTypeName}s order :");
                        lkupList.InvokeSafe(() => lkupList.EditValue = selectedVal);
                        disableEvent = false;
                    }
                    break;

                case "Rename":
                    if (DataSource.Count != 0)
                    {
                        string ren = InputBox.MessageText($"Enter a new name for this {dataTypeName}:", CurrentItem.Name);
                        if (!string.IsNullOrEmpty(ren) && !FindByName(ren))
                        {
                            CurrentItem.Name = ren;
                            lkupList.InvokeSafe(() => lkupList.Refresh());
                        }
                    }
                    break;
            }
        }
    }

    public interface IListControlSource
    {
        string Name { get; set; }
    }
}
