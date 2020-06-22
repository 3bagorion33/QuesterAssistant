﻿using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using System;
using QuesterAssistant.Classes;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Actions.Deprecated
{
    [Serializable]
    public class PathfindDisable : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Deprecated;

        protected override bool IntenalConditions
        {
            get
            {
                Logger.WriteLine(Debug.DeprecatedWriteLine(ActionLabel));
                return false;
            }
        }

        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => string.Empty;

        protected override ActionValidity InternalValidity =>
            new ActionValidity(Debug.DeprecatedMessage(ActionLabel, "PathFinding: Disable"));

        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

        public override void GatherInfos()
        {
            QMessageBox.ShowInfo(Debug.DeprecatedMessage(ActionLabel, "PathFinding: Disable"));
        }

        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            return ActionResult.Running;
        }
    }
}

