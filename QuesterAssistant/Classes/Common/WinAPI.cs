﻿using System;
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
        public static bool SetForegroundWindow(IntPtr intptr)
        {
            return NativeMethods.SetForegroundWindow(intptr);
        }
        public static IntPtr FindWindow(string className, string windowName)
        {
            return NativeMethods.FindWindow(className, windowName);
        }
        public static bool ShowWindowAsync(IntPtr hWnd, int nCmdShow)
        {
            return NativeMethods.ShowWindowAsync(hWnd, nCmdShow);
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

        private static class NativeMethods
        {
            [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);
            [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern int UnhookWindowsHookEx(IntPtr idHook);
            [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);
            [DllImport("user32")]
            public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
            [DllImport("user32")]
            public static extern int GetKeyboardState(byte[] pbKeyState);
            [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern short GetKeyState(int vKey);
            [DllImport("user32")]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr intptr);
            [DllImport("user32", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr FindWindow(string className, string windowName);
            [DllImport("user32")]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
            [DllImport("user32", CharSet = CharSet.Unicode, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern bool SetWindowText(IntPtr hWnd, string text);
            [DllImport("user32", SetLastError = true)]
            public static extern int GetWindowTextLength(IntPtr hwnd);
            [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
            [DllImport("kernel32", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr LoadLibrary(string lpFileName);
        }
    }
}