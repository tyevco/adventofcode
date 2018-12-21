using Advent.Utilities;
using System.Collections.Generic;

namespace Day18
{
    public class ForestParser : DataParser<Grid>
    {
        protected override Grid DeserializeData(IList<string> data)
        {
            Grid grid = new Grid(data[0].Length, data.Count);

            for (int y = 0; y < data.Count; y++)
            {
                var line = data[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];

                    if (c == '|')
                    {
                        grid.AddLocation(x, y, LocationType.Tree);
                    }
                    else if (c == '.')
                    {
                        grid.AddLocation(x, y, LocationType.Open);
                    }
                    else if (c == '#')
                    {
                        grid.AddLocation(x, y, LocationType.Lumberyard);
                    }
                }
            }

            return grid;
        }
    }
}
