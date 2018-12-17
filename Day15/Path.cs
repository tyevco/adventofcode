using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    public class Path
    {
        Queue<Point> Points = new Queue<Point>();
        public Map Map { get; }

        private Path(Map map)
        {
            Map = map;
        }

        private int GetIndex(int x, int y)
        {
            return x + (y * Map.Width);
        }

        public static Point FindTargetPoint(Map map, IList<Entity> entities, Entity entity)
        {
            int x = entity.X;
            int y = entity.Y;

            IList<Plot> available = new List<Plot>();

            var targetEntities = entities.Where(e => e.Type != entity.Type);
            foreach (var target in targetEntities)
            {
                if (target.X > 0)
                {
                    var plot = map[target.X - 1, target.Y];
                    if (plot.Type == PlotType.Open)
                    {
                        available.Add(plot);
                    }
                }

                if (target.X < map.Width - 1)
                {
                    var plot = map[target.X + 1, target.Y];
                    if (plot.Type == PlotType.Open)
                    {
                        available.Add(plot);
                    }
                }

                if (target.Y > 0)
                {
                    var plot = map[target.X, target.Y - 1];
                    if (plot.Type == PlotType.Open)
                    {
                        available.Add(plot);
                    }
                }

                if (target.Y < map.Height - 1)
                {
                    var plot = map[target.X, target.Y + 1];
                    if (plot.Type == PlotType.Open)
                    {
                        available.Add(plot);
                    }
                }
            }

            IList<Point> points = new List<Point>();
            IList<Path> paths = new List<Path>();
            foreach (var plot in available)
            {
                var path = new Path(map);
                path.CalculateDistance(plot.X, plot.Y, entity.X, entity.Y, entities, entity);
                var point = path.GetPointClosestTo(entity.X, entity.Y);
                if (point != null)
                {
                    points.Add(point);
                }
            }

            return points.OrderBy(p => p.Distance).FirstOrDefault();
        }

        private Point GetPointClosestTo(int x, int y)
        {
            return Points.Where(p =>
                                (p.X == x - 1 && p.Y == y) ||
                                (p.X == x + 1 && p.Y == y) ||
                                (p.X == x && p.Y == y - 1) ||
                                (p.X == x && p.Y == y + 1)
                                ).OrderBy(p => p.Distance).FirstOrDefault();
        }

        private void CalculateDistance(int x, int y, int targetX, int targetY, IList<Entity> entities, Entity entity)
        {
            Points.Enqueue(new Point(x, y, 0));
            bool seeking = true;
            int distance = 0;
            int lastPointCount = 0;
            while (seeking)
            {
                var count = Points.Count;

                for (int i = 0; i < count; i++)
                {
                    var point = Points.ElementAt(i);

                    {
                        var p = TryAddPoint(point.X - 1, point.Y, distance, entities, entity);

                        if (p != null)
                        {
                            if (p.X == targetX && p.Y == targetY)
                            {
                                seeking = false;
                                break;
                            }
                        }
                    }

                    {
                        var p = TryAddPoint(point.X + 1, point.Y, distance, entities, entity);

                        if (p != null)
                        {
                            if (p.X == targetX && p.Y == targetY)
                            {
                                seeking = false;
                                break;
                            }
                        }
                    }

                    {
                        var p = TryAddPoint(point.X, point.Y - 1, distance, entities, entity);

                        if (p != null)
                        {
                            if (p.X == targetX && p.Y == targetY)
                            {
                                seeking = false;
                                break;
                            }
                        }
                    }

                    {
                        var p = TryAddPoint(point.X, point.Y + 1, distance, entities, entity);

                        if (p != null)
                        {
                            if (p.X == targetX && p.Y == targetY)
                            {
                                seeking = false;
                                break;
                            }
                        }
                    }
                }

                if (lastPointCount != Points.Count)
                {
                    lastPointCount = Points.Count;
                }
                else
                {
                    break;
                }
                distance++;
            }
        }

        private Point TryAddPoint(int x, int y, int d, IList<Entity> entities, Entity entity)
        {
            Point p = null;
            if (!Points.Any(t => t.X == x && y == t.Y))
            {
                p = new Point(x, y, d);
                if (DetermineValid(p, entities, entity))
                {
                    Points.Enqueue(p);
                }
            }

            return p;
        }

        private bool DetermineValid(Point p, IList<Entity> entities, Entity entity)
        {
            bool valid = true;
            var other = entities.FirstOrDefault(e => e.X == p.X && e.Y == p.Y && e.Health > 0);
            if (other == null)
            {
                var space = Map[p.X, p.Y];
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
