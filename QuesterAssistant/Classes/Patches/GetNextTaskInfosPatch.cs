using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Astral;
using Astral.Professions.Classes;
using Astral.Professions.Controllers;
using Astral.Professions.Functions;
using Astral.Professions.FSM.States;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Classes.Patches
{
    internal class GetNextTaskInfosPatch
    {
        private static uint TriggerTasksCount => Core.SettingsCore.Data.Patches.ProfessionPatchReadyTasksCount;

        private static readonly Func<string, string> Characters_smethod_2 =
            typeof(Characters).GetStaticMethodBySignature<Func<string, string>>
                (typeof(string), new[] { typeof(string) }) as Func<string, string>;

        private static readonly Func<string, string, bool> Characters_smethod_7 =
            typeof(Characters).GetStaticMethodBySignature<Func<string, string, bool>>
                (typeof(bool), new[] { typeof(string), typeof(string) }) as Func<string, string, bool>;

        private static readonly Func<string, string, Characters.SavedCharacter> Characters_smethod_8 =
            typeof(Characters).GetStaticMethodBySignature<Func<string, string, Characters.SavedCharacter>>
                (typeof(Characters.SavedCharacter), new[] { typeof(string), typeof(string) }) as Func<string, string, Characters.SavedCharacter>;

        public static object Astral_Professions_Functions_Tasks_GetNextTaskInfos(bool showLogs = false, bool onlyCurrentAccount = false)
        {
            Debug.WriteLine("Astral_Professions_Functions_Tasks_GetNextTaskInfos hacked!");

            CharacterSettings characterSettings = null;
            ProfAccountProfile nextAccount = null;
            int num = int.MaxValue;
            string text = string.Empty;
            string charIntName = string.Empty;
            List<ProfAccountProfile> list = new List<ProfAccountProfile>();
            var CurrentAccount = typeof(Main).GetStaticPropertyValue("CurrentAccount") as ProfAccountProfile;
            if (onlyCurrentAccount)
            {
                list.Add(CurrentAccount);
            }
            else
            {
                list.AddRange(Astral.Professions.Core.AccountsProfile.Accounts);
                ProfAccountProfile currentAccount = CurrentAccount;
                if (list.Contains(currentAccount) && list.IndexOf(currentAccount) != 0)
                {
                    list.Remove(currentAccount);
                    list.Insert(0, currentAccount);
                }
            }
            foreach (ProfAccountProfile profAccountProfile in list)
            {
                if (!profAccountProfile.Disabled)
                {
                    var HaveToConnectMainChar = (bool) profAccountProfile.GetPropertyValue("HaveToConnectMainChar",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                    if (HaveToConnectMainChar)
                    {
                        CharacterSettings mainCharacter = profAccountProfile.GetMainCharacter();
                        if (mainCharacter.Name.Length > 0)
                        {
                            text = "main char";
                            num = 0;
                            characterSettings = mainCharacter;
                            nextAccount = profAccountProfile;
                            charIntName = mainCharacter.Name + "@" + Characters_smethod_2(profAccountProfile.AccountName);
                            break;
                        }

                        profAccountProfile.SetPropertyValue("HaveToConnectMainChar", false,
                            BindingFlags.Instance | BindingFlags.NonPublic);
                    }
                    foreach (CharacterSettings characterSettings2 in profAccountProfile.CharactersSettings)
                    {
                        if (!characterSettings2.Disabled)
                        {
                            string text2 = characterSettings2.Name + "@" + Characters_smethod_2(profAccountProfile.AccountName);
                            if (!Characters_smethod_7(characterSettings2.Name, profAccountProfile.AccountName))
                            {
                                text = "no informations";
                                num = 0;
                                characterSettings = characterSettings2;
                                nextAccount = profAccountProfile;
                                charIntName = text2;
                                goto IL_2A6;
                            }
                            Characters.SavedCharacter savedCharacter = Characters_smethod_8(characterSettings2.Name, profAccountProfile.AccountName);
                            if (Game.GameTimeSec - savedCharacter.SavedTime > 86400)
                            {
                                text = "obsolete";
                                num = 0;
                                characterSettings = characterSettings2;
                                nextAccount = profAccountProfile;
                                charIntName = text2;
                                goto IL_2A6;
                            }
                            if (Astral.Professions.Core.AccountsProfile.CharSelectionMode > 0 &&
                                savedCharacter.Invocation.RemainingTimeSec > -1 &&
                                savedCharacter.Invocation.RemainingTimeSec < num)
                            {
                                num = savedCharacter.Invocation.RemainingTimeSec;
                                characterSettings = characterSettings2;
                                text = "invocation";
                                nextAccount = profAccountProfile;
                                charIntName = text2;
                            }
                            if (characterSettings2.ActionsProfileExist() && savedCharacter.SavedSlots.Count > 0)
                            {
                                var slots = savedCharacter.SavedSlots;
                                var readySlotsCount = slots.Count(s => s.RemainingTimeSec == 0);
                                var activeSlotsCount = Core.SettingsCore.Data.Patches.ProfessionPatchActiveTasksCount;
                                var fastestTask = slots.FindAll(s => s.Duration > 5)
                                    .Aggregate((a, b) => a.Duration < b.Duration ? a : b);
                                var estimatedTime =
                                    TriggerTasksCount <= readySlotsCount + activeSlotsCount
                                        ? 0
                                        : ((int) ((TriggerTasksCount - readySlotsCount + 1) / activeSlotsCount *
                                                  fastestTask.Duration + fastestTask.StartedTime - Game.GameTimeSec))
                                        .CheckNegative(0);
                                if (estimatedTime < num)
                                {
                                    num = estimatedTime;
                                    characterSettings = characterSettings2;
                                    text = "complete task";
                                    nextAccount = profAccountProfile;
                                    charIntName = text2;
                                }
                            }
                        }
                    }
                }
            }
            IL_2A6:
            if (num == 0 && showLogs)
            {
                switch (text)
                {
                    case "invocation":
                        Logger.WriteLine($"{characterSettings.Name} seems to need to invoke...");
                        break;
                    case "obsolete":
                        Logger.WriteLine($"Obsolete information on {characterSettings.Name} character.");
                        break;
                    case "complete task":
                        Logger.WriteLine($"{characterSettings.Name} have {TriggerTasksCount} or more slots to complete...");
                        break;
                    case "start task":
                        Logger.WriteLine($"{characterSettings.Name} have no active task...");
                        break;
                    case "main char":
                        Logger.WriteLine("Have to connect on main character ...");
                        break;
                    case "no informations":
                        Logger.WriteLine($"No saved information for : {characterSettings.Name}");
                        break;
                }
            }

            var result = typeof(Tasks.NextTaskInfos).CreateInstance();
            result.SetPropertyValue(nameof(Tasks.NextTaskInfos.NextChar), characterSettings);
            result.SetPropertyValue(nameof(Tasks.NextTaskInfos.NearestTime), num);
            result.SetPropertyValue(nameof(Tasks.NextTaskInfos.TaskType), text);
            result.SetPropertyValue(nameof(Tasks.NextTaskInfos.NextAccount), nextAccount);
            result.SetPropertyValue(nameof(Tasks.NextTaskInfos.CharIntName), charIntName);

            return result;
        }
    }
}