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
        private static readonly List<Message> buffMessages = new List<Message>();
        [NonSerialized]
        private readonly Timer resetTimer = new Timer()
        {
            Enabled = true,
        };

        public override bool IsValid
        {
            get
            {
                var msg = buffMessages.FindLast(x => (Channel == ChatLogEntryType.Unknown || x.Channel == Channel) && Regex.IsMatch(x.Text, MessageRegex));
                if (msg != null)
                {
                    lastMatchedMessage = msg;
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

        private void OnChatMessage(ChatLogEntryType channel, string msg)
        {
            const int buffSize = 10;
            if (buffMessages.Count > buffSize)
                buffMessages.RemoveRange(0, buffMessages.Count - buffSize);
            buffMessages.AddOrReplace(x => x.Channel == channel && x.Text == msg, new Message { Channel = channel, Text = msg });
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
