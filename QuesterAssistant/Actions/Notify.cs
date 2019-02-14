using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using System;
using System.ComponentModel;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class Notify : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();

        [Description("Text of message")]
        public string Message { get; set; }

        [Description("Enable or disable Alert")]
        public AStat Type { get; set; }

        public enum AStat
        {
            Information,
            Alert
        }

        public override void OnMapDraw(GraphicsNW graph) {}
        public override void GatherInfos() {}
        public override void InternalReset() {}

        public override ActionResult Run()
        {
            Logger.Notify(this.Message, (this.Type == AStat.Alert)? true : false);
            return ActionResult.Completed;
        }
    }
}
