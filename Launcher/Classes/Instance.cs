using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Launcher.Classes
{
    sealed class Instance : NotifyHashChanged
    {
        public Process Process { get; } = new Process();
        public string OriginalTitle { get; set; } = string.Empty;
        public string NewTitle { get; set; } = string.Empty;
        private const int HASH_SIZE = 4;
        
        // Astral.Forms.Main.method_5
        private readonly List<byte[]> REWRITE_TITLE_ORIG = new List<byte[]>
        {
            new byte[] { 0x1B, 0x30, 0x04, 0x00, 0x46, 0x01, 0x00, 0x00 },
            new byte[] { 0x06, 0x6F, 0x17, 0x01, 0x00, 0x0A, 0x28, 0xD2 },
            new byte[] { 0x18, 0x06, 0x72, 0x1F, 0x89, 0x01, 0x70, 0x7D}
        };

        private readonly List<byte[]> REWRITE_TITLE_NEW = new List<byte[]>
        {
            new byte[] { 0x1B, 0x30, 0x0C, 0x00, 0x46, 0x01, 0x00, 0x00 },
            new byte[] { 0x06, 0x6F, 0xB0, 0x01, 0x00, 0x0A, 0x28, 0xD2 },
            new byte[] { 0x18, 0x06, 0x7E, 0x63, 0x00, 0x00, 0x0A, 0x7D}
        };

        // AssemblyInfo
        private readonly List<byte[]> ASSEMBLYINFO_ORIG = new List<byte[]>
        {
            new byte[] { 0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[] { 0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x2E, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00 },
            new byte[] { 0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x2E, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00 },
            new byte[] { 0x41, 0x00, 0x73, 0x00, 0x74, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00 },
        };

        private readonly List<byte[]> ASSEMBLYINFO_NEW = new List<byte[]>
        {
            new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
        };

        // Astral.Professions.FSM.States.Main.Run
        private readonly List<byte[]> PRAY_PAUSE_ORIG = new List<byte[]>
        {
            new byte[] { 0x06, 0x1B, 0x58, 0x28 },
            new byte[] { 0x1B, 0x30, 0x03, 0x00, 0x24, 0x07, 0x00, 0x00 },
            new byte[] { 0x2C, 0xDB, 0x18, 0x1A }
        };

        private readonly List<byte[]> PRAY_PAUSE_NEW = new List<byte[]>
        {
            new byte[] { 0x06, 0x16, 0x58, 0x28 },
            new byte[] { 0x1B, 0x30, 0x0B, 0x00, 0x24, 0x07, 0x00, 0x00 },
            new byte[] { 0x2C, 0xDB, 0x16, 0x16 }
        };

        // Patrol
        private readonly List<byte[]> PATROL_PAUSE_ORIG = new List<byte[]>
        {
            new byte[] { 0x5E, 0x02, 0x20, 0x10, 0x27, 0x00, 0x00, 0x73 },
            new byte[] { 0x5E, 0x02, 0x20, 0x10, 0x27, 0x00, 0x00, 0x73 },
        };
        private readonly List<byte[]> PATROL_PAUSE_NEW = new List<byte[]>
        {
            new byte[] { 0x5E, 0x02, 0x20, 0xC8, 0x00, 0x00, 0x00, 0x73 },
            new byte[] { 0x5E, 0x02, 0x20, 0xC8, 0x00, 0x00, 0x00, 0x73 },
        };

        public Instance()
        {
            bool ByteArraysEqual(byte[] b1, byte[] b2)
            {
                if (b1 == b2) return true;
                if (b1 == null || b2 == null) return false;
                if (b1.Length != b2.Length) return false;
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i]) return false;
                }
                return true;
            }
            HashChanged += Delete;
            using (MD5 md5Hash = MD5.Create())
            {
                string procName = GetMd5Hash(md5Hash, DateTime.Now.Ticks.ToString()) + ".exe";
                try
                {
                    File.Copy("Astral.exe", procName);

                    void Rewrite(List<byte[]> origBytes, List<byte[]> newBytes)
                    {
                        using (FileStream stream = File.Open(procName, FileMode.Open, FileAccess.ReadWrite))
                        {
                            for (int i = 0; i < origBytes.Count; i++)
                            {
                                byte[] reading = new byte[origBytes[i].Length];
                                stream.Position = 0;
                                while (stream.Read(reading, 0, 1) > 0)
                                {
                                    if (origBytes[i][0] == reading[0])
                                    {
                                        stream.Position--;
                                        stream.Read(reading, 0, origBytes[i].Length);
                                        if (ByteArraysEqual(origBytes[i], reading))
                                        {
                                            stream.Position = stream.Position - origBytes[i].Length;
                                            break;
                                        }
                                    }
                                }

                                if (stream.Length > stream.Position + origBytes[i].Length)
                                    stream.Write(newBytes[i], 0, newBytes[i].Length);
                            }
                        }
                    }

                    Rewrite(REWRITE_TITLE_ORIG, REWRITE_TITLE_NEW);
                    Rewrite(ASSEMBLYINFO_ORIG, ASSEMBLYINFO_NEW);
                    Rewrite(PRAY_PAUSE_ORIG, PRAY_PAUSE_NEW);
                    Rewrite(PATROL_PAUSE_ORIG, PATROL_PAUSE_NEW);
                    Process = Process.Start(procName);
                }
                catch (Exception)
                {
                    ErrorBox.Show("Unable to start a new instance");
                }
                finally
                {
                    Delete();
                }
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;
            try
            {
                hash = OriginalTitle.GetSafeHashCode() ^
                NewTitle.GetSafeHashCode() ^
                Process.ProcessName.GetSafeHashCode() ^
                Process.HasExited.GetSafeHashCode();
            }
            catch { }
            return hash;
        }

        public void ForeGround()
        {
            WinAPI.SetForegroundWindow(Process.MainWindowHandle);
        }

        public void Rename()
        {
            try
            {
                void SetNewTitle()
                {
                    string origTitle = WinAPI.GetWindowText(Process.MainWindowHandle);
                    if (origTitle.Contains("@"))
                    {
                        OriginalTitle = origTitle;
                        NewTitle = OriginalTitle.Split('@')[0].Hide();
                        return;
                    }
                    if (origTitle.Contains("Astral") || origTitle.Contains("Offline"))
                    {
                        OriginalTitle = origTitle;
                        NewTitle = string.Empty;
                        return;
                    }
                }
                SetNewTitle();
                WinAPI.SetWindowText(Process.MainWindowHandle, NewTitle);
            }
            catch (Exception ex) { ErrorBox.Show(ex.ToString()); }
        }

        public void Close()
        {
            Process.CloseMainWindow();
            while (!Process.HasExited)
                Thread.Sleep(200);
        }

        public void Delete()
        {
            while (Process.HasExited && File.Exists($"{Process.ProcessName}.exe"))
            {
                Thread.Sleep(100);
                try
                {
                    File.Delete($"{Process.ProcessName}.exe");
                }
                catch (Exception ex) { ErrorBox.Show(ex.ToString()); }
            }
        }

        public static void Clean()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.EnumerateFiles(path).ForEach(f => { if (Regex.IsMatch(f, $@"[0-9a-f]{{{HASH_SIZE * 2}}}\.exe")) File.Delete(f); });
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < HASH_SIZE; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
