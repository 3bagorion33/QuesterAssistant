using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;

namespace QuesterAssistant.Classes
{
    public static class Insignia
    {
        public static List<InventorySlot> EquippedMounts =>
            EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.MountEquippedActiveSlots).GetItems;
        public static List<InventorySlot> PassiveMounts =>
            EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.MountActiveSlots).GetItems;

        private static int GetInsigniasId(Item item, Func<Type, int> getCount)
        {
            if (!item.ItemDef.Categories.Contains(ItemCategory.Mount)) return -1;
            int dig = 1;
            int result = 0;
            foreach (Type id in Enum.GetValues(typeof(Type)))
            {
                if (id == Type.Universal || id == Type.Unknown) continue;
                result += dig * getCount(id);
                dig *= 10;
            }
            return result;
        }

        private static void MountMove(InventorySlot mount, InventorySlot toSlot)
        {
            if (!mount.IsValid || !mount.Filled) return;
            if (Game.IsCursorModeEnabled)
            {
                Game.ToggleCursorMode(false);
                Pause.Sleep(200);
            }
            if (toSlot.Filled)
            {
                toSlot.MoveAll(InvBagIDs.MountActiveSlots);
                Pause.Sleep(200);
            }
            mount.MoveAll(InvBagIDs.MountEquippedActiveSlots, toSlot.Slot);
            Pause.Sleep(200);
        }

        public static BonusType GetCurrentInsigniaBonusType(this Item item)
        {
            if (!item.ItemDef.Categories.Contains(ItemCategory.Mount))
                return BonusType.NotMount;
            if (item.SpecialProps.ItemGemSlots.Count(slot => slot.SlottedItem.IsValid) !=
                item.ItemDef.EffectiveItemGemSlots.Count)
                return BonusType.Unknown;
            return (BonusType) GetInsigniasId(item,
                id => item.SpecialProps.ItemGemSlots.Count(
                    slot => slot.SlottedItem.InternalName.Contains(id.ToString())));
        }
        public static int GetPossibleInsigniaBonuses(this Item item)
        {
            return GetInsigniasId(item,
                id => item.ItemDef.EffectiveItemGemSlots.Count(def => ((Type) def.Type).HasFlag(id)));
        }

        public static bool IsInsigniaBonusPresent(this Item item)
        {
            if (!item.ItemDef.Categories.Contains(ItemCategory.Mount)) return false;
            int result = item.GetPossibleInsigniaBonuses() - (int) item.GetCurrentInsigniaBonusType();
            if (result < 0) return false;
            while (result > 1)
            {
                if (result % 10 > 2) return false;
                result /= 10;
            }
            return true;
        }

        public static void ExtractSlottedInsignias(this Item mount)
        {
            if (mount.ItemDef.Categories.Contains(ItemCategory.Mount))
            {
                var slots = mount.SpecialProps.ItemGemSlots;
                slots.ForEach(s =>
                {
                    mount.UnGemItem((uint) slots.IndexOf(s));
                    Pause.Sleep(100);
                });
            }
        }

        public static void ExtractFromEquippedMounts()
        {
            EquippedMounts.ForEach(s => s.Item.ExtractSlottedInsignias());
        }

        public static void ExtractFromPassiveMounts()
        {
            var firstEquippedSlot = EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.MountEquippedActiveSlots).Slots[0];
            ulong firstEquippedMountId = 0;
            if (firstEquippedSlot.Filled)
                firstEquippedMountId = firstEquippedSlot.Item.Id;

            if (PassiveMounts.Any(s => s.Item.SpecialProps.ItemGemSlots.Any()))
            {
                var passiveMountIns = PassiveMounts.Find(s => s.Item.SpecialProps.ItemGemSlots.Any());
                MountMove(passiveMountIns, firstEquippedSlot);
                firstEquippedSlot.Item.ExtractSlottedInsignias();
            }

            if (firstEquippedMountId != 0)
            {
                var mountToRestore = PassiveMounts.Find(slot => slot.Item.Id == firstEquippedMountId);
                MountMove(mountToRestore, firstEquippedSlot);
            }
        }

        [Flags]
        public enum Type
        {
            Unknown     = 0,
            Crescent    = 0b00001000000000000000000, // Серповидный
            Regal       = 0b00010000000000000000000, // Царственный
            Barbed      = 0b00100000000000000000000, // Шипастый
            Illuminated = 0b01000000000000000000000, // Украшенный
            Enlightened = 0b10000000000000000000000, // Просвещенный
            Universal   = Crescent | Regal | Barbed | Illuminated | Enlightened
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        public enum BonusType
        {
            NotMount = -1,
            Unknown = 0,           // EIBRC
            Alchemists_Invigoration = 02010,    // Укрепляющее для алхимика
            Alchemists_Refresher    = 01010,    // Освежающее для алхимика
            Artificers_Persuasion   = 01200,    // Убеждение декорщика
            Assassins_Covenant      = 20010,    // Соглашение убийцы
            Barbarians_Delight      = 10001,    // Наслаждение варвара
            Barbarians_Revelry      = 20001,    // Блажество варвара
            Berserkers_Anger        = 01100,    // Гнев берсерка
            Berserkers_Rage         = 02100,    // Ярость берсерка
            Cavalrys_Warning        = 10101,    // Предупреждение кавалерии
            Champions_Return        = 21000,    // Возвращение рыцаря
            Champions_Struggle      = 11000,    // Борьба рыцаря
            Combatants_Maneuver     = 01020,    // Маневр бойца
            Gladiators_Guile        = 11010,    // Хитрость гладиатора
            Knights_Defense         = 00011,    // Оборона рыцаря
            Knights_Rebuke          = 00012,    // Отпор рыцаря
            Magistrates_Patience    = 10200,    // Терпение судьи
            Magistrates_Restraint   = 10100,    // Сдержанность судьи
            Oppressors_Reprieve     = 02001,    // Облегчение для угнетателя
            Oppressors_Respite      = 01001,    // Передышка для угнетателя
            Protectors_Camaraderie  = 00120,    // Товарищество защитника
            Protectors_Friendship   = 00110,    // Дружба защитника
            Shepherds_Devotion      = 01110,    // Преданность пастыря
            Slayers_Bloodlust       = 10002,    // Кровожадность истребителя
            Survivors_Blessing      = 00102,    // Благословение выжившего
            Survivors_Gift          = 00101,    // Дар выжившего
            Travelers_Treasures     = 30000,    // Сокровища путешественника
            Victims_Preservation    = 00201,    // Сохранение жертвы
            Wanderers_Fortune       = 01011,    // Удача скитальца
            Warlords_Encouragement  = 10010,    // Воодушевление полководца
            Warlords_Inspiration    = 10020     // Вдохновение полководца
        }
    }
}