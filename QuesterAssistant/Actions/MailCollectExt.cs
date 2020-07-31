﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using Astral;
using Astral.Classes;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.Classes.Actions;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using API = Astral.Quester.API;

namespace QuesterAssistant.Actions
{
    public class MailCollectExt : Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        protected override bool IntenalConditions
        {
            get
            {
                if (MailEntity.MapName.Length != 0 && MailEntity.MapName != EntityManager.LocalPlayer.MapState.MapName)
                {
                    Logger.WriteLine("Wrong map");
                    return false;
                }
                return true;
            }
        }
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;

        public override bool NeedToRun => 
            VIP.CanSummonMailbox || MailEntity.Position.Distance3DFromPlayer < 15.0;

        protected override Vector3 InternalDestination =>
            EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.ScreenType == ScreenType.MailBox
                ? new Vector3()
                : MailEntity.Position;

        protected override ActionValidity InternalValidity => new ActionValidity();

        public override void InternalReset() { }
        public override void GatherInfos() { }

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (!MailEntity.Position.IsValid)
            {
                return;
            }
            Brush beige = Brushes.Beige;
            graph.drawFillEllipse(MailEntity.Position, new Size(10, 10), beige);
        }

        public MailCollectExt()
        {
            MailEntity = new NPCInfos();
            CleanUpRegex = ".*";
            OnlyDeleteEmptyMails = false;
            MinFreeBagsSlots = 4;
            FilterType = MailCollectFilterType.Body;
        }

        public override ActionResult Run()
        {
            if (EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.ScreenType != ScreenType.MailBox)
            {
                if (VIP.CanSummonMailbox)
                {
                    API.Engine.Navigation.Stop();
                    VIP.SummonMailbox();
                    var timeout = new Timeout(5000);
                    while (!VIP.MailBoxEntity.IsValid && !timeout.IsTimedOut)
                        Pause.Sleep(500);
                    VIP.InteractMailbox();
                }

                if (!VIP.MailBoxEntity.IsValid)
                {
                    var contactInfo = EntityManager.LocalPlayer.Player.InteractInfo.NearbyContacts
                        .OrderBy(c => c.Entity.Location.Distance3D(MailEntity.Position))
                        .FirstOrDefault(f => MailEntity.IsMatching(f.Entity));
                    if (contactInfo == null)
                    {
                        Logger.WriteLine("Not found " + MailEntity.DisplayName);
                        return ActionResult.Fail;
                    }
                    contactInfo.Entity.Interact();
                }
                Pause.Sleep(1500);

                while ((EntityManager.LocalPlayer.BagsFreeSlots > MinFreeBagsSlots || OnlyDeleteEmptyMails) &&
                       EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.ScreenType == ScreenType.MailBox)
                {
                    API.Engine.Navigation.Stop();

                    var mails = Email.Mails;
                }
            }

            return ActionResult.Completed;
        }

        [Description(".* = Collect all items from Mailbox and remove all mails")]
        public string CleanUpRegex { get; set; }
        public MailCollectFilterType FilterType { get; set; }
        [Editor(typeof(NPCInfos), typeof(UITypeEditor))]
        public NPCInfos MailEntity { get; set; }
        public bool OnlyDeleteEmptyMails { get; set; }
        public int MinFreeBagsSlots { get; set; }
    }
}