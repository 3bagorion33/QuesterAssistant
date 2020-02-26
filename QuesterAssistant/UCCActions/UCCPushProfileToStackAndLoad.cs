using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Astral;
using Astral.Logic.UCC.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common.Converters;

namespace QuesterAssistant.UCCActions
{
    public class UCCPushProfileToStackAndLoad : UCCAction, IListConverter
    {
        private string profileName = string.Empty;
        private FileInfo ProfileInfo => new FileInfo(Path.Combine(Core.ProfilesPath, profileName));
        public override UCCAction Clone() =>
            BaseClone(new UCCPushProfileToStackAndLoad {ProfileName = profileName});
        public override bool NeedToRun
        {
            get
            {
                if (string.IsNullOrEmpty(profileName))
                    return false;
                if (API.CurrentSettings.LastQuesterProfile == ProfileInfo.FullName)
                    return false;
                if (!ProfileInfo.Exists)
                    return false;
                return true;
            }
        }
        public UCCPushProfileToStackAndLoad()
        {
            ActionName = GetType().Name;
        }
        public override bool Run()
        {
            ProfilesStack.PushAndLoad(ProfileInfo);
            return true;
        }
        public override string ToString() =>
            $"{ActionName} : {profileName}";

        [Category("Required")]
        [Description("Select profile for loading")]
        [TypeConverter(typeof(FileFullNameListConverter))]
        public string ProfileName
        {
            get => profileName;
            set => profileName = value.Contains(@"/")
                ? value
                : value.Replace(@"\", @"/");
        }

        [Browsable(false), XmlIgnore]
        public IList ListConverterData => ProfilesStack.GetProfiles;
    }
}