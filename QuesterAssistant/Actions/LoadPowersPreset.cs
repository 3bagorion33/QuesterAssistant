using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.PowersManager;
using static QuesterAssistant.PowersManager.PowersManagerData;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class LoadPowersPreset : Astral.Quester.Classes.Action
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

        private PowersManagerData pManager;

        [Description("Number of preset from list to load. Numeration starts with 1.")]
        public int PresetNumber { get; set; } 

        [Description("Name of preset from list to load. Using Regex.")]
        public string PresetName { get; set; }

        protected override bool IntenalConditions
        {
            get
            {
                if (!Paragon.IsValid)
                {
                    Logger.WriteLine(ActionLabel + ": This character haven't a valid paragon!");
                    return false;
                }

                if (!((PresetNumber > 0) ^ (PresetName != string.Empty)))
                {
                    Logger.WriteLine(ActionLabel + ": You must determine only one of PresetNumber or PresetName!");
                    return false;
                }

                pManager = Core.PowersManagerCore.Data;

                if (!pManager.ParagonPresets.Any())
                {
                    Logger.WriteLine(ActionLabel + ": Preset list is empty for this paragon!");
                    return false;
                }

                return true;
            }
        }
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (!((PresetNumber > 0) ^ (PresetName != string.Empty)))
                {
                    return new ActionValidity("You must determine only one of PresetNumber or PresetName!");
                }
                return new ActionValidity();
            }
        }

        private string GetLabel()
        {
            if (!string.IsNullOrEmpty(PresetName))
            {
                return PresetName;
            }

            if (PresetNumber > 0)
            {
                return PresetNumber.ToString();
            }
            return "<Empty>";
        }

        public override ActionResult Run()
        {
            if (IntenalConditions)
            {
                Preset _pres;
                if (PresetNumber > 0)
                {
                    if (PresetNumber > pManager.ParagonPresets.Count)
                    {
                        Logger.WriteLine($"{ActionLabel} : PresetNumber is superior than preset list for this paragon!");
                        return ActionResult.Skip;
                    }
                    _pres = pManager.ParagonPresets.ElementAtOrDefault(PresetNumber - 1);
                    if (_pres != null)
                    {
                        Logger.WriteLine($"{ActionLabel} : Applying preset with name '{_pres.Name}'...");
                        Powers.ApplyPowers(_pres?.PowersList);
                        return ActionResult.Completed;
                    }
                }
                if (PresetName.Any())
                {
                    _pres = pManager.ParagonPresets.ToList().Find(x => Regex.IsMatch(x.Name, PresetName));
                    if (_pres != null)
                    {
                        Logger.WriteLine($"{ActionLabel} : Applying preset with name '{_pres.Name}'...");
                        Powers.ApplyPowers(_pres?.PowersList);
                        return ActionResult.Completed;
                    }
                }
                Logger.WriteLine($"{ActionLabel} : Unable to find a preset for these parameters, skip.");
                return ActionResult.Skip;
            }
            return ActionResult.Fail;
        }
    }
}
