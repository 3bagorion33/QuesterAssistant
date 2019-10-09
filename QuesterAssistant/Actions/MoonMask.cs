using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Threading;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class MoonMask : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;

        protected override bool IntenalConditions
        {
            get
            {
                if ((VIP.Rank >= 2) & (VIP.ExpirationSecondsLeft > 0)) return true;
                Logger.WriteLine("Character haven't necessary VIP Rank or VIP has been expired");
                return false;
            }
        }

        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();

        public override void GatherInfos() {}
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        public override ActionResult Run()
        {
            if (!IntenalConditions) return ActionResult.Fail;

            VIP.TeleportToMoonstoneMask();
            Thread.Sleep(5000);
            while (EntityManager.LocalPlayer.IsLoading)
                Thread.Sleep(500);
            return ActionResult.Completed;
        }
    }
}
