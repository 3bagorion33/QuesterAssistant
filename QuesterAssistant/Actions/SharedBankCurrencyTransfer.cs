using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Threading;
using Astral;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Actions
{
    public class SharedBankCurrencyTransfer : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => GetType().Name;
        public override string Category => Core.Category;
        public override string InternalDisplayName => string.Empty;
        public override bool UseHotSpots => false;
        protected override Vector3 InternalDestination => Banker.Position;
        public override void InternalReset() { }
        protected override ActionValidity InternalValidity => new ActionValidity();

        public override bool NeedToRun =>
            VIP.CanSummonBankingPortal || Banker.Position.Distance3DFromPlayer < 30.0;

        protected override bool IntenalConditions =>
            VIP.CanSummonBankingPortal || EntityManager.LocalPlayer.MapState.MapName == Banker.MapName;

        public override void OnMapDraw(GraphicsNW graph)
        {
            if (Banker.Position.IsValid)
            {
                Brush beige = Brushes.Beige;
                graph.drawFillEllipse(Banker.Position, new Size(10, 10), beige);
            }
        }

        public SharedBankCurrencyTransfer()
        {
            Banker = new NPCInfos();
            MoneyTransfert = 0;
            NumericType = "Resources";
            TransferMode = TransferModeDef.Transfer;
        }

        public override void GatherInfos()
        {
            typeof(Astral.Quester.UIEditors.NPCInfos).ExecStaticMethod(
                nameof(Astral.Quester.UIEditors.NPCInfos.SetInfos), new object[] {Banker});
        }

        public override ActionResult Run()
        {
            int GetTransferCount()
            {
                switch (TransferMode)
                {
                    case TransferModeDef.Keep:
                        return MathTools.Max(EntityManager.LocalPlayer.Inventory.GetNumericCount(NumericType) -
                                             Math.Abs(MoneyTransfert),
                            -EntityManager.SharedBank.Inventory.GetNumericCount(NumericType));
                    case TransferModeDef.Transfer:
                        return MoneyTransfert > 0
                            ? (int)MathTools.Min(MoneyTransfert,
                                EntityManager.LocalPlayer.Inventory.GetNumericCount(NumericType))
                            : (int)MathTools.Max(MoneyTransfert,
                                -EntityManager.SharedBank.Inventory.GetNumericCount(NumericType));
                    default:
                        return 0;
                }
            }

            Entity entity = new Entity(IntPtr.Zero);
            if (VIP.CanSummonBankingPortal)
            {
                VIP.SummonBankingPortal();
                Astral.Classes.Timeout timeout = new Astral.Classes.Timeout(10000);
                while (!(VIP.BankingPortalEntity.IsValid && VIP.BankingPortalEntity.NameUntranslated == "Critterdef.Vip_Bank"
                       ))//&& VIP.BankingPortalEntity.CanInteract))
                {
                    if (timeout.IsTimedOut)
                    {
                        timeout.Reset();
                        VIP.BankingPortalEntity.Location.Face();
                        VIP.SummonBankingPortal();
                    }
                    Pause.Sleep(250);
                }
                entity = VIP.BankingPortalEntity;
            }
            if (!entity.IsValid)
            {
                entity = EntityManager.GetEntities().Find(e => Banker.IsMatching(e)) ?? new Entity(IntPtr.Zero);
            }
            if (entity.IsValid && Approach.EntityForInteraction(entity))
            {
                entity.Interact();
                Astral.Classes.Timeout timeout2 = new Astral.Classes.Timeout(6000);
                while (!EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.IsValid)
                {
                    if (timeout2.IsTimedOut)
                    {
                        return ActionResult.Fail;
                    }
                    Thread.Sleep(500);
                }
                Thread.Sleep(1000);
                if (MoneyTransfert != 0)
                {
                    Logger.WriteLine($"[SharedBank] {TransferMode} {MoneyTransfert} coppers ...");
                    int val = GetTransferCount();
                    Logger.WriteLine("[SharedBank] Value to transfer : " + val);

                    EntityManager.LocalPlayer.SharedBankAddNumeric(val, NumericType);
                }
                Thread.Sleep(1000);
                EntityManager.LocalPlayer.Player.InteractInfo.ContactDialog.Close();
                return ActionResult.Completed;
            }
            return ActionResult.Fail;
        }

        [Editor(typeof(Astral.Quester.UIEditors.NPCInfos), typeof(UITypeEditor))]
        public NPCInfos Banker { get; set; }

        [DisplayName("NumericTransfer")]
        [Description("Use negative number to withdraw currency.")]
        public int MoneyTransfert { get; set; }

        [Editor(typeof(Astral.Quester.UIEditors.SharedBankNumericsEditor), typeof(UITypeEditor))]
        [Description("Resources : coppers")]
        public string NumericType { get; set; }

        [Description("Keep determined value in the source during withdraw or store.\nStore or withdraw determined value to the target")]
        public TransferModeDef TransferMode { get; set; }

        public enum TransferModeDef : byte
        {
            Keep,
            Transfer
        }
    }
}