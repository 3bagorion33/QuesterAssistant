using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using QuesterAssistant.UIEditors.Forms;
using ItemIdEditor = QuesterAssistant.UIEditors.ItemIdEditor;

namespace QuesterAssistant.Actions
{
    public class UseItemExt : Action
    {
        public override string ActionLabel => $"{GetType().Name} : {ItemId}";
        public override string Category => Core.Category;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        public override bool NeedToRun => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void OnMapDraw(GraphicsNW graph) { }
        public override void InternalReset() { }

        protected override bool IntenalConditions => 
            ItemId.Length > 0 && EntityManager.LocalPlayer.GetItemCountByInternalName(ItemId) > 0;

        protected override ActionValidity InternalValidity => 
            ItemId.Length == 0 ? new ActionValidity("Invalid item id.") : new ActionValidity();

        public override void GatherInfos() =>
            ItemId = GetAnItem.Show().ItemId;

        public override ActionResult Run()
        {
            List<InventorySlot> bagsItems = EntityManager.LocalPlayer.BagsItems;
            bagsItems.AddRange(EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.Potions).GetItems);
            foreach (InventorySlot inventorySlot in bagsItems)
            {
                if (inventorySlot.Item.ItemDef.InternalName == ItemId)
                {
                    Logger.WriteLine("Use " + inventorySlot.Item.ItemDef.DisplayName + "...");
                    if (inventorySlot.BagId != InvBagIDs.Potions && inventorySlot.Item.ItemDef.Type != ItemType.Device)
                        inventorySlot.Equip();
                    else
                        inventorySlot.Exec();

                    Pause.Sleep(UseTimeMs);

                    if (Dialogs.Count > 0)
                    {
                        Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(5000);
                        while (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Options.Count == 0)
                        {
                            if (timeout.IsTimedOut)
                                return ActionResult.Fail;
                            Pause.Sleep(100);
                        }
                        Pause.Sleep(500);

                        foreach (var dialog in Dialogs)
                        {
                            EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.SelectOptionByKey(dialog);
                            Pause.Sleep(1000);
                        }
                    }
                    return ActionResult.Completed;
                }
            }
            return ActionResult.Fail;
        }

        [Editor(typeof(ItemIdEditor), typeof(UITypeEditor))]
        public string ItemId { get; set; } = string.Empty;
        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        public List<string> Dialogs { get; set; } = new List<string>();
        public int UseTimeMs { get; set; } = 2000;
    }
}