using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Common.Extensions;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Launcher.Classes
{
    sealed class Instance : NotifyHashChanged
    {
        public Process Process { get; set; } = new Process();
        public string OriginalTitle { get; set; } = string.Empty;
        public string NewTitle { get; set; } = string.Empty;
        private const int HASH_SIZE = 4;
        private readonly List<byte[]> REWRITE_TITLE_ORIG = new List<byte[]>()
        {
            new byte[] { 0x1B, 0x30, 0x04, 0x00, 0x46, 0x01, 0x00, 0x00 },
            new byte[] { 0x06, 0x6F, 0x17, 0x01, 0x00, 0x0A, 0x28, 0xD2 },
        };
        private readonly List<byte[]> REWRITE_TITLE_NEW = new List<byte[]>()
        {
            new byte[] { 0x1B, 0x30, 0x0C, 0x00, 0x46, 0x01, 0x00, 0x00 },
            new byte[] { 0x06, 0x6F, 0xB0, 0x01, 0x00, 0x0A, 0x28, 0xD2 },
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
                    using (FileStream stream = File.Open(procName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        for (int i = 0; i < REWRITE_TITLE_ORIG.Count; i++)
                        {
                            byte[] reading = new byte[REWRITE_TITLE_ORIG[i].Length];
                            while (stream.Read(reading, 0, 1) > 0)
                            {
                                if (REWRITE_TITLE_NEW[i][0] == reading[0])
                                {
                                    stream.Position--;
                                    stream.Read(reading, 0, REWRITE_TITLE_ORIG[i].Length);
                                    if (ByteArraysEqual(REWRITE_TITLE_ORIG[i], reading))
                                    {
                                        stream.Position = stream.Position - REWRITE_TITLE_ORIG[i].Length;
                                        break;
                                    }
                                }
                            }
                            stream.Write(REWRITE_TITLE_NEW[i], 0, REWRITE_TITLE_NEW[i].Length);
                        }
                    }
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

        [SecurityCritical]
        public static void KillSpy()
        {
            try
            {
                Process.GetProcesses().
                    Where(
                        p => Regex.IsMatch(p.ProcessName, @"^(CrashReporter|CrypticError)", RegexOptions.IgnoreCase)
                    ).
                    ForEach(p => p.Kill());
                var handle = WinAPI.FindWindow(null, "Невервинтер Crash");
                if (handle != IntPtr.Zero) WinAPI.CloseWindow(handle);
            }
            catch { }
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
