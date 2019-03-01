using System;
using System.Runtime.InteropServices;

namespace QuesterAssistant.Classes.Common
{
    internal class Win32
    {
        public class User32
        {
            [DllImport("user32", CharSet = CharSet.Auto,
                CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern int SetWindowsHookEx(
                int idHook,
                HookProc lpfn,
                IntPtr hMod,
                int dwThreadId);
            [DllImport("user32", CharSet = CharSet.Auto,
                CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern int UnhookWindowsHookEx(int idHook);
            [DllImport("user32", CharSet = CharSet.Auto,
                CallingConvention = CallingConvention.StdCall)]
            public static extern int CallNextHookEx(
                int idHook,
                int nCode,
                int wParam,
                IntPtr lParam);
            [DllImport("user32")]
            public static extern int ToAscii(
                int uVirtKey,
                int uScanCode,
                byte[] lpbKeyState,
                byte[] lpwTransKey,
                int fuState);
            [DllImport("user32")]
            public static extern int GetKeyboardState(byte[] pbKeyState);
            [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern short GetKeyState(int vKey);
            public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
            [DllImport("user32")]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr intptr);
            [DllImport("user32", CharSet = CharSet.Auto)]
            public static extern IntPtr FindWindow(string className, string windowName);
            [DllImport("user32")]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        }
        public class Kernel32
        {
            [DllImport("kernel32")]
            public static extern IntPtr LoadLibrary(string lpFileName);
        }
    }
}
