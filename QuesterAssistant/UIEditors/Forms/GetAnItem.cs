using System;
using System.Collections.Generic;
using System.Linq;
using Astral.Classes.ItemFilter;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.UIEditors.Forms
{
    public partial class GetAnItem : XtraForm
    {
        private bool isLoaded;
        private bool valid;
        private ItemFilterType itemFilterType;

        private GetAnItem()
        {
            InitializeComponent();
        }

        public static ListItem Show(ItemFilterType filterType = ItemFilterType.ItemName, int defaultList = 0)
        {
            GetAnItem getAnItem = new GetAnItem
            {
                itemListChoice = {SelectedIndex = defaultList},
                itemFilterType = filterType
            };
            getAnItem.ShowDialog();
            if (getAnItem.valid && getAnItem.lbItemsSource.SelectedItem != null)
            {
                return getAnItem.lbItemsSource.SelectedItem as ListItem;
            }
            return null;
        }

        private void addItemToList(dynamic item)
        {
            ListItem listItem = new ListItem();

            if (item is ItemDef itemDef)
            {
                listItem.ItemId = itemDef.InternalName;
                listItem.DisplayName = itemDef.DisplayName;
            }

            if (item is string itemString)
            {
                listItem.ItemId = itemString;
            }

            if (!lbItemsSource.Items.Contains(listItem))
            {
                lbItemsSource.Items.Add(listItem);
            }
        }


        private void refreshList()
        {
            if (!isLoaded) return;
            lbItemsSource.Items.Clear();
            switch (itemFilterType)
            {
                case ItemFilterType.ItemName:
                case ItemFilterType.ItemID:
                    itemListChoice.Enabled = true;
                    switch (itemListChoice.SelectedIndex)
                    {
                        case 0:
                            List<InventorySlot> bagsItems = EntityManager.LocalPlayer.BagsItems;
                            bagsItems.AddRange(Professions2.CraftingBags);
                            bagsItems.OrderBy(bi => bi.Item.ItemDef.DisplayName)
                                .ForEach(s => addItemToList(s.Item.ItemDef));
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
                            if (!Email.IsMailFrameVisible())
                            {
                                Email.OpenMailFrame();
                                Pause.Sleep(500);
                            }
                            Email.Mails.Select(m => m.Message.GetAttachedItems())
                                .ForEach(m => m.ForEach(i => addItemToList(i.ItemDef)));
                            return;
                        case 7:
                            Auction.RequestAuctionsForPlayer();
                            Auction.AuctionLotList.Lots.Select(l => l.Items[0].Item).ForEach(i => addItemToList(i.ItemDef));
                            return;
                        default:
                            return;
                    }

                case ItemFilterType.ItemCatergory:
                    Astral.Logic.NW.General.CategoriesWithItems.ForEach(addItemToList);
                    return;
                case ItemFilterType.ItemType:
                    Enum.GetNames(typeof(ItemType)).ForEach(addItemToList);
                    return;
                case ItemFilterType.ItemFlag:
                    Enum.GetNames(typeof(ItemFlags)).ForEach(addItemToList);
                    return;
                case ItemFilterType.Loot:
                    Astral.Logic.NW.Inventory.LootRewardTables.ForEach(addItemToList);
                    return;
                case ItemFilterType.ItemQuality:
                    Enum.GetNames(typeof(ItemQuality)).ForEach(addItemToList);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetAnId_Load(object sender, EventArgs e)
        {
            isLoaded = true;
            refreshList();
        }

        private void b_Select_Click(object sender, EventArgs e)
        {
            valid = true;
            Close();
        }

        private void b_Refresh_Click(object sender, EventArgs e) => refreshList();
        private void itemListChoice_SelectedIndexChanged(object sender, EventArgs e) => refreshList();

        public class ListItem
        {
            public string ItemId { get; set; }
            public string DisplayName { get; set; }
            public override string ToString()
            {
                if (!string.IsNullOrEmpty(DisplayName) && !string.IsNullOrEmpty(ItemId))
                    return $"{DisplayName}  [{ItemId}]";

                if (string.IsNullOrEmpty(DisplayName))
                    return $"[{ItemId}]";

                if (string.IsNullOrEmpty(ItemId))
                    return DisplayName;

                return string.Empty;
            }

            public override bool Equals(object obj) => ItemId == (obj as ListItem).ItemId;
            public override int GetHashCode() => ItemId.GetHashCode();
        }
    }
}
