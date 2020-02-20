using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Astral.Logic.UCC.Actions;
using Astral.Logic.UCC.Classes;
using DevExpress.XtraEditors;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes.Reflection;
using static Astral.Logic.UCC.Ressources.Enums;

namespace QuesterAssistant.Classes.Patches
{
    public partial class AddClass : XtraForm
    {
        private AddClass(Type usedType)
        {
            InitializeComponent();
            foreach (Type type in typeof(Astral.Core).Assembly.GetTypes())
            {
                if (type.BaseType == typeof(UCCAction))
                {
                    actions.Add(type.Name, type);
                    typesList.Items.Add(type.Name);
                }
            }

            var pluginsTypes = typeof(Astral.Controllers.Plugins).ExecStaticMethod("GetTypes") as List<Type>;
            foreach (Type type in pluginsTypes)
            {
                if (type.BaseType == typeof(UCCAction))
                {
                    actions.Add(type.Name, type);
                    typesList.Items.Add(type.Name);
                }
            }

            typesList.SelectedIndex = typesList.Items.Count - 1;
        }

        internal static object Show(Type type)
        {
            object result;
            try
            {
                AddClass addClass = new AddClass(type)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                addClass.ShowDialog();
                if (!addClass.valid)
                {
                    result = null;
                }
                else
                {
                    UCCAction uccaction = Activator.CreateInstance(addClass.actions[addClass.selecteType]) as UCCAction;
                    string a = addClass.typesList.SelectedItem.ToString();
                    if (a == typeof(Spell).Name)
                    {
                        Spell spell = uccaction as Spell;
                        string a2 = addClass.valuesList.SelectedItem.ToString();
                        List<PowerDef> list = new List<PowerDef>();
                        list.AddRange(EntityManager.LocalPlayer.Character.Powers.Select(p => p.PowerDef));
                        list.AddRange(Astral.Logic.NW.Powers.CurrentPlayerClassPowers);
                        foreach (PowerDef powerDef in list)
                        {
                            if (a2 == addClass.PowerDisplayName(powerDef))
                            {
                                spell.SpellID = powerDef.InternalName;
                                return spell;
                            }
                        }
                    }
                    if (a == typeof(Special).Name)
                    {
                        Special special = uccaction as Special;
                        special.Action = (SpecialUCCAction)addClass.valuesList.SelectedItem;
                        result = special;
                    }
                    else if (a == typeof(Dodge).Name)
                    {
                        Dodge dodge = uccaction as Dodge;
                        dodge.Direction = (DodgeDirection)addClass.valuesList.SelectedItem;
                        result = dodge;
                    }
                    else
                    {
                        if (a == typeof(Consumables).Name)
                        {
                            Consumables consumables = uccaction as Consumables;
                            string a3 = addClass.valuesList.SelectedItem.ToString();
                            foreach (InventorySlot inventorySlot in EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.ArtifactPrimary).GetItems)
                            {
                                if (a3 == inventorySlot.Item.ItemDef.DisplayName)
                                {
                                    consumables.ItemId = inventorySlot.Item.ItemDef.InternalName;
                                    return consumables;
                                }
                            }
                        }
                        result = uccaction;
                    }
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            valid = true;
            selecteType = typesList.Text;
            Close();
        }

        private string PowerDisplayName(PowerDef powerDef)
        {
            string text = string.Empty;
            if (Astral.Logic.NW.Powers.IsSlotted(powerDef))
            {
                text += "[Slotted] ";
            }

            return $"{text}{powerDef.DisplayName} [{powerDef.InternalName}]";
        }

        private void typesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = typesList.SelectedItem.ToString();
            valuesList.Items.Clear();
            if (a == typeof(Spell).Name)
            {
                List<PowerDef> list = new List<PowerDef>();
                list.AddRange(EntityManager.LocalPlayer.Character.Powers.Select(p => p.PowerDef));
                list.AddRange(Astral.Logic.NW.Powers.CurrentPlayerClassPowers);
                foreach (PowerDef powerDef in list)
                {
                    if (powerDef.IsAtWill || powerDef.IsDaily || powerDef.IsEncounter || powerDef.InternalName.StartsWith("Artifact"))
                    {
                        string item = PowerDisplayName(powerDef);
                        if (!valuesList.Items.Contains(item))
                        {
                            valuesList.Items.Add(item);
                        }
                    }
                }
            }
            if (a == typeof(Consumables).Name)
            {
                foreach (InventorySlot inventorySlot in EntityManager.LocalPlayer.GetInventoryBagById(InvBagIDs.ArtifactPrimary).GetItems)
                {
                    if (!valuesList.Items.Contains(inventorySlot.Item.ItemDef.DisplayName))
                    {
                        valuesList.Items.Add(inventorySlot.Item.ItemDef.DisplayName);
                    }
                }
            }
            if (a == typeof(Special).Name)
            {
                foreach (object obj in Enum.GetValues(typeof(SpecialUCCAction)))
                {
                    SpecialUCCAction specialUCCAction = (SpecialUCCAction)obj;
                    valuesList.Items.Add(specialUCCAction);
                }
            }
            if (a == typeof(Dodge).Name)
            {
                foreach (object obj in Enum.GetValues(typeof(DodgeDirection)))
                {
                    DodgeDirection dodgeDirection = (DodgeDirection)obj;
                    valuesList.Items.Add(dodgeDirection);
                }
            }
            if (valuesList.Items.Count > 0)
            {
                valuesList.SelectedIndex = 0;
            }
        }
    }
}
