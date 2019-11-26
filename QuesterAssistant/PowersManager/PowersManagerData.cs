using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Utils.Extensions;
using QuesterAssistant.Panels;

namespace QuesterAssistant.PowersManager
{
    [Serializable]
    public class PowersManagerData : NotifyHashChanged , IParse<PowersManagerData>
    {
        [HashInclude]
        public HotKey HotKey { get; set; } = new HotKey();
        [HashInclude]
        public List<CharClass> CharClassesList { get; set; } = new List<CharClass>();

        public PowersManagerData() { }
        public void Init()
        {
            CharClassesList.Add(new CharClass(ParagonCategory.CW_Masterofflame));
            CharClassesList.Add(new CharClass(ParagonCategory.CW_Spellstormmage));
            CharClassesList.Add(new CharClass(ParagonCategory.DC_Anointedchampion));
            CharClassesList.Add(new CharClass(ParagonCategory.DC_Divineoracle));
            CharClassesList.Add(new CharClass(ParagonCategory.GF_Ironvanguard));
            CharClassesList.Add(new CharClass(ParagonCategory.GF_Swordmaster));
            CharClassesList.Add(new CharClass(ParagonCategory.GW_Ironvanguard));
            CharClassesList.Add(new CharClass(ParagonCategory.GW_Swordmaster));
            CharClassesList.Add(new CharClass(ParagonCategory.HR_Pathfinder));
            CharClassesList.Add(new CharClass(ParagonCategory.HR_Stormwarden));
            CharClassesList.Add(new CharClass(ParagonCategory.OP_Oathofdevotion));
            CharClassesList.Add(new CharClass(ParagonCategory.OP_Oathofprotection));
            CharClassesList.Add(new CharClass(ParagonCategory.SW_Hellbringer));
            CharClassesList.Add(new CharClass(ParagonCategory.SW_Soulbinder));
            CharClassesList.Add(new CharClass(ParagonCategory.TR_Masterinfiltrator));
            CharClassesList.Add(new CharClass(ParagonCategory.TR_Whisperknife));
        }
        protected internal BindingList<Preset> ParagonPresets
        {
            get
            {
                if (EntityManager.LocalPlayer.IsValid)
                {
                    return CharClassesList?.Find(x => x.ParagonCategory == Paragon.Category)?.PresetsList ?? new BindingList<Preset>();
                }
                return new BindingList<Preset>();
            }
        }
        public void Parse(PowersManagerData source)
        {
            HotKey.Parse(source.HotKey);
            foreach (var charclass in CharClassesList)
            {
                charclass.Parse(source.CharClassesList.Find(p => p.ParagonCategory == charclass.ParagonCategory));
            }
        }

        [Serializable]
        public class CharClass : OverrideHash, IParse<CharClass>
        {
            [XmlAttribute, HashInclude]
            public ParagonCategory ParagonCategory { get; set; }
            [HashInclude]
            public BindingList<Preset> PresetsList { get; set; } = new BindingList<Preset>();

            public CharClass() { }
            public CharClass(ParagonCategory charClass)
            {
                ParagonCategory = charClass;
            }
            public void Parse(CharClass source)
            {
                ParagonCategory = source.ParagonCategory;
                PresetsList.Clear();
                source.PresetsList.ForEach(p => PresetsList.Add(p));
            }
            public void Init() { }
        }

        [Serializable]
        public class Preset : OverrideHash, IParse<Preset>, IListControlSource
        {
            [XmlAttribute, HashInclude]
            public string Name { get; set; }
            [HashInclude]
            public HotKey HotKey { get; set; } = new HotKey();
            [HashInclude]
            public List<Power> PowersList { get; set; } = new List<Power>();

            public Preset() { }
            public Preset(string name)
            {
                Name = name;
                PowersList.Add(new Power(TraySlot.AtWill1, string.Empty));
                PowersList.Add(new Power(TraySlot.AtWill2, string.Empty));
                PowersList.Add(new Power(TraySlot.ClassFeature1, string.Empty));
                PowersList.Add(new Power(TraySlot.ClassFeature2, string.Empty));
                PowersList.Add(new Power(TraySlot.Daily1, string.Empty));
                PowersList.Add(new Power(TraySlot.Daily2, string.Empty));
                PowersList.Add(new Power(TraySlot.Encounter1, string.Empty));
                PowersList.Add(new Power(TraySlot.Encounter2, string.Empty));
                PowersList.Add(new Power(TraySlot.Encounter3, string.Empty));
                PowersList.Add(new Power(TraySlot.Mechanic, string.Empty));
            }
            public Preset(string name, List<Power> powers)
            {
                Name = name;
                PowersList = powers;
                HotKey.Enabled = true;
            }
            public override string ToString()
            {
                return Name;
            }
            public void Parse(Preset source)
            {
                Name = source.Name;
                HotKey.Parse(source.HotKey);
                PowersList.Clear();
                source.PowersList.ForEach(p => PowersList.Add(p));
            }
            public void Init() { }
        }

        [Serializable]
        public class Power : OverrideHash, IParse<Power>
        {
            [XmlText]
            public string InternalName { get; set; }
            [XmlAttribute, HashInclude]
            public TraySlot TraySlot { get; set; }
            [HashInclude]
            private string DispName => ToDispName().InternalName;

            public Power() { }
            public Power(TraySlot traySlot, string iName)
            {
                TraySlot = traySlot;
                InternalName = iName;
            }
            internal Power ToDispName()
            {
                var pwr = Astral.Logic.NW.Powers.GetPowerByInternalName(InternalName);
                if (!pwr.IsValid) return new Power(TraySlot, "Unknown spell");
                return new Power(TraySlot, (pwr.IsInTray ? "[Slotted] " : "") + pwr.PowerDef.DisplayName);
            }
            public void Parse(Power source)
            {
                InternalName = source.InternalName;
                TraySlot = source.TraySlot;
            }
            public void Init() { }
        }
    }
}
