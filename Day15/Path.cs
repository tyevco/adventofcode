using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day15
{
    public class Path
    {
        Queue<Point> Points { get; } = new Queue<Point>();

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
                        available.Add(plot);
                    }
                }

                if (target.X < map.Width - 1)
                {
                    if (IsLocationValid(target.X + 1, target.Y, map, entities, entity))
                    {
                        var plot = map[target.X + 1, target.Y];
                        available.Add(plot);
                    }
                }

                if (target.Y > 0)
                {
                    if (IsLocationValid(target.X, target.Y - 1, map, entities, entity))
                    {
                        var plot = map[target.X, target.Y - 1];
                        available.Add(plot);
                    }
                }

                if (target.Y < map.Height - 1)
                {
                    if (IsLocationValid(target.X, target.Y + 1, map, entities, entity))
                    {
                        var plot = map[target.X, target.Y + 1];
                        available.Add(plot);
                    }
                }
            }

            object lockObject = new object();
            ICollection<Point> points = new List<Point>();
            Parallel.ForEach(available, plot =>
            {
                var path = new Path(map);
                path.CalculateDistance(plot.X, plot.Y, entity.X, entity.Y, entities, entity);
                var closePoints = path.GetPointsClosestTo(entity.X, entity.Y);
                if (closePoints.Any())
                {
                    lock (lockObject)
                    {
                        foreach (var p in closePoints)
                            points.Add(p);
                    }
                }
            });

            if (points.Any())
            {
                return points.OrderBy(p => p.Distance).ThenBy(p => p.X + p.Y * map.Width).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<Point> GetPointsClosestTo(int x, int y)
        {
            return Points.Where(p =>
                                (p.X == x - 1 && p.Y == y) ||
                                (p.X == x + 1 && p.Y == y) ||
                                (p.X == x && p.Y == y - 1) ||
                                (p.X == x && p.Y == y + 1)
                                );
        }

        private void CalculateDistance(int x, int y, int targetX, int targetY, IEnumerable<Entity> entities, Entity entity)
        {
            Points.Enqueue(new Point(x, y, 0));
            bool seeking = true;
            int distance = 0;
            int lastPointCount = 0;
            while (seeking)
            {
                distance++;
                var count = Points.Count;

                for (int i = 0; i < count; i++)
                {
                    var point = Points.ElementAt(i);

                    TryAddPoint(point.X - 1, point.Y, distance, Map, entities, entity);
                    TryAddPoint(point.X + 1, point.Y, distance, Map, entities, entity);
                    TryAddPoint(point.X, point.Y - 1, distance, Map, entities, entity);
                    TryAddPoint(point.X, point.Y + 1, distance, Map, entities, entity);
                }

                if (lastPointCount != Points.Count)
                {
                    lastPointCount = Points.Count;
                }
                else
                {
                    break;
                }
            }
        }

        private Point TryAddPoint(int x, int y, int d, Map map, IEnumerable<Entity> entities, Entity entity)
        {
            Point p = null;
            if (!Points.Any(t => t.X == x && y == t.Y))
            {
                p = new Point(x, y, d);
                if (IsPointValid(p, map, entities, entity))
                {
                    Points.Enqueue(p);
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
