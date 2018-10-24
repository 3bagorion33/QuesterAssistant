using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Professions.Functions;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace QuesterAssistant
{
    [Serializable]
    public class DiscardItem : Astral.Quester.Classes.Action
    {

        // Properties
        public override string ActionLabel => "DiscardItem";

        public override bool NeedToRun => true;

        public override string InternalDisplayName => "DiscardItem";

        public override bool UseHotSpots => false;

        protected override bool IntenalConditions => true;

        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity => new ActionValidity();

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemIdFilter { get; set; } = new ItemFilterCore();
        
        // Methods
        public override void GatherInfos() {}

        public override void InternalReset() {}

        public override void OnMapDraw(GraphicsNW graph) {}

        internal static List<InventorySlot> DeletingItems(MyItemFilter.MyItemFilterCore filter)
        {
            List<InventorySlot> list = new List<InventorySlot>();
            List<InventorySlot>.Enumerator enumerator = EntityManager.LocalPlayer.BagsItems.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    InventorySlot slot = enumerator.Current;
                    Item item = slot.Item;
                    if (!item.ItemDef.CantDiscard && filter.IsMatch(item))
                    {
                        list.Add(slot);
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            return list;
        }

        internal static void DeleteItems(MyItemFilter.MyItemFilterCore filter)
        {
            List<InventorySlot> list = DeletingItems(filter);
            list.ForEach(Interact.DiscardItem);
        }

        public override ActionResult Run()
        {
            DeleteItems((MyItemFilter.MyItemFilterCore)this.ItemIdFilter);
            return ActionResult.Completed;
        }
    }
}
