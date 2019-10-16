using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes
{
    internal static class ProfilesStack
    {
        public static readonly List<Item> Items = new List<Item>();

        public static List<FileInfo> GetProfiles =>
            new DirectoryInfo(Core.ProfilesPath)
                .GetFiles("*.amp.zip", SearchOption.AllDirectories)
                .OrderBy(f => f.FullName)
                .ToList();

        public static string CurrentProfilePath => Astral.Controllers.Settings.Get.LastQuesterProfile;
        public static string CurrentProfileName => CurrentProfilePath.Substring(Core.ProfilesPath.Length + 1);

        public static string RelativePath(string destProfile)
        {
            return WinAPI.RelativePath(CurrentProfilePath, destProfile).Replace('\\', '/');
        }

        public struct Item
        {
            public string ProfilePath;
            public Guid ActionID;

            public override string ToString()
            {
                return RelativePath(ProfilePath);
            }
        }
    }
}
