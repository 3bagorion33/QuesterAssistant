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
        public static string CollectLabel(IMailCollectAction a)
        {
            var label = "MailCollect : ";
            if (!string.IsNullOrEmpty(a.CleanUpRegex))
                label += $"[{a.CleanUpRegex}]";
            if (!string.IsNullOrEmpty(a.CleanUpRegex) && !string.IsNullOrEmpty(a.ItemPattern))
                label += a.Logic == LogicType.Conjunction ? " && " : " || ";
            if (!string.IsNullOrEmpty(a.ItemPattern))
                label += $"[{a.ItemPattern}]";

            return label;
        }

        public static Action.ActionValidity CollectValidity(IMailCollectAction a)
        {
            switch (a.Logic)
            {
                case LogicType.Conjunction:
                    if (string.IsNullOrEmpty(a.CleanUpRegex) || string.IsNullOrEmpty(a.ItemPattern))
                        return new Action.ActionValidity($"{nameof(a.CleanUpRegex)} and {nameof(a.ItemPattern)} should be not empty!");
                    break;
                case LogicType.Disjunction:
                    if (string.IsNullOrEmpty(a.CleanUpRegex) && string.IsNullOrEmpty(a.ItemPattern))
                        return new Action.ActionValidity($"{nameof(a.CleanUpRegex)} or {nameof(a.ItemPattern)} should be not empty!");
                    break;
            }
            return new Action.ActionValidity();
        }

        public static Action.ActionResult CollectProcess(IMailCollectAction a)
        {
            foreach (var email in Email.Mails.OrderByDescending(m => m.MessageId))
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
                //bool isAttached = email.Message.GetAttachedItems().Any(i => a.ItemFilter.IsMatch(i));
                bool isAttached = email.Message.GetAttachedItems().Any(i => Regex.IsMatch(i.ItemDef.InternalName, a.ItemPattern));
                switch (a.Logic)
                {
                    case LogicType.Conjunction:
                        isMatch = Regex.IsMatch(input, a.CleanUpRegex) && isAttached;
                        break;
                    case LogicType.Disjunction:
                        isMatch = Regex.IsMatch(input, a.CleanUpRegex) || isAttached;
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
                    Pause.Sleep(500);
                    try
                    {
                        if (email.NumAttachedItems == 0)
                        {
                            Logger.WriteLine($"Delete mail : {email.Subject}");
                            email.DeleteMessage();
                        }
                    }
                    catch { }
                    Pause.Sleep(500);
                    return Action.ActionResult.Running;
                }
            }
            return Action.ActionResult.Completed;
        }
    }
}