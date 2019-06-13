using Astral.Quester.Forms;
using DevExpress.Utils.Extensions;
using MyNW.Classes;
using MyNW.Classes.ItemProgression;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuesterAssistant.UpgradeManager
{
    [Serializable]
    public class UpgradeManagerData : NotifyHashChanged, IParse<UpgradeManagerData>
    {
        public HotKey ToggleHotKey { get; set; } = new HotKey();
        public BindingList<Profile> Profiles { get; set; } = new BindingList<Profile>();

        public override int GetHashCode()
        {
            return Profiles.GetSafeHashCode() ^ ToggleHotKey.GetSafeHashCode();
        }

        public void Init() { }

        public void Parse(UpgradeManagerData source)
        {
            ToggleHotKey.Parse(source.ToggleHotKey);
            Profiles.Clear();
            source.Profiles.ForEach(p => Profiles.Add(p));
        }

        [Serializable]
        public class Profile
        {
            [XmlAttribute]
            public string Name { get; set; } = string.Empty;
            public AlgorithmDirection Algorithm { get; set; } = AlgorithmDirection.UpToDown;
            public ErrorBehavior RunCondition { get; set; } = ErrorBehavior.WhilePossible;
            public int IterationsCount { get; set; } = 0;
            public List<Task> Tasks { get; set; } = new List<Task>();

            public void AddTask(GetAnItem.ListItem item)
            {
                if (item == null) return;
                Task task;
                if ((task = Task.New(item)) == null)
                {
                    ErrorBox.Show($"'{item}' can't be upgraded!");
                    return;
                }
                if (Tasks.Exists(t => t.Rank == task.Rank))
                {
                    if (DialogBox.Show($"Task with Rank{task.Rank} is already exist. Replace?", "Confirm") == DialogResult.Yes)
                        Tasks.AddOrReplace(t => t.Rank == task.Rank, task);
                }
                else
                    Tasks.Add(task);
                Tasks.Sort(Sort);
            }

            public Task.Result Run(int startIdx = -1, int stopIdx = -1, int runCount = 0)
            {
                Task.Result result = Task.Result.Null;
                int count = runCount.CheckZero(IterationsCount.CheckZero(int.MaxValue));

                if (stopIdx < 0)
                    stopIdx = Tasks.Count - 1;

                if (startIdx < 0)
                    startIdx = 0;

                void UpToDown(int start, int stop)
                {
                    for (int j = start; j <= stop; j++)
                    {
                        int i = 0;
                        while ((i < count) && ((result = Tasks[j].Run()) > 0))
                        {
                            if (result == Task.Result.Evolved) i++;
                        }
                    }
                }

                if (Algorithm == AlgorithmDirection.UpToDown || startIdx == stopIdx)
                {
                    UpToDown(startIdx, stopIdx);
                    return result;
                }
                if (Algorithm == AlgorithmDirection.DownToUp)
                {
                    int j = 0;
                    bool runconditions;
                    do
                    {
                        for (int i = stopIdx; i >= startIdx; i--)
                        {
                            result = Tasks[i].Run();
                            if (result == Task.Result.Evolved)
                            {
                                UpToDown(i + 1, stopIdx);
                                j++;
                                break;
                            }
                        }
                        runconditions = 
                            (RunCondition == ErrorBehavior.StopOnError) ? 
                            result > 0 : 
                            (result > Task.Result.HaventRefinimentCurrency && Tasks.Exists(t => t.HaveRequiredItems));
                    }
                    while (j < count && runconditions);
                }
                return result;
            }

            public override int GetHashCode()
            {
                return 
                    Name.GetSafeHashCode() ^
                    Algorithm.GetSafeHashCode() ^
                    RunCondition.GetSafeHashCode() ^
                    IterationsCount.GetSafeHashCode() ^
                    Tasks.GetSafeHashCode();
            }

            private int Sort(Task t1, Task t2)
            {
                if (t1.Rank > t2.Rank) return 1;
                if (t1.Rank < t2.Rank) return -1;
                return 0;
            }

            public override string ToString()
            {
                return Name;
            }

            public enum AlgorithmDirection { UpToDown, DownToUp }
            public enum ErrorBehavior { WhilePossible, StopOnError }
        }

        [Serializable]
        public class Task
        {
            public uint Rank { get; set; } = 0;
            public string ItemId { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public uint Chance { get; set; } = 0;
            public bool UseWard { get; set; } = false;

            [XmlIgnore]
            public string FullName => $"{DisplayName} [{ItemId}]";
            [XmlIgnore]
            public int CountUnfilled => BagsItems.FindAll(FindUnfilled).Sum(s => (int)s.Item.Count);
            [XmlIgnore]
            public int CountFilled => BagsItems.FindAll(s => FindFullFilled(s) || FindPartiallyFilled(s)).Sum(s => (int)s.Item.Count);
            [XmlIgnore]
            public string Count => $"{CountUnfilled} | {CountFilled}";

            private const int TIME_WAIT = 333;
            private InventorySlot slot;
            private static List<InventorySlot> BagsItems => EntityManager.LocalPlayer.BagsItems;
            private Result FindResult
            {
                get
                {
                    Result FindSlot()
                    {
                        if ((slot = BagsItems.LastOrDefault(FindFullFilled)) != null)
                            return Result.FullFilled;
                        if ((slot = BagsItems.LastOrDefault(FindPartiallyFilled)) != null)
                            return Result.PartFilled;
                        if ((slot = BagsItems.LastOrDefault(FindUnfilled)) != null)
                            return Result.Unfilled;
                        return Result.Null;
                    }

                    Result result = FindSlot();
                    if (result == Result.Unfilled && EntityManager.LocalPlayer.BagsFreeSlots == 0)
                        return Result.HaventFreeBagsSlots;
                    if (!HaveRequiredItems)
                        return Result.HaventRequiredItems;

                    return result;
                }
            }
            public bool HaveRequiredItems
            {
                get
                {
                    if (slot == null) return false;
                    if (UseWard && !BagsItems.Any(FindCatal)) return false;
                    if (!slot.Item.ProgressionLogic.CurrentTier.CatalystItems.Any()) return true;
                    foreach (var catalyst in slot.Item.ProgressionLogic.CurrentTier.CatalystItems)
                    {
                        var count = EntityManager.LocalPlayer.GetItemCountByInternalNameInBags(catalyst.ItemDef.InternalName);
                        if (catalyst.ItemDef.InternalName == slot.Item.ItemDef.InternalName)
                            count--;
                        if (count < catalyst.NumRequired)
                            return false;
                    }
                    return true;
                }
            }

            public Task() { }

            public static Task New(GetAnItem.ListItem item)
            {
                Task t = new Task(item);
                var i = BagsItems.Find(s => t.FindAny(s))?.Item;
                if (i == null || i.ProgressionLogic.CurrentRankTotalRequiredXP == 0) return null;
                if (i.ProgressionLogic.CurrentTier.Index == 0)
                {
                    t.Chance = 100;
                    t.Rank = 1;
                }
                else
                {
                    t.Chance = i.ProgressionLogic.CurrentTier.BaseRankUpChance;
                    t.Rank = i.ProgressionLogic.CurrentTier.Index;
                }
                return t;
            }

            private Task(GetAnItem.ListItem item)
            {
                ItemId = item.ItemId;
                DisplayName = item.DisplayName;
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

            bool FindAny(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == ItemId;
            }

            bool FindCatal(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName.Contains("Fuse_Ward_Preservation_");
            }

            public Result Run()
            {
                Result result = Result.Null;

                if (Chance == 0 || string.IsNullOrEmpty(ItemId)) return result;

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

                bool Evolve(InventorySlot s)
                {
                    InventorySlot catal = null;
                    if (UseWard)
                    {
                        catal = BagsItems.LastOrDefault(FindCatal);
                    }
                    s.Evolve(catal);
                    Thread.Sleep(TIME_WAIT);
                    if (s.Item.ProgressionLogic.CurrentRankXP > 0) return false;
                    s.Group();
                    return true;
                }

                switch (result = FindResult)
                {
                    case Result.Unfilled:
                    case Result.PartFilled:
                        if (Feed(slot)) result = Run();
                        else result = Result.HaventRefinimentCurrency;
                        break;
                    case Result.FullFilled:
                        if (!Evolve(slot)) result = Run();
                        else result = Result.Evolved;
                        break;
                    default:
                        break;
                }
                return result;
            }

            public override int GetHashCode()
            {
                return
                    FullName.GetSafeHashCode() ^
                    CountUnfilled;
            }

            public override string ToString()
            {
                return $"{DisplayName} [{ItemId}]";
            }

            public enum Result : int
            {
                Null = 0,
                Unfilled = 1,
                PartFilled = 2,
                FullFilled = 3,
                Evolved = 4,
                HaventRequiredItems = -1,
                HaventFreeBagsSlots = -2,
                HaventRefinimentCurrency = -3
            }
        }
    }
}
