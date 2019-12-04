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
        private bool hashEventEnabled = true;
        private int prevHashCode;
        private readonly Timer checkChanged = new Timer { Enabled = true, Interval = 200, AutoReset = true };
        public event Action HashChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected NotifyHashChanged()
        {
            prevHashCode = 0;
            checkChanged.Elapsed += CheckChanged_Tick;
        }

        private void CheckChanged_Tick(object sender, EventArgs e)
        {
            if (hashEventEnabled && (HashChanged != null ||PropertyChanged != null))
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

        public void HashEventEnable()
        {
            hashEventEnabled = true;
        }

        public void HashEventDisable()
        {
            hashEventEnabled = false;
        }

        private void PropertyChangedUnsubscribe()
        {
            if (PropertyChanged != null)
                foreach (var d in PropertyChanged.GetInvocationList())
                    PropertyChanged -= d as PropertyChangedEventHandler;
        }

        private void HashChangedUnsubscribe()
        {
            if (HashChanged != null)
                foreach (var d in HashChanged.GetInvocationList())
                    HashChanged -= d as Action;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                PropertyChangedUnsubscribe();
                HashChangedUnsubscribe();
                checkChanged.Elapsed -= CheckChanged_Tick;
            }
        }
    }
}
