using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using MyNW.Internals;
using System;

namespace QuesterAssistant
{
    public class RefineAD : Astral.Quester.Classes.Action
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
            EntityManager.LocalPlayer.RefineAstralDiamonds();
            return ActionResult.Completed;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "RefineAD";
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
                return "RefineAD";
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

