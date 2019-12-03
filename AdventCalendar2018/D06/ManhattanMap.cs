using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    public class ManhattanMap
    {
        public IList<ManhattanPoint> Points { get; private set; } = new List<ManhattanPoint>();
        public IList<IList<int>> TrackingData { get; private set; }
        public IList<IList<int>> SeekData { get; private set; }

        public int Bottom { get; private set; } = 0;
        public int Top { get; private set; } = 0;
        public int Left { get; private set; } = 0;
        public int Right { get; private set; } = 0;

        public ManhattanPoint AddPoint(int x, int y)
        {
            var point = new ManhattanPoint
            {
                Id = Points.Count,
                X = x,
                Y = y
            };

            if (y <= Bottom)
                Bottom = y - 1;

            if (y >= Top)
                Top = y + 1;

            if (x <= Left)
                Left = x - 1;

            if (x >= Right)
                Right = x + 1;

            Points.Add(point);

            return point;
        }

        public IList<IList<int>> GenerateAreaTracking()
        {
            IList<IList<int>> trackingColumn = new List<IList<int>>();

            for (int y = Bottom; y <= Top; y++)
            {
                IList<int> trackingRow = new List<int>();

                for (int x = Left; x <= Right; x++)
                {
                    int shortestPath = -1;
                    ManhattanPoint winner = null;

                    foreach (var point in Points)
                    {
                        var distance = point.CalculateDistance(x, y);

                        if (shortestPath == -1 || distance < shortestPath)
                        {
                            shortestPath = distance;
                            winner = point;
                        }
                        else if (distance == shortestPath)
                        {
                            winner = null;
                        }
                    }

                    if (winner != null)
                    {
                        trackingRow.Add(winner.Id);
                        if (x == Left || x == Right || y == Bottom || y == Top)
                        {
                            winner.IsInfinite = true;
                        }
                    }
                    else
                    {
                        trackingRow.Add(-1);
                    }
                }

                trackingColumn.Add(trackingRow);
            }

            TrackingData = trackingColumn;

            return trackingColumn;
        }

        public IList<IList<int>> GeneratePointSeek(int v)
        {
            IList<IList<int>> seekColumn = new List<IList<int>>();

            for (int y = Bottom; y <= Top; y++)
            {
                IList<int> seekRow = new List<int>();

                for (int x = Left; x <= Right; x++)
                {
                    int totalDistance = 0;

                    foreach (var point in Points)
                    {
                        var distance = point.CalculateDistance(x, y);

                        totalDistance += distance;
                    }

                    seekRow.Add(totalDistance);
                }

                seekColumn.Add(seekRow);
            }

            SeekData = seekColumn;

            return seekColumn;
        }

        public int GetArea(ManhattanPoint e)
        {
            int area = 0;
            foreach (var column in TrackingData)
            {
                area += column.Count(x => x.Equals(e.Id));
            }

            return area;
        }
    }
}
