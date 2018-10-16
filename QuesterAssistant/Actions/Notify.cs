﻿using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Professions.Functions;
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

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class Notify : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => "Notify";

        public override bool NeedToRun => true;

        public override string InternalDisplayName => string.Empty;

        public override bool UseHotSpots => false;

        protected override bool IntenalConditions => true;

        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity => new ActionValidity();

        public override void OnMapDraw(GraphicsNW graph)
        {
        }

        public override void GatherInfos()
        {
        }

        public override void InternalReset()
        {
        }

        public override ActionResult Run()
        {
            return ActionResult.Completed;
        }
    }
}
