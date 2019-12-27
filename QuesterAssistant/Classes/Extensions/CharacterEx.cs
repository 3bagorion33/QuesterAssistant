using MyNW.Classes;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class CharacterEx
    {
        public static bool HasAura(this Character @this, string auraInternalName) => 
            !string.IsNullOrEmpty(auraInternalName) && @this.Mods.Exists(m => m.PowerDef.InternalName.Contains(auraInternalName));

        public static bool HasAura(this Character @this, uint pPowerDef) => 
            pPowerDef > 0 && @this.Mods.Exists(m => m.pPowerDef == pPowerDef);
    }
}