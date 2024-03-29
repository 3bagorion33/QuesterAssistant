﻿using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.PushBulletClient.Models.Requests;
using System.Diagnostics;

namespace QuesterAssistant.PushNotify
{
    internal class PushNotifyCore : ACore<PushNotifyData, PushNotifyForm>
    {
        protected override bool IsValid => !string.IsNullOrEmpty(Data.Client.AccessToken);
        protected override bool HookEnableFlag => false;
        private string PushTitle => EntityManager.LocalPlayer.IsValid ? EntityManager.LocalPlayer.InternalName : $"Astral(#{Process.GetCurrentProcess().Id})";

        public void PushMessage(string message)
        {
            
            foreach (var dev in Data.Devices)
            {
                PushNoteRequest request = new PushNoteRequest()
                {
                    DeviceIden = dev.Iden,
                    Title = PushTitle,
                    Body = message
                };
                Data.Client.PushNote(request);
            }
        }

        sealed class NotifyStatus : NotifyHashChanged
        {
        }
    }
}
