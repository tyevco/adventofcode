using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Data.Map
{
    public static class Pathfinding
    {
        private const int DefaultSize = 0;

        private class PathPoint : Point<int>
        {
            public PathPoint(int x, int y, int distance)
            {
                this.X = x;
                this.Y = y;
                this.Data = distance;
            }
        }

        public static Point<int> FindTargetPoint<T>(IGrid<T> map, int startX, int startY, int targetX, int targetY, Predicate<T> locationValid, bool getClosestPoints = false)
        {
            ICollection<Point<T>> available = new List<Point<T>>();

            if (IsLocationValid(targetX, targetY, map, locationValid))
            {
                var plot = map[targetX, targetY];
                if (plot != null && !available.Any(p => p.X == plot.X && p.Y == plot.Y))
                    available.Add(plot);
            }

            object lockObject = new object();
            ICollection<Point<int>> paths = new List<Point<int>>();
            //Parallel.ForEach(available, plot =>
            foreach (var plot in available)
            {
                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid);

                var closePoints =
                    getClosestPoints ? GetPointsClosestTo(queuedPoints, startX, startY)
                                    : new Point<int>[] { queuedPoints[startX, startY] }.Where(x => x != null);

                if (closePoints.Any())
                {
                    lock (lockObject)
                    {
                        foreach (var p in closePoints)
                            paths.Add(p);
                    }
                }
            }

            if (paths.Any())
            {
                return paths.OrderBy(p => p.Data).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Point<int>> GetPointsClosestTo(Grid<int> points, int x, int y)
        {
            return new List<Point<int>> {
                        (Point<int>)points[x - 1, y],
                        (Point<int>)points[x + 1, y],
                        (Point<int>)points[x, y - 1],
                        (Point<int>) points[x, y + 1]
                     }.Where(p => p != null);
        }

        private static Grid<int> CalculateDistance<T>(int x, int y, IGrid<T> map, Predicate<T> locationValid)
        {
            Grid<int> FinishedPoints = new Grid<int>();
            Queue<Point<int>> PointsToProcess = new Queue<Point<int>>();
            PointsToProcess.Enqueue(new PathPoint(x, y, 0));

            int distance = 0;
            while (true)
            {
                distance++;

                Queue<Point<int>> nextQueue = new Queue<Point<int>>();
                Point<int> point = PointsToProcess.Dequeue();
                while (point != null)
                {
                    TryAddPoint(FinishedPoints, nextQueue, point.X - 1, point.Y, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X + 1, point.Y, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y - 1, distance, map, locationValid);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y + 1, distance, map, locationValid);

                    if (!FinishedPoints.Has(point.X, point.Y))
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

            PrintGrid(map, FinishedPoints.Points);

            return FinishedPoints;
        }

        private static Point<int> TryAddPoint<T>(Grid<int> finished, Queue<Point<int>> process, int x, int y, int d, IGrid<T> map, Predicate<T> locationValid)
        {
            Point<int> p = null;
            if (!finished.Has(x, y) && !process.Any(t => t.X == x && t.Y == y))
            {
                p = new PathPoint(x, y, d);
                if (IsPointValid(p, map, locationValid))
                {
                    process.Enqueue(p);
                }
            }

            return p;
        }

        private static bool IsPointValid<T>(Point<int> p, IGrid<T> map, Predicate<T> locationValid)
        {
            return IsLocationValid(p.X, p.Y, map, locationValid);
        }


        private static bool IsLocationValid<T>(int x, int y, IGrid<T> map, Predicate<T> locationValid)
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

        public static void PrintGrid<T>(IGrid<T> map, IDictionary<string, Point<int>> points)
        {
            var xs = map.Points.Select(x => x.Value.X);
            var ys = map.Points.Select(y => y.Value.Y);

            var minX = xs.Any() ? Math.Min(-DefaultSize, xs.Min()) : -DefaultSize;
            var maxX = xs.Any() ? Math.Max(DefaultSize, xs.Max()) : DefaultSize;
            var minY = ys.Any() ? Math.Min(-DefaultSize, ys.Min()) : -DefaultSize;
            var maxY = ys.Any() ? Math.Max(DefaultSize, ys.Max()) : DefaultSize;
            var maxV = points.Select(x => x.Value.Data).Max();
            var len = maxV.ToString().Length + 1;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    string key = $"{x},{y}";
                    if (points.ContainsKey(key))
                    {
                        var data = points[key].Data;
                        Debug.Write($"{data}".PadLeft(len, ' '));
                    }
                    else
                    {
                        Debug.Write(".".PadLeft(len, ' '));
                    }
                }

                Debug.WriteLine();
            }

            Debug.WriteLine();
        }
    }
}