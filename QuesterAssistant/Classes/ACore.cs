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
        public TForm Panel { get; } = new TForm();
        public string Name => Data.GetType().Name.Replace("Data", "");
        public event Action SettingsLoaded;

        protected abstract bool IsValid { get; }
        protected abstract bool HookEnableFlag { get; }
        protected virtual void KeyboardHookDown(KeyEventArgs e) { }

        protected virtual void KeyboardHookPress(KeyPressEventArgs e) { }

        protected ACore()
        {
            Panel.Init(this);
            if (!LoadSettings()) Data.Init();
            Core.KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            Core.KeyboardHook.KeyPress += KeyboardHook_KeyPress;
        }

        private void KeyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (HookEnableFlag) KeyboardHookPress(e);
        }

        private void KeyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (HookEnableFlag) KeyboardHookDown(e);
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
                    using (TData data = Astral.Functions.XmlSerializer.Deserialize<TData>(path))
                        Data.Parse(data);

                    if (IsValid)
                    {
                        Logger.WriteLine($"{Name}.xml has been loaded...");
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
                    QMessageBox.ShowError("Error: Could not read file from disk. Original error:\n" + msg);
                    Logger.WriteLine($"{Name}.xml is wrong, using default...");
                }
                finally
                {
                    SettingsLoaded?.Invoke();
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
                QMessageBox.ShowError("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
    interface ICore
    {
        bool LoadSettings();
        void SaveSettings();
        string Name { get; }
    }
}
