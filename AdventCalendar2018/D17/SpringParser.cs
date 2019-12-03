using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;

namespace AdventCalendar2018.D17
{
    public class SpringParser : DataParser<Grid>
    {
        Regex linePatternXFirst = new Regex(@"x=([0-9]+), y=([0-9]+)..([0-9]+)");
        Regex linePatternYFirst = new Regex(@"y=([0-9]+), x=([0-9]+)..([0-9]+)");
        protected override Grid DeserializeData(IList<string> data)
        {
            Grid grid = null;

            IList<(int, int)> claySpots = new List<(int, int)>();

            foreach (var line in data)
            {
                if (linePatternXFirst.IsMatch(line))
                {
                    var match = linePatternXFirst.Match(line);

                    int x = int.Parse(match.Groups[1].Value);
                    int startY = int.Parse(match.Groups[2].Value);
                    int endY = int.Parse(match.Groups[3].Value);

                    for (int y = startY; y <= endY; y++)
                    {
                        claySpots.Add((x, y));
                    }
                }
                else
                {
                    var match = linePatternYFirst.Match(line);
                    int y = int.Parse(match.Groups[1].Value);
                    int startX = int.Parse(match.Groups[2].Value);
                    int endX = int.Parse(match.Groups[3].Value);

                    for (int x = startX; x <= endX; x++)
                    {
                        claySpots.Add((x, y));
                    }
                }
            }

            int minX = claySpots.Select(x => x.Item1).Min();
            int maxX = claySpots.Select(x => x.Item1).Max();
            int minY = claySpots.Select(x => x.Item2).Min();
            int maxY = claySpots.Select(x => x.Item2).Max();

            grid = new Grid(minX, maxX, minY, maxY);

            foreach ((int x, int y) in claySpots)
            {
                grid[x, y] = new Clay(x, y, grid);
            }

            grid[500, 0] = new Spring(500, 0, grid);

            return grid;
        }
    }
}
