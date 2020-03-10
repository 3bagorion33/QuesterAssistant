using System;
using System.Timers;

namespace QuesterAssistant.Classes.Monitoring
{
    internal class Foreground : AMonitor<Foreground>
    {
        public event EventHandler OnForeground;
        public event EventHandler OnBackground;
        private bool prevForegroundState;
        private IntPtr prevGameHandle;

        protected override void OnTick(object sender, ElapsedEventArgs e)
        {
            if (OnForeground is null && OnBackground is null)
                return;
            var isForeground = Core.IsGameForeground;
            var gameHandle = Core.GameWindowHandle;
            if (gameHandle != prevGameHandle || prevForegroundState != isForeground)
            {
                prevForegroundState = isForeground;
                prevGameHandle = gameHandle;
                if (isForeground)
                    OnForeground?.Invoke(this, new EventArgs());
                else
                    OnBackground?.Invoke(this, new EventArgs());
            }
        }
    }
}