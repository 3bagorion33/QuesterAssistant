using System.Collections.Generic;
using System.Threading.Tasks;
using Astral;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using Timeout = Astral.Classes.Timeout;

namespace QuesterAssistant.Classes.Patches
{
    internal static class Interact
    {
        public static void Patch()
        {
            new PatchMethod(
                typeof(Astral.Logic.NW.Interact).GetMethod(nameof(Astral.Logic.NW.Interact.SellItems)),
                typeof(Interact).GetMethod(nameof(SellItems))).Inject();
            new PatchMethod(
                typeof(Astral.Logic.NW.Interact).GetMethod(nameof(Astral.Logic.NW.Interact.VendorWithDialogs)),
                typeof(Interact).GetMethod(nameof(VendorWithDialogs))).Inject();
        }

        public static void SellItems()
        {
            if (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SellEnabled)
            {
                List<InventorySlot> bagsItems = EntityManager.LocalPlayer.BagsItems;
                bagsItems.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
                bagsItems.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingInventory).GetItems);
                bagsItems.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.FashionItems).GetItems);

                List<Task> tasks = new List<Task>();
                //bagsItems.ForEach(s => tasks.Add(Task.Factory.StartNew(() => SellSlot(s))));
                bagsItems.ForEach(s => SellSlot(s, tasks));
                Task.WaitAll(tasks.ToArray(), 60000);
                Pause.Sleep(800);
            }
        }

        public static bool VendorWithDialogs(Entity vendor, List<string> opKeys = null)
        {
            void Interact(Entity v)
            {
                //var interactOption =
                //    EntityManager.LocalPlayer.Player.InteractStatus.InteractOptions.Find(e => e.Entity == v);
                //if (interactOption != null && interactOption.IsValid && interactOption.InteractOptionType == InteractOptionType.CritterEntity)
                    v.Interact();
            }

            if (Approach.EntityForInteraction(vendor))
            {
                Interact(vendor);
                Pause.Sleep(500);
                var timeout = new Timeout(7500);
                //bool flag = false;
                while (!EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.IsValid)
                {
                    if (timeout.IsTimedOut)
                    {
                        //if (flag || vendor.CanInteract)
                        //{
                        //    return false;
                        //}
                        //flag = true;
                        //Approach.EntityForInteraction(vendor);
                        //Interact(vendor);
                        //Pause.Sleep(500);
                        //timeout.Reset();
                        return false;
                    }
                    Pause.Sleep(500);
                    Interact(vendor);
                }
                Pause.Sleep(500);
                if (opKeys != null && opKeys.Count > 0)
                {
                    Astral.Logic.NW.Interact.DoDialog(opKeys);
                }
                else if (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.ScreenType == ScreenType.List)
                {
                    bool flag2 = false;
                    foreach (ContactDialogOption contactDialogOption in EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Options)
                    {
                        if (contactDialogOption.Type == ContactDialogState.Vendor)
                        {
                            flag2 = true;
                            contactDialogOption.Select();
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        using (List<ContactDialogOption>.Enumerator enumerator = EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Options.GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                enumerator.Current.Select();
                            }
                        }
                    }
                }
                timeout.Reset();
                while (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.StoreItems.Count == 0)
                {
                    if (timeout.IsTimedOut)
                    {
                        return false;
                    }
                    Pause.Sleep(500);
                }
                Pause.Sleep(500);
                return true;
            }
            return false;
        }

        private static void SellSlot(InventorySlot inventorySlot, List<Task> tasks)
        {
            if (Astral.Logic.NW.Interact.AllowedToSell(inventorySlot, API.CurrentSettings.SellFilter))
            {
                if (inventorySlot.Item.ItemDef.CantSell)
                {
                    if (API.CurrentSettings.DiscardIfCantSale && !inventorySlot.Item.ItemDef.CantDiscard)
                    {
                        Logger.WriteLine($"Discard '{inventorySlot.Item.ItemDef.DisplayName}' ...");
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            var timeout = new Timeout(5000);
                            while (inventorySlot.Filled && !timeout.IsTimedOut)
                            {
                                inventorySlot.RemoveAll();
                                Pause.RandomSleep(100, 200);
                            }
                        }));
                    }
                }
                else
                {
                    if (!EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SellEnabled)
                        return;
                    Logger.WriteLine($"Sell '{inventorySlot.Item.ItemDef.DisplayName}' ...");
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        var timeout = new Timeout(5000);
                        while (inventorySlot.Filled && !timeout.IsTimedOut)
                        {
                            inventorySlot.StoreSellItem();
                            Pause.RandomSleep(100, 200);
                        }
                    }));
                }
            }
        }
    }
}