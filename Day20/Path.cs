using Advent.Utilities.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day20
{
    public class Path
    {
        public Building Building { get; }

        private Path(Building Building)
        {
            this.Building = Building;
        }

        public static Point FindTargetPoint(Building building, Room room)
        {
            ICollection<Room> targets = new List<Room>();

            foreach (var target in building.Rooms.Data.Where(r => r != null))
            {
                //TODO FIXME
                //if (target.X > 0)
                //{
                //    if (IsLocationValid(target.X - 1, target.Y, building, entities, room))
                //    {
                //        var plot = building[target.X - 1, target.Y];
                //        if (!targets.Any(p => p.X == plot.X && p.Y == plot.Y))
                //            targets.Add(plot);
                //    }
                //}

                //if (target.X < building.Width - 1)
                //{
                //    if (IsLocationValid(target.X + 1, target.Y, building, entities, room))
                //    {
                //        var plot = building[target.X + 1, target.Y];
                //        if (!targets.Any(p => p.X == plot.X && p.Y == plot.Y))
                //            targets.Add(plot);
                //    }
                //}

                //if (target.Y > 0)
                //{
                //    if (IsLocationValid(target.X, target.Y - 1, building, entities, room))
                //    {
                //        var plot = building[target.X, target.Y - 1];
                //        if (!targets.Any(p => p.X == plot.X && p.Y == plot.Y))
                //            targets.Add(plot);
                //    }
                //}

                //if (target.Y < building.Height - 1)
                //{
                //    if (IsLocationValid(target.X, target.Y + 1, building, entities, room))
                //    {
                //        var plot = building[target.X, target.Y + 1];
                //        if (!targets.Any(p => p.X == plot.X && p.Y == plot.Y))
                //            targets.Add(plot);
                //    }
                //}
            }

            object lockObject = new object();
            ICollection<Point> points = new List<Point>();
            Parallel.ForEach(targets, plot =>
            //foreach (var plot in available)
            {
                var path = new Path(building);
                var queuedPoints = path.CalculateDistance(plot.X, plot.Y, room.X, room.Y, room);

                var closePoints = GetPointsClosestTo(queuedPoints, room.X, room.Y);
                if (closePoints.Any())
                {
                    lock (lockObject)
                    {
                        foreach (var p in closePoints)
                            points.Add(p);
                    }
                }
            });
            //});

            if (points.Any())
            {
                return points.OrderBy(p => p.Distance).ThenBy(p => p.X + p.Y * building.Rooms.Width).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Point> GetPointsClosestTo(Grid<Point> points, int x, int y)
        {
            return new List<Point> { points[x - 1, y], points[x + 1, y], points[x, y - 1], points[x, y + 1] }.Where(p => p != null);
        }

        private Grid<Point> CalculateDistance(int x, int y, int targetX, int targetY, Room Room)
        {
            Grid<Point> FinishedPoints = new Grid<Point>(Building.Rooms.Width, Building.Rooms.Height);
            Queue<Point> PointsToProcess = new Queue<Point>();
            PointsToProcess.Enqueue(new Point(x, y, 0));

            int distance = 0;
            while (true)
            {
                distance++;

                Queue<Point> nextQueue = new Queue<Point>();
                Point point = PointsToProcess.Dequeue();
                while (point != null)
                {
                    TryAddPoint(FinishedPoints, nextQueue, point.X - 1, point.Y, distance, Building, Room);
                    TryAddPoint(FinishedPoints, nextQueue, point.X + 1, point.Y, distance, Building, Room);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y - 1, distance, Building, Room);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y + 1, distance, Building, Room);

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

        private static Point TryAddPoint(Grid<Point> finished, Queue<Point> process, int x, int y, int d, Building building, Room Room)
        {
            Point p = null;
            if (finished[x, y] == null && !process.Any(t => t.X == x && t.Y == y))
            {
                p = new Point(x, y, d);
                process.Enqueue(p);
            }

            return p;
        }
    }
}
