using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D13
{
    [Exercise("Day 13: Shuttle Search")]
    class Y2020D13 : FileSelectionParsingConsole<(int timestamp, IList<(long index, long bus)> buses)>, IExercise
    {
        public void Execute()
        {
            Start("D13/Data");
        }

        protected override (int timestamp, IList<(long index, long bus)> buses) DeserializeData(IList<string> data)
        {
            var ts = int.Parse(data[0]);

            long i = 0;
            List<(long index, long bus)> buses = data[1].Split(",").Select(x => (index: i++, bus: x)).Where(x => x.bus != "x").Select(x => (x.index, bus: long.Parse(x.bus))).ToList();

            return (ts, buses);
        }

        protected override void Execute((int timestamp, IList<(long index, long bus)> buses) data)
        {
            PartOne(data);
            PartTwo(data);
        }

        protected void PartOne((int timestamp, IList<(long index, long bus)> buses) data)
        {
            var (timestamp, buses) = data;

            var earliest = (long)timestamp - buses.Max(b => b.bus);

            IDictionary<long, IList<long>> busTimes = new Dictionary<long, IList<long>>();
            for (long i = earliest; i < timestamp + buses.Max(b => b.bus); i++)
            {
                foreach (var bus in buses)
                {
                    if (i % bus.bus == 0)
                    {
                        AddOrSet(bus.bus, i, busTimes);
                    }
                }
            }

            var closetTimes = busTimes.SelectMany(x => x.Value.Where(y => y >= timestamp).Select(y => (x.Key, y)));

            var max = closetTimes.Min(x => x.y);

            var entry = closetTimes.FirstOrDefault(x => x.y == max);

            Console.WriteLine($"{entry.Key}:{max} - {(timestamp - max)}");

            AnswerPartOne(entry.Key * (timestamp - max));
        }


        protected void PartTwo((int timestamp, IList<(long index, long bus)> buses) data)
        {
            var answer = Mathematics.ChineseRemainderTheorem.Solve(data.buses.Select(x => x.bus).ToArray(), data.buses.Select(b =>
            {
                long i = -b.index;

                while (i < 0)
                {
                    i += b.bus;
                }

                return i;
            }).ToArray());

            AnswerPartTwo(answer);
        }

        private static void AddOrSet(long key, long value, IDictionary<long, IList<long>> table)
        {
            IList<long> entries;
            if (table.ContainsKey(key))
            {
                entries = table[key];
            }
            else
            {
                entries = new List<long>();
            }

            entries.Add(value);

            table[key] = entries;
        }
    }
}
