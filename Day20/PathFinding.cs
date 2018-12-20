using Advent.Utilities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day20
{
    public static class PathFinding
    {
        private readonly static object lockObject = new object();

        // Need to make sure that it uses the /longest/ distance.
        public static Point FindTargetPoint(Building building, Room firstRoom)
        {
            IEnumerable<Room> targets = building.Rooms.Data.Where(r => r != null && r.DoorCount == 1 && !(r.X == firstRoom.X && r.Y == firstRoom.Y)).ToList();

            object lockObject = new object();
            ICollection<Point> points = new List<Point>();
            Parallel.ForEach(targets, room =>
            //foreach (var plot in available)
            {
                var scoredGrid = CalculateDistance(room.X, room.Y, firstRoom.X, firstRoom.Y, building.Rooms);

                lock (lockObject)
                {
                    Console.WriteLine($"Room #{room.Id.ToString().PadLeft(2, '0')} ({room.X},{room.Y})");
                    GridPrinter.Print(scoredGrid);
                }

                // does this need changed to return the firstRoom distance?
                var targetPoint = scoredGrid.Data.FirstOrDefault(r => r != null && r.X == firstRoom.X && r.Y == firstRoom.Y);
                if (targetPoint != null)
                {
                    lock (lockObject)
                    {
                        points.Add(new Point(room.X, room.Y, targetPoint.Distance));
                    }
                }
            });
            //});

            if (points.Any())
            {
                return points.OrderByDescending(p => p.Distance).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private static Grid<Point> CalculateDistance(int x, int y, int targetX, int targetY, Grid<Room> rooms)
        {
            int distance = 0;
            Grid<Point> FinishedPoints = new Grid<Point>(rooms.Width, rooms.Height);
            Queue<Point> PointsToProcess = new Queue<Point>();
            PointsToProcess.Enqueue(new Point(x, y, distance));

            while (true)
            {
                distance++;

                Queue<Point> nextQueue = new Queue<Point>();
                Point point = PointsToProcess.Dequeue();
                while (point != null)
                {
                    if (point.X > 0)
                    {
                        var targetRoom = rooms[point.X - 1, point.Y];
                        if (targetRoom != null && targetRoom.HasDoorwayTo(Direction.East))
                        {
                            TryAddPoint(FinishedPoints, nextQueue, targetRoom, distance, rooms);
                        }
                    }

                    if (point.X < rooms.Width - 1)
                    {
                        var targetRoom = rooms[point.X + 1, point.Y];
                        if (targetRoom != null && targetRoom.HasDoorwayTo(Direction.West))
                        {
                            TryAddPoint(FinishedPoints, nextQueue, targetRoom, distance, rooms);
                        }
                    }

                    if (point.Y > 0)
                    {
                        var targetRoom = rooms[point.X, point.Y - 1];
                        if (targetRoom != null && targetRoom.HasDoorwayTo(Direction.South))
                        {
                            TryAddPoint(FinishedPoints, nextQueue, targetRoom, distance, rooms);
                        }
                    }

                    if (point.Y < rooms.Height - 1)
                    {
                        var targetRoom = rooms[point.X, point.Y + 1];
                        if (targetRoom != null && targetRoom.HasDoorwayTo(Direction.North))
                        {
                            TryAddPoint(FinishedPoints, nextQueue, targetRoom, distance, rooms);
                        }
                    }

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

        private static Point TryAddPoint(Grid<Point> finished, Queue<Point> process, Room targetRoom, int d, Grid<Room> rooms)
        {
            Point p = null;
            if (finished[targetRoom.X, targetRoom.Y] == null && !process.Any(t => t.X == targetRoom.X && t.Y == targetRoom.Y))
            {
                p = new Point(targetRoom.X, targetRoom.Y, d);
                process.Enqueue(p);
            }

            return p;
        }
    }
}
