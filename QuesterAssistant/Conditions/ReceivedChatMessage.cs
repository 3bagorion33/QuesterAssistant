using Astral.Quester.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using QuesterAssistant.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DevExpress.Utils.Extensions;
using System.Windows.Forms;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class ReceivedChatMessage : Condition
    {
        private string DisplayName => GetType().Name;
        private Message lastMatchedMessage = new Message();
        private bool isMatched = false;
        private static List<Message> buffMessages = new List<Message>();
        private Timer resetTimer = new Timer()
        {
            Enabled = true,
            Interval = 1000,
        };
        public ChatLogEntryType Channel { get; set; }
        public string MessageRegex { get; set; }
        public override bool IsValid
        {
            get
            {
                var _msg = buffMessages.FindLast(x => ((Channel != ChatLogEntryType.Unknown) ? x.Channel == Channel : true) && Regex.IsMatch(x.Text, MessageRegex));
                if (_msg != null)
                {
                    Debug.WriteLine(DisplayName + " => [" + _msg.Channel + "] " + _msg.Text);
                    lastMatchedMessage = _msg;
                    isMatched = true;
                }
                Debug.WriteLine(DisplayName + ": IsValid => " + !isMatched);
                return !isMatched;
            }
        }

        public override string TestInfos => string.Format("Last matched message : [{0}] {1}", lastMatchedMessage?.Channel, lastMatchedMessage?.Text);

        public ReceivedChatMessage()
        {
            ChatManager.OnChatMessage += OnChatMessage;
            resetTimer.Start();
            resetTimer.Tick += ResetTimer_Tick;
        }

        private void ResetTimer_Tick(object sender, EventArgs e)
        {
            Reset();
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
            buffMessages.Clear();
            isMatched = false;
        }

        private class Message
        {
            protected internal ChatLogEntryType Channel { get; set; }
            protected internal string Text { get; set; }
        }
    }
}
