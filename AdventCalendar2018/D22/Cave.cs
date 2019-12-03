using System;
using System.Text;
using Advent.Utilities.Data;

namespace AdventCalendar2018.D22
{
    public class Cave
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }
        public Grid<Region> Regions { get; private set; }

        public (int, int) StartPosition { get; set; } = (0, 0);
        public (int, int) TargetPosition { get; set; }
        public int RiskLevel
        {
            get
            {
                int riskLevel = 0;
                for (int y = 0; y < Math.Min(Height, TargetPosition.Item2 + 1); y++)
                {
                    for (int x = 0; x < Math.Min(Width, TargetPosition.Item1 + 1); x++)
                    {
                        if (!(x == TargetPosition.Item1 && y == TargetPosition.Item2))
                            riskLevel += (int)Regions[x, y].Type;
                    }
                }

                return riskLevel;
            }
        }

        public Cave(int width, int height, int depth)
        {
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            Regions = new Grid<Region>(width, height);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == StartPosition.Item1 && y == StartPosition.Item2)
                    {
                        sb.Append("M");
                    }
                    else if (x == TargetPosition.Item1 && y == TargetPosition.Item2)
                    {
                        sb.Append("T");
                    }
                    else
                    {
                        switch (Regions[x, y].Type)
                        {
                            case RegionType.Rocky:
                                sb.Append(".");
                                break;
                            case RegionType.Wet:
                                sb.Append("=");
                                break;
                            case RegionType.Narrow:
                                sb.Append("|");
                                break;
                        }
                    }
                }

                if (y < Height - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}