using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.UpgradeManager;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class RunUpgradeProfile : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        private UpgradeManagerData Data => Core.UpgradeManagerCore.Data;
        private UpgradeManagerData.Profile CurrentProfle => Data.Profiles.ToList().Find(p => p.Name == ProfileName) ?? new UpgradeManagerData.Profile();

        [Description("Name of profile from list to run.")]
        public string ProfileName { get; set; }

        protected override bool IntenalConditions
        {
            get
            {
                if (string.IsNullOrEmpty(ProfileName))
                {
                    Logger.WriteLine(ActionLabel + $": You must determine {nameof(ProfileName)} to run!");
                    return false;
                }
                if (!CurrentProfle.Tasks.Any())
                {
                    Logger.WriteLine(ActionLabel + ": Tasks list is empty!");
                    return false;
                }
                return true;
            }
        }
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (string.IsNullOrEmpty(ProfileName))
                {
                    return new ActionValidity($"You must determine {nameof(ProfileName)} to run!");
                }
                return new ActionValidity();
            }
        }

        public override ActionResult Run()
        {
            if (IntenalConditions)
            {
                Task.WaitAll(Core.UpgradeManagerCore.StartTasks(CurrentProfle, taskStartIdx: 0));
                return ActionResult.Completed;
            }
            return ActionResult.Fail;
        }

    }
}
