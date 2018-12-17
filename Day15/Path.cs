using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    public class Path
    {
        int?[] targets;
        public Map Map { get; }

        public int? this[int x, int y]
        {
            get
            {
                return targets[GetIndex(x, y)];
            }
            set
            {
                targets[GetIndex(x, y)] = value;
            }
        }

        private Path(Map map)
        {
            targets = new int?[map.Width * map.Height];
            Map = map;
        }

        private int GetIndex(int x, int y)
        {
            return x + (y * Map.Width);
        }

        public static Point FindTarget(Map map, IList<Entity> entities, Entity entity)
        {
            Point point = new Point();
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

            IList<Path> paths = new List<Path>();
            foreach (var plot in available)
            {
                var path = new Path(map);
                path.CalculateDistance(plot.X, plot.Y, entities, entity);
                System.Diagnostics.Debug.WriteLine($"{plot.X},{plot.Y}");
                System.Diagnostics.Debug.WriteLine(path);
            }

            return point;
        }

        private void CalculateDistance(int x, int y, IList<Entity> entities, Entity entity)
        {
            this[x, y] = 0;

            var hyp = (int)Math.Sqrt(Math.Pow(Map.Width, 2) + Math.Pow(Map.Height, 2));
            for (int i = 1; i < hyp; i++)
            {
                for (int dy = -i; dy <= i; dy++)
                {
                    for (int dx = -i; dx <= i; dx++)
                    {
                        var mv = Math.Abs(dx) + Math.Abs(dy);
                        if (mv == i)
                        {
                            if (x + dx >= 0 && x + dx < Map.Width &&
                                y + dy >= 0 && y + dy < Map.Height)
                            {
                                DetermineDistance(x + dx, y + dy, entities, entity);
                            }
                        }
                    }
                }
            }
        }

        private int DetermineDistance(int x, int y, IList<Entity> entities, Entity entity)
        {
            int distance = Map.Width * Map.Height + 1;
            if (this[x, y] == null)
            {
                var other = entities.FirstOrDefault(e => e.X == x && e.Y == y && e.Health > 0);
                if (other == null)
                {
                    var space = Map[x, y];
                    if (space.Type == PlotType.Tree)
                    {
                    }
                    else
                    {
                        int min = GetMinimumNeighborDistance(x, y);

                        this[x, y] = min + 1;
                    }
                }
                else
                {
                }
            }

            return distance;
        }

        private int GetMinimumNeighborDistance(int x, int y)
        {
            int cv = Map.Width * Map.Height + 1;

            if (x > 0)
            {
                var val = this[x - 1, y];

                if (val.HasValue && val < cv)
                {
                    cv = val.Value;
                }
            }

            if (x < Map.Width - 1)
            {
                var val = this[x + 1, y];

                if (val.HasValue && val < cv)
                {
                    cv = val.Value;
                }
            }

            if (y > 0)
            {
                var val = this[x, y - 1];

                if (val.HasValue && val < cv)
                {
                    cv = val.Value;
                }
            }

            if (y < Map.Height - 1)
            {
                var val = this[x, y + 1];

                if (val.HasValue && val < cv)
                {
                    cv = val.Value;
                }
            }

            return cv;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    sb.Append($" {(this[x, y].HasValue ? this[x, y].Value.ToString("00") : "__")}");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
