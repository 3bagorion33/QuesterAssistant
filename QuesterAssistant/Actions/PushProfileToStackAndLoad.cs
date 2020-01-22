using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes.Actions;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common.Converters;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class PushProfileToStackAndLoad : Astral.Quester.Classes.Action, IListConverter
    {
        private string profileName = string.Empty;
        public override string ActionLabel => $"{GetType().Name} : {ProfileName}";
        public override string Category => Core.Category;
        protected override bool IntenalConditions => true;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity =>
            ProfileName.Length == 0
                ? new ActionValidity("No profile name set.")
                : new ActionValidity();

        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override ActionResult Run()
        {
            if (ProfileName.Length == 0) return ActionResult.Fail;
            Pause.Sleep(800);
            Logger.WriteLine($"Push profile to stack : {ProfilesStack.CurrentProfileName}");
            var currentProfile = ProfilesStack.CurrentProfilePath;
            var result = new LoadProfile { ProfileName = profileName }.Run();
            if (result == ActionResult.Completed) ProfilesStack.Add(currentProfile, ActionID);
            return result;
        }

        [Description("Select profile for loading")]
        [TypeConverter(typeof(FileInfoListConverter))]
        public string ProfileName
        {
            get => profileName;
            set => profileName = value.Contains(@"/")
                ? value
                : ProfilesStack.RelativePath(Path.Combine(Core.ProfilesPath, value));
        }

        [Browsable(false), XmlIgnore]
        public IList ListConverterData => ProfilesStack.GetProfiles;
    }
}