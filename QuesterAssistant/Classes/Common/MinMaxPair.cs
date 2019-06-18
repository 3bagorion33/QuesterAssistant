using System;
using System.ComponentModel;

namespace QuesterAssistant.Classes.Common
{
    [Serializable]
    [TypeConverter(typeof(PropertySorter))]
    public class MinMaxPair<T> where T : struct
    {
        private T min;
        private T max;

        [PropertyOrder(0)]
        public T Min
        {
            get => min;
            set
            {
                Check(value, max);
                min = value;
            }
        }

        [PropertyOrder(1)]
        public T Max
        {
            get => max;
            set
            {
                Check(min, value);
                max = value;
            }
        }

        public MinMaxPair() { }
        public MinMaxPair(T min, T max)
        {
            Check(min, max);
            this.min = min;
            this.max = max;
        }

        private void Check(T min, T max)
        {
            if (min is int && (int)(object)this.min > 0 && (int)(object)this.max > 0 && (int)(object)min >= (int)(object)max)
                throw new ArgumentOutOfRangeException(nameof(min), $@"{nameof(Min)} value must be smaller than {nameof(Max)}");
            if (min is uint && (uint)(object)this.min > 0 && (uint)(object)this.max > 0 && (uint)(object)min >= (uint)(object)max)
                throw new ArgumentOutOfRangeException(nameof(min), $@"{nameof(Min)} value must be smaller than {nameof(Max)}");
        }

        public override string ToString()
        {
            return $"{Min} ... {Max}";
        }
    }
}
