using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using MyNW.Classes;
using System.Threading;

namespace QuesterAssistant.Actions
{
    public class VIPSell : Astral.Quester.Classes.Action
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

        public override ActionResult Run()
        {
            if (VIP.CanSummonProfessionVendor)
            {
                bool flag = API.CurrentSettings.DiscardIfCantSale;
                API.CurrentSettings.DiscardIfCantSale = true;
                VIP.SummonProfessionVendor();
                Thread.Sleep(2000);
                VIP.InteractProfessionVendor();
                Thread.Sleep(200);
                Interact.SellItems();
                Thread.Sleep(2000);
                API.CurrentSettings.DiscardIfCantSale = flag;
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

