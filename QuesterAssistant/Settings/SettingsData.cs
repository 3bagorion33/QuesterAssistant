using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using System;

namespace QuesterAssistant.Settings
{
    [Serializable]
    public class SettingsData : NotifyHashChanged , IParse<SettingsData>
    {
        [HashInclude]
        public HotKey RoleToggleHotKey { get; set; } = new HotKey();
        [HashInclude]
        public HideClientClass HideClient { get; set; } = new HideClientClass();
        [HashInclude]
        public PauseBotClass PauseBot { get; set; } = new PauseBotClass();

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
            HideClient.Parse(source.HideClient);
            PauseBot.Parse(source.PauseBot);
        }

        public void Init() { }

        public class HideClientClass : OverrideHash, IParse<HideClientClass>
        {
            [HashInclude]
            public HotKey HotKey { get; set; } = new HotKey();
            [HashInclude]
            public Mode HideMode { get; set; }

            public void Init() { }

            public void Parse(HideClientClass source)
            {
                HideMode = source.HideMode;
                HotKey.Parse(source.HotKey);
            }

            public enum Mode { Minimize, Hide }
        }

        public class PauseBotClass : OverrideHash, IParse<PauseBotClass>
        {
            [HashInclude]
            public HotKey HotKey { get; set; } = new HotKey();

            public void Init() { }

            public void Parse(PauseBotClass source)
            {
                HotKey.Parse(source.HotKey);
            }
        }
    }
}
