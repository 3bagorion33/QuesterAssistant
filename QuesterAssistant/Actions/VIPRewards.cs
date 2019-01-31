using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class VIPRewards : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => "VIPRewards";
        public override string Category => "QuesterAssistant";
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();

        private bool IsAccountRewardClaimed = false;
        private bool IsCharacterRewardClaimed = false;

        [Description("Claim account reward if possible")]
        public bool ClaimAccountReward { get; set; }

        [Description("Claim character reward if possible")]
        public bool ClaimCharacterReward { get; set; }

        [Description("Open this rewards")]
        public bool OpenRewards { get; set; }

        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public VIPRewards()
        {
            this.ClaimAccountReward = false;
            this.ClaimCharacterReward = true;
            this.OpenRewards = true;
        }

        private void OpenRewardBoxes()
        {
            List<InventorySlot> slots = new List<InventorySlot>();
            List<InventorySlot>.Enumerator enumerator = EntityManager.LocalPlayer.BagsItems.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    InventorySlot slot = enumerator.Current;
                    Item item = slot.Item;
                    uint num = item.Count;
                    if (item.ItemDef.RewardPackInfo.IsValid && item.ItemDef.InternalName.Contains("Vip"))
                    {
                        Debug.WriteLine("Count: " + item.Count.ToString());
                        for (uint i = 0; i < num; i++)
                        {
                            Logger.WriteLine("Open '" + item.ItemDef.DisplayName + "'");
                            slot.Equip();
                            Astral.Logic.NW.General.RandomPause(200, 350);
                        }
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            Thread.Sleep(500);
            if (Game.IsRewardpackviewerFrameVisible())
            {
                Debug.WriteLine("IsRewardpackviewerFrameVisible");
                Game.CloseRewardpackviewerFrame();
            }
        }

        public override ActionResult Run()
        {
            Astral.Logic.NW.Inventory.FreeOverFlowBags();
            Debug.WriteLine("AccountRewardAvailable: " + VIP.AccountRewardAvailable.ToString());
            while (this.ClaimAccountReward && VIP.AccountRewardAvailable)
            {
                Debug.WriteLine("ClaimAccountReward");
                VIP.ClaimAccountReward();
                Thread.Sleep(1000);
                this.IsAccountRewardClaimed = true;
            }
            Debug.WriteLine("AccountRewardAvailable: " + VIP.AccountRewardAvailable.ToString());
            Debug.WriteLine("CharacterRewardAvailable: " + VIP.CharacterRewardAvailable.ToString());
            while (this.ClaimCharacterReward && VIP.CharacterRewardAvailable)
            {
                Debug.WriteLine("ClaimCharacterReward");
                VIP.ClaimCharacterReward();
                Thread.Sleep(500);
                this.IsCharacterRewardClaimed = true;
            }
            Debug.WriteLine("CharacterRewardAvailable: " + VIP.CharacterRewardAvailable.ToString());
            if (this.IsCharacterRewardClaimed || this.IsAccountRewardClaimed)
            {
                Thread.Sleep(100);
                Debug.WriteLine("Try open rewards");
                OpenRewardBoxes();
            }
            return ActionResult.Completed;
        }
    }
}
