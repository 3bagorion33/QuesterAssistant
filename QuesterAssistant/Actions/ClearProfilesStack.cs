using System;
using System.ComponentModel;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class ClearProfilesStack : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();
        protected override bool IntenalConditions =>
            ClearForAllCharacters ||
            ProfilesStack.Any;

        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override ActionResult Run()
        {
            if (!IntenalConditions)
                return ActionResult.Fail;
            ProfilesStack.Clear(ClearForAllCharacters);
            return ActionResult.Completed;
        }

        [Description("Force cleaning stack for all characters instead current")]
        public bool ClearForAllCharacters { get; set; } = false;
    }
}
