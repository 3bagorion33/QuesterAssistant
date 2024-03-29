﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.ItemFilter;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class AuctionItemCount : Condition
    {
        private LocalPlayerEntity Player => EntityManager.LocalPlayer;

        public AuctionItemCount()
        {
            Sign = Relation.Inferior;
            Value = 1;
            Type = AuctionCountType.AuctionOnly;
            SpecificBag = InvBagIDs.None;
            ItemsFilter = new ItemFilterCore();
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Sign} to {Value}";
        }

        private uint CurrentCount()
        {
            AuctionSearch.RequestAuctionsForPlayer();
            uint num = (uint) Auction.AuctionSellList.Lots
                    .FindAll(l => ItemsFilter.IsMatch(l.Items.First().Item))
                    .Sum(l => l.Items.First().Item.Count);

            if (Type > AuctionCountType.AuctionOnly)
            {
                List<InventorySlot> bags = Player.BagsItems;
                switch (Type)
                {
                    case AuctionCountType.WithBagsOnly:
                        bags.AddRange(Professions2.CraftingBags);
                        break;
                    case AuctionCountType.WithAllInventories:
                        bags = Player.AllItems;
                        break;
                    case AuctionCountType.WithSpecificBag:
                        bags = Player.GetInventoryBagById(SpecificBag).GetItems;
                        break;
                }
                num += (uint) bags.FindAll(s => ItemsFilter.IsMatch(s.Item)).Sum(i => i.Item.Count);
            }
            return num;
        }

        public override bool IsValid
        {
            get
            {
                uint num = CurrentCount();
                switch (Sign)
                {
                    case Relation.Equal:
                        return num == (ulong)Value;
                    case Relation.NotEqual:
                        return num != (ulong)Value;
                    case Relation.Inferior:
                        return num < (ulong)Value;
                    case Relation.Superior:
                        return num > (ulong)Value;
                    default:
                        return false;
                }
            }
        }

        public override string TestInfos => "Item count : " + CurrentCount();

        public override void Reset()
        {
            AuctionSearch.RequestAuctionsForPlayer();
        }

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemsFilter { get; set; }
        public Relation Sign { get; set; }
        public int Value { get; set; }
        public AuctionCountType Type { get; set; }
        [Description("Used ony if Type property is to SpecificBag")]
        public InvBagIDs SpecificBag { get; set; }
        
        public enum AuctionCountType
        {
            AuctionOnly,
            WithAllInventories,
            WithBagsOnly,
            WithSpecificBag
        }
    }
}
