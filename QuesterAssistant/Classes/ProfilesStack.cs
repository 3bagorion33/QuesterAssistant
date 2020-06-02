using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Astral.Quester;
using Astral.Quester.Classes;
using Astral.Quester.Forms;
using DevExpress.Utils;
using MyNW.Internals;
using QuesterAssistant.Actions;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Reflection;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Classes
{
    internal static class ProfilesStack
    {
        private static readonly string StatusFile =
            Path.Combine(Core.SettingsPath, $"{nameof(ProfilesStack)}Status.bin");
        private static readonly Dictionary<string, StackDef> Stack =
            BinFile.Load<Dictionary<string, StackDef>>(StatusFile);
        private static string CurrentCharacter => EntityManager.LocalPlayer.InternalName;
        private static readonly Editor editorForm =
            typeof(Editor).GetStaticFieldValue("editorForm", BindingFlags.NonPublic | BindingFlags.Static) as Editor;
        private static ActionStatus CurrentProfileStatus =>
            API.CurrentProfile.MainActionPack.GetStatus();

        public static FileInfo CurrentProfileInfo =>
            new FileInfo(Astral.Controllers.Settings.Get.LastQuesterProfile);
        public static List<string> GetProfiles =>
            new DirectoryInfo(Core.ProfilesPath)
                .GetFiles("*.amp.zip", SearchOption.AllDirectories)
                .Select(f => f.FullName)
                .ToList();
        public static string CurrentProfileName => CurrentProfileInfo.FullName.Substring(Core.ProfilesPath.Length + 1);
        public static Item Last =>
            Any ? Stack[CurrentCharacter].Items.LastOrDefault() : new Item();
        public static bool Any =>
            Stack.Any() &&
            Stack.ContainsKey(CurrentCharacter) &&
            Stack[CurrentCharacter].Items.Any();

        public static string RelativePath(string destProfile)
        {
            return WinAPI.RelativePath(CurrentProfileInfo.FullName, destProfile).Replace('\\', '/');
        }

        public static void PushAndLoad(FileInfo fileToLoad, Guid pushActionId = default(Guid))
        {
            if (!Stack.ContainsKey(CurrentCharacter))
                Stack.Add(CurrentCharacter, new StackDef());
            Stack[CurrentCharacter].Add(new Item(pushActionId));
            SaveState();
            API.LoadProfile(fileToLoad.FullName);
            InsertReturn(API.CurrentProfile);
            RefreshEditor();
        }

        public static bool Pull()
        {
            var result = Last.Pull();
            if (result) RemoveLast();
            return result;
        }

        private static void InsertReturn(Profile profile)
        {
            if (!profile.GetFullActionList(profile.MainActionPack).Exists(a => a.GetType() == typeof(PullProfileFromStack)))
                profile.MainActionPack.Actions.Add(new PullProfileFromStack());
        }

        private static void SaveState()
        {
            BinFile.Save(Stack, StatusFile);
        }

        private static void RemoveLast()
        {
            Stack[CurrentCharacter].Remove(Last);
            SaveState();
        }

        public static void Clear(bool allCharacters = false)
        {
            if (allCharacters)
                Stack.Clear();
            else if (Any)
                Stack[CurrentCharacter].Clear();
            SaveState();
        }

        public static void Show()
        {
            var message = string.Join("<br><br>", Stack[CurrentCharacter].Items.Select(i => $"<b>{i.ProfilePath}</b><br>    {i.PushActionID}"));
            Task.Factory.StartNew(() => QMessageBox.ShowInfo(message, allowHtml: DefaultBoolean.True));
        }

        private static void RefreshEditor()
        {
            Pause.Sleep(500);
            editorForm?.refreshAll();
        }

        [Serializable]
        public class StackDef
        {
            private readonly List<Item> items;

            public List<Item> Items
            {
                get
                {
                    var lifeTime = Core.SettingsCore.Data.StackLifeTime;
                    if (lifeTime > 0 && DateTime.Now - TimeStamp > TimeSpan.FromMinutes(lifeTime))
                        items.Clear();
                    return items;
                }
            }

            public DateTime TimeStamp { get; private set; }

            public StackDef()
            {
                items = new List<Item>();
                TimeStamp = DateTime.Now;;
            }

            public void Add(Item item)
            {
                items.Add(item);
                UpdateTimeStamp();
            }

            public void Remove(Item item)
            {
                items.Remove(item);
                UpdateTimeStamp();
            }

            public void Clear()
            {
                items.Clear();
                UpdateTimeStamp();
            }

            private void UpdateTimeStamp() =>
                TimeStamp = DateTime.Now;
        }

        [Serializable]
        public struct Item
        {
            public readonly string ProfilePath;
            public readonly ActionStatus ProfileStatus;
            public readonly Guid PushActionID;

            public Item(Guid pushActionId)
            {
                ProfilePath = CurrentProfileInfo.FullName;
                ProfileStatus = CurrentProfileStatus;
                PushActionID = pushActionId;
            }

            public bool Pull()
            {
                var result = new FileInfo(ProfilePath).Exists;
                if (result)
                {
                    API.LoadProfile(ProfilePath);
                    API.CurrentProfile.MainActionPack.SetStatus(ProfileStatus);
                    if (PushActionID != Guid.Empty)
                        API.CurrentProfile.GetActionByID(PushActionID).SetCompleted(true);
                    InsertReturn(API.CurrentProfile);
                    RefreshEditor();
                }
                return result;
            }

            public override string ToString() => RelativePath(ProfilePath).OrDefault("none");
        }
    }
}
