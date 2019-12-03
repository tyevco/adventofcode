using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day10
{
    public class MessageTracker
    {
        static Regex dataExtract = new Regex("position=<([- ]?[0-9]+?), ([- ]?[0-9]+?)> velocity=<([- ]?[0-9]+?), ([- ]?[0-9]+?)>");

        public static IList<Point> ParseData(IList<string> data)
        {
            IList<Point> points = new List<Point>();
            foreach (var entry in data)
            {
                var match = dataExtract.Match(entry);

                var p = new Point
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value),
                    dX = int.Parse(match.Groups[3].Value),
                    dY = int.Parse(match.Groups[4].Value),
                };

                points.Add(p);
            }

            return points;
        }

        public static void Tick(IList<Point> points, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                foreach (var point in points)
                {
                    point.Tick();
                }
            }
        }

        public static int GetWidth(IList<Point> points)
        {
            int left = points.Min(x => x.X);
            int right = points.Max(x => x.X);

            return right - left + 1;
        }

        public static int GetHeight(IList<Point> points)
        {
            int bottom = points.Min(y => y.Y);
            int top = points.Max(y => y.Y);

            return top - bottom + 1;
        }


        public static void Output(IList<Point> points, int iteration = 0)
        {
            int left = points.Min(x => x.X);
            int right = points.Max(x => x.X);
            int bottom = points.Min(y => y.Y);
            int top = points.Max(y => y.Y);

            int w = right - left + 1;
            int h = top - bottom + 1;

            string[][] array = new string[h][];


            for (int i = 0; i < h; i++)
            {
                string[] row = new string[w];

                array[i] = row;
            }

            foreach (var point in points)
            {
                var x = point.X - left;
                var y = point.Y - bottom;

                array[y][x] = "X";
            }

            StringBuilder b = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                b.AppendLine(string.Join(' ', array[i]));
            }

            //for (int y = bottom; y <= top; y++)
            //{
            //    for (int x = left; x <= right; x++)
            //    {
            //        if (points.Any(p => p.X.Equals(x) && p.Y.Equals(y)))
            //        {
            //            b.Append("#");
            //        }
            //        else
            //        {
            //            b.Append(" ");
            //        }
            //    }
            //}

            File.WriteAllText($"day10_{iteration + 1}.txt", b.ToString());
        }
    }
}
