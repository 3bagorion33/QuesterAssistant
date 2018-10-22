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
    public class VIPRewards : Astral.Quester.Classes.Action
    {
        private bool IsAccountRewardClaimed = false;
        private bool IsCharacterRewardClaimed = false;

        public override string ActionLabel => "VIPRewards";

        public override bool NeedToRun => true;

        public override string InternalDisplayName => string.Empty;

        public override bool UseHotSpots => false;

        protected override bool IntenalConditions => true;

        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity => new ActionValidity();

        [Description("")]
        bool ClaimAccountReward => false;

        [Description("")]
        bool ClaimCharacterReward => true;

        [Description("")]
        bool OpenRewards => true;

        public override void GatherInfos()
        {
        }

        public override void InternalReset()
        {
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
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
                    if (item.ItemDef.InternalName.Contains("Vip"))
                    {
                        for (uint i = 0; i < item.Count; i++)
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
                if (Game.IsRewardpackviewerFrameVisible()) Game.CloseRewardpackviewerFrame();
            }
        }

        public override ActionResult Run()
        {
            while (this.ClaimAccountReward && VIP.AccountRewardAvailable)
            {
                Core.DebugWriteLine("AccountRewardAvailable: " + VIP.AccountRewardAvailable.ToString());
                Core.DebugWriteLine("ClaimAccountReward");
                VIP.ClaimAccountReward();
                Thread.Sleep(100);
                Core.DebugWriteLine("AccountRewardAvailable: " + VIP.AccountRewardAvailable.ToString());
                this.IsAccountRewardClaimed = true;
            }

            while (this.ClaimCharacterReward && VIP.CharacterRewardAvailable)
            {
                Core.DebugWriteLine("CharacterRewardAvailable: " + VIP.CharacterRewardAvailable.ToString());
                Core.DebugWriteLine("ClaimCharacterReward");
                VIP.ClaimCharacterReward();
                Thread.Sleep(100);
                Core.DebugWriteLine("CharacterRewardAvailable: " + VIP.CharacterRewardAvailable.ToString());
                this.IsCharacterRewardClaimed = true;
            }

            if (this.IsCharacterRewardClaimed || this.IsAccountRewardClaimed)
            {
                Core.DebugWriteLine("Try open rewards");
                OpenRewardBoxes();
            }

            return ActionResult.Completed;
        }
    }
}
