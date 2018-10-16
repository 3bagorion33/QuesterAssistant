namespace Astral.Logic.NW
{
    using Astral;
    using Astral.Classes.ItemFilter;
    using Astral.Controllers;
    using MyNW.Classes;
    using MyNW.Classes.CurrencyExchange;
    using MyNW.Internals;
    using MyNW.Patchables.Enums;
    using ns1;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class Inventory
    {
        // Fields
        private static int ctilCValue;
        private static uint ctilPlayer;
        private static int ctilValue;

        // Methods
        public static bool AllowedByLootFilter(List<InventoryBag> lootBags)
        {
            if (Class1.CurrentSettings.LootFilter.method_2(lootBags))
            {
                if (Settings.Get.filterMode == 0)
                {
                    return false;
                }
                return true;
            }
            return (Settings.Get.filterMode == 0);
        }

        public static void AutoEquipActivePet()
        {
            EntitySavedSCPData data = Class1.LocalPlayer.get_Saved().get_ScpData();
            if (data.get_SummonedScpRefId() > 0)
            {
                uint num = data.get_SummonedSpcIndex();
                Item item = SuperCritterPet.GetPetSlotByIndex(num).get_Item();
                ActiveSuperCritterPet activeSuperCritterPetByIndex = Class1.LocalPlayer.get_Saved().get_ScpData().GetActiveSuperCritterPetByIndex(num);
                uint num2 = Class1.LocalPlayer.get_Character().get_LevelExp();
                List<InventorySlot> list = Class1.LocalPlayer.get_BagsItems();
                if ((Settings.Get.AutoEquipPet && item.get_IsValid()) && (item.get_SpecialProps().get_SuperCritterPet().get_IsValid() && activeSuperCritterPetByIndex.get_IsValid()))
                {
                    int count = item.get_SpecialProps().get_SuperCritterPet().get_PetDef().get_EquipSlotDefs().Count;
                    for (uint i = 0; i < count; i++)
                    {
                        InventorySlot slotByIndex = activeSuperCritterPetByIndex.get_Equipment().GetSlotByIndex(i);
                        if (!item.get_SpecialProps().get_SuperCritterPet().EquipmentSlotIsLockedByIndex(i))
                        {
                            item.get_SpecialProps().get_SuperCritterPet().get_PetDef().GetEquipmentSlotByIndex(i);
                            InventorySlot slot2 = new InventorySlot(IntPtr.Zero);
                            foreach (InventorySlot slot3 in list)
                            {
                                if ((IsEquipementItem(slot3) && (slot3.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel() <= num2)) && item.get_SpecialProps().get_SuperCritterPet().get_PetDef().CanEquipItemBySlotIndex(slot3.get_Item().get_ItemDef(), i))
                                {
                                    if (slot2.get_IsValid())
                                    {
                                        if (slot3.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel() > slot2.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel())
                                        {
                                            slot2 = slot3;
                                        }
                                    }
                                    else if (slotByIndex.get_Filled())
                                    {
                                        if (slot3.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel() > slotByIndex.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel())
                                        {
                                            slot2 = slot3;
                                        }
                                    }
                                    else
                                    {
                                        slot2 = slot3;
                                    }
                                }
                            }
                            if (slot2.get_IsValid())
                            {
                                Logger.WriteLine("Equip on pet : " + slot2.get_Item().get_DisplayName());
                                slot2.MoveToPet(num, i, true);
                                list.Remove(slot2);
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
                if (Settings.Get.AutoGemPet && item.get_IsValid())
                {
                    foreach (ItemGemSlotDef def in item.get_ItemDef().get_EffectiveItemGemSlots())
                    {
                        if (!item.get_SpecialProps().get_SuperCritterPet().GemSlotIsLockedByIndex(def.get_Index()))
                        {
                            ItemGemSlot gemSlotByIndex = item.get_SpecialProps().GetGemSlotByIndex(def.get_Index());
                            InventorySlot slot5 = new InventorySlot(IntPtr.Zero);
                            foreach (InventorySlot slot6 in list)
                            {
                                if (slot6.get_Item().get_ItemDef().get_Type() == 0x1f)
                                {
                                    Logger.WriteLine(slot6.get_Item().get_DisplayName() + " is gem");
                                    if ((def.get_Type() & slot6.get_Item().get_ItemDef().get_GemType()) == 0)
                                    {
                                        Logger.WriteLine("But no compatible");
                                    }
                                    else if (slot5.get_IsValid())
                                    {
                                        if (slot6.get_Item().get_ItemDef().get_Level() > slot5.get_Item().get_ItemDef().get_Level())
                                        {
                                            slot5 = slot6;
                                        }
                                    }
                                    else if (gemSlotByIndex.get_IsValid())
                                    {
                                        if (slot6.get_Item().get_ItemDef().get_Level() > gemSlotByIndex.get_SlottedItem().get_Level())
                                        {
                                            slot5 = slot6;
                                        }
                                    }
                                    else
                                    {
                                        slot5 = slot6;
                                    }
                                }
                            }
                            if (slot5.get_IsValid())
                            {
                                Logger.WriteLine("Gem pet item with  : " + slot5.get_Item().get_DisplayName());
                                item.GemThisItem(slot5.get_Item(), def.get_Index());
                                list.Remove(slot5);
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
            }
        }

        public static void AutoEquipAll()
        {
            AutoEquipPlayer();
            if (Settings.Get.AutoEquipPet || Settings.Get.AutoGemPet)
            {
                AutoEquipActivePet();
            }
        }

        public static void AutoEquipPlayer()
        {
            if (Settings.Get.EnableAutoEquip)
            {
                new Dictionary<InvBagIDs, InventorySlot>();
                new List<ulong>();
                new Dictionary<InvBagIDs, List<InventorySlot>>();
                List<InvBagIDs> list = new List<InvBagIDs>();
                if (Settings.Get.EquipArmors)
                {
                    list.Add(0x2a);
                }
                if (Settings.Get.EquipArms)
                {
                    list.Add(0x2b);
                }
                if (Settings.Get.EquipFeet)
                {
                    list.Add(0x2d);
                }
                if (Settings.Get.EquipHands)
                {
                    list.Add(0x2e);
                }
                if (Settings.Get.EquipHead)
                {
                    list.Add(40);
                }
                if (Settings.Get.EquipNeck)
                {
                    list.Add(0x29);
                }
                if (Settings.Get.EquipRings)
                {
                    list.Add(0x34);
                }
                if (Settings.Get.EquipShirt)
                {
                    list.Add(50);
                }
                if (Settings.Get.EquipTrousers)
                {
                    list.Add(0x33);
                }
                if (Settings.Get.EquipWaist)
                {
                    list.Add(0x2c);
                }
                uint num = Class1.LocalPlayer.get_Character().get_LevelExp();
                List<InventorySlot> list2 = Class1.LocalPlayer.get_BagsItems();
                if (Settings.Get.EquipBags)
                {
                    InventoryBag inventoryBagById = Class1.LocalPlayer.GetInventoryBagById(9);
                    uint num2 = inventoryBagById.get_MaxSlots() - inventoryBagById.get_FilledSlots();
                    if (num2 > 0)
                    {
                        foreach (InventorySlot slot in list2)
                        {
                            if (slot.get_Item().get_ItemDef().get_Type() == 11)
                            {
                                Logger.WriteLine("Equip '" + slot.get_Item().get_ItemDef().get_DisplayName() + "' bag");
                                slot.Equip();
                                Thread.Sleep(500);
                                num2--;
                                if (num2 <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                foreach (InvBagIDs ds in list)
                {
                    InventoryBag bag2 = Class1.LocalPlayer.GetInventoryBagById(ds);
                    for (uint i = 0; i < bag2.get_MaxSlots(); i++)
                    {
                        InventorySlot slotByIndex = bag2.GetSlotByIndex(i);
                        InventorySlot item = new InventorySlot(IntPtr.Zero);
                        foreach (InventorySlot slot4 in list2)
                        {
                            if ((IsEquipementItem(slot4) && (slot4.get_Item().get_ItemDef().get_UsageRestriction().get_MinLevel() <= num)) && slot4.get_Item().get_ItemDef().get_RestrictBagIDs().Contains(ds))
                            {
                                uint num4 = slot4.get_Item().get_ItemDef().get_RestrictSlotType();
                                if ((((i == 0) && (num4 == 1)) || ((i == 1) && (num4 == 2))) || (num4 == 0))
                                {
                                    if (item.get_IsValid())
                                    {
                                        if (slot4.get_Item().IsRecommended(item.get_Item()))
                                        {
                                            item = slot4;
                                        }
                                    }
                                    else if (slotByIndex.get_Filled())
                                    {
                                        if (slot4.get_Item().IsRecommended(slotByIndex.get_Item()))
                                        {
                                            item = slot4;
                                        }
                                    }
                                    else if (slot4.get_Item().IsRecommended())
                                    {
                                        item = slot4;
                                    }
                                }
                            }
                        }
                        if (item.get_IsValid())
                        {
                            list2.Remove(item);
                            item.Move(ds, slotByIndex.get_Slot(), 1);
                            Logger.WriteLine(string.Concat(new object[] { "Equip ", item.get_Item().get_ItemDef().get_DisplayName(), " in ", ds, i }));
                            Thread.Sleep(500);
                        }
                    }
                }
                if (Settings.Get.AutoEnchant)
                {
                    foreach (InventorySlot slot5 in Class1.LocalPlayer.get_EquippedItem())
                    {
                        foreach (ItemGemSlotDef def in slot5.get_Item().get_ItemDef().get_EffectiveItemGemSlots())
                        {
                            ItemGemSlot gemSlotByIndex = slot5.get_Item().get_SpecialProps().GetGemSlotByIndex(def.get_Index());
                            InventorySlot slot7 = new InventorySlot(IntPtr.Zero);
                            foreach (InventorySlot slot8 in list2)
                            {
                                if ((slot8.get_Item().get_ItemDef().get_Type() == 0x1f) && ((def.get_Type() & slot8.get_Item().get_ItemDef().get_GemType()) != 0))
                                {
                                    if (slot7.get_IsValid())
                                    {
                                        if (slot8.get_Item().get_ItemDef().get_Level() > slot7.get_Item().get_ItemDef().get_Level())
                                        {
                                            slot7 = slot8;
                                        }
                                    }
                                    else if (gemSlotByIndex.get_IsValid())
                                    {
                                        if (slot8.get_Item().get_ItemDef().get_Level() > gemSlotByIndex.get_SlottedItem().get_Level())
                                        {
                                            slot7 = slot8;
                                        }
                                    }
                                    else
                                    {
                                        slot7 = slot8;
                                    }
                                }
                            }
                            if (slot7.get_IsValid())
                            {
                                Logger.WriteLine("Gem " + slot5.get_Item().get_DisplayName() + " item with  : " + slot7.get_Item().get_DisplayName());
                                slot5.get_Item().GemThisItem(slot7.get_Item(), def.get_Index());
                                list2.Remove(slot7);
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
            }
        }

        public static void ClaimAllAD()
        {
            int num = Class1.LocalPlayer.get_Player().get_CurrencyExchangeAccountData().get_ReadyToClaimEscrowTC();
            if (num > 0)
            {
                GameCommands.CurrencyexchangeClaimtc(num);
                General.RandomPause(500, 0x708);
            }
        }

        public static void FreeOverFlowBags()
        {
            uint num = Class1.LocalPlayer.GetInventoryBagById(0x21).get_FilledSlots();
            if (Class1.LocalPlayer.get_BagsFreeSlots() >= num)
            {
                List<InventorySlot> list = Class1.LocalPlayer.GetInventoryBagById(0x21).get_GetItems();
                foreach (InvBagIDs ds in Class1.LocalPlayer.get_BaseBagsIds())
                {
                    InventoryBag inventoryBagById = Class1.LocalPlayer.GetInventoryBagById(ds);
                    for (uint i = 0; i < inventoryBagById.get_MaxSlots(); i++)
                    {
                        if (list.Count == 0)
                        {
                            break;
                        }
                        if (!inventoryBagById.GetSlotByIndex(i).get_Filled())
                        {
                            using (List<InventorySlot>.Enumerator enumerator2 = list.GetEnumerator())
                            {
                                if (enumerator2.MoveNext())
                                {
                                    InventorySlot current = enumerator2.Current;
                                    current.MoveAll(ds, i);
                                    list.Remove(current);
                                    Thread.Sleep(200);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static List<Item> GetMailItems(ItemFilterCore filter)
        {
            List<InventorySlot> list1 = Class1.LocalPlayer.get_BagsItems();
            list1.AddRange(Class1.LocalPlayer.GetInventoryBagById(0x36).get_GetItems());
            list1.AddRange(Class1.LocalPlayer.GetInventoryBagById(0x35).get_GetItems());
            return Enumerable.ToList<Item>(Enumerable.Select<InventorySlot, Item>(from w in list1
                                                                                  where (filter.method_0(w.get_Item()) && !w.get_Item().get_IsBound()) && !w.get_Item().get_ItemDef().get_BindOnPickup()
                                                                                  select w, <> c.<> 9__10_1 ?? (<> c.<> 9__10_1 = new Func<InventorySlot, Item>(<> c.<> 9.< GetMailItems > b__10_1))));
        }

        public static List<Item> GetSalvageableItems(int mode)
        {
            List<Item> list = Enumerable.ToList<Item>(Enumerable.Select<InventorySlot, Item>(Class1.LocalPlayer.get_BagsItems(), <> c.<> 9__16_0 ?? (<> c.<> 9__16_0 = new Func<InventorySlot, Item>(<> c.<> 9.< GetSalvageableItems > b__16_0))));
            List<Item> list2 = new List<Item>();
            bool[] flagArray = Injection.MassCanSalvageByMode(list, mode);
            for (int i = 0; i < list.Count; i++)
            {
                if (flagArray[i])
                {
                    list2.Add(list[i]);
                }
            }
            return list2;
        }

        public static List<Item> GetUnboundMailItems(ItemFilterCore filter)
        {
            return Enumerable.ToList<Item>(Enumerable.Where<Item>(GetMailItems(filter), <> c.<> 9__11_0 ?? (<> c.<> 9__11_0 = new Func<Item, bool>(<> c.<> 9.< GetUnboundMailItems > b__11_0))));
        }

        public static bool HaveSalvageableItem(int SalvageMode, [Optional, DefaultParameterValue(true)] bool skipRecommended, [Optional, DefaultParameterValue(null)] ItemFilterCore filter)
        {
            foreach (Item item in GetSalvageableItems(SalvageMode))
            {
                if ((!skipRecommended || !item.IsRecommended()) && (((filter == null) || (filter.Entries.Count <= 0)) || filter.method_0(item)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCopperLoot(Entity entiy)
        {
            return ((entiy.get_Critter().get_IsValid() && entiy.get_Critter().get_LootBag().get_RewardBagInfo().get_IsValid()) && entiy.get_Critter().get_LootBag().get_RewardBagInfo().get_RewardTable().Contains("Numerics_Resources"));
        }

        private static bool IsEquipementItem(InventorySlot invSlot)
        {
            if (!invSlot.get_Item().get_IsValid() || !invSlot.get_Item().get_ItemDef().get_IsValid())
            {
                return false;
            }
            return ((!invSlot.get_Item().IsItemFlagActive(2) && !invSlot.get_Item().get_ItemDef().get_InternalName().EndsWith("_Unid")) && (invSlot.get_Item().get_ItemDef().get_RestrictBagIDs().Count > 0));
        }

        public static void OpenRewardBoxes(ItemFilterCore filter)
        {
            bool flag = false;
            foreach (InventorySlot slot in RewardBoxes(filter))
            {
                Item item = slot.get_Item();
                flag = true;
                uint num = item.get_Count();
                for (uint i = 0; i < num; i++)
                {
                    Logger.WriteLine("Open '" + item.get_ItemDef().get_DisplayName() + "'...");
                    slot.Equip();
                    General.RandomPause(200, 350);
                }
            }
            if (flag && Game.IsRewardpackviewerFrameVisible())
            {
                Game.CloseRewardpackviewerFrame();
            }
        }

        internal static bool ParseString(string text, string searchText)
        {
            if (searchText == "*")
            {
                return true;
            }
            if (searchText.StartsWith("*") && searchText.EndsWith("*"))
            {
                string str = searchText.Remove(0, 1);
                str = str.Remove(str.Length - 1);
                return text.Contains(str);
            }
            if (searchText.StartsWith("*"))
            {
                string str2 = searchText.Remove(0, 1);
                return text.EndsWith(str2);
            }
            if (searchText.EndsWith("*"))
            {
                string str3 = searchText.Remove(searchText.Length - 1);
                return text.StartsWith(str3);
            }
            return (searchText == text);
        }

        public static List<InventorySlot> RewardBoxes(ItemFilterCore filter)
        {
            List<InventorySlot> list = new List<InventorySlot>();
            foreach (InventorySlot slot in Class1.LocalPlayer.get_BagsItems())
            {
                Item item = slot.get_Item();
                if ((item.get_ItemDef().get_RewardPackInfo().get_IsValid() && !item.get_ItemDef().get_RewardPackInfo().get_RequiredItem().get_IsValid()) && ((item.get_ItemDef().get_Level() <= Class1.LocalPlayer.get_Character().get_LevelExp()) && filter.method_0(item)))
                {
                    list.Add(slot);
                }
            }
            return list;
        }

        public static void StockAllAD([Optional, DefaultParameterValue(0)] uint minimumStock)
        {
            if (Enumerable.Count<OpenOrder>(Enumerable.Where<OpenOrder>(Class1.LocalPlayer.get_Player().get_CurrencyExchangeAccountData().get_OpenOrders(), <> c.<> 9__7_0 ?? (<> c.<> 9__7_0 = new Func<OpenOrder, bool>(<> c.<> 9.< StockAllAD > b__7_0)))) > 4)
            {
                UnStockAllAD(400);
                Thread.Sleep(500);
            }
            int num = Class1.LocalPlayer.get_Inventory().get_AstralDiamonds() + Class1.LocalPlayer.get_Player().get_CurrencyExchangeAccountData().get_ReadyToClaimEscrowTC();
            Logger.WriteLine(string.Concat(new object[] { "Total AD : ", Class1.LocalPlayer.get_Inventory().get_AstralDiamonds(), " (character) + ", Class1.LocalPlayer.get_Player().get_CurrencyExchangeAccountData().get_ReadyToClaimEscrowTC(), " (account) = ", num }));
            num -= (int)minimumStock;
            if (num >= 50)
            {
                int item = num / 50;
                List<int> list = new List<int>();
                if (item > 0x1388)
                {
                    int num3 = item / 0x1388;
                    if (num3 > 5)
                    {
                        num3 = 5;
                    }
                    for (uint i = 0; i < num3; i++)
                    {
                        list.Add(0x1388);
                    }
                    int num4 = item % 0x1388;
                    if (num4 > 0)
                    {
                        list.Add(num4);
                    }
                    UnStockAllAD(400);
                }
                else
                {
                    list.Add(item);
                }
                foreach (int num6 in list)
                {
                    Logger.WriteLine("Create buy order : " + num6 + " zens for 50 AD");
                    GameCommands.CurrencyexchangeCreatebuyorder(num6, 50);
                    General.RandomPause(800, 0x5dc);
                }
            }
        }

        public static bool TryToDoSpace(uint spaceToDo)
        {
            int num = 0;
            Logger.WriteLine("Try to free " + spaceToDo + " slots in bags...");
            bool flag = false;
            while (true)
            {
                List<InventorySlot> list = null;
                if (flag)
                {
                    list = Enumerable.ToList<InventorySlot>(Enumerable.OrderBy<InventorySlot, uint>(Class1.LocalPlayer.get_BagsItems(), <> c.<> 9__23_1 ?? (<> c.<> 9__23_1 = new Func<InventorySlot, uint>(<> c.<> 9.< TryToDoSpace > b__23_1))));
                }
                else
                {
                    list = Enumerable.ToList<InventorySlot>(Enumerable.OrderBy<InventorySlot, uint>(Class1.LocalPlayer.get_BagsItems(), <> c.<> 9__23_0 ?? (<> c.<> 9__23_0 = new Func<InventorySlot, uint>(<> c.<> 9.< TryToDoSpace > b__23_0))));
                }
                foreach (InventorySlot slot in list)
                {
                    if (!slot.get_Item().get_ItemDef().get_CantDiscard())
                    {
                        if ((!flag && (slot.get_Item().get_ItemDef().get_Quality() == null)) || ((flag && (slot.get_Item().get_ItemDef().get_Quality() == 1)) && Interact.AllowedToSell(slot, Class1.CurrentSettings.SellFilter)))
                        {
                            Logger.WriteLine("Delete " + slot.get_Item().get_ItemDef().get_DisplayName());
                            slot.RemoveAll();
                            num++;
                            Thread.Sleep(500);
                        }
                        if (num >= spaceToDo)
                        {
                            return true;
                        }
                    }
                }
                if (flag)
                {
                    return false;
                }
                flag = true;
            }
        }

        public static void UnStockAllAD([Optional, DefaultParameterValue(400)] int maxPrice)
        {
            bool flag = false;
            Label_0002:
            flag = false;
            using (List<OpenOrder>.Enumerator enumerator = Class1.LocalPlayer.get_Player().get_CurrencyExchangeAccountData().get_OpenOrders().GetEnumerator())
            {
                OpenOrder current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (((current.get_OrderID() > 0) && (current.get_OrderType() == 1)) && (current.get_Price() < maxPrice))
                    {
                        goto Label_0051;
                    }
                }
                goto Label_0078;
                Label_0051:
                current.Withdraw();
                flag = true;
                General.RandomPause(0x3e8, 0x640);
            }
            Label_0078:
            if (flag)
            {
                goto Label_0002;
            }
        }

        // Properties
        public static int AstralDiamondSalvageMode
        {
            get
            {
                foreach (ItemSalvageModeDef def in Game.get_ItemSalvageModesDef())
                {
                    if (def.get_SalvageNumeric() == "Astral_Diamonds_Rough")
                    {
                        return def.get_SalvageMode();
                    }
                }
                return -1;
            }
        }

        public static int CachedPlayerTotalItemLevel
        {
            get
            {
                if (!Class1.LocalPlayer.get_IsValid())
                {
                    return 0;
                }
                uint num = Class1.LocalPlayer.get_ContainerId();
                int calculedTotalItemLevel = CalculedTotalItemLevel;
                if ((num != ctilPlayer) || (ctilCValue != calculedTotalItemLevel))
                {
                    ctilPlayer = num;
                    ctilCValue = calculedTotalItemLevel;
                    ctilValue = Class1.LocalPlayer.get_Character().GetTotalItemLevel();
                }
                return ctilValue;
            }
        }

        private static int CalculedTotalItemLevel
        {
            get
            {
                int num = 0;
                foreach (InventorySlot slot in Class1.LocalPlayer.get_EquippedItem())
                {
                    num += (int)slot.get_Item().get_ItemDef().get_Level();
                }
                return num;
            }
        }

        public static bool CanSend
        {
            get
            {
                return (Enumerable.Count<EmailUIMessage>(Email.get_Mails(), <> c.<> 9__9_0 ?? (<> c.<> 9__9_0 = new Func<EmailUIMessage, bool>(<> c.<> 9.< get_CanSend > b__9_0))) < 20);
            }
        }

        public static InvBagIDs GetFreeBankIDs
        {
            get
            {
                if (Class1.LocalPlayer.GetInventoryBagById(20).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(20).get_FilledSlots())
                {
                    return 20;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x15).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x15).get_FilledSlots())
                {
                    return 0x15;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x16).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x16).get_FilledSlots())
                {
                    return 0x16;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x17).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x17).get_FilledSlots())
                {
                    return 0x17;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x18).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x18).get_FilledSlots())
                {
                    return 0x18;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x19).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x19).get_FilledSlots())
                {
                    return 0x19;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x1a).get_MaxSlots() > Class1.LocalPlayer.GetInventoryBagById(0x1a).get_FilledSlots())
                {
                    return 0x1a;
                }
                if (Class1.LocalPlayer.GetInventoryBagById(0x1b).get_MaxSlots() <= Class1.LocalPlayer.GetInventoryBagById(0x1b).get_FilledSlots())
                {
                    return 0x13;
                }
                return 0x1b;
            }
        }

        public static List<string> LootRewardTables
        {
            get
            {
                List<string> list = new List<string>();
                using (List<Entity>.Enumerator enumerator = EntityManager.GetEntities().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        foreach (InventoryBag bag in enumerator.Current.get_Critter().get_LootBags())
                        {
                            if (!list.Contains(bag.get_RewardBagInfo().get_RewardTable()))
                            {
                                list.Add(bag.get_RewardBagInfo().get_RewardTable());
                            }
                        }
                    }
                }
                return list;
            }
        }

        public static int RefinementCurrencySalvageMode
        {
            get
            {
                foreach (ItemSalvageModeDef def in Game.get_ItemSalvageModesDef())
                {
                    if (def.get_SalvageNumeric() == "Refinement_Currency")
                    {
                        return def.get_SalvageMode();
                    }
                }
                return -1;
            }
        }

        // Nested Types
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            // Fields
            public static readonly Inventory.<>c<>9 = new Inventory.<>c();
        public static Func<InventorySlot, Item> <>9__10_1;
            public static Func<Item, bool> <>9__11_0;
            public static Func<InventorySlot, Item> <>9__16_0;
            public static Func<InventorySlot, uint> <>9__23_0;
            public static Func<InventorySlot, uint> <>9__23_1;
            public static Func<OpenOrder, bool> <>9__7_0;
            public static Func<EmailUIMessage, bool> <>9__9_0;

            // Methods
            internal bool <get_CanSend>b__9_0(EmailUIMessage w)
        {
            return (w.get_NumAttachedItems() > 0);
        }

        internal Item<GetMailItems> b__10_1(InventorySlot items)
        {
            return items.get_Item();
        }

        internal Item<GetSalvageableItems> b__16_0(InventorySlot invSlot)
        {
            return invSlot.get_Item();
        }

        internal bool <GetUnboundMailItems>b__11_0(Item w)
        {
            return (!w.IsItemFlagActive(1) && !w.IsItemFlagActive(8));
        }

        internal bool <StockAllAD>b__7_0(OpenOrder o)
        {
            return (o.get_OrderID() > 0);
        }

        internal uint <TryToDoSpace>b__23_0(InventorySlot s)
        {
            return s.get_Item().get_Count();
        }

        internal uint <TryToDoSpace>b__23_1(InventorySlot s)
        {
            return s.get_Item().get_ItemDef().get_Level();
        }
    }
}
}

