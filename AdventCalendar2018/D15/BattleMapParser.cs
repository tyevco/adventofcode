using System.Collections.Generic;
using Advent.Utilities;

namespace AdventCalendar2018.D15
{
    public class BattleMapParser : DataParser<Map>
    {
        protected override Map DeserializeData(IList<string> data)
        {
            Map map = new Map(data[0].Length, data.Count);

            for (int y = 0; y < data.Count; y++)
            {
                var line = data[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];

                    if (c == '#')
                    {
                        map.AddPlot(x, y, PlotType.Tree);
                    }
                    else if (c == '.')
                    {
                        map.AddPlot(x, y, PlotType.Open);
                    }
                    else if (c == 'G')
                    {
                        map.AddPlot(x, y, PlotType.Open);
                        map.AddEntity(x, y, EntityType.Goblin);
                    }
                    else if (c == 'E')
                    {
                        map.AddPlot(x, y, PlotType.Open);
                        map.AddEntity(x, y, EntityType.Elf);
                    }
                }
            }

            return map;
        }
    }
}
