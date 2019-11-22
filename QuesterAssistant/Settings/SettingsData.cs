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
        public HideClientClass HideClient { get; set; } = new HideClientClass();
        public PauseBotClass PauseBot { get; set; } = new PauseBotClass();

        public override int GetHashCode()
        {
            return RoleToggleHotKey.GetSafeHashCode() ^ HideClient.GetSafeHashCode() ^ PauseBot.GetSafeHashCode();
        }

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
            HideClient.Parse(source.HideClient);
            PauseBot.Parse(source.PauseBot);
        }

        public void Init() { }

        public class HideClientClass : IParse<HideClientClass>
        {
            public HotKey HotKey { get; set; } = new HotKey();
            public Mode HideMode { get; set; }

            public void Init() { }

            public void Parse(HideClientClass source)
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

        public class PauseBotClass : IParse<PauseBotClass>
        {
            public HotKey HotKey { get; set; } = new HotKey();

            public void Init() { }

            public void Parse(PauseBotClass source)
            {
                HotKey.Parse(source.HotKey);
            }

            public override int GetHashCode()
            {
                return HotKey.GetSafeHashCode();
            }
        }
    }
}
