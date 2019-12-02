using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Actions.Deprecated
{
    public class PathFinding : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name + Value;
        public override string Category => "Deprecated";
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
            new ActionValidity(Debug.DeprecatedMessage(ActionLabel, "ChangeSettingValue"));
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;

        public PStat Value { get; set; }

        public enum PStat
        {
            Enabled,
            Disabled
        }

        public override void GatherInfos()
        {
            QMessageBox.ShowError(Debug.DeprecatedMessage(ActionLabel, "ChangeSettingValue"));
        }
        public override void InternalReset() {}
        public override void OnMapDraw(GraphicsNW graph) {}

        public PathFinding()
        {
            Value = PStat.Enabled;
        }

        public override ActionResult Run()
        {
            return ActionResult.Running;
        }
    }
}

