using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Data.Map
{
    public static class Pathfinding<TData>
    {
        private const int DefaultSize = 0;

        public delegate DataPoint<TData> CreatePointData<TPoint, TPointData>(TPoint Point, DataPoint<TData> lastDataPoint)
            where TPoint : Point<TPointData>;

        public static IEnumerable<DataPoint<TData>> FindTargetPoints<TGridPoint, TGridData>(
                                            IGrid<TGridPoint, TGridData> map,
                                            int startX,
                                            int startY,
                                            Predicate<TGridPoint> locationValid,
                                            PointMode pointMode = PointMode.Point,
                                            CreatePointData<TGridPoint, TGridData> createDataPoint = null,
                                            params Point<TGridData>[] targetPoints)
            where TGridPoint : Point<TGridData>
        {
            IGrid<TGridPoint, TGridData> available = new Grid<TGridPoint, TGridData>();

            if (targetPoints != null && targetPoints.Length > 0 && IsLocationValid(startX, startY, map, locationValid))
            {
                var plot = map[startX, startY];
                if (plot != null && available[plot.X, plot.Y] == null)
                    available[plot.Y, plot.Y] = plot;

                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid, createDataPoint);

                var closePoints = pointMode == PointMode.Closest ? GetPointsClosestTo(queuedPoints, targetPoints)
                                    : GetPointsAt(queuedPoints, targetPoints);

                if (closePoints.Any())
                    return closePoints; //.OrderBy(x => x.Data);
            }

            return null;
        }

        public static DataPoint<TData> FindTargetPoint<TGridPoint, TGridData>(
                                            IGrid<TGridPoint, TGridData> map,
                                            int startX,
                                            int startY,
                                            int targetX,
                                            int targetY,
                                            Predicate<TGridPoint> locationValid,
                                            CreatePointData<TGridPoint, TGridData> createDataPoint = null,
                                            PointMode pointMode = PointMode.Point)
            where TGridPoint : Point<TGridData>
        {
            IGrid<TGridPoint, TGridData> available = new Grid<TGridPoint, TGridData>();

            if (IsLocationValid(targetX, targetY, map, locationValid))
            {
                var plot = map[targetX, targetY];
                if (plot != null && available[plot.X, plot.Y] == null)
                    available[plot.Y, plot.Y] = plot;

                var queuedPoints = CalculateDistance(plot.X, plot.Y, map, locationValid, createDataPoint);

                var closePoints = pointMode == PointMode.Closest ? GetPointsClosestTo(queuedPoints, startX, startY)
                                    : new DataPoint<TData>[] { queuedPoints[startX, startY] }.Where(x => x != null);

                if (closePoints.Any())
                {
                    return closePoints.OrderBy(p => p.Distance).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
                }
            }

            return null;
        }

        private static IEnumerable<TGridPoint> GetPointsClosestTo<TGridPoint, TPoint>(
                    IGrid<TGridPoint, TPoint> points,
                    int x,
                    int y)
            where TGridPoint : Point<TPoint>
        {
            return new List<TGridPoint> {
                        points[x - 1, y],
                        points[x + 1, y],
                        points[x, y - 1],
                        points[x, y + 1]
                     }.Where(p => p != null);
        }


        private static IEnumerable<TGridPoint> GetPointsClosestTo<TGridPoint, TGridData, TPoint>(
                    IGrid<TGridPoint, TGridData> points,
                    params Point<TPoint>[] targets)
            where TGridPoint : Point<TGridData>
        {
            return targets.SelectMany(t => new TGridPoint[] {
                        points[t.X - 1, t.Y],
                        points[t.X + 1, t.Y],
                        points[t.X, t.Y - 1],
                        points[t.X, t.Y + 1]
                    }).Where(p => p != null);
        }

        private static IEnumerable<TGridPoint> GetPointsAt<TGridPoint, TGridData, TPoint>(
                    IGrid<TGridPoint, TGridData> points,
                    params Point<TPoint>[] targets)
            where TGridPoint : Point<TGridData>
        {
            return targets.Select(t => points[t.X, t.Y]).Where(p => p != null);
        }

        private static IGrid<DataPoint<TData>, TData> CalculateDistance<TGridPoint, TGridData>(
                    int x,
                    int y,
                    IGrid<TGridPoint, TGridData> map,
                    Predicate<TGridPoint> locationValid,
                    CreatePointData<TGridPoint, TGridData> createDataPoint = null)
            where TGridPoint : Point<TGridData>
        {
            IGrid<DataPoint<TData>, TData> FinishedPoints = new Grid<DataPoint<TData>, TData>();
            Queue<DataPoint<TData>> PointsToProcess = new Queue<DataPoint<TData>>();
            PointsToProcess.Enqueue(new DataPoint<TData>(x, y, 0, default(TData)));

            while (PointsToProcess.Any())
            {
                DataPoint<TData> point = PointsToProcess.Dequeue();

                if (FinishedPoints.Has(point.X, point.Y))
                {
                    continue;
                }

                var nearbyPoints = GetPointsClosestTo(map, point);

                foreach (var nearbyPoint in nearbyPoints)
                {
                    if (!FinishedPoints.Has(nearbyPoint.X, nearbyPoint.Y))
                    {
                        if (locationValid.Invoke(nearbyPoint))
                        {
                            DataPoint<TData> pointData;
                            if (createDataPoint != null)
                            {
                                pointData = createDataPoint.Invoke(nearbyPoint, point);
                            }
                            else
                            {
                                pointData = new DataPoint<TData>(nearbyPoint.X, nearbyPoint.Y, point.Distance + 1, default(TData));
                            }

                            PointsToProcess.Enqueue(pointData);
                        }
                    }
                }

                FinishedPoints[point.X, point.Y] = point;
            }

            if (Debug.EnableDebugOutput)
                PrintGrid(map, FinishedPoints.Points);

            return FinishedPoints;
        }

        private static bool IsLocationValid<TGridPoint, TGridData>(int x, int y, IGrid<TGridPoint, TGridData> map, Predicate<TGridPoint> locationValid)
            where TGridPoint : Point<TGridData>
        {
            bool valid = true;

            string key = $"{x},{y}";
            if (map.Points.ContainsKey(key))
            {
                valid = locationValid(map.Points[key]);
            }
            else
            {
                valid = locationValid(default(TGridPoint));
            }

            return valid;
        }

        public static void PrintGrid<TGridPoint, TGridData>(IGrid<TGridPoint, TGridData> map, IDictionary<string, DataPoint<TData>> points)
            where TGridPoint : Point<TGridData>
        {
            var xs = map.Points.Select(x => x.Value.X);
            var ys = map.Points.Select(y => y.Value.Y);

            var minX = xs.Any() ? Math.Min(-DefaultSize, xs.Min()) : -DefaultSize;
            var maxX = xs.Any() ? Math.Max(DefaultSize, xs.Max()) : DefaultSize;
            var minY = ys.Any() ? Math.Min(-DefaultSize, ys.Min()) : -DefaultSize;
            var maxY = ys.Any() ? Math.Max(DefaultSize, ys.Max()) : DefaultSize;
            var maxV = points.Select(x => x.Value.Distance).Max();
            var len = maxV.ToString().Length + 1;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    string key = $"{x},{y}";
                    if (points.ContainsKey(key))
                    {
                        var data = points[key].Distance;
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