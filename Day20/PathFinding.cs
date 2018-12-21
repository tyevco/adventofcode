using Advent.Utilities.Data;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public static class PathFinding
    {
        private readonly static object lockObject = new object();

        // Need to make sure that it uses the /longest/ distance.
        public static Point FindRoomThroughMostDoors(Building building, Room firstRoom)
        {
            IEnumerable<Room> targets = building.Rooms.Data.Where(r => r != null && r.DoorCount == 1 && !(r.X == firstRoom.X && r.Y == firstRoom.Y)).ToList();

            var scoredList = CalculateDistance(firstRoom.X, firstRoom.Y, building.Rooms).Data.Where(r => r != null);
            var targetPoint = scoredList.Where(r => targets.Any(t => t.X == r.X && t.Y == r.Y)).OrderByDescending(r => r.Distance).FirstOrDefault();

            return targetPoint;
        }

        // Need to make sure that it uses the /longest/ distance.
        public static IEnumerable<Point> FindRoomsThroughAtLeastSpecifiedDoorAmount(Building building, Room firstRoom, int roomAmount)
        {
            IList<Point> points = new List<Point>();

            var scoredList = CalculateDistance(firstRoom.X, firstRoom.Y, building.Rooms).Data.Where(r => r != null);

            return scoredList.Where(r => r.Distance >= roomAmount);
        }

        private static Grid<Point> CalculateDistance(int x, int y, Grid<Room> rooms)
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
                    {
                        FinishedPoints[point.X, point.Y] = point;
                    }

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

            GridPrinter.Print(FinishedPoints);

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
