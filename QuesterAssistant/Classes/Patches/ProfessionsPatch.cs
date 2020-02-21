using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Astral;
using Astral.Logic.NW;
using Astral.Professions.Classes;
using Astral.Professions.Controllers;
using Astral.Professions.FSM.States;
using Astral.Professions.Functions;
using MyNW.Classes.ItemAssignment;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    internal class ProfessionsPatch
    {
        private static BindingFlags binding = Helper.DefaultFlags;
        private static uint FreeTasksSlots => Core.SettingsCore.Data.Patches.ProfessionPatchFreeTasksSlots;
        private static System.Threading.Tasks.Task task;

        private static readonly Func<string, string> Characters_smethod_2 =
            typeof(Characters).GetStaticMethod<string, string>();

        private static readonly Func<string, string, bool> Characters_smethod_7 =
            typeof(Characters).GetStaticMethod<string, string, bool>();

        private static readonly Func<string, string, Characters.SavedCharacter> Characters_smethod_8 =
            typeof(Characters).GetStaticMethod<string, string, Characters.SavedCharacter>();
        private static readonly Action Characters_smethod_11 =
            typeof(Characters).GetStaticVoidMethod("\u0005");

        public static void RunOnce()
        {
            if (typeof(Characters).GetStaticFieldValue("\u0001") is Thread thread && thread.IsAlive)
                thread.Abort();

            new PatchMethod(typeof(Tasks).GetMethod("GetNextTaskInfos", binding),
                    typeof(ProfessionsPatch).GetMethod(nameof(Astral_Professions_Functions_Tasks_GetNextTaskInfos), binding))
                .Inject();
            new PatchMethod(typeof(Main).GetMethod("RandomPause", binding),
                    typeof(ProfessionsPatch).GetMethod(nameof(Astral_Professions_FSM_States_Main_RandomPause), binding))
                .Inject();
            new PatchConstructor<Characters.SavedSlot, SavedSlotPatch>
                    (new[] {typeof(uint), typeof(bool), typeof(Assignment)})
                .Inject();

            task = System.Threading.Tasks.Task.Factory.StartNew(Characters_smethod_11, TaskCreationOptions.LongRunning);
        }

        public static void RunAtPlay()
        {
            if (task is null || task.Status != TaskStatus.Running)
                task = System.Threading.Tasks.Task.Factory.StartNew(Characters_smethod_11, TaskCreationOptions.LongRunning);
            //new PatchMethod(new StartATask().GetType().GetMethod("Run", binding),
            //        typeof(StartATaskPatch).GetMethod(nameof(StartATaskPatch.RunPatch), binding))
            //    .Inject();
        }

        private static void Astral_Professions_FSM_States_Main_RandomPause(int min, int max)
        {
            if (max == min + 5)
            {
                Pause.Sleep(250);
                return;
            }
            Pause.RandomSleep(min * 1000, max * 1000);
        }

        private static object Astral_Professions_Functions_Tasks_GetNextTaskInfos(bool showLogs = false, bool onlyCurrentAccount = false)
        {
            //Debug.WriteLine("Astral_Professions_Functions_Tasks_GetNextTaskInfos hacked!");

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
                    var HaveToConnectMainChar = (bool)profAccountProfile.GetPropertyValue("HaveToConnectMainChar",
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
                                goto End;
                            }
                            Characters.SavedCharacter savedCharacter = Characters_smethod_8(characterSettings2.Name, profAccountProfile.AccountName);
                            if (Game.GameTimeSec - savedCharacter.SavedTime > 86400)
                            {
                                text = "obsolete";
                                num = 0;
                                characterSettings = characterSettings2;
                                nextAccount = profAccountProfile;
                                charIntName = text2;
                                goto End;
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
                                var activeSlots = slots.FindAll(s => (int)s.SlotID == -1);
                                var activeSlotsCount = activeSlots.Count;
                                var speed = activeSlots.Sum(s => 1 / (double) s.Duration);
                                var latestTask = slots.FindAll(s => s.Duration > 5)
                                    .Aggregate((a, b) => a.StartedTime > b.StartedTime && a.Duration < b.Duration ? a : b);
                                var maxSlotsCount = slots.FirstOrDefault().Index;
                                var estimatedTime =
                                    ((maxSlotsCount - readySlotsCount - activeSlotsCount - FreeTasksSlots) / speed +
                                     latestTask.StartedTime + latestTask.Duration - Game.GameTimeSec)
                                    .CheckNegative(0);
                                if (estimatedTime < num)
                                {
                                    num = (int) estimatedTime;
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
            End:
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
                        Logger.WriteLine($"{characterSettings.Name} seems to ready to claim tasks...");
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

        private class SavedSlotComparer : IEqualityComparer<Characters.SavedSlot>
        {
            public bool Equals(Characters.SavedSlot x, Characters.SavedSlot y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null || y is null) return false;
                return x.InternalName == y.InternalName && x.Duration == y.Duration;
            }
            public int GetHashCode(Characters.SavedSlot slot)
            {
                return slot.InternalName.GetHashCode() ^ slot.Duration.GetHashCode();
            }
        }

        public class SavedSlotPatch : Characters.SavedSlot
        {
            public SavedSlotPatch(uint slotIndex, bool locked, Assignment assignement = null) :
                base(slotIndex, locked,assignement)
            {
                //Debug.WriteLine("SavedSlot hacked!");
                Index = (uint) Professions2.MaxSlots;
                SlotID = (uint) assignement.RepeatCount;
            }
        }
    }
}
