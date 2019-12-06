using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using DevExpress.Utils.Extensions;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Panels;

namespace QuesterAssistant.InsigniaManager
{
    public class InsigniaManagerData : NotifyHashChanged, IParse<InsigniaManagerData>
    {
        [XmlElement(nameof(Preset))]
        [HashInclude]
        public BindingList<Preset> Presets { get; set; } = new BindingList<Preset>();

        public void Parse(InsigniaManagerData source)
        {
            Presets.Clear();
            source.Presets.ForEach(p => Presets.Add(p));
        }

        public void Init() { }

        public class Preset : IListControlSource
        {
            [XmlElement("Mount")]
            public List<MountDef> MountSlots = new List<MountDef>();
            [XmlAttribute]
            public string Name { get; set; } = string.Empty;
            [XmlIgnore]
            public bool IsValid => MountSlots.Any();
            public override string ToString() => Name;
            public override int GetHashCode() => MountSlots.Count;

            public void GetMounts()
            {
                if (EntityManager.LocalPlayer.IsValid)
                {
                    MountSlots = Insignia.EquippedMounts
                        .Where(s => s.Item.GetCurrentInsigniaBonusType() > 0)
                        .Select(mount => new MountDef
                        {
                            DisplayName = mount.Item.DisplayName,
                            InternalName = mount.Item.ItemDef.InternalName,
                            Bonus = mount.Item.GetCurrentInsigniaBonusType(),
                            InsigniaSlots = mount.Item.SpecialProps.ItemGemSlots.Select(gem => new InsigniaDef
                            {
                                InternalName = gem.SlottedItem.InternalName,
                                DisplayName =
                                    gem.SlottedItem.IsValid
                                        ? $"[R{(int) gem.SlottedItem.Quality - 1}] {gem.SlottedItem.DisplayName}"
                                        : string.Empty,
                                Type = (Insignia.Type) gem.SlottedItem.GemType
                            }).ToList()
                        }).ToList();
                }
            }

            public void Apply()
            {
                if (IsValid)
                {
                    foreach (var mount in MountSlots)
                    {
                        var slottedMount =
                            Insignia.EquippedMounts.Find(s => s.Item.ItemDef.InternalName == mount.InternalName);
                        var existedInsignias = EntityManager.LocalPlayer.BagsItems.FindAll(s =>
                            mount.InsigniaSlots.Exists(i => i.InternalName == s.Item.ItemDef.InternalName));
                        if (slottedMount != null && existedInsignias.Any())
                        {
                            var existedMountInsigniaSlots = slottedMount.Item.ItemDef.EffectiveItemGemSlots;
                            existedMountInsigniaSlots.ForEach(gs =>
                            {
                                var idx = existedMountInsigniaSlots.IndexOf(gs);
                                slottedMount.Item.GemThisItem(existedInsignias.Find(@is =>
                                        @is.Item.ItemDef.InternalName == mount.InsigniaSlots[idx].InternalName).Item,
                                    (uint)idx);
                                Pause.Sleep(200);
                            });
                        }
                    }
                }
            }
        }

        public class MountDef
        {
            public string DisplayName { get; set; }
            [Browsable(false)]
            public string InternalName { get; set; }
            public Insignia.BonusType Bonus { get; set; } = Insignia.BonusType.Unknown;
            [XmlElement("Insignia")]
            public List<InsigniaDef> InsigniaSlots { get; set; } = new List<InsigniaDef>();
        }

        public class InsigniaDef
        {
            public string DisplayName { get; set; }
            [Browsable(false)]
            public string InternalName { get; set; }
            public Insignia.Type Type { get; set; } = Insignia.Type.Unknown;
        }
    }
}