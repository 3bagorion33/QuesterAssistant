using System;
using System.Collections.Generic;
using System.Text;
using MyMemory.Hooks;
using MyNW;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes
{
    public static class ChatManager
    {
        private static Func<int, int, CallbackNotifier.NotifyCallback, bool, HookJmp>
            hooks_smethod_0 =
                typeof(MyNW.Hooks).GetStaticMethod<int, int, CallbackNotifier.NotifyCallback, bool, HookJmp>();
        private static HookJmp hookJmp;

        public delegate void OnChatMessageHandler(ChatLogEntryType chatLogEntryType, List<string> messages);
        public static event OnChatMessageHandler OnChatMessage;

        //static ChatManager()
        //{
        //    hookJmp = hooks_smethod_0(6463344, 15, Notifier, true);
        //}

        public static void Load()
        {
            foreach (var hook in MyNW.Hooks.HookList)
            {
                var m_addr = (IntPtr)(hook as HookJmp)?.GetFieldValue("m_address");
                if (m_addr != IntPtr.Zero && m_addr == (IntPtr)0x62A6B0)
                {
                    (hook as HookJmp).Remove();
                }
            }
            hookJmp = hooks_smethod_0(0x62A6B0, 15, Notifier, true);
            //hookJmp = hooks_smethod_0(0x62A6B0, 15, Notifier, true);
        }

        public static void UnLoad()
        {
            hookJmp?.Remove();
            hookJmp?.Dispose();
        }

        private static void Notifier(CallbackNotifier.NotifyArgs notifyArgs)
        {
            var messages = new List<string>();
            void MessageAdd(IntPtr intPtr)
            {
                if (intPtr != IntPtr.Zero)
                    messages.Add(Memory.MMemory.ReadString(intPtr, Encoding.UTF8, 256));
            }

            ChatLogEntryType chatLogEntryType = (ChatLogEntryType)(int)notifyArgs.Registers.R8;

            if (chatLogEntryType != ChatLogEntryType.Guild)
            {
                return;
            }

            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x8));    //descriptor
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x10));   //full name
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x20));   //empty
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x28));   //body
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x38));   //full name
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x50));
            //MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + 0x58));   //body

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R15 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R14 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R13 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R12 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R11 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R10 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R9 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R8 + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rdi + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsi + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rdx + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rcx + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rbx + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rax + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rbp + i));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + i));


            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R15 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R14 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R13 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R12 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R11 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R10 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R9 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.R8 + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rdi + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsi + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rdx + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rcx + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rbx + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rax + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rbp + i)));

            for (int i = 0; i < 0x100; i++)
                MessageAdd(Memory.MMemory.Read<IntPtr>(Memory.MMemory.Read<IntPtr>(notifyArgs.Registers.Rsp + i)));



            if (messages.Count > 0)
                OnChatMessage?.Invoke(chatLogEntryType, messages);
        }

        public enum ChatLogEntryType : uint
        {
            Unknown,
            Admin,
            Channel,
            ChatSystem,
            Error,
            Spy,
            CombatSelf,
            CombatTeam,
            CombatOther,
            Friend,
            Inventory,
            Mission,
            NPC,
            Reward,
            RewardMinor,
            System,
            Guild,
            Local,
            Officer,
            Private,
            Private_Sent,
            Team,
            TeamUp,
            Zone,
            Match,
            Global,
            Minigame,
            Emote,
            Events,
            LootRolls,
            NeighborhoodChange,
            Trade,
            LookingForGroup,
            AntiAddiction,
            Alliance,
            Raid,
            MRG
        }
    }
}