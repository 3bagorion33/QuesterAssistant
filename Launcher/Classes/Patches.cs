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
            new Patch //Astral.Forms.Main.method_5
            {
                Name = "RewriteTitle",
                Desc = "Hides Astral's main title",
                Active = true,
                Bytes = new List<Bytes>
                {
                    new Bytes
                    {
                        Orig = new byte[] {0x06, 0x6F, 0x17, 0x01, 0x00, 0x0A, 0x28, 0xD2},
                        Ptch = new byte[] {0x06, 0x6F, 0xB0, 0x01, 0x00, 0x0A, 0x28, 0xD2},
                    },
                    new Bytes
                    {
                        Orig = new byte[] {0x18, 0x06, 0x72, 0x1F, 0x89, 0x01, 0x70, 0x7D},
                        Ptch = new byte[] {0x18, 0x06, 0x7E, 0x63, 0x00, 0x00, 0x0A, 0x7D}
                    }
                }
            },
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
            }
        };
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
