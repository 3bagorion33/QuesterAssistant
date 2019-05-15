using Astral;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Panels;
using System;
using System.IO;
using System.Windows.Forms;

namespace QuesterAssistant.Classes
{
    internal abstract class ACore<TData, TForm> : ICore
        where TData : NotifyHashChanged, IParse<TData>, new()
        where TForm : CoreForm, new()
    {
        public TData Data { get; set; } = new TData();
        public TForm Panel => new TForm();
        public string Name => Data.GetType().Name.Replace("Data", "");
        public event EventHandler SettingsLoaded;

        protected abstract bool IsValid { get; }
        protected abstract bool HookEnableFlag { get; }
        protected abstract void KeyboardHook(object sender, KeyEventArgs e);

        public ACore()
        {
            if (!LoadSettings()) Data.Init();
            Data.HashChanged += HookToggle;
        }

        protected void HookToggle()
        {
            if (HookEnableFlag)
            {
                Core.KeyboardHook.KeyDown += KeyboardHook;
                return;
            }
            Core.KeyboardHook.KeyDown -= KeyboardHook;
        }

        public bool LoadSettings()
        {
            var flag = false;
            var path = Path.Combine(Core.SettingsPath, $"{Name}.xml");

            if (File.Exists(path))
            {
                try
                {
                    if (!IsValid) Data.Init();
                    TData data = Astral.Functions.XmlSerializer.Deserialize<TData>(path);
                    Data.Parse(data);

                    if (IsValid)
                    {
                        Logger.WriteLine($"{Name}.xml has been loaded...");
                        SettingsLoaded?.Invoke(this, new EventArgs());
                        flag = true;
                    }
                    else
                    {
                        Logger.WriteLine($"{Name}.xml is wrong, using default...");
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    ErrorBox.Show("Error: Could not read file from disk. Original error:\n" + msg);
                    Logger.WriteLine($"{Name}.xml is wrong, using default...");
                }
                return flag;
            }
            Logger.WriteLine($"{Name}.xml not found, using default...");
            return flag;
        }

        public void SaveSettings()
        {
            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Core.SettingsPath, $"{Name}.xml"), Data);
                Logger.WriteLine($"{Name}.xml has been saved...");
            }
            catch (Exception ex)
            {
                ErrorBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
}
