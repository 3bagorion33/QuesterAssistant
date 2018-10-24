using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using MyNW.Classes;
using System.Threading;

namespace QuesterAssistant
{
    public class VIPSell : Astral.Quester.Classes.Action
    {
        // Properties
        public override string ActionLabel => "VIPSell";

        protected override bool IntenalConditions => true;

        protected override Vector3 InternalDestination => new Vector3();

        public override string InternalDisplayName => "VIPSell";

        protected override ActionValidity InternalValidity => new ActionValidity();

        public override bool NeedToRun => true;

        public override bool UseHotSpots => false;

        // Methods
        public override void GatherInfos() {}

        public override void InternalReset() {}

        public override void OnMapDraw(GraphicsNW graph) {}

        public override ActionResult Run()
        {
            if (VIP.CanSummonProfessionVendor)
            {
                bool flag = API.CurrentSettings.DiscardIfCantSale;
                Core.DebugWriteLine("DiscardIfCantSale: " + flag.ToString());
                API.CurrentSettings.DiscardIfCantSale = true;
                VIP.SummonProfessionVendor();
                Thread.Sleep(0x7d0);
                VIP.InteractProfessionVendor();
                Thread.Sleep(200);
                Interact.SellItems();
                Thread.Sleep(0x7d0);
                API.CurrentSettings.DiscardIfCantSale = flag;
                Core.DebugWriteLine("DiscardIfCantSale: " + API.CurrentSettings.DiscardIfCantSale.ToString());
                Astral.Logic.NW.Inventory.FreeOverFlowBags();
                return ActionResult.Completed;
            }
            else
            {
                Logger.WriteLine("Character haven't necessary VIP Rank or VIP has been expired");
                return ActionResult.Skip;
            }
        }
    }
}

