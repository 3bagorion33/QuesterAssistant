using Astral.Quester.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Utils.Extensions;
using Astral.Classes;
using System.ComponentModel;
using Astral.Quester;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class CountOfRuns : Condition
    {
        private uint count = 1;
        private Timeout resetTimer = new Timeout(100);

        private string DisplayName => GetType().Name;
        public override string TestInfos => "Count of runs : " + count;
        public override bool IsValid
        {
            get
            {
                if (count < Count)
                {
                    Debug.WriteLine(DisplayName + ": IsValid => " + count);
                    return true;
                }
                resetTimer.Reset();
                Debug.WriteLine(DisplayName + ": IsNotValid => " + count);
                return false;
            }
        }
        
        [Description("Is valid while count of runs less then value. For Loop property only!")]
        public uint Count { get; set; }

        public CountOfRuns()
        {
            Debug.WriteLine(DisplayName + ": Const()");
            Count = 1;
        }

        public override void Reset()
        {
            Debug.WriteLine(DisplayName + ": Reset()");
            if (count < Count)
            {
                count++;
                return;
            }
            if (resetTimer.IsTimedOut)
            {
                Debug.WriteLine(DisplayName + ": IsTimedOut");
                count = 1;
            }
        }

        public override string ToString()
        {
            return DisplayName + " : " + Count; 
        }
    }
}
