using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Timers;

namespace QuesterAssistant.Classes.Common
{
    [DataContract]
    public abstract class NotifyHashChanged : INotifyPropertyChanged, IDisposable
    {
        private int prevHashCode;
        private static Timer checkChanged;
        public event Action HashChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyHashChanged()
        {
            prevHashCode = 0;
            checkChanged = new Timer()
            {
                Enabled = true,
                Interval = 200,
                AutoReset = true
            };
            checkChanged.Elapsed += CheckChanged_Tick;
        }

        private void CheckChanged_Tick(object sender, EventArgs e)
        {
            void Tick()
            {
                var hashCode = GetHashCode();
                if (prevHashCode != hashCode)
                {
                    prevHashCode = hashCode;
                    HashChanged?.Invoke();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(GetType().Name));
                }
            }
            Task.Factory.StartNew(Tick);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public abstract override int GetHashCode();

        public void Dispose()
        {
            checkChanged.Dispose();
        }
    }
}
