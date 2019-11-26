using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.PushBulletClient;
using QuesterAssistant.Classes.PushBulletClient.Models.Responses;
using System;
using System.Collections.Generic;

namespace QuesterAssistant.PushNotify
{
    [Serializable]
    public class PushNotifyData : NotifyHashChanged, IParse<PushNotifyData>
    {
        [HashInclude]
        public PushBulletClientLite Client { get; set; } = new PushBulletClientLite();
        [HashInclude]
        public List<Device> Devices { get; set; } = new List<Device>();

        public void Init() { }

        public void Parse(PushNotifyData source)
        {
            Client.Parse(source.Client);
            Devices.Clear();
            source.Devices.ForEach(p => Devices.Add(p));
            OnPropertyChanged();
        }
    }
}
