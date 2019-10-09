using System;

namespace Launcher.Classes
{
    internal class LogEvent
    {
        public DateTime DateTime { get; }
        public string Event { get; }

        public LogEvent(string @event)
        {
            DateTime = DateTime.Now;
            Event = @event;
        }
    }
}
