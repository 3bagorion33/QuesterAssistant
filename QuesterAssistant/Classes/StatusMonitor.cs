//#define DEBUG

using Astral;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuesterAssistant.Classes
{
    /// <summary>
    /// Класс, сохраняющий "слепок" состояния бота в момент создания
    /// </summary>
    public class AstralStatus : IEquatable<AstralStatus>
    {
        public AstralStatus()
        {
            TimeStamp = DateTime.Now;

            //EntityManager.LocalPlayer?.InternalName - полное имя персонажа, включающее аккаунт
            Account = Game.CharacterSelectionData?.PublicAccountName;
            IsCharacterSelectScreenVisible = Game.IsCharacterSelectionScreenVisible();
            CharacterName = EntityManager.LocalPlayer?.Name;

            MapName = EntityManager.LocalPlayer?.MapState?.MapName;
            RegionName = EntityManager.LocalPlayer?.RegionInternalName;
            CharacterPosition = EntityManager.LocalPlayer?.Location?.Clone();

            CharacterStatus = GetCharacterStatus();

            Role = "";
            RoleIsRunning = Astral.API.RoleIsRunning;
            //ProfileName = Astral.Quester.API.CurrentProfile.MainActionPack.DisplayName;

        }

        /// <summary>
        /// Определение состояния персонажа
        /// </summary>
        /// <returns></returns>
        public static CharacterStatus GetCharacterStatus()
        {
            if (EntityManager.LocalPlayer?.InCombat == true)
                return CharacterStatus.Combat;
            else if (EntityManager.LocalPlayer?.IsDead == true)
                return CharacterStatus.Dead;
            else if (EntityManager.LocalPlayer?.IsLoading == true)
                return CharacterStatus.Loading;
            else if (Astral.Logic.NW.Stuck.IsStuck)
                return CharacterStatus.Stuck;
            else if (Astral.Logic.NW.Interact.IsInteracting)
                return CharacterStatus.Interacting;
            else if (Astral.Logic.NW.Navigator.IsRunning)
                return CharacterStatus.Moving;
            else return CharacterStatus.Stay;
        }

        internal double Distance(Vector3 pos_1, Vector3 pos_2)
        {
            if (pos_1 != null && pos_2 != null)
            {
                return pos_1.Distance3D(pos_2);
            }
            return double.MaxValue;
        }

        public bool Equals(AstralStatus otherStatus)
        {
#if DEBUG
            Logger.WriteLine($"NotifyStatusMonitor debug: Compare Status[{TimeStamp.Ticks}] and Status[{otherStatus.TimeStamp.Ticks}]");

            if(otherStatus != null)
            {
                bool checkTime = TimeStamp != otherStatus.TimeStamp;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.TimeStamp[{TimeStamp.Ticks}] ? New.TimeStamp[{otherStatus.TimeStamp.Ticks}]: {checkTime}");
                bool checkAcc = Account == otherStatus.Account;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.Account[{Account}] ? New.Account[{otherStatus.Account}]: {checkAcc}");
                bool checkChar = CharacterName == otherStatus.CharacterName;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.CharacterName[{CharacterName}] ? New.CharacterName[{otherStatus.CharacterName}]: {checkChar}");
                bool checkMap = MapName == otherStatus.MapName;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Las.tMapName[{MapName}] ? New.MapName[{otherStatus.MapName}]: {MapName}");
                bool checkReg = RegionName == otherStatus.RegionName;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.RegionName[{RegionName}] ? New.RegionName[{otherStatus.RegionName}]: {checkReg}");
                bool checkRole = Role == otherStatus.Role;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.Role[{Role}] ? New.Role[{otherStatus.Role}]: {checkRole}");
                bool checkRRun = RoleIsRunning == otherStatus.RoleIsRunning;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.RoleIsRunning[{RoleIsRunning}] ? New.RoleIsRunning[{otherStatus.RoleIsRunning}]: {checkRRun}");
                bool checkCharStat = CharacterStatus == otherStatus.CharacterStatus;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Last.CharacterStatus[{CharacterStatus}] ? New.CharacterStatus[{otherStatus.CharacterStatus}]: {checkCharStat}");
                double dist = Distance(CharacterPosition, otherStatus.CharacterPosition);
                bool checkDist = dist <= CharacterPositionDeviation;
                Logger.WriteLine($"NotifyStatusMonitor debug:\t Distance(Last, New)={dist} ? CharacterPositionDeviation{CharacterPositionDeviation}: {checkDist}");

                bool result = checkTime && checkAcc && checkChar && checkMap && checkReg && checkRole && checkRRun && checkCharStat && checkDist;
                Logger.WriteLine($"NotifyStatusMonitor debug: Result = {result}");

                return result;
            }
#else

            if (otherStatus != null && TimeStamp != otherStatus.TimeStamp)
            {
                return (Account == otherStatus.Account &&
                        CharacterName == otherStatus.CharacterName &&
                        MapName == otherStatus.MapName &&
                        RegionName == otherStatus.RegionName &&
                        Role == otherStatus.Role &&
                        RoleIsRunning == otherStatus.RoleIsRunning &&
                        CharacterStatus == otherStatus.CharacterStatus &&
                        Distance(CharacterPosition, otherStatus.CharacterPosition) <= CharacterPositionDeviation);
            }
#endif
            return false;
        }


        /// <summary>
        /// Метка времени, когда зафиксировано состояние 
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Загруженные аккаунт при последней проверке
        /// </summary>
        public string Account { get; }

        /// <summary>
        /// Активность страницы выбора персонажа при последней проверке
        /// </summary>
        public bool IsCharacterSelectScreenVisible { get; }

        /// <summary>
        /// Имя персонажа при последней проверке
        /// </summary>
        public string CharacterName { get; }

        /// <summary>
        /// Карта, на которой находится персонаж
        /// </summary>
        public string MapName { get; }
        /// <summary>
        /// Регион текущей карты, в котором находится персонаж
        /// </summary>
        public string RegionName { get; }

        /// <summary>
        /// Координаты персонажа при последней проверке
        /// </summary>
        public Vector3 CharacterPosition { get; }

        /// <summary>
        /// Допустимое отклонение (расстояние) сравниваемых позицый персонажа,
        /// в пределах которого сравниваемые позицыи считаются совпадающими
        /// </summary>
        public static double CharacterPositionDeviation = 15;

        /// <summary>
        /// Состояние персонажа на момент проверки
        /// </summary>
        public CharacterStatus CharacterStatus { get; }

        /// <summary>
        /// Исполняемая роль
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// Статус роли
        /// </summary>
        public bool RoleIsRunning { get; }

        /// <summary>
        /// Профиль
        /// </summary>
        public string ProfileName { get; }
    }

    /// <summary>
    /// Класс, отслеживающий изменение состояния бота
    /// и генерирующий push-уведомление в случае застревания/остановки и т.п.
    /// </summary>
    public class NotifyStatusMonitor
    {
        private AstralStatus lastStatus;

        [Description("Интервал времени между проверками состояния (в милисекундах)")]
        public int CheckingTime { get; set; } = 30000;

        /// <summary>
        /// Максимальное допустимое чисто совпадения состояний персонажа при проверке
        /// при превышении которого посылается сообщение
        /// </summary>
        public int MaxDeadCount
        {
            get => MaxCounters[CharacterStatus.Dead];
            set => MaxCounters[CharacterStatus.Dead] = value;
        }
        public int MaxStayCount
        {
            get => MaxCounters[CharacterStatus.Stay];
            set => MaxCounters[CharacterStatus.Stay] = value;
        }
        public int MaxStuckCount
        {
            get => MaxCounters[CharacterStatus.Stuck];
            set => MaxCounters[CharacterStatus.Stuck] = value;
        }
        public int MaxInteractCount
        {
            get => MaxCounters[CharacterStatus.Interacting];
            set => MaxCounters[CharacterStatus.Interacting] = value;
        }
        public int MaxLoadingCount
        {
            get => MaxCounters[CharacterStatus.Dead];
            set => MaxCounters[CharacterStatus.Dead] = value;
        }
        public int MaxMovingCount
        {
            get => MaxCounters[CharacterStatus.Moving];
            set => MaxCounters[CharacterStatus.Moving] = value;
        }
        public int MaxCombatCount
        {
            get => MaxCounters[CharacterStatus.Combat];
            set => MaxCounters[CharacterStatus.Combat] = value;
        }

        private Dictionary<CharacterStatus, int> MaxCounters;
        private Dictionary<CharacterStatus, int> Counters;


        //[Description("Text of message")]
        //public string Message { get; set; } = "Check Astral! Possible it is stopped. ";

        private static Task task;

        bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled == false)
                {
                    enabled = value;
                    if (enabled)
                    {
                        if (task == null)
                            task = Task.Factory.StartNew(Run);
                        else task.Start();
                    }
                }
                else
                {
                    enabled = value;
                    if (task != null && !enabled)
                        task.Wait();
                }
            }
        }

        public NotifyStatusMonitor()
        {
            Counters = new Dictionary<CharacterStatus, int>
            {
                { CharacterStatus.Dead, 0 },
                { CharacterStatus.Stay, 0 },
                { CharacterStatus.Stuck, 0 },
                { CharacterStatus.Interacting, 0 },
                { CharacterStatus.Moving, 0 },
                { CharacterStatus.Loading, 0 },
                { CharacterStatus.Combat, 0 }
            };

            MaxCounters = new Dictionary<CharacterStatus, int>
            {
                { CharacterStatus.Dead, 10 },
                { CharacterStatus.Stay, 10 },
                { CharacterStatus.Stuck, 4 },
                { CharacterStatus.Interacting, 3 },
                { CharacterStatus.Moving, 4 },
                { CharacterStatus.Loading, 10 },
                { CharacterStatus.Combat, 10 }
            };
        }

        protected void Run()
        {
            if (lastStatus == null)
            {
                lastStatus = new AstralStatus();
            }
            while(enabled)
            {
                task.Wait(CheckingTime);
                AstralStatus newStatus = new AstralStatus();
                if(lastStatus.Equals(newStatus))
                {
                    Counters[newStatus.CharacterStatus] += 1;
                    if (Counters[newStatus.CharacterStatus] >= MaxCounters[newStatus.CharacterStatus])
                    {
                        //Послать push-сообщение
                        try
                        {
                            Core.PushNotifyCore.PushMessage(DateTime.Now.ToLongTimeString() + '\n' +
                                "Check Astral! Possible there are problems\n" +
                                $"Character status '{newStatus.CharacterStatus}' detected {Counters[newStatus.CharacterStatus]} times.");
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine($"PushNotify Error: {ex.Message}");
                        }
                        ResetConters();
                    }
                }
                else ResetConters();
                
                lastStatus = newStatus;
            }
        }

        /// <summary>
        /// Сброс счетчиков совпадения состояний
        /// </summary>
        private void ResetConters()
        {
            Counters[CharacterStatus.Dead] = 0;
            Counters[CharacterStatus.Stay] = 0;
            Counters[CharacterStatus.Stuck] = 0;
            Counters[CharacterStatus.Interacting] = 0;
            Counters[CharacterStatus.Moving] = 0;
            Counters[CharacterStatus.Loading] = 0;
            Counters[CharacterStatus.Combat] = 0;
        }
    }

    /// <summary>
    /// Перечень детектируемых состояний персонажа
    /// </summary>
    public enum CharacterStatus
    {
        Loading,
        Combat,
        Interacting,
        Moving,
        Stay,
        Stuck,
        Dead
    }

    //public enum BotStatus
    //{
    //    WorldWaiting,
    //    Stopped,
    //    Running,
    //    Looping,
    //    Undefined
    //}
}
