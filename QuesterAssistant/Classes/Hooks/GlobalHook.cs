using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes.Hooks
{
    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    internal abstract class GlobalHook : IDisposable
    {

        #region Windows API Code

        [StructLayout(LayoutKind.Sequential)]
        protected class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        protected const int WH_MOUSE_LL = 14;
        protected const int WH_KEYBOARD_LL = 13;

        protected const int WH_MOUSE = 7;
        protected const int WH_KEYBOARD = 2;
        protected const int WM_MOUSEMOVE = 0x200;
        protected const int WM_LBUTTONDOWN = 0x201;
        protected const int WM_RBUTTONDOWN = 0x204;
        protected const int WM_MBUTTONDOWN = 0x207;
        protected const int WM_LBUTTONUP = 0x202;
        protected const int WM_RBUTTONUP = 0x205;
        protected const int WM_MBUTTONUP = 0x208;
        protected const int WM_LBUTTONDBLCLK = 0x203;
        protected const int WM_RBUTTONDBLCLK = 0x206;
        protected const int WM_MBUTTONDBLCLK = 0x209;
        protected const int WM_MOUSEWHEEL = 0x020A;
        protected const int WM_KEYDOWN = 0x100;
        protected const int WM_KEYUP = 0x101;
        protected const int WM_SYSKEYDOWN = 0x104;
        protected const int WM_SYSKEYUP = 0x105;

        protected const byte VK_SHIFT = 0x10;
        protected const byte VK_CAPITAL = 0x14;
        protected const byte VK_NUMLOCK = 0x90;

        protected const byte VK_LSHIFT = 0xA0;
        protected const byte VK_RSHIFT = 0xA1;
        protected const byte VK_LCONTROL = 0xA2;
        protected const byte VK_RCONTROL = 0x3;
        protected const byte VK_LALT = 0xA4;
        protected const byte VK_RALT = 0xA5;

        protected const byte LLKHF_ALTDOWN = 0x20;

        #endregion

        #region Private Variables

        protected int _hookType;
        protected IntPtr _handleToHook;
        protected bool _isStarted;
        protected NativeMethods.HookProc _hookCallback;

        #endregion

        #region Properties

        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }

        #endregion

        #region Constructor

        public GlobalHook()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        #endregion

        #region Methods

        public void Start()
        {
            if (!_isStarted &&
                _hookType != 0)
            {
                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                _hookCallback = new NativeMethods.HookProc(HookCallbackProcedure);
                _handleToHook = NativeMethods.SetWindowsHookEx(
                    _hookType,
                    _hookCallback,
                    NativeMethods.LoadLibrary("User32"),
                    0);
                // Were we able to sucessfully start hook?
                if (_handleToHook != IntPtr.Zero)
                {
                    _isStarted = true;
                }
            }
        }

        public void Stop()
        {
            if (_isStarted)
            {
                NativeMethods.UnhookWindowsHookEx(_handleToHook);
                _isStarted = false;
            }
        }

        protected virtual IntPtr HookCallbackProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return IntPtr.Zero;
        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            Stop();
        }

        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }

        ~GlobalHook()
        {
            Stop();
        }

        #endregion

    }
}
