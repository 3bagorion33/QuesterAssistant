using DevExpress.Utils.Extensions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    public sealed class Instance : NotifyHashChanged
    {
        public Process Process { get; } = new Process();
        public string Title { get; set; } = string.Empty;
        public int PID { get; set; }
        public Process AttachedProcess => PID != 0 ? Process.GetProcessById(PID) : null;
        private const int HASH_SIZE = 4;
        private const string ASTRAL = "Astral.exe";
        private readonly LockPosition position;
        
        public Instance(IList<Patch> patches, LockPosition position)
        {
            HashEventEnable();
            HashChanged += Delete;
            ClearZoneIdentifier();
            this.position = position;
            using (MD5 md5Hash = MD5.Create())
            {
                string procName = GetMd5Hash(md5Hash, DateTime.Now.Ticks.ToString()) + ".exe";
                try
                {
                    File.Copy(ASTRAL, procName);

                    var stream = File.ReadAllBytes(procName);
                    patches.ToList().FindAll(p => p.Active).ForEach(p => Rewrite(ref stream, p.Bytes));
                    File.WriteAllBytes(procName, stream);

                    Process = Process.Start(procName);

                    while (Process.MainWindowHandle == IntPtr.Zero)
                        Thread.Sleep(250);

                    Move();
                }
                catch (Exception)
                {
                    QMessageBox.ShowError("Unable to start a new instance");
                }
                finally
                {
                    Thread.Sleep(500);
                    Delete();
                }

                //var stream = File.ReadAllBytes(ASTRAL);
                //patches.ToList().FindAll(p => p.Active).ForEach(p => Rewrite(ref stream, p.Bytes));

                ////    //var a = Assembly.Load(stream);
                ////    //var ep = a.EntryPoint;
                ////    //if (ep != null)
                ////    //{
                ////    //    var o = a.CreateInstance(ep.Name);
                ////    //    Task.Factory.StartNew(() => ep.Invoke(a, new object[] { new[] { Path.GetFullPath(ASTRAL) } }));
                ////    //}
                //CMemoryExecute.Run(stream, Path.GetFullPath(ASTRAL));
                //Thread.Sleep(1000);
                //var processes = Process.GetProcesses().Where(p => p.ProcessName == "Astral");
                //Process = processes.First();

                //    //IntPtr buf = MemoryExecute.VirtualAlloc(IntPtr.Zero, (uint) stream.Length);
                //    //Marshal.Copy(stream, 0, buf, stream.Length);
                //    //var ptr = (MemoryExecute.IntReturner)Marshal.GetDelegateForFunctionPointer(buf, typeof(MemoryExecute.IntReturner));
                //    //ptr();
            }
        }

        public void Move()
        {
            var point = Point.Empty;
            switch (position.PositionType)
            {
                case PositionType.Right:
                    point = new Point(Program.FormRectangle.Right, Program.FormRectangle.Y);
                    break;
                case PositionType.Bottom:
                    point = new Point(Program.FormRectangle.X, Program.FormRectangle.Bottom);
                    break;
                case PositionType.Cascade:
                    point = new Point(Program.FormRectangle.X + position.OffsetX, Program.FormRectangle.Y + position.OffsetY);
                    break;
            }
            WinAPI.MoveWindow(Process.MainWindowHandle, point);
        }

        private void ClearZoneIdentifier()
        {
            var dir = new DirectoryInfo("Plugins");
            var files = dir.GetFiles("*.dll");
            foreach (var file in files)
            {
                var streamName = file.FullName + ":Zone.Identifier";
                if (FileHelper.FileExists(streamName))
                    FileHelper.Delete(streamName);
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
            //catch (Exception ex) { QMessageBox.ShowError(ex.ToString()); }
            catch { }
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
            try
            {
                while (Process != null && Process.HasExited && File.Exists($"{Process.ProcessName}.exe"))
                {
                    Thread.Sleep(100);
                    File.Delete($"{Process.ProcessName}.exe");
                }
            }
            //catch (Exception ex) { QMessageBox.ShowError(ex.ToString()); }
            catch { }
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

        public enum PositionType
        {
            Right,
            Bottom,
            Cascade
        }

        public class LockPosition
        {
            public PositionType PositionType;
            public int OffsetX;
            public int OffsetY;
        }
    }
}
