using Astral.Classes.ItemFilter;
using QuesterAssistant.Enums;

namespace QuesterAssistant.Classes
{
    public interface IMailAction
    {
        bool OnlyDeleteEmptyMails { get; }
        MailCollectFilterTypeExt FilterType { get; }
        string CleanUpRegex { get; }
        ItemFilterCore ItemFilter { get; }
        LogicType Logic { get; }
    }
}