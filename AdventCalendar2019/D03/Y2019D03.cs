using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            IDictionary<string, (Segment, Segment, Point)> intersections = new Dictionary<string, (Segment, Segment, Point)>();

            Timer.Monitor(() =>
            {
                var maps = ParseData(File.ReadAllLines(file));

                var firstMap = maps[0];
                var secondMap = maps[1];
                bool skipOrigin = true;


                foreach (var segment in firstMap.Segments)
                {
                    foreach (var otherSegment in secondMap.Segments)
                    {
                        var intersection = otherSegment.Intersection(segment, skipOrigin);

                        if (intersection == null)
                        {
                            continue;
                        }
                        else
                        {
                            if (!intersections.ContainsKey(intersection.ToString()))
                            {
                                intersections.Add(intersection.ToString(), (segment, otherSegment, intersection));
                            }
                        }
                    }

                    skipOrigin = false;
                }

                var seekPoint = intersections.OrderBy(x => x.Value.Item3.CalculateDistance(0, 0)).FirstOrDefault().Value.Item3;

                if (seekPoint != null)
                {
                    Console.WriteLine($"{seekPoint?.X},{seekPoint?.Y}:{seekPoint.CalculateDistance(0, 0)}");
                }
                else
                {
                    Console.WriteLine("NO answer...");
                }


                var winningIntersection = intersections.OrderBy(x => CalculateDistanceToIntersection(x.Value)).FirstOrDefault().Value;
                Console.WriteLine($"{winningIntersection.Item1.Start.Distance + winningIntersection.Item1.Start.CalculateDistance(winningIntersection.Item3)} + {winningIntersection.Item2.Start.Distance + winningIntersection.Item2.Start.CalculateDistance(winningIntersection.Item3)} : {CalculateDistanceToIntersection(winningIntersection)}");
            });
        }

        private int CalculateDistanceToIntersection((Segment, Segment, Point) intersection)
        {
            return intersection.Item1.Start.CalculateDistance(intersection.Item3)
                    + intersection.Item2.Start.CalculateDistance(intersection.Item3)
                    + intersection.Item1.Start.Distance
                    + intersection.Item2.Start.Distance;
        }

        private IList<Map> ParseData(IList<string> data)
        {
            IList<Map> maps = new List<Map>();
            int startX = 0, startY = 0;

            foreach (var wirePathData in data)
            {
                Map map = new Map();
                var wirePath = wirePathData.Split(",");
                int currX = startX, currY = startY;

                var lastPoint = new Point { X = currX, Y = currY, Distance = 0 };
                int totalDistance = 0;

                foreach (var wireDir in wirePath)
                {
                    Segment segment = new Segment();
                    var distance = int.Parse(wireDir.Substring(1));

                    segment.Start = lastPoint;
                    totalDistance += distance;

                    //System.Console.WriteLine($"{currX},{currY}:{wireDir}");
                    switch (wireDir[0])
                    {
                        case 'U':
                            currY -= distance;
                            break;
                        case 'D':
                            currY += distance;
                            break;
                        case 'L':
                            currX -= distance;
                            break;
                        case 'R':
                            currX += distance;
                            break;
                    }

                    lastPoint = new Point { X = currX, Y = currY, Distance = totalDistance };

                    segment.End = lastPoint;
                    map.Segments.Add(segment);
                }

                maps.Add(map);
            }

            return maps;
        }
    }
}
