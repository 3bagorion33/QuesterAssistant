using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Astral.Logic.Classes.FSM;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Classes.Patches
{
    internal class WayPointFilter
    {
        private const string BOAT_AURA_S = "Becritter_Boat_Costume";
        private const string GROUND_AURA_S = "Volume_Ground_Slippery";
        private static uint pBoatAura;
        private static uint pGroundAura;
        private IntPtr gameHandle = IntPtr.Zero;

        private static readonly Timer timer = new Timer {AutoReset = true, Interval = 200, Enabled = true};
        private static double cachedWPD = Astral.API.CurrentSettings.ChangeWaypointDist;

        public WayPointFilter()
        {
            timer.Elapsed += UpdateCachedWPD;
        }

        private static void UpdateCachedWPD(object sender, ElapsedEventArgs e)
        {
            if (Astral.Quester.API.Engine.Navigation.IsRunning)
            {
                cachedWPD = Task.Factory.StartNew(GetChangeWPDist).Result;
            }
        }

        public void Run()
        {
            if (Core.GameWindowHandle != gameHandle)
            {
                gameHandle = Core.GameWindowHandle;
                pBoatAura = pGroundAura = 0;
            }
            var methodToReplace = typeof(Navigation)
                .GetProperty("ChangeWPDist", BindingFlags.Instance | BindingFlags.NonPublic).GetMethod;
            var methodToInject = GetType()
                .GetProperty(nameof(Astral_Logic_Classes_FSM_Navigation_ChangeWPDist),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetMethod;
            new Patch(methodToReplace, methodToInject).Inject();
        }

        private static double GetChangeWPDist()
        {
            if (!EntityManager.LocalPlayer.IsMounted)
                return Astral.API.CurrentSettings.ChangeWaypointDist;
            if (pBoatAura == 0)
                pBoatAura = EntityManager.LocalPlayer.Character.Mods
                    .Find(m => m.PowerDef.InternalName.Contains(BOAT_AURA_S))?.pPowerDef ?? 0;
            if (EntityManager.LocalPlayer.Character.HasAura(pBoatAura))
                return 90.0;
            if (pGroundAura == 0)
                pGroundAura = EntityManager.LocalPlayer.Character.Mods
                    .Find(m => m.PowerDef.InternalName.Contains(GROUND_AURA_S))?.pPowerDef ?? 0;
            if (EntityManager.LocalPlayer.Character.HasAura(pGroundAura))
                return 50.0;
            return Astral.API.CurrentSettings.MountedChangeWPDist;
        }

        private static double Astral_Logic_Classes_FSM_Navigation_ChangeWPDist =>
            (double)
            MathTools.Min(
                MathTools.Max(Astral.Quester.API.Engine.Navigation.LastWaypoint.Distance2DFromPlayer, 3.0),
                cachedWPD);
    }
}