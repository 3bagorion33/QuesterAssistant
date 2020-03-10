using System;
using System.Timers;

namespace QuesterAssistant.Classes.Monitoring
{
    internal class GameClient : AMonitor<GameClient>
    {
        public event EventHandler OnNew;
        public event EventHandler OnCrash;
        private IntPtr prevGameHandle;

        protected override void OnTick(object sender, ElapsedEventArgs e)
        {
            if (OnNew is null && OnCrash is null)
                return;
            var gameHandle = Core.GameWindowHandle;
            if (gameHandle != prevGameHandle)
            {
                prevGameHandle = gameHandle;
                if (gameHandle == IntPtr.Zero)
                    OnCrash?.Invoke(this, new EventArgs());
                else
                    OnNew?.Invoke(this, new EventArgs());
            }
        }
    }
}