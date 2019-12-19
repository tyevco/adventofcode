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

        public static IEnumerable<Point<int>> FindTargetPoints<T>(
                                            IGrid<T> map,
                                            int startX,
                                            int startY,
                                            Predicate<T> locationValid,
                                            PointMode pointMode = PointMode.Point,
                                            params Point<T>[] targetPoints)
        {
            IGrid<T> available = new Grid<T>();

            if (targetPoints != null && targetPoints.Length > 0 && IsLocationValid(startX, startY, map, locationValid))
            {
                var plot = map[startX, startY];
                if (plot != null && available[plot.X, plot.Y] == null)
                    available[plot.Y, plot.Y] = plot;

                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid);

                var closePoints = pointMode == PointMode.Closest ? GetPointsClosestTo(queuedPoints, targetPoints)
                                    : GetPointsAt(queuedPoints, targetPoints);

                if (closePoints.Any())
                    return closePoints; //.OrderBy(x => x.Data);
            }

            return null;
        }

        public static Point<int> FindTargetPoint<T>(
                                            IGrid<T> map,
                                            int startX,
                                            int startY,
                                            int targetX,
                                            int targetY,
                                            Predicate<T> locationValid,
                                            PointMode pointMode = PointMode.Point)
        {
            IGrid<T> available = new Grid<T>();

            if (IsLocationValid(targetX, targetY, map, locationValid))
            {
                var plot = map[targetX, targetY];
                if (plot != null && available[plot.X, plot.Y] == null)
                    available[plot.Y, plot.Y] = plot;

                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid);

                var closePoints = pointMode == PointMode.Closest ? GetPointsClosestTo(queuedPoints, startX, startY)
                                    : new Point<int>[] { queuedPoints[startX, startY] }.Where(x => x != null);

                if (closePoints.Any())
                {
                    return closePoints.OrderBy(p => p.Data).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
                }
            }

            return null;
        }

        private static IEnumerable<Point<TGrid>> GetPointsClosestTo<TGrid>(IGrid<TGrid> points, int x, int y)
        {
            return new List<Point<TGrid>> {
                        points[x - 1, y],
                        points[x + 1, y],
                        points[x, y - 1],
                        points[x, y + 1]
                     }.Where(p => p != null);
        }


        private static IEnumerable<Point<TGrid>> GetPointsClosestTo<TGrid, TPoint>(IGrid<TGrid> points, params Point<TPoint>[] targets)
        {
            return targets.SelectMany(t => new Point<TGrid>[] {
                        points[t.X - 1, t.Y],
                        points[t.X + 1, t.Y],
                        points[t.X, t.Y - 1],
                        points[t.X, t.Y + 1]
                    }).Where(p => p != null);
        }

        private static IEnumerable<Point<TGrid>> GetPointsAt<TGrid, TPoint>(IGrid<TGrid> points, params Point<TPoint>[] targets)
        {
            return targets.Select(t => points[t.X, t.Y]).Where(p => p != null);
        }

        private static IGrid<int> CalculateDistance<T>(int x, int y, IGrid<T> map, Predicate<T> locationValid)
        {
            IGrid<int> FinishedPoints = new Grid<int>();
            Queue<Point<int>> PointsToProcess = new Queue<Point<int>>();
            PointsToProcess.Enqueue(new PathPoint(x, y, 0));

            while (PointsToProcess.Any())
            {
                Point<int> point = PointsToProcess.Dequeue();

                if (FinishedPoints.Has(point.X, point.Y))
                {
                    continue;
                }

                var nearbyPoints = GetPointsClosestTo<T, int>(map, point);

                foreach (var nearbyPoint in nearbyPoints)
                {
                    if (!FinishedPoints.Has(nearbyPoint.X, nearbyPoint.Y))
                    {
                        if (locationValid.Invoke(map[nearbyPoint.X, nearbyPoint.Y].Data! ?? default(T)))
                        {
                            PointsToProcess.Enqueue(new PathPoint(nearbyPoint.X, nearbyPoint.Y, point.Data + 1));
                        }
                    }
                }

                FinishedPoints[point.X, point.Y] = point;
            }

            if (Debug.EnableDebugOutput)
                PrintGrid(map, FinishedPoints.Points);

            return FinishedPoints;
        }

        private static bool IsLocationValid<T>(int x, int y, IGrid<T> map, Predicate<T> locationValid)
        {
            bool valid = true;

            string key = $"{x},{y}";
            if (map.Points.ContainsKey(key))
            {
                valid = locationValid(map.Points[key].Data);
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