using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Panels
{
    internal partial class ChangeListOrder<T> : XtraForm where T : class
    {
        private List<T> usedList;
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

        internal ChangeListOrder(List<T> list)
        {
            InitializeComponent();
            usedList = list;
        }

        internal static void Show<T2>(List<T2> list, string message) where T2 : class
        {
            new ChangeListOrder<T2>(list) { lblMessage = { Text = message } }.ShowDialog();
        }

        private void RefreshList()
        {
            lbxItems.DataSource = usedList;
        }

        private void ReverseList(int _startIdx)
        {
            T selected = SelectedItem;
            usedList.Reverse(_startIdx, 2);
            RefreshList();
            lbxItems.SelectedItem = selected;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void ChangeListOrder_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedIndex > 0)
            {
                ReverseList(lbxItems.SelectedIndex - 1);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if ((lbxItems.SelectedIndex + 1) < usedList.Count)
            {
                ReverseList(lbxItems.SelectedIndex);
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedIndex > 0)
            {
            }
        }
    }
}