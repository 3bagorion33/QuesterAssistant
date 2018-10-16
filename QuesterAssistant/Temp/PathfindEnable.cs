using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using System;

namespace QuesterAssistant
{
    public class PathfindEnable : Astral.Quester.Classes.Action
    {
        // Methods
        public override void GatherInfos()
        {
            //Logger.WriteLine("GatherInfos");
        }

        public override void InternalReset()
        {
            //Logger.WriteLine("GatherInfos");
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
        }

        public override ActionResult Run()
        {
            API.CurrentSettings.UsePathfinding3 = true;
            return ActionResult.Completed;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "PathfindEnable";
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
                return "PathfindEnable";
            }
        }

        protected override ActionValidity InternalValidity
        {
            get
            {
                return new ActionValidity();
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
    }
}

