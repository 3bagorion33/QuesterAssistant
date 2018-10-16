namespace Astral.Quester.Classes.Actions
{
    using Astral;
    using Astral.Classes;
    using Astral.Classes.ItemFilter;
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

    [Serializable]
    public class MailSendEntity : Action
    {
        // Fields
        private ItemFilterCore itemsFilter = new ItemFilterCore();

        // Methods
        public MailSendEntity()
        {
            this.MailEntity = new NPCInfos();
            this.SendRegex = string.Empty;
            this.MailTo = string.Empty;
            this.ItsAnotherAccount = false;
            this.MailSubject = string.Empty;
        }

        public override void GatherInfos()
        {
            NPCInfos.SetInfos(this.MailEntity);
        }

        public override void InternalReset()
        {
        }

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (this.MailEntity.Position.get_IsValid())
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(this.MailEntity.Position, new Size(10, 10), beige);
            }
        }

        public override Action.ActionResult Run()
        {
            if (VIP.CanSummonMailbox)
            {
                Engine.MainEngine.Navigation.Stop();
                VIP.SummonMailbox();
                Timeout timeout = new Timeout(0x1388);
                while (!VIP.MailBoxEntity.get_IsValid())
                {
                    if (timeout.IsTimedOut)
                    {
                        goto Label_0062;
                    }
                    Thread.Sleep(500);
                }
                Thread.Sleep(0x5dc);
                VIP.InteractMailbox();
            }
            Label_0062:
            if (EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_ContactDialog().get_ScreenType() != 0x1d)
            {
                ContactInfo info = Enumerable.FirstOrDefault<ContactInfo>(from c in EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_NearbyContacts()
                                                                          orderby c.get_Entity().get_Location().Distance3D(this.MailEntity.Position)
                                                                          select c, f => this.MailEntity.IsMatching(f.get_Entity()));
                if (info == null)
                {
                    Logger.WriteLine(string.Format("Not found {0}", this.MailEntity.DisplayName));
                    return Action.ActionResult.Fail;
                }
                info.get_Entity().Interact();
                Thread.Sleep(0x9c4);
            }
            if (EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_ContactDialog().get_ScreenType() != 0x1d)
            {
                return Action.ActionResult.Running;
            }
            if ((!string.IsNullOrEmpty(this.MailTo) || !this.ItsAnotherAccount) && (EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_ContactDialog().get_ScreenType() == 0x1d))
            {
                string str = string.IsNullOrEmpty(this.MailSubject) ? "bank items" : this.MailSubject;
                if (this.ItsAnotherAccount && (Inventory.GetUnboundMailItems(this.ItemsFilter).Count > 0))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Send items:");
                    List<Item> list = new List<Item>();
                    for (int i = 0; (i < 5) && (i < Inventory.GetUnboundMailItems(this.ItemsFilter).Count); i++)
                    {
                        list.Add(Inventory.GetUnboundMailItems(this.ItemsFilter)[i]);
                    }
                    list.ForEach(delegate (Item f) {
                        sb.AppendLine(string.Format("{0} Count : {1}", f.get_ItemDef().get_DisplayName(), f.get_Count()));
                    });
                    Logger.WriteLine(sb.ToString());
                    try
                    {
                        Email.SendMail(string.Format("{0}", this.MailTo), str, "", list);
                    }
                    catch
                    {
                    }
                    Thread.Sleep(0x5dc);
                    return Action.ActionResult.Running;
                }
                if (!this.ItsAnotherAccount && (Inventory.GetMailItems(this.ItemsFilter).Count > 0))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Send items:");
                    List<Item> list2 = new List<Item>();
                    for (int j = 0; (j < 5) && (j < Inventory.GetMailItems(this.ItemsFilter).Count); j++)
                    {
                        list2.Add(Inventory.GetMailItems(this.ItemsFilter)[j]);
                    }
                    list2.ForEach(delegate (Item f) {
                        sb.AppendLine(string.Format("{0} Count : {1}", f.get_ItemDef().get_DisplayName(), f.get_Count()));
                    });
                    Logger.WriteLine(sb.ToString());
                    try
                    {
                        Email.SendMail(string.Format("{0}", EntityManager.get_LocalPlayer().get_AccountLoginUsername()), str, "", list2);
                    }
                    catch
                    {
                    }
                    Thread.Sleep(0x5dc);
                    return Action.ActionResult.Running;
                }
            }
            Email.CloseMailFrame();
            return Action.ActionResult.Completed;
        }

        // Properties
        public override string ActionLabel
        {
            get
            {
                return "MailSendEntity";
            }
        }

        protected override bool IntenalConditions
        {
            get
            {
                if (this.MailEntity.MapName == EntityManager.get_LocalPlayer().get_MapState().get_MapName())
                {
                    return true;
                }
                Logger.WriteLine("Wrong map");
                return false;
            }
        }

        protected override Vector3 InternalDestination
        {
            get
            {
                return this.MailEntity.Position;
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

        [Editor(typeof(ItemIdFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemsFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SendRegex))
                {
                    ItemFilterEntry entry1 = new ItemFilterEntry();
                    entry1.(this.SendRegex);
                    entry1.(ItemFilterType.ItemID);
                    entry1.(ItemFilterStringType.Regex);
                    ItemFilterEntry item = entry1;
                    this.itemsFilter.().Add(item);
                    this.SendRegex = string.Empty;
                }
                return this.itemsFilter;
            }
            set
            {
                this.itemsFilter = value;
            }
        }

        [Description("If receiver is same account or not")]
        public bool ItsAnotherAccount { get; set; }

        [Editor(typeof(NPCInfos), typeof(UITypeEditor))]
        public NPCInfos MailEntity { get; set; }

        [Description("Optionnal")]
        public string MailSubject { get; set; }

        [Description("If mails should be send to another account then this is mandatory to fill")]
        public string MailTo { get; set; }

        public override bool NeedToRun
        {
            get
            {
                return (VIP.CanSummonMailbox || (this.MailEntity.Position.get_Distance3DFromPlayer() < 25.0));
            }
        }

        [Browsable(false)]
        public string SendRegex { get; set; }

        public override bool UseHotSpots
        {
            get
            {
                return false;
            }
        }
    }
}

