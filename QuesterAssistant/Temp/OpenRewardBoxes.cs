namespace Astral.Quester.Classes.Actions
{
    using Astral;
    using Astral.Classes.ItemFilter;
    using Astral.Logic.Classes.Map;
    using Astral.Logic.NW;
    using Astral.Quester.Classes;
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
    public class OpenRewardBoxes : Action
    {
        // Fields
        private ItemFilterCore boxesFilter = new ItemFilterCore();

        // Methods
        public OpenRewardBoxes()
        {
            this.Boxes = new List<string>();
            this.BoxesFilter = new ItemFilterCore();
        }

        public override void GatherInfos()
        {
        }

        public override void InternalReset()
        {
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
        }

        public override Action.ActionResult Run()
        {
            if (Inventory.RewardBoxes(this.BoxesFilter).Count <= 0)
            {
                Logger.WriteLine("No more boxes to open...");
                return Action.ActionResult.Completed;
            }
            Inventory.OpenRewardBoxes(this.BoxesFilter);
            Thread.Sleep(0x5dc);
            if ((Inventory.RewardBoxes(this.BoxesFilter).Count > 0) && (EntityManager.get_LocalPlayer().get_BagsFreeSlots() > 1))
            {
                return Action.ActionResult.Running;
            }
            return Action.ActionResult.Completed;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "OpenRewardBoxes";
            }
        }

        [Browsable(false), Description("Deprecated")]
        public List<string> Boxes { get; set; }

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore BoxesFilter
        {
            get
            {
                if (this.Boxes.Count > 0)
                {
                    this.boxesFilter = ItemFilterCore.(this.Boxes, ItemFilterType.ItemID);
                    this.Boxes.Clear();
                }
                return this.boxesFilter;
            }
            set
            {
                this.boxesFilter = value;
            }
        }

        protected override bool IntenalConditions
        {
            get
            {
                return ((Inventory.RewardBoxes(this.BoxesFilter).Count > 0) && (EntityManager.get_LocalPlayer().get_BagsFreeSlots() > 1));
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
                return new Action.ActionValidity();
            }
        }

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
    }
}

