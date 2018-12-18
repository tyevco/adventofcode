using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    public class Map
    {
        public int Width { get; }
        public int Height { get; }

        public Plot[] Plots { get; internal set; }

        public IList<Entity> Entities { get; set; } = new List<Entity>();

        public Map(int width, int height)
        {
            Plots = new Plot[width * height];
            Width = width;
            Height = height;
        }

        public Plot this[int x, int y]
        {
            get
            {
                return Plots[GetIndex(x, y)];
            }
            set
            {
                Plots[GetIndex(x, y)] = value;
            }
        }

        public void AddPlot(int x, int y, PlotType type)
        {
            Plots[GetIndex(x, y)] = new Plot(x, y, type);
        }

        private int GetIndex(int x, int y)
        {
            return x + (y * Width);
        }

        public void AddEntity(int x, int y, EntityType type)
        {
            var e = new Entity(x, y, type);
            e.Id = (Entities.Count + 1).ToString("X").PadLeft(2, '0');
            Entities.Add(e);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var f = Entities.FirstOrDefault(e => e.X == x && e.Y == y);
                    if (f != null)
                    {
                        sb.Append(f);
                    }
                    else
                    {
                        sb.Append(Plots[GetIndex(x, y)]);
                    }
                }

                sb.Append("   ");
                foreach (var entity in Entities.Where(e => e.Y == y && e.Health > 0).OrderBy(e => e.X))
                {
                    sb.Append($"{entity}({entity.Health}), ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}