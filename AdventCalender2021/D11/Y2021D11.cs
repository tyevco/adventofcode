using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Extensions;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D11
{
    [Exercise("Day 11: Dumbo Octopus")]
    class Y2021D11 : FileSelectionParsingConsole<CartesianGrid<int>>, IExercise
    {
        public void Execute()
        {
            Start("D11/Data");
        }

        protected override CartesianGrid<int> DeserializeData(IList<string> data)
        {
            return new CartesianGrid<int>(data.SelectMany(d => d.Split("").SelectMany(x => x.ToCharArray())).Select(x => int.Parse(x.ToString())), 10, 10);
        }

        protected override void Execute(CartesianGrid<int> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                int totalFlashes = 0;

                for (int iterations = 0; iterations < 100; iterations++)
                {
                    data.Perform(o => data.Set(o.x, o.y, o.item + 1));

                    var pendingFlash = data.Where(i => i.item > 9);
                    HashSet<(int x, int y)> flashed = new HashSet<(int x, int y)>(pendingFlash.Select(o => (o.x, o.y)));
                    Queue<(int x, int y, int item)> flashing = new Queue<(int x, int y, int item)>(pendingFlash);

                    while (flashing.TryPeek(out (int x, int y, int item) octopus))
                    {
                        var adjs = data.GetAdjascent(octopus.x, octopus.y, true);

                        foreach (var adj in adjs)
                        {
                            if (!flashed.Contains((adj.x, adj.y)) && adj.item + 1 > 9)
                            {
                                flashing.Enqueue(adj);
                                flashed.Add((adj.x, adj.y));
                            }

                            data.Set(adj.x, adj.y, adj.item + 1);
                        }

                        totalFlashes++;

                        flashing.Dequeue();
                    }

                    data.PerformIf(o => o.item > 9, o => data.Set(o.x, o.y, 0));
                }

                AnswerPartOne(totalFlashes);
            });

            Timer.Monitor("Part 2", () =>
            {
                bool eval = true;

                int iteration = 100;

                while (eval)
                {
                    data.Perform(o => data.Set(o.x, o.y, o.item + 1));

                    var pendingFlash = data.Where(i => i.item > 9);
                    HashSet<(int x, int y)> flashed = new HashSet<(int x, int y)>(pendingFlash.Select(o => (o.x, o.y)));
                    Queue<(int x, int y, int item)> flashing = new Queue<(int x, int y, int item)>(pendingFlash);

                    while (flashing.TryPeek(out (int x, int y, int item) octopus))
                    {
                        var adjs = data.GetAdjascent(octopus.x, octopus.y, true);

                        foreach (var adj in adjs)
                        {
                            if (!flashed.Contains((adj.x, adj.y)) && adj.item + 1 > 9)
                            {
                                flashing.Enqueue(adj);
                                flashed.Add((adj.x, adj.y));
                            }

                            data.Set(adj.x, adj.y, adj.item + 1);
                        }

                        flashing.Dequeue();
                    }

                    data.PerformIf(o => o.item > 9, o => data.Set(o.x, o.y, 0));

                    iteration++;

                    if (data.All(o => o.item == 0))
                    {
                        eval = false;
                    }
                }

                data.Print();
                AnswerPartOne(iteration);
            });
        }
    }
}
