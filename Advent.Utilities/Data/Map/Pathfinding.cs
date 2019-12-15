using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advent.Utilities.Data.Manhattan;

namespace Advent.Utilities.Data.Map
{
    public static class Pathfinding
    {
        public class Path
        {
            public Path(int x, int y, int distance)
            {
                X = x;
                Y = y;
                Distance = distance;
            }

            public int X { get; }
            public int Y { get; }
            public int Distance { get; set; }
        }

        public static Point FindTargetPoint<T>(IMap<T> map, int startX, int startY, int targetX, int targetY, Predicate<T> locationValid)
        {
            ICollection<Point> available = new List<Point>();

            if (targetX > 0)
            {
                if (IsLocationValid(targetX - 1, targetY, map, locationValid))
                {
                    var plot = map[targetX - 1, targetY];
                    if (plot != null && !available.Any(p => p.X == plot.X && p.Y == plot.Y))
                        available.Add(plot);
                }
            }

            if (targetX < map.Width - 1)
            {
                if (IsLocationValid(targetX + 1, targetY, map, locationValid))
                {
                    var plot = map[targetX + 1, targetY];
                    if (plot != null && !available.Any(p => p.X == plot.X && p.Y == plot.Y))
                        available.Add(plot);
                }
            }

            if (targetY > 0)
            {
                if (IsLocationValid(targetX, targetY - 1, map, locationValid))
                {
                    var plot = map[targetX, targetY - 1];
                    if (plot != null && !available.Any(p => p.X == plot.X && p.Y == plot.Y))
                        available.Add(plot);
                }
            }

            if (targetY < map.Height - 1)
            {
                if (IsLocationValid(targetX, targetY + 1, map, locationValid))
                {
                    var plot = map[targetX, targetY + 1];
                    if (plot != null && !available.Any(p => p.X == plot.X && p.Y == plot.Y))
                        available.Add(plot);
                }
            }


            object lockObject = new object();
            ICollection<Path> paths = new List<Path>();
            Parallel.ForEach(available, plot =>
            //foreach (var plot in available)
            {
                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid);

                var closePoints = GetPointsClosestTo(queuedPoints, targetX, targetY);
                if (closePoints.Any())
                {
                    lock (lockObject)
                    {
                        foreach (var p in closePoints)
                            paths.Add(p);
                    }
                }
            });
            //});

            if (paths.Any())
            {
                return paths.OrderBy(p => p.Distance).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Path> GetPointsClosestTo(Grid<Path> points, int x, int y)
        {
            return new List<Path> { points[x - 1, y], points[x + 1, y], points[x, y - 1], points[x, y + 1] }.Where(p => p != null);
        }

        private static Grid<Path> CalculateDistance<T>(int x, int y, IMap<T> map, Predicate<T> locationValid)
        {
            Grid<Path> FinishedPoints = new Grid<Path>(map.Width, map.Height);
            Queue<Path> PointsToProcess = new Queue<Path>();
            PointsToProcess.Enqueue(new Path(x, y, 0));

            int distance = 0;
            while (true)
            {
                distance++;

                Queue<Point> nextQueue = new Queue<Point>();
                Point point = PointsToProcess.Dequeue();
                while (point != null)
                {
                    TryAddPoint(FinishedPoints, nextQueue, point.X - 1, point.Y, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X + 1, point.Y, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y - 1, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y + 1, distance, map, locationValid);

                    if (FinishedPoints[point.X, point.Y] == null)
                        FinishedPoints[point.X, point.Y] = point;

                    if (PointsToProcess.Any())
                    {
                        point = PointsToProcess.Dequeue();
                    }
                    else
                    {
                        point = null;
                    }
                }

                if (!nextQueue.Any())
                {
                    break;
                }
                else
                {
                    PointsToProcess = nextQueue;
                }
            }

            return FinishedPoints;
        }

        private static Point TryAddPoint<T>(Grid<Path> finished, Queue<Path> process, int x, int y, int d, IMap<T> map, Predicate<T> locationValid)
        {
            Path p = null;
            if (finished[x, y] == null && !process.Any(t => t.X == x && t.Y == y))
            {
                p = new Path(x, y, d);
                if (IsPointValid(p, map, locationValid))
                {
                    process.Enqueue(p);
                }
            }

            return p;
        }

        private static bool IsPointValid<T>(Path p, IMap<T> map, Predicate<T> locationValid)
        {
            return IsLocationValid(p.X, p.Y, map, locationValid);
        }


        private static bool IsLocationValid<T>(int x, int y, IMap<T> map, Predicate<T> locationValid)
        {
            bool valid = true;

            string key = $"{x},{y}";
            if (map.Points.ContainsKey(key))
            {
                valid = locationValid((T)map.Points[key].Data);
            }
            else
            {
                valid = locationValid(default(T));
            }

            return valid;
        }
    }
}
