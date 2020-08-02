using System.Linq;
using System.Text.RegularExpressions;
using Astral;
using MyNW.Internals;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.ItemFilter;
using QuesterAssistant.Enums;
using Action = Astral.Quester.Classes.Action;

namespace QuesterAssistant.Classes
{
    internal static class EmailHelper
    {
        public static Action.ActionResult Process(IMailAction a)
        {
            foreach (var email in Email.Mails)
            {
                if (a.OnlyDeleteEmptyMails && email.NumAttachedItems == 0)
                {
                    email.DeleteMessage();
                    Pause.Sleep(300);
                    return Action.ActionResult.Running;
                }
                string input = string.Empty;
                switch (a.FilterType)
                {
                    case MailCollectFilterTypeExt.Body:
                        input = email.Body;
                        break;
                    case MailCollectFilterTypeExt.Subject:
                        input = email.Subject;
                        break;
                    case MailCollectFilterTypeExt.Sender:
                        input = email.Sender;
                        break;
                }
                bool isMatch = false;
                switch (a.Logic)
                {
                    case LogicType.Conjunction:
                        isMatch = Regex.IsMatch(input, a.CleanUpRegex) && email.Message.GetAttachedItems().Any(i => a.ItemFilter.IsMatch(i));
                        break;
                    case LogicType.Disjunction:
                        isMatch = Regex.IsMatch(input, a.CleanUpRegex) || email.Message.GetAttachedItems().Any(i => a.ItemFilter.IsMatch(i));
                        break;
                }
                if (email != null && email.IsValid && isMatch)
                {
                    try
                    {
                        if (email.NumAttachedItems > 0)
                            email.TakeAttachedItems();
                    }
                    catch
                    {
                        Logger.WriteLine("Failed to collect items ...");
                    }
                    Pause.Sleep(300);
                    try
                    {
                        if (email.NumAttachedItems == 0)
                        {
                            Logger.WriteLine($"Delete mail : {email.Subject}");
                            email.DeleteMessage();
                        }
                    }
                    catch { }
                    Pause.Sleep(300);
                    return Action.ActionResult.Running;
                }
            }
            return Action.ActionResult.Completed;
        }
    }
}