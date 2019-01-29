using Astral.Quester.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Utils.Extensions;
using Astral.Classes;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class ReceivedChatMessage : Condition
    {
        private Message lastMatchedMessage;
        private bool isMatched = false;
        private static List<Message> buffMessages = new List<Message>();
        private Timeout resetTimer = new Timeout(2000);

        private string DisplayName => GetType().Name;
        public ChatLogEntryType Channel { get; set; }
        public string MessageRegex { get; set; }
        public override bool IsValid
        {
            get
            {
                Debug.WriteLine(DisplayName + ": IsValid");
                if (resetTimer.IsTimedOut)
                    isMatched = false;
                return !isMatched;
            }
        }

        public override string TestInfos => string.Format("Last matched message : [{0}] {1}", lastMatchedMessage.Channel, lastMatchedMessage.Text);

        public ReceivedChatMessage()
        {
            ChatManager.OnChatMessage += OnChatMessage;
        }

        private void OnChatMessage(ChatLogEntryType _channel, string _msg)
        {
            const int BUFF_SIZE = 10;
            if (buffMessages.Count > BUFF_SIZE)
                buffMessages.RemoveRange(0, buffMessages.Count - BUFF_SIZE);
            buffMessages.AddOrReplace(x => x.Channel == _channel && x.Text == _msg, new Message() { Channel = _channel, Text = _msg });
        }

        public override string ToString()
        {
            return DisplayName + ": [" + Channel + "] " + MessageRegex;
        }

        public override void Reset()
        {
            Debug.WriteLine(DisplayName + ": Reset()");
            var _msg = buffMessages.FindLast(x => x.Channel == Channel && Regex.IsMatch(x.Text, MessageRegex));
            if (_msg is null)
            {
                buffMessages.Clear();
                return;
            }
            Debug.WriteLine(DisplayName + " => [" + _msg.Channel + "] " + _msg.Text);
            lastMatchedMessage = _msg;
            isMatched = true;
            resetTimer.Reset();
            buffMessages.Clear();
        }

        private class Message
        {
            protected internal ChatLogEntryType Channel { get; set; }
            protected internal string Text { get; set; }
        }
    }
}
