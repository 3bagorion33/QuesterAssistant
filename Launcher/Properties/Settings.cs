using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using QuesterAssistant.Panels;
using XmlSerializer = Launcher.Classes.XmlSerializer;

namespace Launcher.Properties
{
    [Serializable]
    public class Settings
    {
        private readonly string path = Path.Combine(Application.StartupPath, $"Launcher.xml");
        public ProcessPriorityClass Priority { get; set; } = ProcessPriorityClass.Normal;
        public bool KillCrypticError { get; set; }
        public bool CloseCrashError { get; set; }
        [XmlArrayItem(ElementName = "Active")]
        public List<bool> Patches { get; set; }

        public void Load()
        {
            if (File.Exists(path))
            {
                try
                {
                    Settings data = XmlSerializer.Deserialize<Settings>(path);
                    if (data != null)
                    {
                        Priority = data.Priority;
                        KillCrypticError = data.KillCrypticError;
                        CloseCrashError = data.CloseCrashError;
                        Patches = data.Patches;
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    QMessageBox.ShowError("Error: Could not read file from disk. Original error:\n" + msg);
                }
            }
        }

        public void Save()
        {
            try
            {
                XmlSerializer.Serialize(path, this);
            }
            catch (Exception ex)
            {
                QMessageBox.ShowError("Error: Could not save file. Original error: " + ex.Message);
            }
        }
    }
}