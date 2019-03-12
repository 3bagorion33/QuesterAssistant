using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes.Hooks
{

    /// <summary>
    /// Captures global keyboard events
    /// </summary>
    internal class KeyboardHook : GlobalHook
    {
        #region Properties

        public List<Keys> KeysMask { get; set; } = new List<Keys>();

        #endregion

        #region Events

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;

        #endregion

        #region Constructor

        public KeyboardHook()
        {
            _hookType = WH_KEYBOARD_LL;
        }

        public KeyboardHook(List<Keys> keys)
        {
            _hookType = WH_KEYBOARD_LL;
            KeysMask.AddRange(keys);
        }

        #endregion

        #region Methods

        protected override IntPtr HookCallbackProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool handled = false;
            if (nCode > -1 && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                KeyboardHookStruct keyboardHookStruct =
                    (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                // Is Control being held down?
                bool control = ((NativeMethods.GetKeyState(VK_LCONTROL) & 0x80) != 0) ||
                               ((NativeMethods.GetKeyState(VK_RCONTROL) & 0x80) != 0);
                // Is Shift being held down?
                bool shift = ((NativeMethods.GetKeyState(VK_LSHIFT) & 0x80) != 0) ||
                             ((NativeMethods.GetKeyState(VK_RSHIFT) & 0x80) != 0);
                // Is Alt being held down?
                bool alt = ((NativeMethods.GetKeyState(VK_LALT) & 0x80) != 0) ||
                           ((NativeMethods.GetKeyState(VK_RALT) & 0x80) != 0);
                // Is CapsLock on?
                bool capslock = (NativeMethods.GetKeyState(VK_CAPITAL) != 0);
                // Create event using keycode and control/shift/alt values found above
                KeyEventArgs e = new KeyEventArgs(
                    (Keys)(
                        keyboardHookStruct.vkCode |
                        (control ? (int)Keys.Control : 0) |
                        (shift ? (int)Keys.Shift : 0) |
                        (alt ? (int)Keys.Alt : 0)
                        ));
                // Handle KeyDown and KeyUp events
                switch ((int)wParam)
                {
                    case WM_KEYDOWN:
                    case WM_SYSKEYDOWN:
                        if (KeyDown != null && !KeysMask.Exists(x => x == e.KeyCode))
                        {
                            KeyDown(this, e);
                            handled = handled || e.Handled;
                        }
                        break;
                    case WM_KEYUP:
                    case WM_SYSKEYUP:
                        if (KeyUp != null)
                        {
                            KeyUp(this, e);
                            handled = handled || e.Handled;
                        }
                        break;
                }
                // Handle KeyPress event
                if ((int)wParam == WM_KEYDOWN &&
                   !handled &&
                   !e.SuppressKeyPress &&
                    KeyPress != null)
                {
                    byte[] keyState = new byte[256];
                    byte[] inBuffer = new byte[2];
                    NativeMethods.GetKeyboardState(keyState);
                    if (NativeMethods.ToAscii(keyboardHookStruct.vkCode,
                              keyboardHookStruct.scanCode,
                              keyState,
                              inBuffer,
                              keyboardHookStruct.flags) == 1)
                    {
                        char key = (char)inBuffer[0];
                        if ((capslock ^ shift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressEventArgs e2 = new KeyPressEventArgs(key);
                        KeyPress(this, e2);
                        handled = handled || e.Handled;
                    }
                }
            }
            if (handled)
            {
                return new IntPtr(1);
            }
            else
            {
                return NativeMethods.CallNextHookEx(_handleToHook, nCode, wParam, lParam);
            }
        }

        #endregion

    }
}
