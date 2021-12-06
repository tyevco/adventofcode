using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D06
{
    [Exercise("Day 6: Lanternfish")]
    class Y2021D06 : FileSelectionParsingConsole<IList<long>>, IExercise
    {
        public void Execute()
        {
            Start("D06/Data");
        }

        protected override IList<long> DeserializeData(IList<string> data)
        {
            var indices = data.SelectMany(x => x.Split(",")).Select(x => int.Parse(x));

            var items = new long[9];

            foreach (var index in indices)
            {
                items[index]++;
            }

            return items.ToList();
        }

        protected override void Execute(IList<long> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                Console.WriteLine($"{string.Join(", ", data)} : [0] {data.Sum()}");

                for (int d = 0; d < 256; d++)
                {
                    var spawn = data[0];

                    for (int i = 1; i < data.Count; i++)
                    {
                        data[i - 1] = data[i];
                    }

                    data[8] = spawn;
                    data[6] += spawn;

                    Console.WriteLine($"{string.Join(", ", data)} : [{d + 1}] {data.Sum()}");
                }
            });

            Timer.Monitor("Part 2", () =>
            {

            });
        }
    }
}
