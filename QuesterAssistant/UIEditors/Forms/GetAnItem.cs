using System;
using System.Collections.Generic;
using System.Linq;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.UIEditors.Forms
{
    public partial class GetAnItem : XtraForm
    {
        private bool valid;

        public GetAnItem()
        {
            InitializeComponent();
        }

        public static ListItem Show(int defaultList = 0)
        {
            GetAnItem getAnItem = new GetAnItem {itemListChoice = {SelectedIndex = defaultList}};
            getAnItem.ShowDialog();
            if (getAnItem.valid && getAnItem.lbItemsSource.SelectedItem != null)
            {
                return getAnItem.lbItemsSource.SelectedItem as ListItem;
            }
            return null;
        }

        private void addItemToList(ItemDef itemDef)
        {
            ListItem item = new ListItem(itemDef.InternalName, itemDef.DisplayName);
            if (!lbItemsSource.Items.Contains(item))
            {
                lbItemsSource.Items.Add(item);
            }
        }

        private void refreshList()
        {
            lbItemsSource.Items.Clear();
            switch (itemListChoice.SelectedIndex)
            {
                case 0:
                    List<InventorySlot> bagsItems = EntityManager.LocalPlayer.BagsItems;
                    bagsItems.AddRange(Professions2.CraftingBags);
                    bagsItems.OrderBy(bi => bi.Item.ItemDef.DisplayName).ForEach(s => addItemToList(s.Item.ItemDef));
                    return;
                case 1:
                    EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.StoreItems
                        .OrderBy(bi => bi.Item.ItemDef.DisplayName).ForEach(s => addItemToList(s.Item.ItemDef));
                    return;
                case 2:
                    EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.Potions).GetItems
                        .OrderBy(bi => bi.Item.ItemDef.DisplayName).ForEach(s => addItemToList(s.Item.ItemDef));
                    return;
                case 3:
                    EntityManager.LocalPlayer.EquippedItem.OrderBy(bi => bi.Item.ItemDef.DisplayName)
                        .ForEach(s => addItemToList(s.Item.ItemDef));
                    return;
                case 4:
                    EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.RewardBags.ForEach(b =>
                        b.GetItems.ForEach(s => addItemToList(s.Item.ItemDef)));
                    return;
                case 5:
                    EntityManager.LocalPlayer.AllItems.OrderBy(i => i.Item.DisplayName)
                        .ForEach(s => addItemToList(s.Item.ItemDef));
                    return;
                case 6:
                    Email.Mails.Select(m => m.Message.GetAttachedItems()).ForEach(m => m.ForEach(i => addItemToList(i.ItemDef)));
                    return;
                default:
                    return;
            }
        }

        private void GetAnId_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void b_Select_Click(object sender, EventArgs e)
        {
            valid = true;
            Close();
        }

        private void b_Refresh_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void itemListChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshList();
        }

        public class ListItem
        {
            public string ItemId { get; set; }
            public string DisplayName { get; set; }
            public ListItem(string itemId, string displayName)
            {
                ItemId = itemId;
                DisplayName = displayName;
            }
            public override string ToString() => DisplayName + " [" + ItemId + "]";
            public override bool Equals(object obj) => ItemId == (obj as ListItem).ItemId;
            public override int GetHashCode() => ItemId.GetHashCode();
        }
    }
}
