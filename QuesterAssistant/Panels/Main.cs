using Astral;
using Astral.Forms;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using MyNW.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace QuesterAssistant.Panels
{
    public partial class Main : BasePanel
    {
        protected Timer timerIsConnecting;

        protected void Initialize()
        {
            this.components = new Container();
            this.timerIsConnecting = new Timer(this.components)
            {
                Enabled = true,
                Interval = 1000
            };
            this.timerIsConnecting.Tick += new EventHandler(this.IsConnectingTick);
            this.CharClassChanging += new EventHandler(this.FormUpdate);
        }

        protected event EventHandler CharClassChanging;
        protected void OnCharClassChanged()
        {
            CharClassChanging?.Invoke(this, EventArgs.Empty);
        }

        protected CharClassCategory prevCharacterClass;
        protected void IsConnectingTick(object sender, EventArgs e)
        {
            //Core.DebugWriteLine("timerIsConnecting.Tick");
            if (EntityManager.LocalPlayer.IsValid &&
                !EntityManager.LocalPlayer.IsLoading &&
                (EntityManager.LocalPlayer.Character.Class.Category != prevCharacterClass))
            {
                OnCharClassChanged();
                prevCharacterClass = EntityManager.LocalPlayer.Character.Class.Category;
            }
        }

        List<PowerData> powerList = new List<PowerData>();

        protected void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("{0}: {1}",
                "Name",
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("{0}: {1}",
                "Class",
                EntityManager.LocalPlayer.Character.Class.DisplayName);

            //this.gridViewPowers. = EntityManager.LocalPlayer.Character.SlottedPowers;
            //this.gridControlPowers.DataSource = EntityManager.LocalPlayer.Character.SlottedPowers;

            Dictionary<TraySlot, Power> slottedPowers = new Dictionary<TraySlot, Power>();
            slottedPowers.Add(TraySlot.Mechanic, Powers.GetPowerBySlot((int)TraySlot.Mechanic));

            for (int i = 0; i < 10; i++)
            {
                slottedPowers.Add((TraySlot)i, Powers.GetPowerBySlot(i));
            }

            this.gridControlPowers.DataSource = slottedPowers;
        }

        public Main() : base ("Quester Assistant")
        {
            InitializeComponent();
            Initialize();
        }
    }
}
