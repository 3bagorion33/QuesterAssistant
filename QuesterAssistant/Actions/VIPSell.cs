using Astral;
using Astral.Classes.ItemFilter;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.Classes.Actions;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Threading;

namespace QuesterAssistant.Actions
{
    public class VIPSell : Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => string.Empty;
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;
        public override void GatherInfos() {}
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        public VIPSell()
        {
            Vendor = new NPCInfos();
            BuyOptions = new List<BuyItemsOption>();
            SellOptions = new ItemFilterCore();
            BuyMenus = new List<string>();
            UseGeneralSettingsToBuy = false;
            UseGeneralSettingsToSell = false;
        }

        public override ActionResult Run()
        {
            NPCInfos GetVendor()
            {
                var npc = new NPCInfos();

                if (VIP.CanSummonProfessionVendor)
                {
                    npc.CostumeName = "VIPProfessionVendor";
                    goto Return;
                }
                if (VIP.CanSummonSealTrader)
                {
                    npc.CostumeName = "VIPSummonSealTrader";
                    goto Return;
                }
                if (SpecialVendor.VendorArtifactUsable())
                {
                    npc.CostumeName = "ArtifactVendor";
                    goto Return;
                }
                Return:
                return npc;
            }

            var slot1 = EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.MainArtifact).GetItems.First();
            var slot2 = EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.SecondaryArtifacts).GetItems.First();

            slot1.MoveAll(InvBagIDs.SecondaryArtifacts);

            bool flag = API.CurrentSettings.DiscardIfCantSale;
            API.CurrentSettings.DiscardIfCantSale = true;

            var buySellItems = new BuyItems()
            {
                Vendor = GetVendor(),
                BuyOptions = BuyOptions,
                SellOptions = SellOptions,
                UseGeneralSettingsToBuy = UseGeneralSettingsToBuy,
                UseGeneralSettingsToSell = UseGeneralSettingsToSell,
                BuyMenus = BuyMenus
            };

            if (buySellItems.Vendor.CostumeName == string.Empty)
            {
                Logger.WriteLine("Character haven't necessary VIP Rank or VIP has been expired");
                return ActionResult.Skip;
            }

            var result = buySellItems.Run();

            API.CurrentSettings.DiscardIfCantSale = flag;
            Astral.Logic.NW.Inventory.FreeOverFlowBags();
            return result;
        }

        [Browsable(false)]
        [Editor(typeof(NPCVendorInfos), typeof(UITypeEditor))]
        public NPCInfos Vendor { get; set; }

        [Editor(typeof(BuyOptionsEditor), typeof(UITypeEditor))]
        public List<BuyItemsOption> BuyOptions { get; set; }

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore SellOptions { get; set; }

        [Description("Use options set in general settings to buy")]
        public bool UseGeneralSettingsToBuy { get; set; }

        [Description("Use options set in general settings to sell")]
        public bool UseGeneralSettingsToSell { get; set; }

        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        [Description("Specific dialogs before reaching item list.")]
        public List<string> BuyMenus { get; set; }
    }
}

