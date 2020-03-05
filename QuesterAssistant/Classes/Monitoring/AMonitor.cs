using System.Timers;

namespace QuesterAssistant.Classes.Monitoring
{
    internal abstract class AMonitor
    {
        private static readonly Timer TickTimer = new Timer {Interval = 250, Enabled = true};
        protected AMonitor()
        {
            TickTimer.Elapsed += OnTick;
        }
        protected abstract void OnTick(object sender, ElapsedEventArgs e);
    }
}