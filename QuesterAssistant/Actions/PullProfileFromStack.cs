using System;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes.Actions;
using MyNW.Classes;
using QuesterAssistant.Classes;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class PullProfileFromStack : Astral.Quester.Classes.Action
    {
        private ProfilesStack.Item lastProfile => ProfilesStack.Items.LastOrDefault();
        public override string ActionLabel => $"{GetType().Name} : {lastProfile}";
        public override string Category => Core.Category;
        protected override bool IntenalConditions => ProfilesStack.Items.Any();
        public override bool NeedToRun => true;
        public override string InternalDisplayName => ActionLabel;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();

        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override ActionResult Run()
        {
            if (!IntenalConditions)
                return ActionResult.Fail;

            Logger.WriteLine($"Pull profile from stack : {lastProfile}");
            var result =  new LoadProfile {ProfileName = lastProfile.ToString()}.Run();
            if (result == ActionResult.Fail)
                return result;

            var profile = Astral.Quester.API.CurrentProfile;
            var lastAction = profile.GetActionByID(lastProfile.ActionID);
            profile.MainActionPack.SetStartPoint(lastAction);
            lastAction.SetCompleted(true);

            ProfilesStack.Items.Remove(lastProfile);
            if (ClearStack)
                ProfilesStack.Items.Clear();
            return result;
        }

        public bool ClearStack { get; set; } = false;
    }
}
