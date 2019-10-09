using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using System;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Settings
{
    [Serializable]
    public class SettingsData : NotifyHashChanged , IParse<SettingsData>
    {
        public HotKey RoleToggleHotKey { get; set; } = new HotKey();
        public HideGameClient HideClient { get; set; } = new HideGameClient();
        public override int GetHashCode()
        {
            return RoleToggleHotKey.GetSafeHashCode() ^ HideClient.GetSafeHashCode();
        }

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
            HideClient.Parse(source.HideClient);
        }

        public void Init() { }

        public class HideGameClient : IParse<HideGameClient>
        {
            public HotKey HotKey { get; set; } = new HotKey();
            public Mode HideMode { get; set; }

            public void Init() { }

            public void Parse(HideGameClient source)
            {
                HideMode = source.HideMode;
                HotKey.Parse(source.HotKey);
            }

            public override int GetHashCode()
            {
                return HotKey.GetSafeHashCode() ^ HideMode.GetSafeHashCode();
            }

            public enum Mode { Minimize, Hide }
        }
    }
}
