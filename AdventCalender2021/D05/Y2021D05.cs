using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D05
{
    [Exercise("Day 5: Hydrothermal Venture")]
    class Y2021D05 : FileSelectionParsingConsole<IList<LineSegment>>, IExercise
    {
        public void Execute()
        {
            Start("D05/Data");
        }

        protected override IList<LineSegment> DeserializeData(IList<string> data)
        {
            return data
                        .Select(x => x.Split(" -> "))
                        .Select(x => x.SelectMany(i => i.Split(",")))
                        .Select(x => x.Select(i => int.Parse(i)).ToArray())
                        .Select(x => new LineSegment(x[0], x[1], x[2], x[3]))
                        .ToList();
        }

        protected override void Execute(IList<LineSegment> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                Grid<DataPoint<int>, int> vents = new Grid<DataPoint<int>, int>();

                foreach (var line in data)
                {
                    if (line.Start.X == line.End.X
                        || line.Start.Y == line.End.Y)
                    {
                        for (int x = Math.Min(line.Start.X, line.End.X); x <= Math.Max(line.Start.X, line.End.X); x++)
                        {
                            for (int y = Math.Min(line.Start.Y, line.End.Y); y <= Math.Max(line.Start.Y, line.End.Y); y++)
                            {
                                DataPoint<int> p;

                                if (vents.Has(x, y))
                                {
                                    p = vents[x, y];
                                    p.Data++;
                                }
                                else
                                {
                                    p = new DataPoint<int>(x, y, 1);
                                }

                                vents[x, y] = p;
                            }
                        }

                    }
                }

                AnswerPartOne(vents.Points.Count(x => x.Value.Data > 1));
            });

            Timer.Monitor("Part 2", () =>
            {
                Grid<DataPoint<int>, int> vents = new Grid<DataPoint<int>, int>();

                foreach (var line in data)
                {
                    if (line.Start.X == line.End.X
                        || line.Start.Y == line.End.Y)
                    {
                        for (int x = Math.Min(line.Start.X, line.End.X); x <= Math.Max(line.Start.X, line.End.X); x++)
                        {
                            for (int y = Math.Min(line.Start.Y, line.End.Y); y <= Math.Max(line.Start.Y, line.End.Y); y++)
                            {
                                DataPoint<int> p;

                                if (vents.Has(x, y))
                                {
                                    p = vents[x, y];
                                    p.Data++;
                                }
                                else
                                {
                                    p = new DataPoint<int>(x, y, 1);
                                }

                                vents[x, y] = p;
                            }
                        }
                    }
                    else
                    {
                        var step = line.Start;
                        DataPoint<int> p;

                        while (!step.Equals(line.End))
                        {

                            if (vents.Has(step.X, step.Y))
                            {
                                p = vents[step.X, step.Y];
                                p.Data++;
                            }
                            else
                            {
                                p = new DataPoint<int>(step.X, step.Y, 1);
                            }

                            vents[step.X, step.Y] = p;

                            step = step.Step(line.End);
                        }

                        if (vents.Has(step.X, step.Y))
                        {
                            p = vents[step.X, step.Y];
                            p.Data++;
                        }
                        else
                        {
                            p = new DataPoint<int>(step.X, step.Y, 1);
                        }

                        vents[step.X, step.Y] = p;
                    }
                }

                AnswerPartTwo(vents.Points.Count(x => x.Value.Data > 1));
            });
        }
    }
}
