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
        public HotKey HideGameHotKey { get; set; } = new HotKey();
        public override int GetHashCode()
        {
            return RoleToggleHotKey.GetSafeHashCode() ^ HideGameHotKey.GetSafeHashCode();
        }

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
            HideGameHotKey.Parse(source.HideGameHotKey);
        }

        public void Init() { }
    }
}
