using System.Collections.Generic;
using System.ComponentModel;
using QuesterAssistant.Classes.Common;

namespace Launcher.Classes
{
    sealed class Patches : NotifyHashChanged
    {
        [HashInclude]
        public BindingList<Patch> Items { get; } = new BindingList<Patch>
        {
            new Patch
            {
                Name = "AssemblyInfo",
                Desc = "Cleans AssemblyInfo which contains 'Astral' word",
                Active = true,
                Bytes = new List<Bytes>
                {
                    new Bytes
                    {
                        Orig = new byte[] {0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00},
                        Ptch = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},

                    },
                    new Bytes
                    {
                        Orig = new byte[] {0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x2E, 0x00, 0x65, 0x00,0x78, 0x00, 0x65, 0x00},
                        Ptch = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x00, 0x00, 0x00, 0x00},

                    },
                }
            },
            new Patch // Astral.Quester.FSM.States.CheckAction.get_NeedToRun()
            {
                Name = "PatrolPause",
                Desc = "Decreases pause in patrol mode from 10sec to 200msec",
                Active = true,
                Bytes = new List<Bytes>
                {
                    new Bytes
                    {
                        Orig = new byte[] {0x06, 0x02, 0x20, 0x10, 0x27, 0x00, 0x00, 0x73},
                        Ptch = new byte[] {0x06, 0x02, 0x20, 0xC8, 0x00, 0x00, 0x00, 0x73}
                    }
                }
            },
            new Patch // Astral.Professions.Controllers.Characters.\u0001()
            {
                Name = "ProfessionStatistic",
                Desc = "Compability with QA patch for professions",
                Active = true,
                Bytes = new List<Bytes>
                {
                    new Bytes
                    {
                        Orig = new byte[] { 0xFE, 0x06, 0xE9, 0x0F },
                        Ptch = new byte[] { 0xFE, 0x06, 0x0D, 0x1B },
                    }
                }
            }
        };
        public Patches() => HashEventEnable();
    }
    public class Patch : OverrideHash
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        [HashInclude]
        public bool Active { get; set; }
        public List<Bytes> Bytes;
    }

    public class Bytes
    {
        public byte[] Orig;
        public byte[] Ptch;
    }
}
