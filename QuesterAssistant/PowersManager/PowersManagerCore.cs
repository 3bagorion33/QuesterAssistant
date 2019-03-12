using Astral;
using DevExpress.XtraEditors;
using MyNW;
using QuesterAssistant.Classes;
using QuesterAssistant.Classes.Common;
using QuesterAssistant.Enums;
using QuesterAssistant.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QuesterAssistant.PowersManager
{
    internal class PowersManagerCore
    {
        public PowersManagerData Data { get; set; } = new PowersManagerData();

        public PowersManagerCore()
        {
            Data.OnChanged += HookToggle;
            if (!LoadSettings()) Init();
        }

        private void HookToggle()
        {
            if (Data.HotKeysEnabled)
            {
                Logger.WriteLine("Powers Manager hotkeys enabled...");
                Core.KeyboardHook.KeyDown += KeyboardHook;
                return;
            }
            Logger.WriteLine("Powers Manager hotkeys disabled...");
            Core.KeyboardHook.KeyDown -= KeyboardHook;
        }

        private void Init()
        {
            Data.CharClassesList = new List<CharClass>
            {
                { new CharClass(ParagonCategory.CW_Masterofflame) },
                { new CharClass(ParagonCategory.CW_Spellstormmage) },
                { new CharClass(ParagonCategory.DC_Anointedchampion) },
                { new CharClass(ParagonCategory.DC_Divineoracle) },
                { new CharClass(ParagonCategory.GF_Ironvanguard) },
                { new CharClass(ParagonCategory.GF_Swordmaster) },
                { new CharClass(ParagonCategory.GW_Ironvanguard) },
                { new CharClass(ParagonCategory.GW_Swordmaster) },
                { new CharClass(ParagonCategory.HR_Pathfinder) },
                { new CharClass(ParagonCategory.HR_Stormwarden) },
                { new CharClass(ParagonCategory.OP_Oathofdevotion) },
                { new CharClass(ParagonCategory.OP_Oathofprotection) },
                { new CharClass(ParagonCategory.SW_Hellbringer) },
                { new CharClass(ParagonCategory.SW_Soulbinder) },
                { new CharClass(ParagonCategory.TR_Masterinfiltrator) },
                { new CharClass(ParagonCategory.TR_Whisperknife) }

            };
            Data.HotKeysEnabled = false;
        }
        protected internal bool LoadSettings()
        {
            var flag = false;
            var path = Path.Combine(Core.SettingsPath, "PowersManager.xml");

            if (File.Exists(path))
            {
                PowersManagerData pManager;
                try
                {
                    pManager = Astral.Functions.XmlSerializer.Deserialize<PowersManagerData>(path);
                    if (pManager.CharClassesList == null)
                    {
                        Logger.WriteLine("Powers Manager settings is wrong, using default...");
                    }
                    else
                    {
                        Logger.WriteLine("Powers Manager settings has been loaded...");
                        Data.CharClassesList = pManager.CharClassesList;
                        Data.HotKeysEnabled = pManager.HotKeysEnabled;
                        Data.HotKeys = pManager.HotKeys;
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Logger.WriteLine("Powers Manager settings is wrong, using default...");
                }
                return flag;
            }
            Logger.WriteLine("Powers Manager settings not found, using default...");
            return flag;
        }
        protected internal void SaveSettings()
        {
            try
            {
                Astral.Functions.XmlSerializer.Serialize(Path.Combine(Core.SettingsPath, "PowersManager.xml"), Data);
                Logger.WriteLine("Powers Manager settings has been saved...");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
            }
        }
        private void KeyboardHook(object sender, KeyEventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessById((int)Memory.ProcessId).MainWindowHandle == NativeMethods.GetForegroundWindow())
            {
                Preset _pres;
                if (e.KeyData == Data.Keys)
                {
                    string _name = InputBox.MessageText("Type partial name of preset and press Enter:", center: true);
                    _pres = Data.CurrPresets?.Find(x => x.Name.CaseContains(_name));
                }
                else
                {
                    _pres = Data.CurrPresets?.Find(x => x.Keys == e.KeyData);
                }
                if (_pres != null)
                {
                    Logger.WriteLine("Applying preset with name '" + _pres.Name + "'...");
                    Powers.ApplyPowers(_pres?.PowersList);
                }
            }
        }
    }
}
