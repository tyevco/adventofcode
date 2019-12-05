using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D03
{
    [Exercise("Day 3: Crossed Wires")]
    class Y2019D03 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D03/Data");
        }

        protected override void Execute(string file)
        {
            Timer.Monitor(() =>
            {
                var maps = new WirePlotter().ParseData(file);

                int lowestDistance = int.MaxValue;
                ManhattanPoint seekPoint = null;
                object obj = new object();

                var firstMap = maps[0];
                var secondMap = maps[1];

                ConcurrentBag<(ManhattanPoint, ManhattanPoint)> intersections = new ConcurrentBag<(ManhattanPoint, ManhattanPoint)>();

                ParallelOptions options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 100
                };
                Parallel.ForEach(firstMap.Points, options, (firstPoint) =>
                {
                    if (!intersections.Any(p => p.Item1.X == firstPoint.X && p.Item1.Y == firstPoint.Y))
                    {
                        var secondPoint = secondMap.Points.FirstOrDefault(s => firstPoint.X == s.X && firstPoint.Y == s.Y);

                        if (secondPoint != null)
                        {
                            intersections.Add((firstPoint, secondPoint));

                            var mDis = firstPoint.CalculateDistance(0, 0);

                            lock (obj)
                            {
                                if (mDis < lowestDistance)
                                {
                                    lowestDistance = mDis;
                                    seekPoint = firstPoint;
                                }
                            }
                            //Console.WriteLine($"{point.X},{point.Y}:{mDis} [{point.Value}]");
                        }
                    }
                });
                if (seekPoint != null)
                {
                    Console.WriteLine($"{seekPoint?.X},{seekPoint?.Y}:{lowestDistance}");
                }
                else
                {
                    Console.WriteLine("NO answer...");
                }

                foreach (var intersection in intersections.OrderBy(x => (x.Item1.Id + x.Item2.Id + 2)))
                {
                    Console.WriteLine($"{intersection.Item1.Id + 1} {intersection.Item2.Id + 1} : {intersection.Item1.Id + intersection.Item2.Id + 2 }");
                }
            });
        }
    }
}
