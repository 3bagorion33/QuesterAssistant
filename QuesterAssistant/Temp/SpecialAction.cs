namespace Astral.Quester.Classes.Actions
{
    using Astral.Controllers;
    using Astral.Logic.Classes.Map;
    using Astral.Logic.NW;
    using Astral.Logic.UCC;
    using Astral.Logic.UCC.Classes;
    using Astral.Quester.Classes;
    using MyNW.Classes;
    using MyNW.Internals;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class SpecialAction : Astral.Quester.Classes.Action
    {
        // Methods
        public SpecialAction()
        {
            this.Action = SAction.Invoke;
        }

        public override void GatherInfos()
        {
        }

        public override void InternalReset()
        {
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
        }

        public override Astral.Quester.Classes.Action.ActionResult Run()
        {
            bool flag;
            switch (this.Action)
            {
                case SAction.Invoke:
                    if (Powers.Pray())
                    {
                        return Astral.Quester.Classes.Action.ActionResult.Completed;
                    }
                    return Astral.Quester.Classes.Action.ActionResult.Fail;

                case SAction.InvokeACompanion:
                    if (Pets.InvokeACompanion())
                    {
                        return Astral.Quester.Classes.Action.ActionResult.Completed;
                    }
                    return Astral.Quester.Classes.Action.ActionResult.Fail;

                case SAction.LeaveTeam:
                    EntityManager.get_LocalPlayer().get_PlayerTeam().Leave();
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.ReturnInstance:
                    Thread.Sleep(0x5dc);
                    GameCommands.QueueReturn();
                    Thread.Sleep(0x157c);
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.StopBot:
                    Roles.ToggleRole(false);
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.StopBotAndLogOut:
                    new Thread((<>c.<>9__17_0 == null) ? (<>c.<>9__17_0 = new ParameterizedThreadStart(<>c.<>9.<Run>b__17_0)) : <>c.<>9__17_0).Start();
                    Thread.Sleep(0x3e8);
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.LeaveMatchGroup:
                    EntityManager.get_LocalPlayer().get_Player().get_PlayerMatchEngineData().get_PlayerMatchRequestGroup().LeaveGroup();
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.KillPlayer:
                    EntityManager.get_LocalPlayer().KillMe();
                    Thread.Sleep(0x5dc);
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                case SAction.RemoveAllTempUCCActions:
                    flag = false;
                    break;

                case SAction.RefineRAstralDiamonds:
                    if (EntityManager.get_LocalPlayer().get_Inventory().get_AstralDiamondsRough() > 0)
                    {
                        EntityManager.get_LocalPlayer().RefineAstralDiamonds();
                    }
                    return Astral.Quester.Classes.Action.ActionResult.Completed;

                default:
                    return Astral.Quester.Classes.Action.ActionResult.Fail;
            }
        Label_00C1:
            flag = false;
            using (List<UCCAction>.Enumerator enumerator = Core.Get.mProfil.ActionsCombat.GetEnumerator())
            {
                UCCAction current;
                do
                {
                    if (!enumerator.MoveNext())
                    {
                        goto Label_0127;
                    }
                    current = enumerator.Current;
                }
                while (!current.TempAction);
                Core.Get.mProfil.ActionsCombat.Remove(current);
                goto Label_00C1;
            }
        Label_0127:
            if (flag)
            {
                goto Label_00C1;
            }
            return Astral.Quester.Classes.Action.ActionResult.Completed;
        }

        // Properties
        public SAction Action { get; set; }

        public override string ActionLabel
        {
            get
            {
                return ("SpecialAction " + this.Action);
            }
        }

        protected override bool IntenalConditions
        {
            get
            {
                return true;
            }
        }

        protected override Vector3 InternalDestination
        {
            get
            {
                return new Vector3();
            }
        }

        public override string InternalDisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        protected override Astral.Quester.Classes.Action.ActionValidity InternalValidity
        {
            get
            {
                return new Astral.Quester.Classes.Action.ActionValidity();
            }
        }

        public override bool NeedToRun
        {
            get
            {
                if (this.Action == SAction.InvokeACompanion)
                {
                    return !EntityManager.get_LocalPlayerCompanion().get_IsValid();
                }
                return true;
            }
        }

        public override bool UseHotSpots
        {
            get
            {
                return false;
            }
        }

        // Nested Types
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            // Fields
            public static readonly SpecialAction.<>c <>9 = new SpecialAction.<>c();
            public static ParameterizedThreadStart <>9__17_0;

            // Methods
            internal void <Run>b__17_0(object t)
            {
                Roles.ToggleRole(false);
                GameCommands.LogOut();
            }
        }

        public enum SAction
        {
            Invoke,
            InvokeACompanion,
            LeaveTeam,
            ReturnInstance,
            StopBot,
            StopBotAndLogOut,
            LeaveMatchGroup,
            KillPlayer,
            RemoveAllTempUCCActions,
            RefineRAstralDiamonds
        }
    }
}

