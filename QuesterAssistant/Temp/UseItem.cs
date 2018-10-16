namespace Astral.Quester.Classes.Actions
{
    using Astral;
    using Astral.Classes;
    using Astral.Logic.Classes.Map;
    using Astral.Logic.NW;
    using Astral.Quester.Classes;
    using Astral.Quester.Forms;
    using Astral.Quester.UIEditors;
    using MyNW.Classes;
    using MyNW.Internals;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class UseItem : Action
    {
        // Methods
        public UseItem()
        {
            this.ItemId = string.Empty;
            this.Dialogs = new List<string>();
            this.UseTimeMs = 0x7d0;
        }

        public override void GatherInfos()
        {
            this.ItemId = GetAnItem.Show(0).ItemId;
        }

        public override void InternalReset()
        {
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
        }

        public override Action.ActionResult Run()
        {
            List<InventorySlot> list1 = EntityManager.get_LocalPlayer().get_BagsItems();
            list1.AddRange(EntityManager.get_LocalPlayer().GetInventoryBagById(0x4a).get_GetItems());
            List<InventorySlot>.Enumerator enumerator = list1.GetEnumerator();
            goto Label_01C8;
            try
            {
                InventorySlot slot;
                Label_0031:
                slot = enumerator.Current;
                if (!(slot.get_Item().get_ItemDef().get_InternalName() == this.ItemId))
                {
                    goto Label_01C8;
                }
                Logger.WriteLine("Use " + slot.get_Item().get_ItemDef().get_DisplayName() + "...");
                if ((slot.get_BagId() == 0x4a) || (slot.get_Item().get_ItemDef().get_Type() == 8))
                {
                    slot.Exec();
                }
                else if (!slot.get_Item().get_IsBound() && (slot.get_Item().get_ItemDef().get_Type() == 0x23))
                {
                    slot.BindPet(Pets.GetACompanionName());
                }
                else
                {
                    slot.Equip();
                }
                Thread.Sleep(this.UseTimeMs);
                if (this.Dialogs.Count <= 0)
                {
                    goto Label_01C1;
                }
                Timeout timeout = new Timeout(0x1388);
                goto Label_011D;
                Label_0116:
                Thread.Sleep(100);
                Label_011D:
                if (EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_ContactDialog().get_Options().Count == 0)
                {
                    if (timeout.IsTimedOut)
                    {
                        return Action.ActionResult.Fail;
                    }
                    goto Label_0116;
                }
                Thread.Sleep(500);
                foreach (string str in this.Dialogs)
                {
                    EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_ContactDialog().SelectOptionByKey(str, "");
                    Thread.Sleep(0x3e8);
                }
                Label_01C1:
                return Action.ActionResult.Completed;
                Label_01C8:
                if (enumerator.MoveNext())
                {
                    goto Label_0031;
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            return Action.ActionResult.Fail;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "UseItem";
            }
        }

        public override string Category
        {
            get
            {
                return "Basic";
            }
        }

        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        public List<string> Dialogs { get; set; }

        protected override bool IntenalConditions
        {
            get
            {
                return ((this.ItemId.Length > 0) && (EntityManager.get_LocalPlayer().GetItemCountByInternalName(this.ItemId) > 0));
            }
        }

        protected override Vector3 InternalDestination
        {
            get
            {
                return new Vector3();
            }
        }

        public override string InternalDisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        protected override Action.ActionValidity InternalValidity
        {
            get
            {
                if (this.ItemId.Length == 0)
                {
                    return new Action.ActionValidity("Invalid item id.");
                }
                return new Action.ActionValidity();
            }
        }

        [Editor(typeof(ItemIdEditor), typeof(UITypeEditor))]
        public string ItemId { get; set; }

        public override bool NeedToRun
        {
            get
            {
                return true;
            }
        }

        public override bool UseHotSpots
        {
            get
            {
                return false;
            }
        }

        public int UseTimeMs { get; set; }
    }
}

