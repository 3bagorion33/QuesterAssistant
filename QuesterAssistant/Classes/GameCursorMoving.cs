using System;
using MyNW;
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Enums;

namespace QuesterAssistant.Classes
{
    internal static class GameCursorMoving
    {
        private static readonly IntPtr pEnable = Memory.MMemory.Read<IntPtr>(Memory.BaseAdress + (int) Offsets.GameCursorMoving);
        private static readonly IntPtr pDisable = Memory.MMemory.AllocateRawMemory(1);

        static GameCursorMoving()
        {
            Memory.MMemory.Write<byte>(pDisable, 0xC3);
        }

        private static void Enable(object sender = null, EventArgs e = null)
        {
            Memory.MMemory.Write(Memory.BaseAdress + (int) Offsets.GameCursorMoving, pEnable);
        }

        private static void Disable(object sender = null, EventArgs e = null)
        {
            Memory.MMemory.Write(Memory.BaseAdress + (int) Offsets.GameCursorMoving, pDisable);
        }

        public static void Start()
        {
            if (Core.SettingsCore.Data.GameCursorMoving)
            {
                Monitor.Foreground.OnForeground += Enable;
                Monitor.Foreground.OnBackground += Disable;
            }
        }

        public static void Stop()
        {
            Monitor.Foreground.OnForeground -= Enable;
            Monitor.Foreground.OnBackground -= Disable;
            Enable();
        }
    }
}