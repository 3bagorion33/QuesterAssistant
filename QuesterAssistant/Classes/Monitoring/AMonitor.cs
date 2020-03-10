using System.Timers;

namespace QuesterAssistant.Classes.Monitoring
{
    internal abstract class AMonitor<TMonitor> where TMonitor : AMonitor<TMonitor>, new()
    {
        public static TMonitor Monitor
        {
            get
            {
                if (!instanted)
                    monitor = new TMonitor();
                return monitor;
            }
        }

        private static TMonitor monitor;
        private static bool instanted;
        private static readonly Timer TickTimer = new Timer {Interval = 250, Enabled = true};
        protected AMonitor()
        {
            instanted = true;
            TickTimer.Elapsed += OnTick;
        }
        protected abstract void OnTick(object sender, ElapsedEventArgs e);
    }
}