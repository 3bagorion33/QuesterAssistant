using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Astral.Quester.Forms;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes
{
    internal static class ProfilesStack
    {
        private static readonly Dictionary<string, List<Item>> Items = new Dictionary<string, List<Item>>();
        private static string CurrentCharacter => EntityManager.LocalPlayer.InternalName;
        public static string CurrentProfilePath =>
            new FileInfo(Astral.Controllers.Settings.Get.LastQuesterProfile).FullName;

        public static List<FileInfo> GetProfiles =>
            new DirectoryInfo(Core.ProfilesPath)
                .GetFiles("*.amp.zip", SearchOption.AllDirectories)
                .OrderBy(f => f.FullName)
                .ToList();

        public static string CurrentProfileName => CurrentProfilePath.Substring(Core.ProfilesPath.Length + 1);
        public static Item Last =>
            Any ? Items[CurrentCharacter].LastOrDefault() : new Item();
        public static bool Any =>
            Items.Any() &&
            Items.ContainsKey(CurrentCharacter) &&
            Items[CurrentCharacter].Any();

        public static string RelativePath(string destProfile)
        {
            return WinAPI.RelativePath(CurrentProfilePath, destProfile).Replace('\\', '/');
        }

        public static void Add(string profilePath, Guid actionId)
        {
            if (!Items.ContainsKey(CurrentCharacter))
                Items.Add(CurrentCharacter, new List<Item>());
            Items[CurrentCharacter].Add(new Item { ProfilePath = profilePath, ActionID = actionId });
        }

        public static void RemoveLast()
        {
            Items[CurrentCharacter].Remove(Last);
        }

        public static void Clear(bool allCharacters = false)
        {
            if (allCharacters)
                Items.Clear();
            else if (Any)
                Items[CurrentCharacter].Clear();
        }

        public static string Show()
        {
            return string.Join("<br><br>", Items[CurrentCharacter].Select(i => $"<b>{i.ProfilePath}</b><br>    {i.ActionID}"));
        }

        public struct Item
        {
            public string ProfilePath;
            public Guid ActionID;

            public bool Load()
            {
                var result = new FileInfo(ProfilePath).Exists;
                if (result)
                {
                    Astral.Quester.API.LoadProfile(ProfilePath);
                    var profile = Astral.Quester.API.CurrentProfile;
                    var lastAction = profile.GetActionByID(ActionID);
                    profile.MainActionPack.SetStartPoint(lastAction);
                    lastAction.SetCompleted(true);
                    Pause.Sleep(500);
                    (typeof(Editor).GetStaticFieldValue("editorForm", BindingFlags.NonPublic | BindingFlags.Static) as Editor)?.refreshAll();
                }

                return result;
            }

            public override string ToString()
            {
                return RelativePath(ProfilePath).OrDefault("none");
            }
        }
    }
}
