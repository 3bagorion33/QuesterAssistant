﻿using Astral.Forms;
using QuesterAssistant.Classes;
using System;

namespace QuesterAssistant.Panels
{
    internal class CoreForm : BasePanel
    {
        protected ICore core;

        public CoreForm() : base(string.Empty) { }
        public CoreForm(ICore core) : base(core.Name)
        {
            this.core = core;
            Dock = System.Windows.Forms.DockStyle.Fill;
            Main.LoadSettings += LoadSettings;
            Main.SaveSettings += SaveSettings;
        }
        // protected из-за атавизма Power Manager
        protected void SaveSettings(object sender, EventArgs e)
        {
            if (sender == Parent)
            {
                core.SaveSettings();
            }
        }
        protected void LoadSettings(object sender, EventArgs e)
        {
            if (sender == Parent)
            {
                ActiveControl = null;
                core.LoadSettings();
            }
        }
    }
}