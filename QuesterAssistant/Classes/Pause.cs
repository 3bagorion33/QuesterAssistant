using System;
using System.Threading;
using QuesterAssistant.Classes.Common;
using Timeout = Astral.Classes.Timeout;

namespace QuesterAssistant.Classes
{
    internal class Pause
    {
        private static readonly Random Random = new Random();
        private readonly Timeout timeout;

        private int min;
        private int max;

        public Pause(int @int)
        {
            timeout = new Timeout(@int);
        }

        public Pause(int min, int max)
        {
            this.min = min;
            this.max = max;
            timeout = new Timeout(RandomNext());
        }

        public Pause(MinMaxPair<uint> timeOut)
        {
            min = (int)timeOut.Min;
            max = (int)timeOut.Max;
            timeout = new Timeout(RandomNext());
        }

        private int RandomNext()
        {
            return Random.Next(min, max);
        }

        public static void Sleep(int @int)
        {
            Thread.Sleep(@int);
        }

        public static void RandomSleep(int min, int max)
        {
            Thread.Sleep(Random.Next(min, max));
        }

        public void Waiting()
        {
            Thread.Sleep(timeout.Left);
        }

        public void RandomWaiting()
        {
            Waiting();
            timeout.ChangeTime(RandomNext());
        }

        public void Reset()
        {
            timeout.Reset();
        }
    }
}
