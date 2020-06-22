using System;
using System.Collections.Generic;
using System.Linq;
using Astral.Logic.Classes.Map;
using MyNW.Classes;
using System.IO;
using System.ComponentModel;
using QuesterAssistant.UIEditors;
using System.Drawing.Design;
using MyNW.Patchables.Enums;
using MyNW.Internals;
using QuesterAssistant.Classes;
using Astral.Classes.ItemFilter;
using QuesterAssistant.Classes.ItemFilter;
using Astral;
using QuesterAssistant.Classes.Extensions;

namespace QuesterAssistant.Actions
{
    public class InventoryLog : Astral.Quester.Classes.Action
    {
        public override string ActionLabel => $"{GetType().Name} : [{LogName}]";
        public override string Category => Core.Category;
        public override string InternalDisplayName => string.Empty;
        public override bool NeedToRun => true;
        public override bool UseHotSpots => false;
        protected override bool IntenalConditions => true;
        protected override Vector3 InternalDestination => new Vector3();
        protected override ActionValidity InternalValidity
        {
            get
            {
                foreach (var c in Path.GetInvalidFileNameChars())
                    if (LogName.Contains(c))
                        return new ActionValidity($"Invalid char {c} in {nameof(LogName)}!");
                foreach (var c in Path.GetInvalidPathChars())
                    if (LogPath.Contains(c))
                        return new ActionValidity($"Invalid char {c} in {nameof(LogPath)}!");
                return new ActionValidity();
            }
        }

        public override void GatherInfos() { }
        public override void InternalReset() { }
        public override void OnMapDraw(GraphicsNW graph) { }

        public string LogPath { get; set; }
        public string LogName { get; set; }

        [Description("Default is %displayName%;%internalName%;%isBound%;%itemCount%;%itemPrice%")]
        public string Mask { get; set; }

        [Description("Items to log. All if empty")]
        [Editor(typeof(ItemFilterEditor), typeof(UITypeEditor))]
        public ItemFilterCore ItemsFilter { get; set; } = new ItemFilterCore();

        [Description("Choose bags in which to do")]
        [Editor(typeof(CheckedListBoxEditor<InvBagIDs>), typeof(UITypeEditor))]
        public CheckedListBoxSelector<InvBagIDs> SpecificBags { get; set; } = new CheckedListBoxSelector<InvBagIDs>();

        public InventoryLog()
        {
            LogName = "InventoryLog";
            LogPath = string.Empty;
            Mask = "%displayName%;%internalName%;%isBound%;%itemCount%;%itemPrice%";
        }

        public override ActionResult Run()
        {

            var curAccount = EntityManager.LocalPlayer.AccountLoginUsername;
            var curChar = EntityManager.LocalPlayer.Name;
            int AccStart = -1, AccEnd = -1;
            int CharStart = -1, CharEnd = -1;
            var fullFilePath = Path.Combine(Astral.Controllers.Directories.AstralStartupPath, LogPath, LogName + ".txt");

            //check or create file 
            try
            {
                if (!File.Exists(fullFilePath))
                    File.Create(fullFilePath);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Failed to access the file");
                Logger.WriteLine(ex.ToString());
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
            }
            System.Threading.Thread.Sleep(500);
            return ActionResult.Completed;
        }

        private List<string> GetCharItems()
        {
            List<string> tempItems = new List<string>();
            if (EntityManager.LocalPlayer.IsValid)
            {
                SpecificBags.Items.ForEach(b => AddItems(b, tempItems));
            }
            return tempItems;
        }
        private void AddItems(InvBagIDs bagID, List<string> data)
        {
            if (EntityManager.LocalPlayer.GetInventoryBagById(bagID).IsValid)
            {
                var items = EntityManager.LocalPlayer.GetInventoryBagById(bagID).GetItems;
                if (ItemsFilter.Entries.Count > 0)
                    items = items.FindAll(x => ItemsFilter.IsMatch(x.Item));

                if (items.Count > 0)
                {
                    data.Add($"[{bagID}]");
                    foreach (var s in items)
                    {
                        var item = s.Item;
                        string line = Mask;
                        line = line.Replace("%itemCount%", item.Count.ToString());
                        line = line.Replace("%internalName%", item.ItemDef.InternalName);
                        line = line.Replace("%displayName%", item.DisplayName);
                        line = line.Replace("%isBound%", item.IsBound.ToString());
                        if (line.Contains("%itemPrice%"))
                        {
                            var auctionSearch = new AuctionSearch(item.ItemDef);
                            var lots = auctionSearch.Result.Lots;
                            Logger.WriteLine(auctionSearch.LoggerMessage.CarryOnLength());
                            var price = lots.Any() ? lots.First().PricePerItem.ToString() : "null";
                            line = line.Replace("%itemPrice%", price);
                        }
                        data.Add(line);
                    }
                }
            }
        }
    }
}
