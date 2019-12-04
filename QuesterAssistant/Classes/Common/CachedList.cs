using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace QuesterAssistant.Classes.Common
{
    public class CachedList<T> : List<T>
    {
        private Func<List<T>> search;
        private Timer timer = new Timer
        {
            Enabled = true,
            AutoReset = true,
            Interval = 20
        };

        public CachedList(Func<List<T>> searchGetter)
        {
            search = searchGetter;
            timer.Elapsed += TimerOnElapsed;
        }

        private void Update()
        {
            lock ((this as ICollection).SyncRoot)
            {
                Clear();
                AddRange(search());
            }
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Task.Factory.StartNew(Update);
        }
    }
}