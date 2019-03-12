using Astral.Quester.Classes;
using MyNW.Internals;
using MyNW.Patchables.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DevExpress.Utils.Extensions;
using System.Windows.Forms;
using System.ComponentModel;

namespace QuesterAssistant.Conditions
{
    [Serializable]
    public class ReceivedChatMessage : Condition
    {
        private string DisplayName => GetType().Name;
        [NonSerialized]
        private Message lastMatchedMessage = new Message();
        private bool isMatched = false;
        private static List<Message> buffMessages = new List<Message>();
        [NonSerialized]
        private Timer resetTimer = new Timer()
        {
            Enabled = true,
        };

        public override bool IsValid
        {
            get
            {
                var _msg = buffMessages.FindLast(x => ((Channel != ChatLogEntryType.Unknown) ? x.Channel == Channel : true) && Regex.IsMatch(x.Text, MessageRegex));
                if (_msg != null)
                {
                    lastMatchedMessage = _msg;
                    isMatched = true;
                }
                return CheckAbsence ? !isMatched : isMatched;
            }
        }

        public override string TestInfos => $"Last matched message : [{lastMatchedMessage?.Channel}] {lastMatchedMessage?.Text}";

        public ReceivedChatMessage()
        {
            ChatManager.OnChatMessage += OnChatMessage;
            resetTimer.Interval = ResultLifeTime;
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
            buffMessages.Clear();
            isMatched = false;
        }

        private class Message
        {
            protected internal ChatLogEntryType Channel { get; set; }
            protected internal string Text { get; set; }
        }

        [Description("Specify channel for monitoring, all if Unknown")]
        public ChatLogEntryType Channel { get; set; }

        [Description("Enter the regex pattern for searching")]
        public string MessageRegex { get; set; }

        [Description("Check absence instead presence")]
        public bool CheckAbsence { get; set; } = true;

        [Description("Lifetime for result, ms")]
        public int ResultLifeTime { get; set; } = 1000;
    }
}
