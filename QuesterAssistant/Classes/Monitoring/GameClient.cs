using System;
using System.Diagnostics;
using System.Timers;
using Astral;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes.Monitoring
{
    internal class GameClient : AMonitor<GameClient>
    {
        public static Process Process => API.AttachedGameProcess;
        public static IntPtr WindowHandle => Process?.MainWindowHandle ?? new IntPtr();
        public static bool IsForeground => WindowHandle == WinAPI.GetForegroundWindow();
        public static bool IsVisible => WinAPI.IsWindowVisible(WindowHandle);
        public static bool IsMinimize => WinAPI.IsWindowMinimize(WindowHandle);

        public event EventHandler OnNew;
        public event EventHandler OnCrash;
        public event EventHandler OnForeground;
        public event EventHandler OnBackground;

        private bool prevForegroundState = true;
        private IntPtr prevHandle;
        private static IntPtr hideHandle;

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
            }
        }

        protected override void OnTick(object sender, ElapsedEventArgs e)
        {
            if (OnNew is null && OnCrash is null && OnForeground is null && OnBackground is null)
                return;
            if (WindowHandle != prevHandle)
            {
                prevHandle = WindowHandle;
                if (WindowHandle == IntPtr.Zero)
                    OnCrash?.Invoke(this, new EventArgs());
                else
                    OnNew?.Invoke(this, new EventArgs());
            }
            if (prevForegroundState != IsForeground)
            {
                prevForegroundState = IsForeground;
                if (IsForeground)
                    OnForeground?.Invoke(this, new EventArgs());
                else
                    OnBackground?.Invoke(this, new EventArgs());
            }
        }
    }
}