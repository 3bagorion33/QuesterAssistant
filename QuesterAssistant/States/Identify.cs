using Astral;
using Astral.Logic.Classes.FSM;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using System;
using System.Linq;
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
                            var scroll = scrolls.FindLast(s => s.Item.ItemDef.Level >= x.Item.AlgoItemProps.MinLevel) ?? new InventorySlot(IntPtr.Zero);
                            if (!scroll.IsValid)
                                return;
                            Logger.WriteLine($"Identifying '{x.Item.ItemDef.DisplayName}' ...");
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
