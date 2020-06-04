using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
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
using System.Threading.Tasks;

namespace Launcher.Classes
{
    sealed class Instance : NotifyHashChanged
    {
        public Process Process { get; } = new Process();
        public string Title { get; set; } = string.Empty;
        public int PID { get; set; }
        public Process AttachedProcess => PID != 0 ? Process.GetProcessById(PID) : null;
        private const int HASH_SIZE = 4;
        
        public Instance(IList<Patch> patches)
        {
            HashEventEnable();
            HashChanged += Delete;
            ClearZoneIdentifier();
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
                    Delete();
                }
            }
        }

        private void ClearZoneIdentifier()
        {
            var dir = new DirectoryInfo("Plugins");
            var files = dir.GetFiles("*.dll");
            foreach (var file in files)
            {
                var streamName = file.FullName + ":Zone.Identifier";
                if (BinFileHelper.FileExists(streamName))
                    BinFileHelper.Delete(streamName);
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
                    for (j = pattern.Length - 1; j >= 1 && src[i + j] == pattern[j]; j--);
                    if (j == 0) yield return i;
                }
            }

            foreach (Bytes patch in patches)
            {
                var idx3 = Search(stream, patch.Orig);
                foreach (var i in idx3)
                    for (int j = 0; j < patch.Orig.Length; j++)
                        stream[i + j] = patch.Ptch[j];
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;
            try
            {
                hash = Title.GetSafeHashCode() ^
                Process.ProcessName.GetSafeHashCode() ^
                Process.HasExited.GetSafeHashCode();
            }
            catch { }
            return hash;
        }

        public void ForeGround() => WinAPI.SetForegroundWindow(Process.MainWindowHandle);
        public void ForeGroundAttach() => WinAPI.SetForegroundWindow(AttachedProcess?.MainWindowHandle ?? IntPtr.Zero);

        public void Rename()
        {
            try
            {
                var split = WinAPI.GetWindowText(Process.MainWindowHandle).Split(new []{" - PID:"}, StringSplitOptions.None);
                Title = split[0];
                PID = split.Length > 1 ? int.Parse(split[1]) : 0;
            }
            catch (Exception ex) { QMessageBox.ShowError(ex.ToString()); }
        }

        public void Close() => Close(Process);
        public void CloseAttach() => Close(AttachedProcess);

        private static void Close(Process process)
        {
            Task.Factory.StartNew(() =>
            {
                if (process.Responding)
                    process.CloseMainWindow();
                Thread.Sleep(500);
                if (!process.HasExited)
                    process.Kill();
                while (!process.HasExited)
                    Thread.Sleep(200);
            });
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
