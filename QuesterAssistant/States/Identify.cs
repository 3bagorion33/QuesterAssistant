using Astral;
using Astral.Controllers;
using Astral.Logic.Classes.FSM;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuesterAssistant.States
{
    class Identify : State
    {
        public override int Priority => 75;
        public override string DisplayName => GetType().Name;
        public override bool NeedToRun => CheckTO.IsTimedOut;
        public override int CheckInterval => 5000;
        public override bool StopNavigator => false;

        public override void Run()
        {
            Debug.WriteLine(DisplayName + ": tick with Priority=" + Priority + ", CheckInterval=" + CheckInterval);
            if (true)
            {
                var player = EntityManager.LocalPlayer;
                if (player.IsValid)
                {
                    var bagsItems = player.BagsItems;
                    var items = bagsItems.FindAll(x => x.Item.ItemDef.Type == ItemType.UnidentifiedWrapper && x.Item.ItemDef.Quality >= API.CurrentSettings.IdentifyQuality);
                    var scrolls = bagsItems.FindAll(x => x.Item.ItemDef.Type == ItemType.IdentifyScroll);
                    if (items.Any() && scrolls.Any())
                    {
                        items.ForEach(x =>
                        {
                            var scroll = scrolls.FindLast(s => s.Item.ItemDef.Level >= x.Item.AlgoItemProps.MinLevel);
                            if (scroll is null)
                                return;
                            Debug.WriteLine(scroll.Item.DisplayName + " => " + x.Item.AlgoItemProps.MinLevel + " => " + scroll.Item.ItemDef.Level);
                            Logger.WriteLine("Identifying '" + x.Item.ItemDef.DisplayName + "' ...");
                            x.Identify(scroll.Item);
                            Thread.Sleep(200);
                        });
                    }
                }
            }
            CheckTO.Reset();
        }
    }
}
