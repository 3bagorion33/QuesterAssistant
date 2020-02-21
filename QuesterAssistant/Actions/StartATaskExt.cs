using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Professions.Classes;
using Astral.Professions.Forms;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;
using QuesterAssistant.Classes;
using StartATask = Astral.Quester.Classes.Actions.StartATask;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class StartATaskExt : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Task}";
        public override string Category => "Tasks";
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true; //EntityManager.LocalPlayer.MapState.MapName == "M15_Professions_Workshop" || ShouldUseSendingStone();

        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (Task.InternalName == string.Empty)
                {
                    return new ActionValidity("Task is not set.");
                }
                if (SendingStone != StartATask.SendingStoneUsage.None && !Professions2.TaskIsOrder(Task.InternalName))
                {
                    return new ActionValidity("Sending stone can be used for orders only (not gathering).");
                }
                return new ActionValidity();
            }
        }

        public override void GatherInfos()
        {
            Task = TasksEditor.Show();
        }

        protected override bool IntenalConditions
        {
            get
            {
                if (SendingStone == StartATask.SendingStoneUsage.Only && !Professions2.HaveSendingStone)
                {
                    Logger.WriteLine("Don't have sending stone.");
                    return false;
                }
                if (!Task.Instant)
                {
                    if (Professions2.TaskIsOrder(Task.InternalName) && Professions2.CurrentOrders.Count >= 3)
                    {
                        Logger.WriteLine("Can't start an order, all slots in use.");
                        return false;
                    }
                    if (Professions2.TaskIsGatheringTask(Task.InternalName) &&
                        Professions2.CurrentGatheringTasks.Count >= 3)
                    {
                        Logger.WriteLine("Can't start a gathering task, all slots in use.");
                        return false;
                    }
                }
                return true;
            }
        }

        public StartATaskExt()
        {
            Task = new Task();
            SendingStone = StartATask.SendingStoneUsage.None;
        }

        private bool ShouldUseSendingStone()
        {
            return SendingStone != StartATask.SendingStoneUsage.None && Professions2.TaskIsOrder(Task.InternalName) &&
                   Professions2.HaveSendingStone;
        }

        public override ActionResult Run()
        {
            var definition = Professions.AllTasks.Find(def => def.InternalName == Task.InternalName);
            if (definition is null)
            {
                Logger.WriteLine($"Task not found [{Task.DisplayName}]");
                return ActionResult.Fail;
            }

            if (Professions2.FreeSlots <= 0)
            {
                Logger.WriteLine("No free slot to start a task !");
                return ActionResult.Fail;
            }

            if (!ShouldUseSendingStone() && EntityManager.LocalPlayer.MapState.MapName != "M15_Professions_Workshop")
            {
                Logger.WriteLine("Wrong map...");
                return ActionResult.Fail;
            }

            if (ShouldUseSendingStone())
            {
                Professions2.OpenCraftingWindow();
            }
            else
            {
                //if (!(Professions2.TaskIsOrder(Task.InternalName) && Professions2.ArtisansCounter.IsValid &&
                //      Professions2.InteractArtisansCounter())
                //    &&
                //    !(Professions2.TaskIsGatheringTask(Task.InternalName) && Professions2.DispatchBoard.IsValid &&
                //      Professions2.InteractDispatchBoard()))
                //{
                //    Logger.WriteLine("Fail to interact with entity !");
                //    return ActionResult.Fail;
                //}
            }

            Logger.WriteLine($"{definition} task found !");
            var orderCount = 0;
            for (;;)
            {
                EntityManager.LocalPlayer.Player.RefreshAssignments();
                Pause.Sleep(500);
                if (Task.SkipIfAlreadyActive && Professions2.HaveActiveTask(definition.InternalName))
                {
                    Logger.WriteLine("Task already active, skip.");
                    return ActionResult.Skip;
                }
                if (!Professions2.HaveRequiredConsumables(definition, Task.GetHQMatsAsDic()))
                {
                    if (!Professions2.HaveRequiredConsumables(definition))
                    {
                        Logger.WriteLine("Don't have required materials for this task...");
                        return ActionResult.Fail;
                    }
                    Logger.WriteLine("Don't have high quality materials, use normal ones.");
                }
                var usedAssets = new List<InventorySlot>();
                List<InventorySlot> assets =
                    Professions2.AssetsBags.OrderByDescending(slot => Professions2.AssetProfessionLevel(slot.Item))
                        .ToList();
                int i = 0;
                Task.UsedAssets.RemoveAll(t => t.ItemInternalName == "-");
                while (i < Task.UsedAssets.Count)
                {
                    TaskAsset taskAsset = Task.UsedAssets[i];
                    Slot slot = definition.Slots[i];
                    InventorySlot inventorySlot = new InventorySlot(IntPtr.Zero);
                    InventorySlot inventorySlot2 = new InventorySlot(IntPtr.Zero);
                    int num2 = int.MaxValue;
                    using (List<InventorySlot>.Enumerator enumerator3 = assets.GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            InventorySlot inventorySlot3 = enumerator3.Current;
                            if (Professions2.AssetCanBeSlotted(definition, slot, inventorySlot3))
                            {
                                if (!inventorySlot2.IsValid)
                                {
                                    inventorySlot2 = inventorySlot3;
                                }
                                if (taskAsset.ItemInternalName == "AutoBestLevel")
                                {
                                    inventorySlot = inventorySlot3;
                                    break;
                                }
                                if (taskAsset.ItemInternalName == "AutoNearestLevel")
                                {
                                    long num3 = Math.Abs(
                                        definition.Requirements.RequiredNumericValue -
                                        Professions2.AssetProfessionLevel(inventorySlot3.Item));
                                    if (num3 < num2)
                                    {
                                        num2 = (int) num3;
                                        inventorySlot = inventorySlot3;
                                    }
                                }
                                if (inventorySlot3.Item.ItemDef.InternalName == taskAsset.ItemInternalName)
                                {
                                    inventorySlot = inventorySlot3;
                                    break;
                                }
                            }
                        }
                        if (inventorySlot.IsValid)
                        {
                            usedAssets.Add(inventorySlot);
                            assets.Remove(inventorySlot);
                            i++;
                            continue;
                        }
                        if (inventorySlot2.IsValid)
                        {
                            Logger.WriteLine($"Can't find asset with parameters for slot {i + 1}, use default one !");
                            usedAssets.Add(inventorySlot2);
                            assets.Remove(inventorySlot2);
                            i++;
                            continue;
                        }
                        Logger.WriteLine($"Can't find required asset for slot {i + 1} !");
                        return ActionResult.Fail;
                    }
                }

                Logger.WriteLine("Start task using " + usedAssets.Count + " assets");
                for (int j = 0; j < usedAssets.Count; j++)
                {
                    Logger.WriteLine($"{j + 1}. {usedAssets[j].Item.DisplayName}");
                }

                var haveMorale = false;
                if (definition.InternalName.StartsWith("Professions_") &&
                    !definition.InternalName.StartsWith("Professions_Gathering_"))
                {
                    var moraleCost = definition.ForceCompleteCost;
                    if (ShouldUseSendingStone())
                        moraleCost += 5;
                    if (moraleCost <= (ulong) Professions2.Morale)
                        haveMorale = true;
                }
                Task.TaskType taskType = Task.Type;
                if (taskType == Task.TaskType.InstantOnly && !haveMorale)
                {
                    Logger.WriteLine("Can't instant this order !");
                    return ActionResult.Fail;
                }
                if (taskType == Task.TaskType.InstantIfPossible && !haveMorale)
                    taskType = Task.TaskType.Order;

                var firstAvailableSlot = Professions2.FirstAvailableSlot;
                if (taskType == Task.TaskType.Order)
                {
                    definition.StartAssignment(usedAssets, Task.GetHQMatsAsDic(), firstAvailableSlot,
                        (uint) (Task.OrdersCount - orderCount));
                    break;
                }

                definition.StartAssignment(usedAssets, Task.GetHQMatsAsDic(), firstAvailableSlot, 0, true);
                Pause.Sleep(500);
                EntityManager.LocalPlayer.Player.RefreshAssignments();
                Pause.Sleep(500);
                if (taskType != Task.TaskType.Order)
                {
                    foreach (Assignment assignment in Professions2.DeliveryBoxAll.OrderByDescending(a => a.TimeStarted))
                    {
                        if (assignment.ReadyToComplete && assignment.Def.InternalName == Task.InternalName)
                        {
                            assignment.Complete();
                            break;
                        }
                    }
                }
                orderCount++;
                if (orderCount >= Task.OrdersCount && Task.OrdersCount != -1)
                    break;
            }
            return ActionResult.Completed;
        }

        [Editor(typeof(TaskEditor), typeof(UITypeEditor))]
        public Task Task { get; set; }
        public StartATask.SendingStoneUsage SendingStone { get; set; }
    }
}
