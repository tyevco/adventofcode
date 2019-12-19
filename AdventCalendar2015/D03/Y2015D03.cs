using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2015.D03
{
    [Exercise("Day 3: Perfectly Spherical Houses in a Vacuum")]
    class Y2015D03 : FileSelectionConsole
    {
        public void Execute()
        {
            this.Start("D03/Data");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            Console.WriteLine("Santa");

            foreach (var line in lines)
            {
                IDictionary<string, DataPoint<int>> points = new Dictionary<string, DataPoint<int>>();
                points.Add("0,0", new DataPoint<int>(0, 0) { Data = 1 });
                int expected = -1;
                var sections = line.Split("=>");

                if (sections.Length > 1)
                {
                    expected = int.Parse(sections[1]);
                }

                int currX = 0, currY = 0;

                foreach (var direction in sections[0])
                {
                    switch (direction)
                    {
                        case '>':
                            currX++;
                            break;
                        case '^':
                            currY--;
                            break;
                        case 'v':
                            currY++;
                            break;
                        case '<':
                            currX--;
                            break;
                    }

                    var key = $"{currX},{currY}";

                    DataPoint<int> point;
                    if (!points.ContainsKey(key))
                    {
                        point = new DataPoint<int>(currX, currY)
                        {
                            Data = 0
                        };

                        points.Add(key, point);
                    }
                    else
                    {
                        point = points[key];
                    }

                    var count = (int)point.Data;
                    point.Data = ++count;
                }

                PrintData(points, expected);
            }

            Console.WriteLine();
            Console.WriteLine("Santa & Robo-Santa");

            foreach (var line in lines)
            {
                IDictionary<string, DataPoint<int>> points = new Dictionary<string, DataPoint<int>>();
                points.Add("0,0", new DataPoint<int>(0, 0) { Data = 2 });
                int expected = -1;
                var sections = line.Split("=>");

                if (sections.Length > 1)
                {
                    expected = int.Parse(sections[1]);
                }

                int santaCurrX = 0, santaCurrY = 0,
                    robotCurrX = 0, robotCurrY = 0;

                for (int i = 0; i < sections[0].Length; i++)
                {
                    char direction = sections[0][i];
                    bool isSanta;

                    int currX = 0, currY = 0;
                    if (i % 2 == 0)
                    {
                        isSanta = true;
                        currX = santaCurrX;
                        currY = santaCurrY;
                    }
                    else
                    {
                        isSanta = false;
                        currX = robotCurrX;
                        currY = robotCurrY;
                    }

                    switch (direction)
                    {
                        case '>':
                            currX = isSanta ? ++santaCurrX : ++robotCurrX;
                            break;
                        case '^':
                            currY = isSanta ? --santaCurrY : --robotCurrY;
                            break;
                        case 'v':
                            currY = isSanta ? ++santaCurrY : ++robotCurrY;
                            break;
                        case '<':
                            currX = isSanta ? --santaCurrX : --robotCurrX;
                            break;
                    }

                    var key = $"{currX},{currY}";

                    DataPoint<int> point;
                    if (!points.ContainsKey(key))
                    {
                        point = new DataPoint<int>(currX, currY)
                        {
                            Data = 1
                        };

                        points.Add(key, point);
                    }
                    else
                    {
                        point = points[key];
                    }

                    var count = (int)point.Data;
                    point.Data = ++count;
                }

                PrintData(points, expected);
            }
        }

        private void PrintData(IDictionary<string, DataPoint<int>> points, int expected = -1)
        {
            var xMin = points.Select(x => x.Value.X).Min();
            var xMax = points.Select(x => x.Value.X).Max();
            var yMin = points.Select(x => x.Value.Y).Min();
            var yMax = points.Select(x => x.Value.Y).Max();
            var houseCount = points.Select(x => (int)x.Value.Data).Max();
            var numLen = 1; // houseCount.ToString().Length;

            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if (points.ContainsKey($"{x},{y}"))
                    {
                        var p = points[$"{x},{y}"];
                        //Console.Write($"{p.Data.ToString().PadLeft(numLen, ' ')}");
                        if (x == 0 && y == 0)
                        {
                            Console.Write(ConsoleCodes.Colorize("█".PadLeft(numLen, ' '), 0x2a));
                        }
                        else
                        {
                            Console.Write($"{"█".PadLeft(numLen, ' ')}");
                        }
                    }
                    else
                    {
                        Console.Write($"{string.Empty.PadLeft(numLen, ' ')}");
                    }
                }

                Console.WriteLine();
            }

            if (expected > 0)
            {
                Console.WriteLine($"{points.Count} found, expected {expected}. {points.Count == expected}");
            }
            else
            {
                Console.WriteLine($"{points.Count} found.");
            }
        }
    }
}
