using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class ChangeSettingValue : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name + " : " + Property + " => " + Value;
        public override string Category => GetType().Namespace.Split(char.Parse("."))[0];
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        protected override bool IntenalConditions => true;
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public enum SProperty
        {
            PathFinding,
            Loot
        }

        [Description("Select property to change")]
        public SProperty Property { get; set; }

        [Description("Set value")]
        public bool Value { get; set; }

        public override ActionResult Run()
        {
            switch (Property)
            {
                case SProperty.PathFinding:
                    API.CurrentSettings.UsePathfinding3 = Value;
                    break;
                case SProperty.Loot:
                    API.CurrentSettings.Loot = Value;
                    break;
                default:
                    return ActionResult.Fail;
            }
            return ActionResult.Completed;
        }
    }
}
