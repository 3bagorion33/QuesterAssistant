using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;
using System;
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
        
        public Instance()
        {
            HashChanged += Delete;
            using (MD5 md5Hash = MD5.Create())
            {
                string procName = GetMd5Hash(md5Hash, DateTime.Now.Ticks.ToString()) + ".exe";
                try
                {
                    File.Copy("Astral.exe", procName);
                    Patches.Apply(procName);
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
            while ((Process?.HasExited ?? true) && File.Exists($"{Process?.ProcessName}.exe"))
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
