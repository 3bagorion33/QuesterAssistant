using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.UIEditors;

namespace QuesterAssistant.Classes
{
    public class InventorySelect
    {
        private PatternDef pattern = PatternDef.BagsOnly;
        [Description("Choose bags pattern")]
        public PatternDef Pattern
        {
            get => pattern;
            set
            {
                pattern = value;
                if (pattern != PatternDef.SpecificBags)
                {
                    SpecificBags.Items.Clear();
                }
            }
        }
        [Description("Choose multiple bags")]
        [Editor(typeof(CheckedListBoxEditor<InvBagIDs>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<InvBagIDs> SpecificBags { get; set; } = new CheckedListBoxSelector<InvBagIDs>();

        [XmlIgnore, Browsable(false)]
        public List<InventorySlot> Slots
        {
            get
            {
                var items = new List<InventorySlot>();
                switch (Pattern)
                {
                    case PatternDef.AllInventories:
                        items = EntityManager.LocalPlayer.AllNonBankItems;
                        break;
                    case PatternDef.BagsOnly:
                        var bags = new List<InvBagIDs>(EntityManager.LocalPlayer.BaseBagsIds);
                        bags.Add(InvBagIDs.Overflow);
                        bags.ForEach(b => items.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(b).GetItems));
                        break;
                    case PatternDef.SpecificBags:
                        SpecificBags.Items.ForEach(b => items.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(b).GetItems));
                        break;
                }
                return items;
            }
        }
        public override string ToString() =>
            Pattern == PatternDef.SpecificBags ? $"{SpecificBags} selected" : $"{Pattern}";

        public enum PatternDef
        {
            AllInventories,
            BagsOnly,
            SpecificBags,
        }
    }
}