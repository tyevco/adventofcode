using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day04
{
    public class GuardDataParser
    {
        Regex splitRegex = new Regex(@"\[(.+)\] (.+)");
        Regex guardRegex = new Regex(@"Guard #([0-9]+?) begins shift");

        public static IList<Guard> Parse(IList<string> data)
        {
            var processor = new GuardDataParser();
            var guards = processor.parse(data);
            return processor.process(guards);
        }

        private IList<Guard> parse(IList<string> data)
        {
            List<DataEntry> entries = new List<DataEntry>();

            // parse the entries
            foreach (var entryText in data)
            {
                var result = splitRegex.Match(entryText);

                var entry = new DataEntry
                {
                    Timestamp = DateTime.Parse(result.Groups[1].Value),
                    Message = result.Groups[2].Value
                };

                entries.Add(entry);
            }

            // sort by timestamp
            entries.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));

            // extract guard data from entries
            IDictionary<string, Guard> guards = new Dictionary<string, Guard>();
            Guard currentGuard = null;

            foreach (var entry in entries)
            {
                if (entry.Timestamp.Hour == 23)
                {
                    var newTime = entry.Timestamp;
                    newTime = newTime.AddMinutes(-entry.Timestamp.Minute);
                    newTime = newTime.AddHours(1);

                    entry.Timestamp = newTime;
                }

                if (currentGuard != null)
                {
                    if (entry.Message.Equals("falls asleep"))
                    {
                        currentGuard.LogEntries.Add(new LogEntry
                        {
                            Timestamp = entry.Timestamp,
                            State = GuardStatus.Sleep
                        });
                    }
                    else if (entry.Message.Equals("wakes up"))
                    {
                        currentGuard.LogEntries.Add(new LogEntry
                        {
                            Timestamp = entry.Timestamp,
                            State = GuardStatus.Awake
                        });
                    }

                }
                if (entry.Message.EndsWith(" begins shift"))
                {
                    var guardResult = guardRegex.Match(entry.Message);
                    var guardNumber = guardResult.Groups[1].Value;

                    if (guards.ContainsKey(guardNumber))
                    {
                        currentGuard = guards[guardNumber];
                    }
                    else
                    {
                        currentGuard = new Guard()
                        {
                            Id = guardNumber
                        };

                        guards.Add(guardNumber, currentGuard);
                    };

                    currentGuard.LogEntries.Add(new LogEntry
                    {
                        Timestamp = entry.Timestamp,
                        State = GuardStatus.Start
                    });
                }
            }

            return guards.Values.ToList();
        }


        private IList<Guard> process(IList<Guard> guards)
        {
            // process guard times
            foreach (var guard in guards)
            {

                GuardStatus? previousState = null;
                DateTime? previousTime = null;
                foreach (var entry in guard.LogEntries)
                {
                    evaluateStatus(entry.State, entry.Timestamp, guard, previousTime, previousState);

                    previousTime = entry.Timestamp;
                    previousState = entry.State;
                }

                evaluateStatus(GuardStatus.Unknown, null, guard, previousTime, previousState);
            }

            return guards;
        }

        private void evaluateStatus(GuardStatus currentStatus, DateTime? currentTime, Guard guard, DateTime? previousTime, GuardStatus? previousState)
        {
            switch (currentStatus)
            {

                case GuardStatus.Unknown:
                case GuardStatus.Start:
                    if (previousTime != null && previousState == GuardStatus.Sleep)
                    {
                        processTimeEntry(getKey(previousTime.Value), guard, previousTime.Value.Minute, 60);
                    }
                    break;
                case GuardStatus.Awake:
                    if (previousTime != null && previousState == GuardStatus.Sleep)
                    {
                        processTimeEntry(getKey(previousTime.Value), guard, previousTime.Value.Minute, currentTime.Value.Minute);
                    }
                    break;

                case GuardStatus.Sleep:
                    if (previousState == GuardStatus.Sleep)
                    {
                        throw new Exception();
                    }
                    break;
            }
        }

        private string getKey(DateTime timestamp)
        {
            return $"{timestamp.ToString("yyyy")}-{timestamp.ToString("MM")}-{timestamp.ToString("dd")}"; ;
        }

        private void processTimeEntry(string key, Guard guard, int startMinute, int endMinute)
        {
            int[] timeEntry;

            if (guard.TimeEntries.ContainsKey(key))
            {
                timeEntry = guard.TimeEntries[key];
            }
            else
            {
                timeEntry = new int[60];

                for (var i = 0; i < timeEntry.Length; i++)
                {
                    timeEntry[i] = 0;
                }

                guard.TimeEntries.Add(key, timeEntry);
            }

            for (var t = startMinute; t < endMinute; t++)
            {
                timeEntry[t]++;
            }

        }
    }
}
