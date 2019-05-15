using Astral;
using Astral.Quester.Forms;
using DevExpress.Utils.Extensions;
using MyNW.Classes;
using MyNW.Classes.ItemProgression;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace QuesterAssistant.UpgradeManager
{
    [Serializable]
    public class UpgradeManagerData : NotifyHashChanged, IParse<UpgradeManagerData>
    {
        public BindingList<Profile> Profiles { get; set; } = new BindingList<Profile>();

        public override int GetHashCode()
        {
            return Profiles.GetSafeHashCode();
        }

        public void Init() { }

        public void Parse(UpgradeManagerData source)
        {
            Profiles.Clear();
            source.Profiles.ForEach(p => Profiles.Add(p));
        }

        [Serializable]
        public class Profile : NotifyHashChanged, IParse<Profile>
        {
            [XmlAttribute]
            public string Name { get; set; } = string.Empty;
            public List<Task> Tasks { get; set; } = new List<Task>();

            public void AddTask(GetAnItem.ListItem item)
            {
                if (item == null) return;
                Tasks.Add(new Task(item));
                Tasks.Sort(Equals);
                //OnPropertyChanged(nameof(Tasks));
            }

            public override int GetHashCode()
            {
                return Name.GetSafeHashCode() ^ Tasks.GetSafeHashCode();
            }

            private int Equals(Task t1, Task t2)
            {
                if (t1.Rank > t2.Rank)
                {
                    return 1;
                }
                if (t1.Rank < t2.Rank)
                {
                    return -1;
                }
                return 0;
            }

            public void Init() { }

            public void Parse(Profile source)
            {
                Name = source.Name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        [Serializable]
        public class Task : NotifyHashChanged, IParse<Task>
        {
            private List<InventorySlot> BagsItems => EntityManager.LocalPlayer.BagsItems;
            private const int TIME_WAIT = 333;

            public uint Rank { get; set; } = 0;
            public string ItemId { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string Chance { get; set; } = string.Empty;
            public bool UseWard { get; set; } = false;
            [XmlIgnore]
            public string FullName => $"{DisplayName} [{ItemId}]";
            [XmlIgnore]
            public string Count => $"{CountUnfilled} | {CountFilled}";
            private int CountUnfilled => BagsItems.FindAll(FindUnfilled).Sum(s => (int)s.Item.Count);
            private int CountFilled => BagsItems.FindAll(s => FindFullFilled(s) || FindPartiallyFilled(s)).Sum(s => (int)s.Item.Count);

            public Task() { }

            public Task(GetAnItem.ListItem item)
            {
                ItemId = item.ItemId;
                DisplayName = item.DisplayName;
                var i = BagsItems.Find(s => FindUnfilled(s) || FindPartiallyFilled(s) || FindFullFilled(s)).Item;
                if (i.ProgressionLogic.CurrentTier.Index == 0)
                {
                    Chance = "100%";
                    Rank = 1;
                }
                else
                {
                    Chance = $"{i.ProgressionLogic.CurrentTier.BaseRankUpChance}%";
                    Rank = i.ProgressionLogic.CurrentTier.Index;
                }
            }

            bool FindUnfilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == ItemId &&
                    s.Item.ProgressionLogic.CurrentRankXP == 0;
            }

            bool FindPartiallyFilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == ItemId &&
                    s.Item.ProgressionLogic.CurrentRankXP > 0 &&
                    s.Item.ProgressionLogic.CurrentRankTotalRequiredXP > s.Item.ProgressionLogic.CurrentRankXP;
            }

            bool FindFullFilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == ItemId &&
                    s.Item.ProgressionLogic.CurrentTier.Index > 0 &&
                    s.Item.ProgressionLogic.CurrentRankTotalRequiredXP == s.Item.ProgressionLogic.CurrentRankXP;
            }

            public void UpgradeOnce()
            {
                if (string.IsNullOrEmpty(ItemId)) return;

                InventorySlot slot;

                FindResult FindSlot()
                {
                    if ((slot = BagsItems.Find(FindFullFilled)) != null)
                    {
                        if (!slot.Item.ProgressionLogic.CurrentTier.HaveRequiredCatalystItems)
                        {
                            return FindResult.HaventCatalystItems;
                        }
                        return FindResult.FullFilled;
                    }
                    if ((slot = BagsItems.Find(FindPartiallyFilled)) != null)
                    {
                        return FindResult.PartFilled;
                    }
                    if ((slot = BagsItems.Find(FindUnfilled)) != null)
                    {
                        if (EntityManager.LocalPlayer.BagsFreeSlots == 0)
                        {
                            return FindResult.HaventFreeBagsSlots;
                        }
                        return FindResult.Unfilled;
                    }
                    return FindResult.Null;
                }

                bool Feed(InventorySlot s)
                {
                    ItemProgressionLogic progress = s.Item.ProgressionLogic;
                    int points = (int)(progress.CurrentRankTotalRequiredXP - progress.CurrentRankXP);

                    if (EntityManager.LocalPlayer.Inventory.RefinementCurrency >= points)
                    {
                        s.Feed(points);
                        Thread.Sleep(TIME_WAIT);
                        return true;
                    }
                    return false;
                }

                void Evolve(InventorySlot s)
                {
                    InventorySlot catal = null;
                    if (UseWard)
                    {
                        catal = BagsItems.First(sl => sl.Item.ItemDef.InternalName.Contains("Fuse_Ward_Preservation_")); // Fuse_Ward_Preservation_Invocation
                    }
                    s.Evolve(catal);
                    Thread.Sleep(TIME_WAIT);
                    s.Group();
                }

                switch (FindSlot())
                {
                    case FindResult.Unfilled:
                    case FindResult.PartFilled:
                        Feed(slot);
                        UpgradeOnce();
                        return;
                    case FindResult.FullFilled:
                        Evolve(slot);
                        return;
                    case FindResult.HaventCatalystItems:
                        Logger.WriteLine("Havent items for upgrade!");
                        return;
                    case FindResult.HaventFreeBagsSlots:
                        Logger.WriteLine("Havent free bags slots!");
                        return;
                    default:
                        Logger.WriteLine("Upgrade impossible!");
                        return;
                }
            }

            public override int GetHashCode()
            {
                return
                    Rank.GetHashCode() ^
                    ItemId.GetSafeHashCode() ^
                    DisplayName.GetSafeHashCode() ^
                    Count.GetSafeHashCode() ^
                    Chance.GetSafeHashCode() ^
                    UseWard.GetSafeHashCode();
            }

            public void Init() { }

            public void Parse(Task source)
            {
                Rank = source.Rank;
                Chance = source.Chance;
                ItemId = source.ItemId;
                DisplayName = source.DisplayName;
                UseWard = source.UseWard;
            }

            public override string ToString()
            {
                return $"{DisplayName} [{ItemId}]";
            }

            private enum FindResult { Null, Unfilled, PartFilled, FullFilled, HaventCatalystItems, HaventFreeBagsSlots }
        }
    }
}
