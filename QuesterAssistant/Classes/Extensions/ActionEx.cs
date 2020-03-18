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
                if (i < actions.Count && (actions[i].Disabled || actions[i].GetType() == typeof(ActionPack))) continue;
                if (i == actions.Count || actions[i].GetType() != type)
                {
                    if (type == typeof(AuctionSellItems))
                    {
                        Auction.CloseAuctionFrame();
                        return;
                    }
                    if (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.IsValid)
                    {
                        EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();
                        return;
                    }
                    Game.ToggleCursorMode(false);
                }
                break;
            }
        }
    }
}