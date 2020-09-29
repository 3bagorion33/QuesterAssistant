using Astral.Quester;
using Astral.Quester.Classes;
using MyNW.Internals;
using QuesterAssistant.Actions;

namespace QuesterAssistant.Classes.Monitoring
{
    internal static class Frames
    {
        private static Action prevAction = new ActionPack();

        public static void Start()
        {
            API.BeforePlayingAction += API_BeforePlayingAction;
        }

        public static void Stop()
        {
            API.BeforePlayingAction -= API_BeforePlayingAction;
        }

        private static void API_BeforePlayingAction(object sender, API.BeforePlayingActionEventArgs e)
        {
            var actionToPlay = e.ActionToPlay;

            if (prevAction.ActionID == actionToPlay.ActionID)
                return;
            if (actionToPlay is ActionPack || actionToPlay.Disabled)
                return;

            if (prevAction is ActionPack || prevAction.Disabled)
                goto Exit;
            if (prevAction is IActionCloseFrame a2 && !a2.CloseFrame)
                goto Exit;
            if (prevAction is CloseAllFrames a3 && !a3.Value)
                goto Exit;
            if (actionToPlay is CloseAllFrames a4 && !a4.Value)
                goto Exit;
            if (actionToPlay.GetType() != prevAction.GetType())
                Close();

            Exit:
            prevAction = actionToPlay;
        }

        private static void Close()
        {
            if (Auction.IsAuctionFrameVisible())
                Auction.CloseAuctionFrame();

            if (Email.IsMailFrameVisible())
                Email.CloseMailFrame();

            if (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.IsValid)
                EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();

            if (Game.IsRewardpackviewerFrameVisible())
                Game.CloseRewardpackviewerFrame();

            if (Game.IsInvocationResultsFrameVisible())
                Game.CloseInvocationResultsFrame();

            Game.ToggleCursorMode(false);
        }

        public interface IActionCloseFrame
        {
            bool CloseFrame { get; }
        }
    }
}