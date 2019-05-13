using Astral;
using MyNW.Classes;
using MyNW.Classes.ItemProgression;
using MyNW.Internals;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QuesterAssistant.UpgradeManager
{
    internal class UpgradeManagerCore : ACore<UpgradeManagerData, UpgradeManagerForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => false;

        private List<InventorySlot> BagsItems => EntityManager.LocalPlayer.BagsItems;
        private const int TIME_WAIT = 300;

        public void UpgradeOnce()
        {
            InventorySlot slot;
            string iName = "T5_Runestone_Red"; // Fuse_Ward_Preservation_Invocation

            bool FindUnfilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == iName &&
                    s.Item.ProgressionLogic.CurrentRankXP == 0;
            }

            bool FindPartiallyFilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == iName &&
                    s.Item.ProgressionLogic.CurrentRankXP > 0 &&
                    s.Item.ProgressionLogic.CurrentRankTotalRequiredXP > s.Item.ProgressionLogic.CurrentRankXP;
            }

            bool FindFullFilled(InventorySlot s)
            {
                return s.Item.ItemDef.InternalName == iName &&
                    s.Item.ProgressionLogic.CurrentTier.Index > 0 &&
                    s.Item.ProgressionLogic.CurrentRankTotalRequiredXP == s.Item.ProgressionLogic.CurrentRankXP;
            }

            bool Feed(InventorySlot s)
            {
                int points;
                ItemProgressionLogic progress = s.Item.ProgressionLogic;

                if (progress.CurrentTier.Index == 0)
                    points = 1;
                else
                    points = (int)(progress.CurrentRankTotalRequiredXP - progress.CurrentRankXP);

                if (EntityManager.LocalPlayer.Inventory.RefinementCurrency >= points)
                {
                    s.Feed(points);
                    Thread.Sleep(TIME_WAIT);
                    return true;
                }
                return false;
            }

            slot = BagsItems.Find(FindFullFilled);
            if (slot != null)
            {
                if (slot.Item.ProgressionLogic.CurrentTier.HaveRequiredCatalystItems)
                {
                    slot.Evolve();
                    //var catal = BagsItems.First(s => s.Item.ItemDef.InternalName.Contains("Fuse_Ward_Preservation_"));
                    //slot.Evolve(catal);
                    Thread.Sleep(TIME_WAIT);
                    slot.Group();
                }
                Logger.WriteLine("Havent Required Items!");
                return;
            }

            slot = BagsItems.Find(FindPartiallyFilled);
            if (slot != null)
            {
                Feed(slot);
                UpgradeOnce();
                return;
            }

            slot = BagsItems.Find(FindUnfilled);
            if (slot != null && EntityManager.LocalPlayer.BagsFreeSlots > 0)
            {
                Feed(slot);
                UpgradeOnce();
                return;
            }
        }

        protected override void KeyboardHook(object sender, KeyEventArgs e)
        {
        }
    }
}
