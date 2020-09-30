using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Classes.GroupProject;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.UIEditors;
using Action = Astral.Quester.Classes.Action;
using API = Astral.Quester.API;

namespace QuesterAssistant.Actions
{
    public class DonateToStronghold : Action
    {
        public override string ActionLabel => $"{GetType().Name} : {Coffer}";
        public override string Category => Core.Category;

        public override bool NeedToRun =>
            !IntenalConditions || InternalDestination.Distance3DFromPlayer < 35;

        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;

        protected override bool IntenalConditions
        {
            get
            {
                if (string.IsNullOrEmpty(Coffer.InternalName) || !Coffer.Items.Any())
                {
                    Logger.WriteLine("Coffer doesn't contain any items or is wrong !");
                    return false;
                }

                if (!Coffer.Items.Any(i => i.InBags > 0))
                {
                    Logger.WriteLine("Character hasn't any items to donate, skip ...");
                    return false;
                }

                if (Player.PlayerGuild.GuildID == 0)
                {
                    Logger.WriteLine("Character hasn't a guild !");
                    return false;
                }

                if (!IgnoreGuildMarksLimit && GuildMark.Count == GUILD_MARK_LIMIT)
                {
                    Logger.WriteLine($"Character has {GuildMark.Count} of '{GuildMark.ItemDef.DisplayName}'");
                    return false;
                }

                if (!IsStrongholdMap)
                {
                    Logger.WriteLine("This is no StrongHold map !");
                    return false;
                }
                return true;
            }
        }

        protected override Vector3 InternalDestination =>
            IsStrongholdMap ? new Vector3(1901.699f, 3969.451f, 42.43262f) : new Vector3();
        protected override ActionValidity InternalValidity
        {
            get
            {
                if (string.IsNullOrEmpty(Coffer.InternalName))
                {
                    return new ActionValidity($"You must select '{nameof(Coffer)}' to donate.");
                }
                return new ActionValidity();
            }
        }

        private static Player Player => EntityManager.LocalPlayer.Player;
        private static List<InventorySlotLite> NumericList =>
            EntityManager.LocalPlayer.Inventory.NumericsBagLite.GetItems;
        private static List<InventorySlot> SlotList
        {
            get
            {
                var bags = EntityManager.LocalPlayer.BagsItems;
                bags.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.CraftingResources).GetItems);
                return bags;
            }
        }

        private static bool IsStrongholdMap => EntityManager.LocalPlayer.MapState.MapName == "Sh_Pve";
        private static Entity CofferEntity =>
            EntityManager.GetEntities()
                .Find(e => e.NameUntranslated ==
                           "Maps_Stronghold_Sh_Pve_Content_N0.Encounter_Coffer_Actor_1_Displayname.Encounter_Coffer") ??
            new Entity(IntPtr.Zero);

        private static InventorySlotLite GuildMark =>
            NumericList.Find(s => s.Name == "Stronghold_Currency_Guild_Mark") ??
            new InventorySlotLite(IntPtr.Zero);
        private const int GUILD_MARK_LIMIT = 30000;

        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public override ActionResult Run()
        {
            bool Interact(Entity entity, ScreenType screenType)
            {
                if (!Approach.EntityForInteraction(entity)) return false;

                API.Engine.Navigation.Stop();
                entity.Interact();
                var timeout = new Astral.Classes.Timeout(6000);
                while (Player.InteractInfo.ContactDialog.ScreenType != screenType)
                {
                    if (timeout.IsTimedOut)
                    {
                        Logger.WriteLine("Interaction fail !");
                        return false;
                    }
                    Pause.Sleep(250);
                }
                return true;
            }

            if (!Interact(CofferEntity, ScreenType.GarrisonCoffer)) return ActionResult.Fail;

            Pause.Sleep(1000);

            GroupProjectState shCoffer = Player.PlayerGuild.GroupProjectContainer.ProjectList
                .Find(p => p.ProjectDef.Name == "Nw_Stronghold");
            GroupProjectCofferNumericData cofferData = shCoffer?.CofferNumericData.Find(d => d.CofferNumericDef.Name == Coffer.InternalName);

            if (cofferData != null)
            {
                var pause = new Pause(InteractionTimeOut);
                foreach (var item in Coffer.Items)
                {
                    uint count;
                    uint toDonate = (uint) item.Donate;
                    var cItem = cofferData.CofferNumericDef.ItemConversion
                        .Find(i => i.Item.InternalName == item.InternalName);
                    uint countByGuildMarksLimit = IgnoreGuildMarksLimit
                            ? uint.MaxValue
                            : (uint) (GUILD_MARK_LIMIT - GuildMark.Count) * cItem.BatchSize;

                    uint countByMaxValueLimit = uint.MaxValue; // (uint) shCoffer.CofferNumericGetMaxValue(cofferData) /
                                                //cItem.ValuePerBatch * cItem.BatchSize;
                    
                    if (item.Type == ItemType.Numeric)
                    {
                        var slotNum = NumericList.Find(s => s.Name == item.InternalName);
                        if (slotNum == null) continue;
                        count = MathTools.Min(toDonate, (uint) item.InBags, countByGuildMarksLimit, countByMaxValueLimit);
                        if (count == 0) continue;
                        pause.WaitingRandom();
                        shCoffer.DonateToCoffer(cofferData, slotNum, count);
                        pause.Reset();
                    }
                    else
                    {
                        InventorySlot slotItem;
                        while (toDonate > 0 && (slotItem = SlotList.LastOrDefault(s => s.Item.ItemDef.InternalName == item.InternalName)) != null)
                        {
                            if (string.IsNullOrEmpty(slotItem.Item.ItemDef.InternalName)) break;
                            count = MathTools.Min(toDonate, slotItem.Item.Count, countByGuildMarksLimit, countByMaxValueLimit);
                            if (count == 0) break;
                            pause.WaitingRandom();
                            shCoffer.DonateToCoffer(cofferData, slotItem, count);
                            pause.Reset();
                            toDonate -= count;
                        }
                    }
                }
                return ActionResult.Completed;
            }
            return ActionResult.Fail;
        }

        [Editor(typeof(DonateTaskEditor), typeof(UITypeEditor))]
        [Description("")]
        public Coffer Coffer { get; set; } = new Coffer();

        [Description("Limit of Guild marks is 30000")]
        public bool IgnoreGuildMarksLimit { get; set; }

        [TypeConverter(typeof(PropertySorter))]
        public MinMaxPair<uint> InteractionTimeOut { get; set; } = new MinMaxPair<uint>(2000, 3000);
    }
}
