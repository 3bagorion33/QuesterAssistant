using System.ComponentModel;
using System.Linq;
using Astral;
using Astral.Logic.UCC.Classes;
using Astral.Quester.Classes;
using QuesterAssistant.Actions;
using QuesterAssistant.Classes;

namespace QuesterAssistant.UCCActions
{
    public class UCCLoadPowersPreset : UCCAction
    {
        private readonly LoadPowersPreset action = new LoadPowersPreset();
        private int presetNumber;
        private string presetName;
        private static int prevHash;

        [Category("Required")]
        [Description("Number of preset from list to load. Numeration starts with 1.")]
        public int PresetNumber
        {
            get => presetNumber;
            set
            {
                action.PresetNumber = value;
                presetNumber = value;
                prevHash = 0;
            }
        }

        [Category("Required")]
        [Description("Name of preset from list to load. Using Regex.")]
        public string PresetName
        {
            get => presetName;
            set
            {
                action.PresetName = value;
                presetName = value;
                prevHash = 0;
            }
        }

        public override bool NeedToRun
        {
            get
            {
                if (!Paragon.IsValid)
                {
                    Logger.WriteLine(Label + ": This character haven't a valid paragon!");
                    return false;
                }

                if (!((PresetNumber > 0) ^ (PresetName != string.Empty)))
                {
                    Logger.WriteLine(Label + ": You must determine only one of PresetNumber or PresetName!");
                    return false;
                }

                if (!Core.PowersManagerCore.Data.ParagonPresets.Any())
                {
                    Logger.WriteLine(Label + ": Preset list is empty for this paragon!");
                    return false;
                }

                return true;
            }
        }

        public override bool Run()
        {
            var hash = GetHashCode();
            if (hash != prevHash)
            {
                prevHash = hash;
                action.PresetName = PresetName;
                action.PresetNumber = PresetNumber;
                return action.Run() == Action.ActionResult.Completed;
            }
            return false;
        }

        public override UCCAction Clone() => BaseClone(new UCCLoadPowersPreset
            {PresetNumber = PresetNumber, PresetName = PresetName});

        public override string ToString() =>
            $"UCC{action.ActionLabel}";
    }
}