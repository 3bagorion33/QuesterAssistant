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
        [HashInclude]
        public bool GameCursorMoving { get; set; }
        [HashInclude]
        public uint StackLifeTime { get; set; } = 1440;
        [HashInclude]
        public PatchesDef Patches { get; set; } = new PatchesDef();

        public void Parse(SettingsData source)
        {
            RoleToggleHotKey.Parse(source.RoleToggleHotKey);
            HideClient.Parse(source.HideClient);
            PauseBot.Parse(source.PauseBot);
            GameCursorMoving = source.GameCursorMoving;
            StackLifeTime = source.StackLifeTime;
            Patches.Parse(source.Patches);
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
            [HashInclude]
            public int Delay { get; set; } = 1;

            public void Init() { }

            public void Parse(PauseBotClass source)
            {
                HotKey.Parse(source.HotKey);
                Delay = source.Delay;
            }
        }

        public class PatchesDef : OverrideHash, IParse<PatchesDef>
        {
            [HashInclude]
            public bool WayPointFilterPatch { get; set; } = true;
            [HashInclude]
            public bool ProfessionPatch { get; set; } = true;
            [HashInclude]
            public uint ProfessionPatchFreeTasksSlots { get; set; } = 1;

            public void Init() { }

            public void Parse(PatchesDef source)
            {
                WayPointFilterPatch = source.WayPointFilterPatch;
                ProfessionPatch = source.ProfessionPatch;
                ProfessionPatchFreeTasksSlots = source.ProfessionPatchFreeTasksSlots;
            }
        }
    }
}
