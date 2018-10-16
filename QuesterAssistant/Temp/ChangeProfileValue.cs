namespace Astral.Quester.Classes.Actions
{
    using Astral;
    using Astral.Logic.Classes.Map;
    using Astral.Quester.Classes;
    using Astral.Quester.FSM.States;
    using MyNW.Classes;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ChangeProfileValue : Action
    {
        // Methods
        public ChangeProfileValue()
        {
            this.ValueType = ProfileValue.KillRadius;
            this.Value = string.Empty;
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

        public override Action.ActionResult Run()
        {
            int result = 0;
            int.TryParse(this.Value, out result);
            switch (this.ValueType)
            {
                case ProfileValue.KillRadius:
                    Combat.Range = result;
                    break;

                case ProfileValue.MinFreeBagsSlots:
                    Vendor.minFreBagsSlots = result;
                    break;

                case ProfileValue.AllowPetTraining:
                    try
                    {
                        CheckAction.AllowPetTraining = Convert.ToBoolean(this.Value);
                    }
                    catch
                    {
                    }
                    break;

                case ProfileValue.IgnoreCombat:
                    Combat.ignoreCombat = Convert.ToBoolean(this.Value);
                    break;

                case ProfileValue.IgnoreCombatMinHP:
                    Combat.ignoreCombatMinHP = result;
                    break;

                case ProfileValue.DisablePet:
                    CheckAction.DisablePet = Convert.ToBoolean(this.Value);
                    break;

                case ProfileValue.FollowDistance:
                    ActionPlayer.followDistance = result;
                    break;

                case ProfileValue.DisableFollow:
                    ActionPlayer.disableFollow = Convert.ToBoolean(this.Value);
                    break;

                case ProfileValue.DisableMount:
                    CheckAction.DisableMount = Convert.ToBoolean(this.Value);
                    break;
            }
            Logger.WriteLine(this.ValueType + " set to " + this.Value);
            return Action.ActionResult.Completed;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "ChangeProfileValue";
            }
        }

        public override string Category
        {
            get
            {
                return "Basic";
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

        protected override Action.ActionValidity InternalValidity
        {
            get
            {
                if (this.Value.Length == 0)
                {
                    return new Action.ActionValidity("No fixed value.");
                }
                return new Action.ActionValidity();
            }
        }

        public override bool NeedToRun
        {
            get
            {
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

        public string Value { get; set; }

        public ProfileValue ValueType { get; set; }

        // Nested Types
        public enum ProfileValue
        {
            KillRadius,
            MinFreeBagsSlots,
            AllowPetTraining,
            IgnoreCombat,
            IgnoreCombatMinHP,
            DisablePet,
            FollowDistance,
            DisableFollow,
            DisableMount
        }
    }
}

