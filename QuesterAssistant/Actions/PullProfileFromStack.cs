using System;
using System.ComponentModel;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class PullProfileFromStack : Astral.Quester.Classes.Action
    {
        private ProfilesStack.Item lastProfile => ProfilesStack.Last;
        public override string ActionLabel => $"{GetType().Name} : {lastProfile}";
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (Astral.Quester.API.CurrentProfile.MainActionPack.Actions.Count(a => a.GetType() == GetType()) > 1)
                {
                    return new ActionValidity("This profile contains more than one such action!");
                }
                return new ActionValidity();
            }
        }

        protected override bool IntenalConditions => ProfilesStack.Any;
        
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override ActionResult Run()
        {
            if (!IntenalConditions)
                return ActionResult.Fail;

            if (!ProfilesStack.Pull())
            {
                Logger.WriteLine($"{lastProfile.ProfilePath} don't exist, skip...".CarryOnLength());
                return ActionResult.Fail;
            }

            if (ClearStack)
                ProfilesStack.Clear();
            return ActionResult.Completed;
        }

        [Description("Force clean stack for current character")]
        public bool ClearStack { get; set; } = false;
    }
}
