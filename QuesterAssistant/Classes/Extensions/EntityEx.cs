using System;
using MyNW.Classes;
using QuesterAssistant.Classes.NwInternals;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class EntityEx
    {
        public static MountCostumeDef GetMountCostume(this Entity entity) =>
            new MountCostumeDef((IntPtr) entity.CostumeRef.pMountCostume);
    }
}