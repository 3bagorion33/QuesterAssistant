using Astral;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using DevExpress.XtraEditors;
using MyNW.Classes;
using System;

namespace QuesterAssistant.Actions.Deprecated
{
    [Serializable]
    public class PathfindDisable : Astral.Quester.Classes.Action
    {
        // Properties
        public override string ActionLabel => "PathfindDisable";
        public override string Category => "Deprecated";

        protected override bool IntenalConditions
        {
            get
            {
                Logger.WriteLine(Core.DeprecatedWriteLine(this.ActionLabel));
                //Roles.ToggleRole(false);
                return false;
            }
        }

        protected override Vector3 InternalDestination => new Vector3();
        public override string InternalDisplayName => string.Empty;

        protected override ActionValidity InternalValidity
        {
            get
            {
                return new ActionValidity(Core.DeprecatedMessage(this.ActionLabel, "PathFinding: Disable"));
            }
        }

        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

        // Methods
        public override void GatherInfos()
        {
            XtraMessageBox.Show(Core.DeprecatedMessage(this.ActionLabel, "PathFinding: Disable"));
        }

        public override void InternalReset() { }

        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            return ActionResult.Running;
        }
    }
}

