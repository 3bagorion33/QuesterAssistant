using MyNW.Patchables.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuesterAssistant.Enums
{
    public enum ParagonCategory : uint
    {
        // EntityManager.LocalPlayer.Character.CurrentPowerTreeBuild.SecondaryPaths.FirstOrDefault()?.Path.PowerTree.Name
        Unknown              = 0,
        CW_Masterofflame     = (CharClassCategory.ControlWizard     * 10) + 1, // Paragon_Masterofflame
        CW_Spellstormmage    = (CharClassCategory.ControlWizard     * 10) + 2, // Paragon_Spellstormmage
        DC_Divineoracle      = (CharClassCategory.DevotedCleric     * 10) + 1, // Paragon_Divineoracle
        DC_Anointedchampion  = (CharClassCategory.DevotedCleric     * 10) + 2, // Paragon_Anointedchampion
        GW_Swordmaster       = (CharClassCategory.GreatWeaponFigher * 10) + 1, // Paragon_Swordmaster
        GW_Ironvanguard      = (CharClassCategory.GreatWeaponFigher * 10) + 2, // Paragon_Ironvanguard_Gwf
        GF_Swordmaster       = (CharClassCategory.GuardianFighter   * 10) + 1, // Paragon_Swordmaster_Gf
        GF_Ironvanguard      = (CharClassCategory.GuardianFighter   * 10) + 2, // Paragon_Ironvanguard
        HR_Stormwarden       = (CharClassCategory.HunterRanger      * 10) + 1, // Paragon_Stormwarden
        HR_Pathfinder        = (CharClassCategory.HunterRanger      * 10) + 2, // Paragon_Pathfinder
        OP_Oathofdevotion    = (CharClassCategory.OathboundPaladin  * 10) + 1, // Paragon_Oathofdevotion
        OP_Oathofprotection  = (CharClassCategory.OathboundPaladin  * 10) + 2, // Paragon_Oathofprotection
        SW_Hellbringer       = (CharClassCategory.SourgeWarlock     * 10) + 1, // Paragon_Hellbringer
        SW_Soulbinder        = (CharClassCategory.SourgeWarlock     * 10) + 2, // Paragon_Soulbinder
        TR_Whisperknife      = (CharClassCategory.TricksterRogue    * 10) + 1, // Paragon_Whisperknife
        TR_Masterinfiltrator = (CharClassCategory.TricksterRogue    * 10) + 2, // Paragon_Masterinfiltrator
    }
}
