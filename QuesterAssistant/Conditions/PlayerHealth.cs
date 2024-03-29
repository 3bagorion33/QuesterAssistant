﻿using Astral.Quester.Classes;
using MyNW.Classes;
using MyNW.Internals;
using System;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class PlayerHealth : Condition
    {
        public float Value { get; set; }

        public Relation Sign { get; set; }

        public override bool IsValid
        {
            get
            {
                if (!EntityManager.LocalPlayer.IsValid)
                    return false;

                Character character = EntityManager.LocalPlayer.Character;
                if ( !character.IsValid )
                    return false;

                float HealthPercent = (character.AttribsBasic.MaxHealth > 0) ? 100 * character.AttribsBasic.Health / character.AttribsBasic.MaxHealth : 0;

                switch (Sign)
                {
                    case Relation.Equal:
                        return HealthPercent == Value;
                    case Relation.NotEqual:
                        return HealthPercent != Value;
                    case Relation.Inferior:
                        return HealthPercent < Value;
                    case Relation.Superior:
                        return HealthPercent > Value;
                    default:
                        return false;
                }
            }
        }

        public override void Reset()
        {
        }

        public override string ToString()
        {
            return $"Check if {GetType().Name} {Sign} to {Value}";
        }

        public override string TestInfos
        {
            get
            {
                if (!EntityManager.LocalPlayer.IsValid)
                    return "'LocalPlayer' not valid";

                Character character = EntityManager.LocalPlayer.Character;
                if (!character.IsValid)
                    return "'LocalPlayer.Character' not valid";

                float HealthPercent = (character.AttribsBasic.MaxHealth > 0) ? 100 * character.AttribsBasic.Health / character.AttribsBasic.MaxHealth : 0;

                return string.Format("{0} is {1:0.##} %", GetType().Name, HealthPercent);
            }
        }
    }
}
