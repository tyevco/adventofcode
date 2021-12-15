using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Extensions;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D09
{
    [Exercise("Day 9: Smoke Basin")]
    class Y2021D09 : FileSelectionParsingConsole<CartesianGrid<int>>, IExercise
    {
        public void Execute()
        {
            Start("D09/Data");
        }

        protected override CartesianGrid<int> DeserializeData(IList<string> data)
        {
            var height = data.Count;
            var width = data.Max(x => x.Length);

            var grid = new CartesianGrid<int>(width, height);

            int y = 0;

            foreach (var line in data)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    grid.Set(x, y, int.Parse(line[x].ToString()));
                }

                y++;
            }

            return grid;
        }

        protected override void Execute(CartesianGrid<int> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                List<(int x, int y, int item)> lowSpots = new List<(int x, int y, int item)>();

                foreach (var p in data)
                {
                    var adjs = data.GetAdjascent(p.x, p.y);
                    if (adjs.All(a => a.item > p.item))
                    {
                        lowSpots.Add(p);
                    }
                }

                AnswerPartOne(lowSpots.Select(x => x.item + 1).Sum());
            });

            Timer.Monitor("Part 2", () =>
            {
                List<(int x, int y, int item)> lowSpots = new List<(int x, int y, int item)>();

                List<int> totals = new List<int>();

                foreach (var p in data)
                {
                    var adjs = data.GetAdjascent(p.x, p.y);
                    if (adjs.All(a => a.item > p.item))
                    {
                        // go through all the low spots and find neighbors that are higher, then recurse through it
                        HashSet<(int x, int y, int item)> inspected = new HashSet<(int x, int y, int item)>();

                        inspected.Add(p);

                        Queue<(int x, int y, int item)> inspectionQueue = new Queue<(int x, int y, int item)>();

                        foreach (var adj in adjs)
                        {
                            if (adj.item < 9)
                            {
                                inspectionQueue.Enqueue(adj);
                            }

                            inspected.Add(adj);
                        }

                        while (inspectionQueue.TryPeek(out (int x, int y, int item) adj))
                        {
                            if (adj.item < 9)
                            {
                                var adjAdjs = data.GetAdjascent(adj.x, adj.y);

                                foreach (var adjAdj in adjAdjs)
                                {
                                    if (!inspected.Contains(adjAdj) || adjAdj.item > adj.item)
                                    {
                                        if (adjAdj.item < 9)
                                        {
                                            if (adjAdj.item > adj.item)
                                            {
                                                inspectionQueue.Enqueue(adjAdj);
                                            }
                                        }

                                        inspected.Add(adjAdj);
                                    }
                                }
                            }
                            else
                            {
                                inspected.Add(adj);
                            }

                            inspectionQueue.Dequeue();
                        }

                        totals.Add(inspected.Where(p => p.item < 9).Count());
                    }
                }

                AnswerPartTwo(totals.OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b));
            });
        }
    }
}
