using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;

namespace AdventCalendar2018.D22
{
    public class CaveBuilder : DataParser<(Cave, int, string)>
    {
        Regex depthRegex = new Regex("depth: ([0-9]+)");
        Regex targetRegex = new Regex("target: ([0-9]+),([0-9]+)");
        Regex riskRegex = new Regex("risk: ([0-9]+)");

        protected override (Cave, int, string) DeserializeData(IList<string> data)
        {
            Cave cave = null;
            string expected = null;
            int risk = -1;
            if (data.Any())
            {
                var depth = int.Parse(depthRegex.Match(data[0]).Groups[1].Value);
                var targetMatch = targetRegex.Match(data[1]);
                var targetX = int.Parse(targetMatch.Groups[1].Value);
                var targetY = int.Parse(targetMatch.Groups[2].Value);

                if (data.Count > 2)
                {
                    risk = int.Parse(riskRegex.Match(data[2]).Groups[1].Value);
                }

                if (data.Count > 3)
                {
                    expected = string.Join("\r\n", data.Skip(3).Take(data.Count - 3));
                }

                // build cave
                cave = new Cave(targetX + 5 + 1, targetY + 5 + 1, depth)
                {
                    TargetPosition = (targetX, targetY)
                };

                for (int y = 0; y < cave.Height; y++)
                {
                    for (int x = 0; x < cave.Width; x++)
                    {
                        int geologicalIndex;
                        if (x == 0 && y == 0)
                        {
                            geologicalIndex = 0;
                        }
                        else if (targetX == x && targetY == y)
                        {
                            geologicalIndex = 0;
                        }
                        else if (x == 0)
                        {
                            geologicalIndex = y * 48271;
                        }
                        else if (y == 0)
                        {
                            geologicalIndex = x * 16807;
                        }
                        else
                        {
                            Region r1 = cave.Regions[x - 1, y];
                            Region r2 = cave.Regions[x, y - 1];

                            geologicalIndex = r1.ErosionLevel * r2.ErosionLevel;
                        }

                        int erosionLevel = (geologicalIndex + depth) % 20183;

                        cave.Regions[x, y] = new Region(x, y, erosionLevel);
                    }
                }
            }

            return (cave, risk, expected);
        }


    }
}
