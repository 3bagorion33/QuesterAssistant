using System;
using System.Threading;
using QuesterAssistant.Classes.Common;
using Timeout = Astral.Classes.Timeout;

namespace QuesterAssistant.Classes
{
    internal class Pause
    {
        private static readonly Random random = new Random();
        private readonly Timeout timeout;
        private int min;
        private int max;

        public int Left => timeout.Left;
        public int TimeOut { get; private set; }

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
            TimeOut = random.Next(min, max);
            return TimeOut;
        }

        public static int Random(int min, int max)
        {
            return new Pause(min, max).RandomNext();
        }

        public static void Sleep(int @int)
        {
            Thread.Sleep(@int);
        }

        public static void RandomSleep(MinMaxPair<uint> timeOut)
        {
            new Pause(timeOut).Waiting();
        }

        public static void RandomSleep(int min, int max)
        {
            Thread.Sleep(new Pause(min, max).RandomNext());
        }

        public void Waiting()
        {
            Thread.Sleep(timeout.Left);
        }

        public void WaitingRandom()
        {
            Waiting();
            timeout.ChangeTime(RandomNext());
        }

        public void Reset()
        {
            timeout.Reset();
        }

        public void ResetRandom()
        {
            timeout.ChangeTime(RandomNext());
            timeout.Reset();
        }
    }
}
