using System.Collections.Generic;
using Advent.Utilities;
using Advent.Utilities.Data;

namespace AdventCalendar2019.D03
{
    class WirePlotter : DataParser<IList<ManhattanMap>>
    {
        protected override IList<ManhattanMap> DeserializeData(IList<string> data)
        {
            IList<ManhattanMap> maps = new List<ManhattanMap>();
            int startX = 0, startY = 0;

            foreach (var wirePathData in data)
            {
                ManhattanMap wireGrid = new ManhattanMap();
                var wirePath = wirePathData.Split(",");
                int currX = startX, currY = startY;

                foreach (var wireDir in wirePath)
                {
                    var distance = int.Parse(wireDir.Substring(1));

                    //System.Console.WriteLine($"{currX},{currY}:{wireDir}");
                    switch (wireDir[0])
                    {
                        case 'U':
                            while (distance > 0)
                            {
                                wireGrid.AddPoint(currX, --currY);
                                distance--;
                            }
                            break;
                        case 'D':
                            while (distance > 0)
                            {
                                wireGrid.AddPoint(currX, ++currY);
                                distance--;
                            }
                            break;
                        case 'L':
                            while (distance > 0)
                            {
                                wireGrid.AddPoint(--currX, currY);
                                distance--;
                            }
                            break;
                        case 'R':
                            while (distance > 0)
                            {
                                wireGrid.AddPoint(++currX, currY);

                                distance--;
                            }
                            break;
                    }
                }
                maps.Add(wireGrid);
            }

            return maps;
        }
    }
}
