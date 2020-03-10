using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Astral.Logic.Classes.FSM;
using MyNW.Internals;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Classes.NwInternals;

namespace QuesterAssistant.Classes.Patches
{
    internal class WayPointFilter
    {
        private const string GROUND_AURA = "Volume_Ground_Slippery";
        private static uint pGroundAura;

        private static readonly Timer timer = new Timer {AutoReset = true, Interval = 1000, Enabled = true};
        private static double cachedWPD = Astral.API.CurrentSettings.ChangeWaypointDist;

        public WayPointFilter()
        {
            timer.Elapsed += UpdateCachedWPD;
            GameClient.Monitor.OnNew += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            pGroundAura = 0;
        }

        private static void UpdateCachedWPD(object sender, ElapsedEventArgs e)
        {
            if (Astral.Quester.API.Engine.Navigation.IsRunning)
                cachedWPD = Task.Factory.StartNew(GetChangeWPDist).Result;
        }

        public void Run()
        {
            var methodToReplace = typeof(Navigation)
                .GetProperty("ChangeWPDist", BindingFlags.Instance | BindingFlags.NonPublic).GetMethod;
            var methodToInject = GetType()
                .GetProperty(nameof(Astral_Logic_Classes_FSM_Navigation_ChangeWPDist),
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetMethod;
            new PatchMethod(methodToReplace, methodToInject).Inject();
        }

        private static double GetChangeWPDist()
        {
            if (!EntityManager.LocalPlayer.IsMounted)
                return Astral.API.CurrentSettings.ChangeWaypointDist;

            switch (EntityManager.LocalPlayer.GetMountCostume().Type)
            {
                case MountCostumeDef.MountType.BoatWhite:
                    return 100;
                case MountCostumeDef.MountType.BoatGreen:
                    return 90;
                case MountCostumeDef.MountType.BoatPurple:
                    return 80;
            }
            if (pGroundAura == 0)
                pGroundAura = EntityManager.LocalPlayer.Character.Mods
                    .Find(m => m.PowerDef.InternalName.Contains(GROUND_AURA))?.pPowerDef ?? 0;
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