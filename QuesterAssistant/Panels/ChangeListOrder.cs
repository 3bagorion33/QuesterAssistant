using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal partial class ChangeListOrder<T> : XtraForm where T : class
    {
        private IList<T> usedList;
        private T SelectedItem
        {
            get
            {
                if (lbxItems.SelectedIndex >= 0)
                {
                    return lbxItems.SelectedValue as T;
                }
                return default(T);
            }
        }

        private ChangeListOrder(IList<T> list, T selected)
        {
            InitializeComponent();
            usedList = list;
            RefreshList();
            lbxItems.SelectedItem = selected;
        }

        public static void Show(IList<T> list, T selected, string message)
        {
            new ChangeListOrder<T>(list, selected) { lblMessage = { Text = message } }.ShowDialog();
        }

        private void RefreshList()
        {
            lbxItems.DataSource = usedList;
        }

        private void ReverseList(int startIdx, Direction direction)
        {
            T selected = SelectedItem;
            usedList.Remove(selected);
            usedList.Insert(startIdx + (int)direction, selected);
            RefreshList();
            lbxItems.SelectedItem = selected;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedIndex > 0)
            {
                ReverseList(lbxItems.SelectedIndex, Direction.Up);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if ((lbxItems.SelectedIndex + 1) < usedList.Count)
            {
                ReverseList(lbxItems.SelectedIndex, Direction.Down);
            }
        }

        private enum Direction : int
        {
            Up = -1,
            Down = 1
        }
    }
}