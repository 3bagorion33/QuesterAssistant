using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using System;

namespace QuesterAssistant.Settings
{
    [Serializable]
    public class SettingsData : NotifyHashChanged , IParse<SettingsData>
    {
        public HotKey RoleToggleHotKey { get; set; } = new HotKey();
        public override int GetHashCode()
        {
            return RoleToggleHotKey.GetHashCode();
        }

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
        }

        public void Init() { }
    }
}
