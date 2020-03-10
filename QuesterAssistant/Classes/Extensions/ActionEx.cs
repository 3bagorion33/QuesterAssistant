using Astral.Quester;
using Astral.Quester.Classes;
using MyNW.Internals;
using QuesterAssistant.Actions;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class ActionEx
    {
        public static void CloseFrames(this Action action)
        {
            var type = action.GetType();
            var actions = API.CurrentProfile.GetFullActionList(API.CurrentProfile.MainActionPack);
            for (int i = actions.IndexOf(action) + 1; i < actions.Count + 1; i++)
            {
                if (actions[i].GetType() != typeof(ActionPack))
                {
                    if (actions[i].GetType() != type || i == actions.Count)
                    {
                        if (type == typeof(AuctionSellItems))
                            Auction.CloseAuctionFrame();
                    }
                    break;
                }
            }
        }
    }
}