using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal partial class ChangeListOrder : XtraForm
    {
        private IList usedList;
        private object SelectedItem
        {
            get
            {
                if (lbxItems.SelectedIndex >= 0)
                {
                    return lbxItems.SelectedValue;
                }
                return null;
            }
        }

        private ChangeListOrder(IList list, object selected)
        {
            InitializeComponent();
            usedList = list;
            RefreshList();
            lbxItems.SelectedItem = selected;
        }

        public static void Show(IList list, object selected, string message)
        {
            new ChangeListOrder(list, selected) { lblMessage = { Text = message } }.ShowDialog();
        }

        private void RefreshList()
        {
            lbxItems.DataSource = usedList;
        }

        private void ReverseList(int startIdx, Direction direction)
        {
            object selected = SelectedItem;
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