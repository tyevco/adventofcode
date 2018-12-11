using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Day04
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
