using Astral;
using MyNW.Classes;
using MyNW.Classes.ItemProgression;
using MyNW.Internals;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QuesterAssistant.UpgradeManager
{
    internal class UpgradeManagerCore : ACore<UpgradeManagerData, UpgradeManagerForm>
    {
        protected override bool IsValid => Data.Profiles?.Any() ?? false;
        protected override bool HookEnableFlag => false;



        protected override void KeyboardHook(object sender, KeyEventArgs e)
        {
        }
    }
}
