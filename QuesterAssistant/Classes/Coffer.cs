using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using MyNW.Classes.GroupProject;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes
{
    [Serializable]
    [XmlRoot]
    public class Coffer : NotifyHashChanged, IParse<Coffer>
    {
        [Browsable(false)]
        public string InternalName { get; set; } = string.Empty;
        [Browsable(false)]
        public string DisplayName { get; set; } = string.Empty;
        [DisplayName(nameof(Coffer)), XmlIgnore]
        public string FullName => $"{DisplayName} [{InternalName}]";
        [HashInclude]
        public List<Item> Items { get; set; } = new List<Item>();

        public Coffer() { }

        public Coffer(GroupProjectCofferNumericData data)
        {
            InternalName = data.CofferNumericDef.Name;
            DisplayName = data.CofferNumericDef.DisplayName;
            data.CofferNumericDef.ItemConversion.ForEach(i => Items.Add(new Item(i)));
        }

        public void Parse(Coffer source)
        {
            if (source.Items.Any())
                source.Items.ForEach(si => Items.Find(i => i.InternalName == si.InternalName).Parse(si));
        }

        public void Init() { }

        public void RemoveZero()
        {
            Items.RemoveAll(i => i.Donate == 0);
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(InternalName) ? "Select a coffer to donate" : FullName;
        }

        [XmlRoot]
        public class Item : IParse<Item>
        {
            [XmlAttribute, Browsable(false)]
            public string InternalName { get; set; } = string.Empty;
            [XmlIgnore, Browsable(false)]
            public string DisplayName { get; set; } = string.Empty;
            [DisplayName(nameof(Item)), XmlIgnore]
            public string FullName => $"{DisplayName} [{InternalName}]";
            [XmlAttribute, Browsable(false)]
            public ItemType Type { get; set; }
            [XmlIgnore]
            public int InBags => 
                Type == ItemType.Numeric ?
                    EntityManager.LocalPlayer.Inventory.GetNumericCount(InternalName) :
                    (int)EntityManager.LocalPlayer.GetItemCountByInternalName(InternalName);
            [XmlIgnore]
            public uint BatchSize { get; }
            [XmlText]
            public int Donate { get; set; }

            public Item() { }

            public Item(DonateCofferItemConversion item)
            {
                InternalName = item.Item.InternalName;
                DisplayName = item.Item.DisplayName;
                BatchSize = item.BatchSize;
                Type = item.Item.Type;
            }

            public void Parse(Item source)
            {
                Donate = source.Donate;
            }

            public void Init() { }

            public override int GetHashCode()
            {
                return Donate.GetHashCode();
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}
