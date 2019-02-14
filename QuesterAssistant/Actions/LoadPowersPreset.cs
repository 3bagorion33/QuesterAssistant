using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.PowersManager;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class LoadPowersPreset : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
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

                pManager = new PowersManagerData();
                if (!pManager.LoadSettings())
                {
                    Logger.WriteLine(ActionLabel + ": Unable to read preset file!");
                    return false;
                }

                if (!pManager.CurrPresets.Any())
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

        public override ActionResult Run()
        {
            if (IntenalConditions)
            {
                Preset _pres;
                if (PresetNumber > 0)
                {
                    if (PresetNumber > pManager.CurrPresets.Capacity)
                    {
                        Logger.WriteLine(ActionLabel + ": PresetNumber is superior than preset list for this paragon!");
                        return ActionResult.Skip;
                    }
                    _pres = pManager.CurrPresets.ElementAtOrDefault(PresetNumber - 1);
                    if (_pres != null)
                    {
                        Logger.WriteLine(ActionLabel + ": Applying preset with name '" + _pres.Name + "'...");
                        Powers.ApplyPowers(_pres?.PowersList);
                        return ActionResult.Completed;
                    }
                }
                if (PresetName.Any())
                {
                    _pres = pManager.CurrPresets.Find(x => Regex.IsMatch(x.Name, PresetName));
                    if (_pres != null)
                    {
                        Logger.WriteLine(ActionLabel + ": Applying preset with name '" + _pres.Name + "'...");
                        Powers.ApplyPowers(_pres?.PowersList);
                        return ActionResult.Completed;
                    }
                }
                Logger.WriteLine(ActionLabel + ": Unable to find a preset for these parameters, skip.");
                return ActionResult.Skip;
            }
            return ActionResult.Fail;
        }
    }
}
