using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.UpgradeManager;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Common.Converters;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class RunUpgradeProfile : Astral.Quester.Classes.Action, IListConverter
    {
        public override string ActionLabel => $"{GetType().Name} : {ProfileName}";
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        private UpgradeManagerData Data => Core.UpgradeManagerCore.Data;
        private UpgradeManagerData.Profile CurrentProfile => Data.Profiles.ToList().Find(p => p.Name == ProfileName) ?? new UpgradeManagerData.Profile();

        [Browsable(false), XmlIgnore]
        public IList ListConverterData => Data.Profiles;
        
        [Description("Name of profile from list to run.")]
        [TypeConverter(typeof(ListConverter))]
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
                if (!CurrentProfile.Tasks.Any())
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
            if (!IntenalConditions) return ActionResult.Fail;
            CurrentProfile.Run(startIdx: 0);
            return ActionResult.Completed;
        }
    }
}
