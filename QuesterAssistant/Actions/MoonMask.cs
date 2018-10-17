﻿using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace QuesterAssistant
{
    [Serializable]
    public class MoonMask : Astral.Quester.Classes.Action
    {
        // Properties
        public override string ActionLabel => "MoonMask";

        public override bool NeedToRun => true;

        public override string InternalDisplayName => "MoonMask";

        public override bool UseHotSpots => false;

        protected override bool IntenalConditions
        {
            get
            {
                if ((VIP.Rank >= 2) & (VIP.ExpirationSecondsLeft > 0)) return true;
                Logger.WriteLine("Character haven't necessary VIP Rank or VIP has been expired");
                return false;
            }
        }

        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity => new ActionValidity();

        // Methods
        public override void GatherInfos() {}

        public override void InternalReset() {}

        public override void OnMapDraw(GraphicsNW graph) {}

        public override ActionResult Run()
        {
            if (IntenalConditions)
            {
                VIP.TeleportToMoonstoneMask();
                Thread.Sleep(3000);
                while (EntityManager.LocalPlayer.IsLoading)
                    Thread.Sleep(500);
                return ActionResult.Completed;
            }
            else
            {
                return ActionResult.Fail;
            }
        }
    }
}