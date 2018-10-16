namespace Astral.Professions.Functions
{
    using Astral;
    using Astral.Classes;
    using Astral.Logic.NW;
    using Astral.Professions;
    using Astral.Professions.Classes;
    using MyNW.Classes;
    using MyNW.Internals;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class Sale
    {
        // Methods
        public static void Discard()
        {
            List<InventorySlot>.Enumerator enumerator = EntityManager.get_LocalPlayer().get_BagsItems().GetEnumerator();
            goto Label_0080;
            try
            {
                InventorySlot slot;
                Label_001A:
                slot = enumerator.Current;
                if (!slot.get_Item().get_ItemDef().get_CantDiscard() && Core.AccountsProfile.ToDiscardFilter.(slot.get_Item()))
                {
                    Logger.WriteLine("Discard '" + slot.get_Item().get_DisplayName() + "' ...");
                    slot.RemoveAll();
                    Thread.Sleep(400);
                }
                Label_0080:
                if (enumerator.MoveNext())
                {
                    goto Label_001A;
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static bool SaleToNearestMechant()
        {
            Logger.WriteLine("Try to sell...");
            Entity vendor = new Entity(IntPtr.Zero);
            if (SpecialVendor.IsAvailable() && SpecialVendor.IsAvailable())
            {
                Thread.Sleep(0x7d0);
                SpecialVendor.UseItem();
                Timeout timeout = new Timeout(0x1964);
                while (!SpecialVendor.VendorEntity.get_IsValid())
                {
                    if (timeout.IsTimedOut)
                    {
                        goto Label_0072;
                    }
                    Thread.Sleep(400);
                }
                vendor = SpecialVendor.VendorEntity;
            }
            Label_0072:
            if (!vendor.get_IsValid())
            {
                string map = EntityManager.get_LocalPlayer().get_MapState().get_MapName();
                string region = EntityManager.get_LocalPlayer().get_RegionInternalName();
                foreach (NPCInfos infos in Enumerable.ToList<NPCInfos>(Enumerable.OrderBy<NPCInfos, double>(from v in Core.AccountsProfile.Vendors
                                                                                                            where (v.Map == map) && (v.Region == region)
                                                                                                            select v, (<> c.<> 9__1_1 == null) ? (<> c.<> 9__1_1 = new Func<NPCInfos, double>(<> c.<> 9.< SaleToNearestMechant > b__1_1)) : <> c.<> 9__1_1)))
                {
                    if (!Move.MoveTo(infos.Location, 10.0))
                    {
                        break;
                    }
                    using (List<ContactInfo>.Enumerator enumerator2 = EntityManager.get_LocalPlayer().get_Player().get_InteractInfo().get_NearbyContacts().GetEnumerator())
                    {
                        ContactInfo current;
                        do
                        {
                            if (!enumerator2.MoveNext())
                            {
                                break;
                            }
                            current = enumerator2.Current;
                        }
                        while (!current.get_Entity().get_IsValid() || (current.get_Entity().get_Location().Distance3D(infos.Location) >= 1.0));
                        vendor = current.get_Entity();
                        break;
                    }
                }
            }
            if (vendor.get_IsValid())
            {
                Interact.IdentifyItems();
                if (Interact.Vendor(vendor, null))
                {
                    Interact.SellItems();
                    Interact.CloseVendor();
                    Thread.Sleep(0x3e8);
                    return true;
                }
            }
            else
            {
                Logger.WriteLine("Unable to found a merchant...");
            }
            return false;
        }

        // Nested Types
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            // Fields
            public static readonly Sale.<>c<>9 = new Sale.<>c();
        public static Func<NPCInfos, double> <>9__1_1;

            // Methods
            internal double <SaleToNearestMechant>b__1_1(NPCInfos v)
        {
            return v.Location.get_Distance3DFromPlayer();
        }
    }
}
}
