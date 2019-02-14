using Astral.Quester.Classes;
using System;
using Astral.Classes;
using System.ComponentModel;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class CountOfRuns : Condition
    {
        private uint count = 1;
        private Timeout resetTimer = new Timeout(100);

        private string DisplayName => GetType().Name;
        public override string TestInfos => $"Count of runs : {count}";
        public override bool IsValid
        {
            get
            {
                if (count < Count)
                    return true;
                resetTimer.Reset();
                return false;
            }
        }
        
        [Description("Is valid while count of runs less then value. For Loop property only!")]
        public uint Count { get; set; }

        public CountOfRuns()
        {
            Count = 1;
        }

        public override void Reset()
        {
            if (count < Count)
            {
                count++;
                return;
            }
            if (resetTimer.IsTimedOut)
            {
                count = 1;
            }
        }

        public override string ToString()
        {
            return $"{DisplayName} : {Count}"; 
        }
    }
}
