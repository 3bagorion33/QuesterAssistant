using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private readonly bool doRename;
        
        public Instance(IList<Patch> patches)
        {
            HashEventEnable();
            HashChanged += Delete;
            using (MD5 md5Hash = MD5.Create())
            {
                string procName = GetMd5Hash(md5Hash, DateTime.Now.Ticks.ToString()) + ".exe";
                try
                {
                    File.Copy("Astral.exe", procName);

                    var stream = File.ReadAllBytes(procName);
                    patches.ToList().FindAll(p => p.Active).ForEach(p => Rewrite(ref stream, p.Bytes));
                    File.WriteAllBytes(procName, stream);

                    Process = Process.Start(procName);
                }
                catch (Exception)
                {
                    QMessageBox.ShowError("Unable to start a new instance");
                }
                finally
                {
                    doRename = patches[0].Active;
                    Delete();
                }
            }
        }

        private void Rewrite(ref byte[] stream, List<Bytes> patches)
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

            foreach (Bytes patch in patches)
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
            if (!doRename) return;
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
            catch (Exception ex) { QMessageBox.ShowError(ex.ToString()); }
        }

        public void Close()
        {
            if (Process.Responding)
                Process.CloseMainWindow();
            else
                Process.Kill();
            while (!Process.HasExited)
                Thread.Sleep(200);
        }

        private void Delete()
        {
            while (Process != null && Process.HasExited && File.Exists($"{Process.ProcessName}.exe"))
            {
                Thread.Sleep(100);
                try
                {
                    File.Delete($"{Process.ProcessName}.exe");
                }
                catch (Exception ex) { QMessageBox.ShowError(ex.ToString()); }
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
