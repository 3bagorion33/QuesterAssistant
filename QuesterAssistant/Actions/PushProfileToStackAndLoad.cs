using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Astral;
using Astral.Logic.Classes.Map;
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
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (ProfileName.Length == 0)
                    return new ActionValidity("No profile name set.");
                if (!new FileInfo(Path.Combine(ProfilesStack.CurrentProfileInfo.DirectoryName, profileName)).Exists)
                    return new ActionValidity("This profile don't exist!");
                return new ActionValidity();
            }
        }

        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override ActionResult Run()
        {
            if (string.IsNullOrEmpty(ProfileName)) return ActionResult.Fail;

            Pause.Sleep(500);
            while (string.IsNullOrEmpty(Astral.Controllers.Settings.Get.LastQuesterProfile))
                Pause.Sleep(200);

            var fileInfo = new FileInfo(Path.Combine(ProfilesStack.CurrentProfileInfo.DirectoryName, ProfileName));

            if (!fileInfo.Exists)
            {
                Logger.WriteLine($"Unable to found profile {fileInfo.FullName}");
                return ActionResult.Fail;
            }

            Logger.WriteLine($"Push profile to stack : {ProfilesStack.CurrentProfileName}");
            ProfilesStack.PushAndLoad(fileInfo, ActionID);


            return ActionResult.Completed;
        }

        [Description("Select profile for loading")]
        [TypeConverter(typeof(FileFullNameListConverter))]
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