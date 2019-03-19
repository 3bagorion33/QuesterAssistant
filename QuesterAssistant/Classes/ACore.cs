using Astral;
using DevExpress.XtraEditors;
using QuesterAssistant.Classes.Common;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace QuesterAssistant.Classes
{
    internal abstract class ACore<T> where T : NotifyHashChanged , IParse<T>
    {
        public abstract T Data { get; set; }
        protected abstract bool IsValid { get; }
        protected abstract bool HookEnableFlag { get; }
        protected abstract void KeyboardHook(object sender, KeyEventArgs e);

        protected string DataName => Data.GetType().Name;

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
            var path = Path.Combine(Core.SettingsPath, $"{DataName}.xml");

            if (File.Exists(path))
            {
                try
                {
                    if (!IsValid) Data.Init();
                    T data = Astral.Functions.XmlSerializer.Deserialize<T>(path);
                    Data.Parse(data);

                    if (IsValid)
                    {
                        Logger.WriteLine($"{DataName} has been loaded...");
                        flag = true;
                    }
                    else
                    {
                        Logger.WriteLine($"{DataName} is wrong, using default...");
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    XtraMessageBox.Show("Error: Could not read file from disk. Original error:\n" + msg);
                    Logger.WriteLine($"{DataName} is wrong, using default...");
                }
                return flag;
            }
            Logger.WriteLine($"{DataName}.xml not found, using default...");
            return flag;
        }

        public void SaveSettings()
        {
            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Core.SettingsPath, $"{DataName}.xml"), Data);
                Logger.WriteLine($"{DataName} has been saved...");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
}
