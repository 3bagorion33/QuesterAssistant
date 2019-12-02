using System;
using System.ComponentModel;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes.Common;
using API = Astral.API;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class ChangeSettingValue : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Property} => {Value}";
        public override string Category => Core.Category;
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
            Loot,
            AutoEquip,
            AutoUCC
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
                    //Astral.Controllers.Settings.Save();
                    //typeof(Astral.Controllers.Settings).ExecStaticMethod("CommitChanges");
                    break;
                case SProperty.AutoEquip:
                    API.CurrentSettings.EnableAutoEquip = Value;
                    break;
                //case SProperty.AutoUCC:
                //    var customClass = Value ? "AutoUCC" : "UCC";
                //    ReflectionHelper.GetTypeByName("Astral", "CustomClasses")
                //        ?.ExecStaticMethod("SelectCC", new object[] {customClass, true});
                //    Astral.Controllers.Settings.Save();
                //    Astral.Logic.UCC.Entrypoint2.ForceRefresh();
                //    break;
                default:
                    return ActionResult.Fail;
            }
            return ActionResult.Completed;
        }
    }
}
