using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventCalendar2018.D11
{
    public class FuelGrid
    {
        IList<IList<FuelCell>> fuelGrid = new List<IList<FuelCell>>();

        public FuelGrid(int width, int height, int serialNumber)
        {
            for (int y = 0; y < height; y++)
            {
                var fuelRow = new List<FuelCell>();
                for (int x = 0; x < width; x++)
                {
                    var fuelCell = new FuelCell(x + 1, y + 1, serialNumber);

                    fuelRow.Add(fuelCell);
                }

                fuelGrid.Add(fuelRow);
            }
        }

        object itemLock = new object();

        public FuelPoint FindLargestSubSectionWithMostPower()
        {
            IList<int> widths = new List<int>();

            for (int i = 10; i <= 30; i++)
            {
                widths.Add(i);
            }

            int maxPower = int.MinValue;
            FuelPoint maxPoint = null;
            int complete = 0;

            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 10
            };

            Parallel.ForEach(widths, parallelOptions, i =>
            {
                var point = FindSubSectionWithMostPower(i);
                lock (itemLock)
                {
                    if (point != null && (maxPoint == null || point.TotalPower > maxPower))
                    {
                        maxPoint = point;
                        maxPower = point.TotalPower;
                    }
                    complete++;
                }

                System.Diagnostics.Debug.WriteLine($"{i} : {complete} of {widths.Count} finished.");
            });

            return maxPoint;
        }

        public FuelPoint FindSubSectionWithMostPower(int size)
        {
            int maxPower = int.MinValue;
            FuelPoint maxPoint = null;

            for (int y = 0; y < fuelGrid.Count - size; y++)
            {
                for (int x = 0; x < fuelGrid[y].Count - size; x++)
                {
                    var point = new FuelPoint
                    {
                        X = x + 1,
                        Y = y + 1,
                        Size = size
                    };

                    for (int dy = y; dy < y + size; dy++)
                    {
                        var fuelRow = fuelGrid[dy];


                        for (int dx = x; dx < x + size; dx++)
                        {
                            var fuelCell = fuelRow[dx];
                            point.TotalPower += fuelCell.PowerLevel;
                        }
                    }

                    if (maxPoint == null || point.TotalPower > maxPower)
                    {
                        maxPoint = point;
                        maxPower = point.TotalPower;
                    }
                }
            }

            return maxPoint;
        }
    }
}
