﻿using MyNW.Internals;
using QuesterAssistant.Enums;
using System.Linq;

namespace QuesterAssistant.Classes
{
    internal class Paragon
    {
        public static ParagonCategory Category
        {
            get
            {
                switch (EntityManager.LocalPlayer.Character.CurrentPowerTreeBuild.SecondaryPaths.FirstOrDefault()?.Path.PowerTree.Name)
                {
                    case "Paragon_Masterofflame":
                        return ParagonCategory.CW_Masterofflame;

                    case "Paragon_Spellstormmage":
                        return ParagonCategory.CW_Spellstormmage;

                    case "Paragon_Divineoracle":
                        return ParagonCategory.DC_Divineoracle;

                    case "Paragon_Anointedchampion":
                        return ParagonCategory.DC_Anointedchampion;

                    case "Paragon_Swordmaster":
                        return ParagonCategory.GW_Swordmaster;

                    case "Paragon_Ironvanguard_Gwf":
                        return ParagonCategory.GW_Ironvanguard;

                    case "Paragon_Swordmaster_Gf":
                        return ParagonCategory.GF_Swordmaster;

                    case "Paragon_Ironvanguard":
                        return ParagonCategory.GF_Ironvanguard;

                    case "Paragon_Stormwarden":
                        return ParagonCategory.HR_Stormwarden;

                    case "Paragon_Pathfinder":
                        return ParagonCategory.HR_Pathfinder;

                    case "Paragon_Oathofdevotion":
                        return ParagonCategory.OP_Oathofdevotion;

                    case "Paragon_Oathofprotection":
                        return ParagonCategory.OP_Oathofprotection;

                    case "Paragon_Hellbringer":
                        return ParagonCategory.SW_Hellbringer;

                    case "Paragon_Soulbinder":
                        return ParagonCategory.SW_Soulbinder;

                    case "Paragon_Whisperknife":
                        return ParagonCategory.TR_Whisperknife;

                    case "Paragon_Masterinfiltrator":
                        return ParagonCategory.TR_Masterinfiltrator;

                    default:
                        return ParagonCategory.Unknown;
                }
            }
        }

        public static string DisplayName =>
            EntityManager.LocalPlayer.Character.CurrentPowerTreeBuild.SecondaryPaths.FirstOrDefault()?.Path.DisplayName ?? string.Empty;

        public static bool IsValid =>
            Category != ParagonCategory.Unknown;
    }
}
