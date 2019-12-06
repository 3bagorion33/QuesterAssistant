using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.InsigniaManager;
using static QuesterAssistant.InsigniaManager.InsigniaManagerData;

namespace QuesterAssistant.Actions
{
    public class LoadInsigniaPreset : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : {GetLabel()}";
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => new Vector3();
        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        private InsigniaManagerData iManager;

        [Description("Number of preset from list to load. Numeration starts with 1.")]
        public int PresetNumber { get; set; }

        [Description("Name of preset from list to load. Using Regex. Type 'Extract' to remove all insignias from active mounts")]
        public string PresetName { get; set; }

        protected override bool IntenalConditions
        {
            get
            {
                if (!((PresetNumber > 0) ^ (PresetName != string.Empty)))
                {
                    Logger.WriteLine(ActionLabel + ": You must determine only one of PresetNumber or PresetName!");
                    return false;
                }
                iManager = Core.InsigniaManagerCore.Data;
                if (!iManager.Presets.Any())
                {
                    Logger.WriteLine(ActionLabel + ": Preset list is empty!");
                    return false;
                }
                return true;
            }
        }
        protected override ActionValidity InternalValidity =>
            !((PresetNumber > 0) ^ !string.IsNullOrEmpty(PresetName))
                ? new ActionValidity("You must determine only one of PresetNumber or PresetName!")
                : new ActionValidity();

        private string GetLabel()
        {
            if (!string.IsNullOrEmpty(PresetName))
                return PresetName;
            if (PresetNumber > 0)
                return PresetNumber.ToString();
            return "<Empty>";
        }

        public override ActionResult Run()
        {
            if (IntenalConditions)
            {
                Preset pres;
                if (PresetNumber > 0)
                {
                    if (PresetNumber > iManager.Presets.Count)
                    {
                        Logger.WriteLine($"{ActionLabel} :\n PresetNumber is superior than preset list!");
                        return ActionResult.Skip;
                    }
                    pres = iManager.Presets.ElementAtOrDefault(PresetNumber - 1);
                    if (pres != null)
                    {
                        Logger.WriteLine($"{ActionLabel} :\n Applying preset with name '{pres.Name}'...");
                        pres.Apply();
                        return ActionResult.Completed;
                    }
                }
                if (!string.IsNullOrEmpty(PresetName))
                {
                    if (PresetName.ToLower() == "extract")
                    {
                        Logger.WriteLine($"{ActionLabel} :\n Extract all insignias to Inventory'...");
                        Insignia.ExtractFromEquippedMounts();
                        return ActionResult.Completed;
                    }
                    pres = iManager.Presets.ToList().Find(x => Regex.IsMatch(x.Name, PresetName));
                    if (pres != null)
                    {
                        Logger.WriteLine($"{ActionLabel} :\n Applying preset with name '{pres.Name}'...");
                        pres.Apply();
                        return ActionResult.Completed;
                    }
                }
                Logger.WriteLine($"{ActionLabel} :\n Unable to find a preset for these parameters, skip.");
                return ActionResult.Skip;
            }
            return ActionResult.Fail;
        }
    }
}