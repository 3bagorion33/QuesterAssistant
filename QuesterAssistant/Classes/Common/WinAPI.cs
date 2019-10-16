using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace QuesterAssistant.Classes.Common
{
    public static class WinAPI
    {
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public static IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId)
        {
            return NativeMethods.SetWindowsHookEx(idHook, lpfn, hMod, dwThreadId);
        }
        public static int UnhookWindowsHookEx(IntPtr idHook)
        {
            return NativeMethods.UnhookWindowsHookEx(idHook);
        }
        public static IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.CallNextHookEx(idHook, nCode, wParam, lParam);
        }
        public static int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState)
        {
            return NativeMethods.ToAscii(uVirtKey, uScanCode, lpbKeyState, lpbKeyState, fuState);
        }
        public static int GetKeyboardState(byte[] pbKeyState)
        {
            return NativeMethods.GetKeyboardState(pbKeyState);
        }
        public static short GetKeyState(int vKey)
        {
            return NativeMethods.GetKeyState(vKey);
        }
        public static IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }
        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }
        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return NativeMethods.IsWindowVisible(hWnd);
        }
        public static IntPtr FindWindow(string className, string windowName)
        {
            return NativeMethods.FindWindow(className, windowName);
        }
        public static bool SetWindowText(IntPtr hWnd, string text)
        {
            return NativeMethods.SetWindowText(hWnd, text);
        }
        public static int GetWindowTextLength(IntPtr hwnd)
        {
            return NativeMethods.GetWindowTextLength(hwnd);
        }
        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder title = new StringBuilder(NativeMethods.GetWindowTextLength(hWnd) + 1);
            NativeMethods.GetWindowText(hWnd, title, title.Capacity);
            return title.ToString();
        }
        public static IntPtr LoadLibrary(string lpFileName)
        {
            return NativeMethods.LoadLibrary(lpFileName);
        }
        public static bool TaskKill(IntPtr hWnd)
        {
            var procId = NativeMethods.GetProcessId(hWnd);
            if (procId == 0) return false;
            Process.Start($"cmd /c taskkill /pid {procId} /t /f");
            return true;
        }
        public static IntPtr CloseWindow(IntPtr hWnd)
        {
            return NativeMethods.SendMessage(hWnd, NativeMethods.WM_SYSCOMMAND, NativeMethods.SC_CLOSE, 0);
        }
        public static bool UnhideWindow(IntPtr hWnd)
        {
            return NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOWNORMAL);
        }
        public static bool MinimizeWindow(IntPtr hWnd)
        {
            return NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_MINIMIZE);
        }
        public static bool RestoreWindow(IntPtr hWnd)
        {
            return NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_RESTORE);
        }
        public static bool HideWindow(IntPtr hWnd)
        {
            return NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_HIDE);
        }
        public static int GetProcessId(IntPtr hWnd)
        {
            return NativeMethods.GetProcessId(hWnd);
        }
        public static int GetWindowState(IntPtr hWnd)
        {
            NativeMethods.WINDOWPLACEMENT wp = new NativeMethods.WINDOWPLACEMENT();
            NativeMethods.GetWindowPlacement(hWnd, ref wp);
            return wp.showCmd; // 1- Normal; 2 - Minimize; 3 - Maximize;
        }
        public static bool IsWindowMinimize(IntPtr hWnd)
        {
            return GetWindowState(hWnd) == 2;
        }
        public static string RelativePath(string fNameFrom, string fNameTo)
        {
            StringBuilder str = new StringBuilder(256);
            NativeMethods.PathRelativePathTo(str, fNameFrom, FileAttributes.Normal, fNameTo, FileAttributes.Normal);
            return str.ToString();
        }

        private static class NativeMethods
        {
            private const string USER = "user32";
            private const string KERNEL = "kernel32";

            public const int SW_FORCEMINIMIZE = 11;
            public const int SW_HIDE = 0;
            public const int SW_MINIMIZE = 6;
            public const int SW_RESTORE = 9;
            public const int SW_SHOWNORMAL = 1;
            public const int WM_SYSCOMMAND = 0x0112;
            public const int SC_CLOSE = 0xF060;
            public const int SC_RESTORE = 0xF120;

            [DllImport(USER, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);
            [DllImport(USER, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern int UnhookWindowsHookEx(IntPtr idHook);
            [DllImport(USER, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);
            [DllImport(USER)]
            public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
            [DllImport(USER)]
            public static extern int GetKeyboardState(byte[] pbKeyState);
            [DllImport(USER, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern short GetKeyState(int vKey);
            [DllImport(USER)]
            public static extern IntPtr GetForegroundWindow();
            [DllImport(USER)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr intptr);
            [DllImport(USER)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowVisible(IntPtr hWnd);
            [DllImport(USER, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr FindWindow(string className, string windowName);
            [DllImport(USER)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
            [DllImport(USER)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
            [DllImport(USER)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
            [DllImport(USER, CharSet = CharSet.Unicode, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern bool SetWindowText(IntPtr hWnd, string text);
            [DllImport(USER, SetLastError = true)]
            public static extern int GetWindowTextLength(IntPtr hWnd);
            [DllImport(USER, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
            [DllImport(KERNEL, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr LoadLibrary(string lpFileName);
            [DllImport(USER, CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);
            [DllImport(USER)]
            public static extern bool DestroyWindow(IntPtr hWnd);
            [DllImport(KERNEL, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool TerminateProcess(IntPtr processHandle, int exitCode);
            [DllImport(KERNEL)]
            public static extern int GetProcessId(IntPtr hWnd);
            [DllImport(KERNEL, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(IntPtr hWnd);
            [DllImport(KERNEL)]
            public static extern IntPtr OpenProcess(int Access, bool InheritHandle, int ProcessId);
            [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
            public static extern bool PathRelativePathTo(
                [Out] StringBuilder pszPath,
                [In] string pszFrom,
                [In] FileAttributes dwAttrFrom,
                [In] string pszTo,
                [In] FileAttributes dwAttrTo
            );

            public struct POINTAPI
            {
                public int x;
                public int y;
            }
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            public struct WINDOWPLACEMENT
            {
                public int length;
                public int flags;
                public int showCmd;
                public POINTAPI ptMinPosition;
                public POINTAPI ptMaxPosition;
                public RECT rcNormalPosition;
            }
        }
    }
}