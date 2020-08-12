using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading;
using System.Windows.Forms;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.Forms;
using Astral.Quester.UIEditors;
using Astral.Quester.UIEditors.Forms;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Panels;

namespace QuesterAssistant.Actions
{
    public class BuyItemRemote : Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public BuyItemRemote()
        {
            Dialogs = new List<string>();
            RemoteContact = string.Empty;
            BuyOptions = new List<BuyItemsOption>();
        }

        public override void GatherInfos()
        {
            RemoteContact = GetAnId.GetARemoteContact();
            
            if (!string.IsNullOrEmpty(RemoteContact) && 
                QMessageBox.ShowDialog("Call it now ?") == DialogResult.Yes)
                Injection.cmdwrapper_contact_StartRemoteContact(RemoteContact);
           
            if (QMessageBox.ShowDialog("Add a dialog ? (open the dialog window before)") == DialogResult.Yes)
                DialogEdit.Show(Dialogs);
        }

        protected override ActionValidity InternalValidity
        {
            get
            {
                if (string.IsNullOrEmpty(RemoteContact))
                    return new ActionValidity("No remote contact set.");
                if(BuyOptions == null || BuyOptions.Count == 0)
                    return new ActionValidity("Items to buy are not specified.");
                return new ActionValidity();
            }
        }

        public override ActionResult Run()
        {
            if (!string.IsNullOrEmpty(RemoteContact))
            {
                Injection.cmdwrapper_contact_StartRemoteContact(RemoteContact);
                Interact.WaitForInteraction();
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

                foreach (BuyItemsOption buyItemsOption in BuyOptions)
                {
                    foreach (StoreItemInfo storeItemInfo in EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog
                        .StoreItems)
                    {
                        if (storeItemInfo.CanBuyError == 0u &&
                            buyItemsOption.ItemId == storeItemInfo.Item.ItemDef.InternalName)
                        {
                            Logger.WriteLine(
                                $"Buy {buyItemsOption.Count} {storeItemInfo.Item.ItemDef.DisplayName} ...");
                            storeItemInfo.BuyItem(buyItemsOption.Count);
                            Thread.Sleep(250);
                            break;
                        }
                    }
                }

                this.CloseFrames();
                return ActionResult.Completed;
            }

            Logger.WriteLine("Contact not found !");
            return ActionResult.Fail;
        }

        [Editor(typeof(DialogEditor), typeof(UITypeEditor))]
        [Category("Purchase")]
        public List<string> Dialogs { get; set; }

        [Editor(typeof(RemoteContactEditor), typeof(UITypeEditor))]
        [Category("Purchase")]
        public string RemoteContact { get; set; }

        [Editor(typeof(BuyOptionsEditor), typeof(UITypeEditor))]
        [Category("Purchase")]
        public List<BuyItemsOption> BuyOptions { get; set; }
    }
}
