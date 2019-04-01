namespace QuesterAssistant.Classes
{
    interface ICore
    {
        bool LoadSettings();
        void SaveSettings();
        string Name { get; }
    }
}
