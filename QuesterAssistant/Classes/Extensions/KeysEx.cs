using System.Windows.Forms;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class KeysEx
    {
        // KeyCode - последняя нажатая клавиша
        // KeyData - все нажатые клавиши

        private static KeysConverter kc = new KeysConverter();
        public static string ConvertToString(this Keys k)
        {
            var hasModifiers = ((k & Keys.Shift) == Keys.Shift) || ((k & Keys.Control) == Keys.Control) || ((k & Keys.Alt) == Keys.Alt);
            if (hasModifiers && !k.IsNotModifier())
            {
                string str = kc.ConvertToString(k & Keys.Modifiers);
                return str = str.Remove(str.Length - 4);
            }
            return kc.ConvertToString(k);
        }
        public static bool IsNotModifier(this Keys k)
        {
            k &= Keys.KeyCode;
            return k != Keys.LWin && k != Keys.RWin && k != Keys.ShiftKey &&
                k != Keys.ControlKey && k != Keys.Menu && k != Keys.Apps;
        }
        public static Keys IgnoreBack(this Keys k)
        {
            return ((k & Keys.KeyCode) != Keys.Back) ? k : Keys.None;
        }
    }
}
