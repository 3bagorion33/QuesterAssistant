using Astral;
using Astral.Forms;
using Astral.Logic.NW;
using DevExpress.Utils.Extensions;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace QuesterAssistant
{
    public partial class Main
    {
        protected IContainer timersContainer;
        protected Timer timerIsConnecting;

        protected void Initialize()
        {
            this.timersContainer = new Container();
            this.timerIsConnecting = new Timer(this.timersContainer)
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

        protected string prevCharacterClassName;
        protected void IsConnectingTick(object sender, EventArgs e)
        {
            //Core.DebugWriteLine("timerIsConnecting.Tick");
            if (EntityManager.LocalPlayer.IsValid &&
                !EntityManager.LocalPlayer.IsLoading &&
                (EntityManager.LocalPlayer.Character.Class.Name != prevCharacterClassName))
            {
                OnCharClassChanged();
                prevCharacterClassName = EntityManager.LocalPlayer.Character.Class.Name;
            }
        }

        protected void FormUpdate(object sender, EventArgs e)
        {
            Core.DebugWriteLine("FormUpdate Event");
            this.labelCharacterName.Text = string.Format("{0}: {1}",
                "Name",
                EntityManager.LocalPlayer.Name);

            this.labelCharacterClass.Text = string.Format("{0}: {1}",
                "Class",
                EntityManager.LocalPlayer.Character.Class.DisplayName);
        }
    }
}