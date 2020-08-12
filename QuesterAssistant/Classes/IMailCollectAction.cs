using Astral.Classes.ItemFilter;
using QuesterAssistant.Enums;

namespace QuesterAssistant.Classes
{
    public interface IMailCollectAction
    {
        bool OnlyDeleteEmptyMails { get; }
        MailCollectFilterTypeExt FilterType { get; }
        string CleanUpRegex { get; }
        ItemFilterCore ItemFilter { get; }
        string ItemPattern { get; }
        LogicType Logic { get; }
    }
}