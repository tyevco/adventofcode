using System;

namespace AdventCalendar.Day04
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public GuardStatus State { get; set; }
    }
}