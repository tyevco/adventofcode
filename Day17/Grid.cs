using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day17
{
    public class Grid
    {
        Material[] Materials;

        public int Width { get; }
        public int Height { get; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }
        public bool Added { get; set; }

        public Material this[int x, int y]
        {
            get
            {
                if (x >= Left && x <= Right &&
                    y >= 0 && y < Height)
                    return Materials[GetIndex(x, y)];

                return null;
            }
            set
            {
                if (x >= Left && x <= Right &&
                    y >= 0 && y < Height)
                {
                    Materials[GetIndex(x, y)] = value;
                }
            }
        }

        internal void Propogate()
        {
            Added = false;
            foreach (var m in Materials)
            {
                if (m != null)
                {
                    m.Propagate();
                }
            }
        }

        private int GetIndex(int x, int y)
        {
            return (x - Left) + (y) * Width;
        }

        public Grid(int minX, int maxX, int minY, int maxY)
        {
            Left = minX - 1;
            Right = maxX + 1;
            Top = minY;
            Bottom = maxY;

            Width = (Right - Left) + 1;
            Height = (Bottom + 1);
            Materials = new Material[Width * Height];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = Left; x <= Right; x++)
                {
                    var m = this[x, y];
                    if (m != null)
                        sb.Append(m);
                    else
                        sb.Append(".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public IList<Material> Active()
        {
            return Materials.Skip(Width * Top).Take(Width * ((Bottom - Top) + 1)).ToList();
        }
    }
}
