using Astral;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using QuesterAssistant.UIEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace QuesterAssistant.Actions
{
    [Serializable]
    public class Notify : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override bool NeedToRun => true;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity => new ActionValidity();
        public override void OnMapDraw(GraphicsNW graph) {}
        public override void GatherInfos() {}
        public override void InternalReset() {}

        [Description("Text of message")]
        public string Message { get; set; }

        [Description("Enable or disable Alert")]
        public AStat Type { get; set; }

        [Description("Specify where to be send")]
        [Editor(typeof(CheckedListBoxEditor<Address>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<Address> SendTo { get; set; } = new CheckedListBoxSelector<Address>();

        public enum AStat
        {
            Information,
            Alert
        }

        public enum Address
        {
            PopUpMessage,
            PushMessage,
            LogMessage
        }

        public override ActionResult Run()
        {
            if (SendTo.Items.Contains(Address.PopUpMessage))
            {
                Logger.Notify(Message, (Type == AStat.Alert) ? true : false);
            }
            if (SendTo.Items.Contains(Address.PushMessage))
            {
                try
                {
                    Core.PushNotifyCore.PushMessage(Message);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine($"PushNotify Error: {ex.Message}");
                }
            }
            if (SendTo.Items.Contains(Address.LogMessage) && !SendTo.Items.Contains(Address.PopUpMessage))
            {
                Logger.WriteLine(Message);
            }
            return ActionResult.Completed;
        }
    }
}
