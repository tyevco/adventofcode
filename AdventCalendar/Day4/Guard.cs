using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCalendar.Day4
{
    public class Guard
    {
        public IDictionary<string, int[]> TimeEntries { get; set; } = new Dictionary<string, int[]>();

        public IList<LogEntry> LogEntries { get; set; } = new List<LogEntry>();

        public string Id { get; set; }

        public GuardStatus GetState(string date, int minute)
        {
            if (TimeEntries.ContainsKey(date))
            {
                var minutes = TimeEntries[date];

                if (minutes[minute] > 0)
                {
                    return GuardStatus.Sleep;
                }
                else
                {
                    return GuardStatus.Awake;
                }
            }
            else
            {
                return GuardStatus.Unknown;
            }

        }

        public int GetTimeMostAsleep()
        {
            Dictionary<int, int> totalMinuteCounts = new Dictionary<int, int>();

            foreach (var Entry in TimeEntries)
            {
                for (int i = 0; i < Entry.Value.Length; i++)
                {
                    var minute = Entry.Value[i];

                    if (totalMinuteCounts.ContainsKey(i))
                    {
                        totalMinuteCounts[i] += minute;
                    }
                    else
                    {
                        totalMinuteCounts.Add(i, minute);
                    }
                }
            }

            return totalMinuteCounts.FirstOrDefault(x => x.Value >= totalMinuteCounts.Max(y => y.Value)).Key;
        }

        public int GetTotalTimeAsleep()
        {
            var totalMinutes = 0;
            foreach (var Entry in TimeEntries)
            {
                foreach (var minute in Entry.Value)
                {
                    totalMinutes += minute;
                }
            }

            return totalMinutes;
        }

        public int Answer
        {
            get
            {
                return int.Parse(Id) * GetTimeMostAsleep();
            }
        }
    }
}
