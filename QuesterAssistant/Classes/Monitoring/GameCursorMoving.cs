using System;
using MyNW;
using QuesterAssistant.Classes.Patches;

namespace QuesterAssistant.Classes.Monitoring
{
    internal static class GameCursorMoving
    {
        private static readonly IntPtr pEnable = Memory.MMemory.Read<IntPtr>(Memory.BaseAdress + Offsets.GameCursorMoving);
        private static IntPtr pDisable;

        private static void Enable(object sender = null, EventArgs e = null)
        {
            Memory.MMemory.Write(Memory.BaseAdress + Offsets.GameCursorMoving, pEnable);
        }

        private static void Disable(object sender = null, EventArgs e = null)
        {
            Memory.MMemory.Write(Memory.BaseAdress + Offsets.GameCursorMoving, pDisable);
        }

        public static void Start()
        {
            if (Core.SettingsCore.Data.GameCursorMoving)
            {
                GameClient.Monitor.OnNew += Allocate;
                GameClient.Monitor.OnForeground += Enable;
                GameClient.Monitor.OnBackground += Disable;
            }
        }

        private static void Allocate(object sender, EventArgs e)
        {
            pDisable = Memory.MMemory.AllocateRawMemory(1);
            while (!Memory.MMemory.Write<byte>(pDisable, 0xC3))
                Pause.Sleep(100);
        }

        public static void Stop()
        {
            GameClient.Monitor.OnNew -= Allocate;
            GameClient.Monitor.OnForeground -= Enable;
            GameClient.Monitor.OnBackground -= Disable;
            Enable();
        }
    }
}