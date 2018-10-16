        private void Refine()
        {
            bool bActionTaken;

            while (bRefining == true) // Global bool, used to determine if the function should be called again. Should be replaced by a return value...
            {
                bActionTaken = false;

                // Do we have available Refinement Points (need at least 2)?
                if (MyNW.Internals.EntityManager.LocalPlayer.Inventory.RefinementCurrency >= 2)
                {
                    // First, find an item to feed:
                    foreach (MyNW.Classes.InventorySlot oInvSlot in MyNW.Internals.EntityManager.LocalPlayer.BagsItems)
                    {
                        if ((oInvSlot.Item.ItemDef.Categories.Contains(MyNW.Patchables.Enums.ItemCategory.Enchantment) ||
                              oInvSlot.Item.ItemDef.Categories.Contains(MyNW.Patchables.Enums.ItemCategory.Runestone)) &&
                              oInvSlot.Item.ProgressionLogic.CurrentTier.Index == 0)
                        {
                            if (oInvSlot.Item.ProgressionLogic.CurrentRankXP == 0)
                            {
                                oInvSlot.Feed(2); // Hard coded to feed 2 refinement points...
                                System.Threading.Thread.Sleep(500);
                                Application.DoEvents();
                                bActionTaken = true;
                                break;
                            }
                        }
                    }

                    // Now find the item we fed and evolve it:
                    foreach (MyNW.Classes.InventorySlot oInvSlot in MyNW.Internals.EntityManager.LocalPlayer.BagsItems)
                    {
                        if (oInvSlot.Item.ItemDef.Categories.Contains(MyNW.Patchables.Enums.ItemCategory.Enchantment) ||
                             oInvSlot.Item.ItemDef.Categories.Contains(MyNW.Patchables.Enums.ItemCategory.Runestone))
                        {
                            if (oInvSlot.Item.ProgressionLogic.CurrentRankXP == 2)
                            {
                                oInvSlot.Evolve();
                                System.Threading.Thread.Sleep(500);
                                Application.DoEvents();
                                bActionTaken = true;
                                //break;
                            }
                        }
                    }

                    //Sort bags: TODO: Need to check counts and only move if won't throw an error.
                    foreach (MyNW.Classes.InventorySlot dstInvSlot in MyNW.Internals.EntityManager.LocalPlayer.BagsItems)
                    {
                        foreach (MyNW.Classes.InventorySlot srcInvSlot in MyNW.Internals.EntityManager.LocalPlayer.BagsItems)
                        {
                            if (srcInvSlot.Filled && dstInvSlot.Filled)
                            {
                                if (srcInvSlot.Item.DisplayName == dstInvSlot.Item.DisplayName)
                                {
                                    if (srcInvSlot.Item.Count + dstInvSlot.Item.Count <= dstInvSlot.Item.ItemDef.StackLimit)
                                    {
                                        srcInvSlot.Move(dstInvSlot.BagId, srcInvSlot.Item.Count);
                                        System.Threading.Thread.Sleep(250);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                // If no action was taken, exit the loop:
                if (bActionTaken == false)
                {
                    bRefining = false;
                }
            }
            btnRefine.Text = "Ready";
        }
