using System;

namespace AdventCalendar2018.D04
{
    public class DataEntry
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public string ToString()
        {
            return $"[{Timestamp}] {Message}";
        }
    }
}
