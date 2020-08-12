using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Astral;
using Astral.Classes;
using Astral.Controllers;
using MyNW;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes.Monitoring
{
    internal class GameClient : AMonitor<GameClient>
    {
        public static Process Process => API.AttachedGameProcess;
        public static IntPtr WindowHandle => Process?.MainWindowHandle ?? new IntPtr();
        public static bool IsForeground => WindowHandle == WinAPI.GetForegroundWindow();
        public static bool IsVisible => WinAPI.IsWindowVisible(WindowHandle);
        public static bool IsMinimize => WinAPI.IsWindowMinimize(WindowHandle);

        public static bool IsWindow => hideHandle == IntPtr.Zero
            ? WinAPI.IsWindow(WindowHandle)
            : WinAPI.IsWindow(hideHandle);

        public static bool IsHung => hideHandle == IntPtr.Zero
            ? WinAPI.IsWindowHung(WindowHandle)
            : WinAPI.IsWindowHung(hideHandle);

        private bool NeedRelaunch => (API.RoleIsRunning || MultiRole.IsRunning) && (!IsWindow || IsHung);

        public event EventHandler OnNew;
        public event EventHandler OnCrash;
        public event EventHandler OnForeground;
        public event EventHandler OnBackground;

        private Action KillAndRestart = typeof(Relogger).GetStaticAction("KillAndRestart");
        private Action launchNewProcessAndLog = typeof(Relogger).GetStaticAction("launchNewProcessAndLog");

        private bool prevForegroundState = true;
        private IntPtr prevHandle;
        private static IntPtr hideHandle;
        private static Timeout HungCheckStarted = new Timeout(15000);
        private static bool isHungCheckStarted = false;

        public GameClient()
        {
            OnNew += (sender, args) => { prevForegroundState = true; };
        }

        public static void ToggleVisibility(bool hide = false)
        {
            if (Process is null && Process.HasExited) return;
            if (IsVisible && !IsMinimize)
            {
                hideHandle = WindowHandle;
                WinAPI.MinimizeWindow(hideHandle);
                if (hide)
                    WinAPI.HideWindow(hideHandle);
                return;
            }
            if (hideHandle != IntPtr.Zero)
            {
                if (hide)
                    WinAPI.UnhideWindow(hideHandle);
                WinAPI.RestoreWindow(hideHandle);
                WinAPI.SetForegroundWindow(hideHandle);
                hideHandle = IntPtr.Zero;
            }
        }

        private void Kill()
        {
            if (!IsWindow)
            {
                Logger.WriteLine("Advanced Crash Monitor : can't found game process, relaunch!");
                Memory.Detach();
                launchNewProcessAndLog();
                return;
            }
            if (IsHung)
            {
                Logger.WriteLine($"Advanced Crash Monitor : game process {Process.Id} is hung!");
                KillAndRestart();
            }
        }

        protected override void OnTick(object sender, ElapsedEventArgs e)
        {
            //var flag1 = WinAPI.IsWindowHung(WindowHandle);
            //var flag2 = WinAPI.IsWindowHung(prevHandle);
            //var flag3 = WinAPI.IsWindowHung(hideHandle);

            //var flag4 = WinAPI.IsWindow(WindowHandle);
            //var flag5 = WinAPI.IsWindow(prevHandle);
            //var flag6 = WinAPI.IsWindow(hideHandle);

            if (OnNew != null && WindowHandle != prevHandle)
            {
                prevHandle = WindowHandle;
                if (WindowHandle != IntPtr.Zero)
                    OnNew.Invoke(this, new EventArgs());
            }
            if (OnForeground != null && OnBackground != null && prevForegroundState != IsForeground)
            {
                prevForegroundState = IsForeground;
                if (IsForeground)
                    OnForeground.Invoke(this, new EventArgs());
                else
                    OnBackground.Invoke(this, new EventArgs());
            }

            if (Core.SettingsCore.Data.GameCrashMonitoring && !isHungCheckStarted && NeedRelaunch)
            {
                Task.Factory.StartNew(CheckHung);
            }
        }

        private void CheckHung()
        {
            isHungCheckStarted = true;
            HungCheckStarted.Reset();
            while (NeedRelaunch && !HungCheckStarted.IsTimedOut)
                Pause.Sleep(250);
            if (NeedRelaunch)
            {
                Kill();
                OnCrash?.Invoke(this, new EventArgs());
                var waiting = new Timeout(60000);
                while (WindowHandle == IntPtr.Zero && !waiting.IsTimedOut)
                    Pause.Sleep(1000);
            }
            isHungCheckStarted = false;
        }
    }
}