using System;
using System.Timers;

namespace QuesterAssistant.Classes.Common
{
    public abstract class NotifyChanged
    {
        private const int seedPrimeNumber = 691;
        private const int fieldPrimeNumber = 397;
        private int prevHashCode;
        private static Timer checkChanged;
        public event Action OnChanged;

        public NotifyChanged()
        {
            prevHashCode = 0;
            checkChanged = new Timer()
            {
                Enabled = true,
                Interval = 100,
                AutoReset = true
            };
            checkChanged.Elapsed += CheckChanged_Tick;
        }

        private void CheckChanged_Tick(object sender, EventArgs e)
        {
            if (prevHashCode != GetHashCode())
            {
                prevHashCode = GetHashCode();
                OnChanged?.Invoke();
            }
        }

        protected int GetHashCodeFromFields<T1>(T1 obj1)
        {
            int hashCode = seedPrimeNumber;
            if (obj1 != null)
                hashCode *= fieldPrimeNumber + obj1.GetHashCode();
            return hashCode;
        }
        protected int GetHashCodeFromFields<T1, T2>(T1 obj1, T2 obj2)
        {
            int hashCode = GetHashCodeFromFields(obj1);
            if (obj2 != null)
                hashCode *= fieldPrimeNumber + obj2.GetHashCode();
            return hashCode;
        }
        protected int GetHashCodeFromFields<T1, T2, T3>(T1 obj1, T2 obj2, T3 obj3)
        {
            int hashCode = GetHashCodeFromFields(obj1, obj2);
            if (obj3 != null)
                hashCode *= fieldPrimeNumber + obj3.GetHashCode();
            return hashCode;
        }
        protected int GetHashCodeFromFields<T1, T2, T3, T4>(T1 obj1, T2 obj2, T3 obj3, T4 obj4)
        {
            int hashCode = GetHashCodeFromFields(obj1, obj2, obj3);
            if (obj4 != null)
                hashCode *= fieldPrimeNumber + obj4.GetHashCode();
            return hashCode;
        }

        public abstract override int GetHashCode();
    }
}
