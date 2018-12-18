﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day15
{
    public class Path
    {
        public Map Map { get; }

        private Path(Map map)
        {
            Map = map;
        }

        public static Point FindTargetPoint(Map map, IEnumerable<Entity> entities, Entity entity)
        {
            ICollection<Plot> available = new List<Plot>();

            var targetEntities = entities.Where(e => e.Type != entity.Type);
            foreach (var target in targetEntities)
            {
                if (target.X > 0)
                {
                    if (IsLocationValid(target.X - 1, target.Y, map, entities, entity))
                    {
                        var plot = map[target.X - 1, target.Y];
                        if (!available.Any(p => p.X == plot.X && p.Y == plot.Y))
                            available.Add(plot);
                    }
                }

                if (target.X < map.Width - 1)
                {
                    if (IsLocationValid(target.X + 1, target.Y, map, entities, entity))
                    {
                        var plot = map[target.X + 1, target.Y];
                        if (!available.Any(p => p.X == plot.X && p.Y == plot.Y))
                            available.Add(plot);
                    }
                }

                if (target.Y > 0)
                {
                    if (IsLocationValid(target.X, target.Y - 1, map, entities, entity))
                    {
                        var plot = map[target.X, target.Y - 1];
                        if (!available.Any(p => p.X == plot.X && p.Y == plot.Y))
                            available.Add(plot);
                    }
                }

                if (target.Y < map.Height - 1)
                {
                    if (IsLocationValid(target.X, target.Y + 1, map, entities, entity))
                    {
                        var plot = map[target.X, target.Y + 1];
                        if (!available.Any(p => p.X == plot.X && p.Y == plot.Y))
                            available.Add(plot);
                    }
                }
            }

            object lockObject = new object();
            ICollection<Point> points = new List<Point>();
            Parallel.ForEach(available, plot =>
            //foreach (var plot in available)
            {
                var path = new Path(map);
                var queuedPoints = path.CalculateDistance(plot.X, plot.Y, entity.X, entity.Y, entities, entity);

                //for (int y = 0; y < map.Height; y++)
                //{
                //    for (int x = 0; x < map.Width; x++)
                //    {
                //        if (entity.X == x && entity.Y == y)
                //        {
                //            System.Diagnostics.Debug.Write("EE ");
                //        }
                //        else if (plot.X == x && plot.Y == y)
                //        {
                //            System.Diagnostics.Debug.Write("SS ");
                //        }
                //        else
                //        {
                //            var qp = queuedPoints.FirstOrDefault(q => q.X == x && q.Y == y);
                //            if (qp != null)
                //                System.Diagnostics.Debug.Write($"{qp.Distance.ToString("00")} ");
                //            else
                //                System.Diagnostics.Debug.Write("__ ");
                //        }
                //    }


                //    System.Diagnostics.Debug.WriteLine("");
                //}

                var closePoints = GetPointsClosestTo(queuedPoints, entity.X, entity.Y);
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
                return points.OrderBy(p => p.Distance).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Point> GetPointsClosestTo(IEnumerable<Point> points, int x, int y)
        {
            return points.Where(p =>
                                (p.X == x - 1 && p.Y == y) ||
                                (p.X == x + 1 && p.Y == y) ||
                                (p.X == x && p.Y == y - 1) ||
                                (p.X == x && p.Y == y + 1)
                                );
        }

        private IEnumerable<Point> CalculateDistance(int x, int y, int targetX, int targetY, IEnumerable<Entity> entities, Entity entity)
        {
            Queue<Point> FinishedPoints = new Queue<Point>();
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
                    TryAddPoint(FinishedPoints, nextQueue, point.X - 1, point.Y, distance, Map, entities, entity);
                    TryAddPoint(FinishedPoints, nextQueue, point.X + 1, point.Y, distance, Map, entities, entity);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y - 1, distance, Map, entities, entity);
                    TryAddPoint(FinishedPoints, nextQueue, point.X, point.Y + 1, distance, Map, entities, entity);

                    if (!FinishedPoints.Any(p => p.X == point.X && p.Y == point.Y))
                        FinishedPoints.Enqueue(point);

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

        private static Point TryAddPoint(Queue<Point> finished, Queue<Point> process, int x, int y, int d, Map map, IEnumerable<Entity> entities, Entity entity)
        {
            Point p = null;
            if (!finished.Any(t => t.X == x && y == t.Y) && !process.Any(t => t.X == x && t.Y == y))
            {
                p = new Point(x, y, d);
                if (IsPointValid(p, map, entities, entity))
                {
                    process.Enqueue(p);
                }
            }

            return p;
        }

        private static bool IsPointValid(Point p, Map map, IEnumerable<Entity> entities, Entity entity)
        {
            return IsLocationValid(p.X, p.Y, map, entities, entity);
        }


        private static bool IsLocationValid(int x, int y, Map map, IEnumerable<Entity> entities, Entity entity)
        {
            bool valid = true;
            var other = entities.FirstOrDefault(e => e.X == x && e.Y == y && e.Health > 0);
            if (other == null)
            {
                var space = map[x, y];
                if (space.Type == PlotType.Tree)
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }
    }
}
