using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Launcher.Classes
{
    sealed class Patches
    {
        private static readonly List<Patch> REWRITE_TITLE = new List<Patch> // Astral.Forms.Main.method_5
        {
            //new Patch
            //{
            //    Orig = new byte[] {0x1B, 0x30, 0x04, 0x00, 0x46, 0x01, 0x00, 0x00},
            //    Ptch = new byte[] {0x1B, 0x30, 0x0C, 0x00, 0x46, 0x01, 0x00, 0x00},
            //},
            new Patch
            {
                Orig = new byte[] {0x06, 0x6F, 0x17, 0x01, 0x00, 0x0A, 0x28, 0xD2},
                Ptch = new byte[] {0x06, 0x6F, 0xB0, 0x01, 0x00, 0x0A, 0x28, 0xD2},
            },
            new Patch
            {
                Orig = new byte[] {0x18, 0x06, 0x72, 0x1F, 0x89, 0x01, 0x70, 0x7D},
                Ptch = new byte[] {0x18, 0x06, 0x7E, 0x63, 0x00, 0x00, 0x0A, 0x7D}
            }
        };
        
        private static readonly List<Patch> ASSEMBLYINFO = new List<Patch> // AssemblyInfo
        {
            new Patch
            {
                Orig = new byte[] {0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00},
                Ptch = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},

            },
            new Patch
            {
                Orig = new byte[] {0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x2E, 0x00, 0x65, 0x00,0x78, 0x00, 0x65, 0x00},
                Ptch = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x00, 0x00, 0x00, 0x00},

            },
        };

        private static readonly List<Patch> PRAY_PAUSE = new List<Patch> // Astral.Professions.FSM.States.Main.Run
        {
            new Patch
            {
                Orig = new byte[] {0x06, 0x1B, 0x58, 0x28},
                Ptch = new byte[] {0x06, 0x16, 0x58, 0x28},

            },
            //new Patch
            //{
            //    Orig = new byte[] {0x1B, 0x30, 0x03, 0x00, 0x24, 0x07, 0x00, 0x00},
            //    Ptch = new byte[] {0x1B, 0x30, 0x0B, 0x00, 0x24, 0x07, 0x00, 0x00},
            //},
            new Patch
            {
                Orig = new byte[] {0x2C, 0xDB, 0x18, 0x1A},
                Ptch = new byte[] {0x2C, 0xDB, 0x16, 0x16}
            }
        };

        private static readonly List<Patch> PATROL_PAUSE = new List<Patch> // Patrol
        {
            new Patch // Astral.Quester.FSM.States.CheckAction.get_NeedToRun()
            {
                Orig = new byte[] {0x06, 0x02, 0x20, 0x10, 0x27, 0x00, 0x00, 0x73},
                Ptch = new byte[] {0x06, 0x02, 0x20, 0xC8, 0x00, 0x00, 0x00, 0x73}
            }
        };

        private static void Rewrite(ref byte[] stream, List<Patch> patches)
        {
            IEnumerable<int> Search(byte[] src, byte[] pattern)
            {
                int c = src.Length - pattern.Length + 1;
                int j;
                for (int i = 0; i < c; i++)
                {
                    if (src[i] != pattern[0]) continue;
                    for (j = pattern.Length - 1; j >= 1 && src[i + j] == pattern[j]; j--) ;
                    if (j == 0) yield return i;
                }
            }

            foreach (Patch patch in patches)
            {
                var idx3 = Search(stream, patch.Orig);
                foreach (var i in idx3)
                {
                    for (int j = 0; j < patch.Orig.Length; j++)
                    {
                        stream[i + j] = patch.Ptch[j];
                    }
                }
            }
        }

        public static void Apply(string procName)
        {
            var stream = File.ReadAllBytes(procName);
            typeof(Patches).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .ToList()
                .FindAll(l => l.FieldType == typeof(List<Patch>))
                .ForEach(l => Rewrite(ref stream, l.GetValue(new Patches()) as List<Patch>));
            File.WriteAllBytes(procName, stream);
        }

        private class Patch
        {
            public byte[] Orig;
            public byte[] Ptch;
        }
    }
}
