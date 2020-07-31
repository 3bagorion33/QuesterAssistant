using System;
using System.Collections.Generic;
using MyNW;
using MyNW.Classes;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class EmailMessageEx
    {
        public static List<Item> GetAttachedItems(this EmailMessage message) =>
            NWList.Get<Item>(Memory.MMemory.Read<IntPtr>(message.Pointer + 32));
    }
}