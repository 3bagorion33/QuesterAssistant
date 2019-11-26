using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Timers;

namespace QuesterAssistant.Classes.Common
{
    [DataContract]
    public abstract class NotifyHashChanged : OverrideHash, INotifyPropertyChanged, IDisposable
    {
        private int prevHashCode;
        private static readonly Timer checkChanged = new Timer { Enabled = true, Interval = 200, AutoReset = true };
        public event Action HashChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected NotifyHashChanged()
        {
            prevHashCode = 0;
            checkChanged.Elapsed += CheckChanged_Tick;
        }

        private void CheckChanged_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Tick);
        }

        private void Tick()
        {
            var hashCode = GetHashCode();
            if (prevHashCode != hashCode)
            {
                prevHashCode = hashCode;
                HashChanged?.Invoke();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(GetType().Name));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (PropertyChanged != null)
                    foreach (var d in PropertyChanged.GetInvocationList())
                        PropertyChanged -= d as PropertyChangedEventHandler;

                if (HashChanged != null)
                    foreach (var d in HashChanged.GetInvocationList())
                        HashChanged -= d as Action;

                checkChanged.Elapsed -= CheckChanged_Tick;
            }
        }
    }
}
