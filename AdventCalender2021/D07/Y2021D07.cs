using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D07
{
    [Exercise("Day 7: The Treachery of Whales")]
    class Y2021D07 : FileSelectionParsingConsole<IList<int>>, IExercise
    {
        public void Execute()
        {
            Start("D07/Data");
        }

        protected override IList<int> DeserializeData(IList<string> data)
        {
            return data
                    .SelectMany(x => x.Split(","))
                    .Select(x => int.Parse(x))
                    .ToList();
        }

        protected override void Execute(IList<int> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                //Console.WriteLine($"{string.Join(", ", data)}");

                var left = data.Min();
                var right = data.Max();

                //Console.WriteLine($"Avg:{data.Average()}");

                int minFuel = int.MaxValue;
                int bestPos = -1;

                for (int i = left; i <= right; i++)
                {
                    var fuel = data.Select(x => Math.Abs(x - i));

                    var sum = fuel.Sum();

                    if (sum < minFuel)
                    {
                        minFuel = sum;
                        bestPos = i;
                    }
                    
                    //Console.WriteLine($"[{i}] : {string.Join(", ", fuel)} : {fuel.Sum()}");
                }

                //Console.WriteLine($"Best: {minFuel} @ {bestPos}");

                AnswerPartOne(minFuel);
            });

            Timer.Monitor("Part 2", () =>
            {
                //Console.WriteLine($"{string.Join(", ", data)}");

                var left = data.Min();
                var right = data.Max();

                var avgF = Math.Floor(data.Average());
                var avgC = Math.Ceiling(data.Average());

                //Console.WriteLine($"Avg:{data.Average()}");

                int minFuel = int.MaxValue;
                int bestPos = -1;

                for (int i = (int)avgF; i <= (int)avgC; i++)
                {
                    var fuel = data.Select(x => {
                        var delta = Math.Abs(x - i);
                        var increments = Enumerable.Range(1, delta);

                        return increments.Sum();
                    });

                    var sum = fuel.Sum();

                    if (sum < minFuel)
                    {
                        minFuel = sum;
                        bestPos = i;
                    }

                    //Console.WriteLine($"[{i}] : {string.Join(", ", fuel)} : {fuel.Sum()}");
                }

                //Console.WriteLine($"Best: {minFuel} @ {bestPos}");

                AnswerPartTwo(minFuel);
            });
        }
    }
}
