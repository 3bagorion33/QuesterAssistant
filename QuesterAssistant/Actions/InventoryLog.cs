using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astral.Logic.Classes.Map;
using Astral.Quester.Classes;
using MyNW.Classes;
using System.IO;

namespace QuesterAssistant
{
    [Serializable]
    public class InventoryLog : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => "InventoryLog";

        public override string InternalDisplayName => string.Empty;

        public override bool NeedToRun => true;

        public override bool UseHotSpots => false;

        protected override bool IntenalConditions => true;

        protected override Vector3 InternalDestination => new Vector3();

        protected override ActionValidity InternalValidity => new ActionValidity();

        public string LogPath { get; set; }
        public string Mask { get; set; }
        public string LogName { get; set; }
        public bool LogBags { get; set; }
        public bool LogBank { get; set; }
        public bool LogCraftingTools { get; set; }
        public bool LogCraftingRes { get; set; }

        public override void GatherInfos() {}

        public override void InternalReset() {}

        public override void OnMapDraw(GraphicsNW graph) {}

        public InventoryLog()
        {
            this.LogName = "InventoryLog";
            this.LogPath = Astral.Controllers.Directories.AstralStartupPath;
            this.LogBags = true;
            this.LogBank = false;
            this.LogCraftingTools = false;
            this.LogCraftingRes = false;
            this.Mask = "%displayName%;%internalName%;%isBound%;%itemCount%";
        }

        public override ActionResult Run()
        {

            var curAccount = MyNW.Internals.EntityManager.LocalPlayer.AccountLoginUsername;
            var curChar = MyNW.Internals.EntityManager.LocalPlayer.Name;
            int AccStart = -1, AccEnd = -1;
            int CharStart = -1, CharEnd = -1;
            var fullFilePath = Path.Combine(LogPath, LogName + ".txt");

            //check or create file 
            try
            {
                if (!File.Exists(fullFilePath))
                    File.Create(fullFilePath);
            }
            catch (Exception ex)
            {
                Astral.Logger.WriteLine("failed to access the file");
                Astral.Logger.WriteLine(ex.ToString());
                return ActionResult.Fail;
            }

            List<string> oldText = new List<string>();
            //read old data
            using (StreamReader sr = new StreamReader(fullFilePath))
            {
                while (!sr.EndOfStream)
                {
                    oldText.Add(sr.ReadLine());
                }
                sr.Close();
            }
            AccEnd = oldText.Count;
            // find account start line
            for (int i = 0; i < oldText.Count; i++)
            {
                if (oldText[i].Contains($"<<<{curAccount}>>>"))
                {
                    AccStart = i;
                }
                if (oldText[i].Contains("<<<") & i > AccStart)
                {
                    AccEnd = i;
                    break;
                }
            }

            // find character start line for existed account
            if (AccStart >= 0)
            {
                for (int i = AccStart; i < AccEnd; i++)
                {
                    if (oldText[i].Contains($"<{curChar}>"))
                    {
                        CharStart = i;
                    }
                    if (oldText[i].Contains("<") & i > CharStart)
                    {
                        CharEnd = i;
                        //break;
                    }
                }
            }

            // write new data
            using (StreamWriter sw = new StreamWriter(fullFilePath))
            {
                var tempCharData = GetCharItems();
                //on new account 
                if (AccStart == -1)
                {
                    oldText.Add($"<<<{curAccount}>>>");
                    CharStart = oldText.Count;
                    CharEnd = -1;
                    //add new chardata
                    oldText.Add($"<{curChar}> date:{DateTime.Now}");

                    for (int i = 0; i < tempCharData.Count; i++)
                    {
                        oldText.Add(tempCharData[i]);
                    }
                }
                //for existed account
                else
                {
                    //on new char
                    if (CharStart == -1)
                    {
                        oldText.Insert(AccEnd, $"<{curChar}> date:{DateTime.Now}");
                        for (int i = 0; i < tempCharData.Count; i++)
                        {
                            oldText.Insert(AccEnd + i + 1, tempCharData[i]);
                        }
                    }
                    //for existed char
                    else
                    {
                        //delete old data
                        if (CharEnd < CharStart)
                            CharEnd = AccEnd;
                        oldText.RemoveRange(CharStart, CharEnd - CharStart);
                        //insert new data
                        oldText.Insert(CharStart, $"<{curChar}> date:{DateTime.Now}");
                        for (int i = 0; i < tempCharData.Count; i++)
                        {
                            oldText.Insert(CharStart + i + 1, tempCharData[i]);
                        }
                    }
                }

                //find character
                //save data
                foreach (var t in oldText)
                {
                    sw.WriteLine(t);
                }
                sw.Close();

            }
            System.Threading.Thread.Sleep(500);
            return ActionResult.Completed;
        }

        private void EditFile()
        {

        }

        private List<string> GetCharItems()
        {
            List<string> tempItems = new List<string>();
            if (MyNW.Internals.EntityManager.LocalPlayer.IsValid)
            {
                if (this.LogBags)
                {
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Inventory, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag1, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag2, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag3, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag4, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag5, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag6, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag7, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag8, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.PlayerBag9, tempItems);
                }
                if (this.LogBank)
                {
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank1, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank2, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank3, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank4, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank5, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank6, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank7, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank8, tempItems);
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.Bank9, tempItems);
                }
                if (this.LogCraftingTools)
                {
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.CraftingInventory, tempItems);
                }
                if (this.LogCraftingRes)
                {
                    AddItems(MyNW.Patchables.Enums.InvBagIDs.CraftingResources, tempItems);
                }
            }
            return tempItems;
        }
        private void AddItems(MyNW.Patchables.Enums.InvBagIDs bagID, List<string> data)
        {
            if (MyNW.Internals.EntityManager.LocalPlayer.GetInventoryBagById(bagID).IsValid)
            {

                var items = MyNW.Internals.EntityManager.LocalPlayer.GetInventoryBagById(bagID).GetItems;
                if (items.Count > 0)
                {
                    data.Add($"[{bagID}]");
                    foreach (var currentSlot in items)
                    {
                        var itemCount = currentSlot.Item.Count;
                        var internalName = currentSlot.Item.ItemDef.InternalName;
                        var displayName = currentSlot.Item.ItemDef.DisplayName;
                        var isBound = currentSlot.Item.IsBound;
                        string line = Mask;
                        line = line.Replace("%itemCount%", itemCount.ToString());
                        line = line.Replace("%internalName%", internalName.ToString());
                        line = line.Replace("%displayName%", displayName.ToString());
                        line = line.Replace("%isBound%", isBound.ToString());
                        data.Add(line);
                    }
                }
            }
        }
    }
}
