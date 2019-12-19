using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Data.Extensions;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D18
{
    public class Maze : IGrid<DataPoint<char>, char>
    {
        public Maze(string mazeLayout, int width, int height)
        {
            Width = width;
            Height = height;

            int i = 0;

            int robotCount = mazeLayout.Count(x => x == '@');
            Points = new DataPoint<char>[mazeLayout.Length];
            Robots = new Robot[robotCount];

            foreach (char c in mazeLayout)
            {
                (int x, int y) = Position(i);
                DataPoint<char> point = new DataPoint<char>(x, y)
                {
                    Data = c
                };

                if (c == '@')
                {
                    point.Data = char.Parse(robotCount.ToString());
                    Robots[--robotCount] = new Robot
                    {
                        X = x,
                        Y = y,
                    };
                }

                Points[i] = point;

                if (c != '#' && c != '@' && char.IsLower(c))
                {
                    KeyLocations.Add(point);
                }

                i++;
            }
        }

        public Robot[] Robots { get; set; }

        public DataPoint<char> this[int x, int y]
        {
            get
            {
                int i = Index(x, y);

                if (i >= 0)
                {
                    return Points[i];
                }

                return null;
            }

            set
            {
                int i = Index(x, y);

                if (i >= 0)
                {
                    Points[i] = value;
                }
            }
        }

        public IList<DataPoint<char>> KeyLocations { get; } = new List<DataPoint<char>>();

        public int Width { get; }

        public int Height { get; }

        public int MinX { get; } = 0;

        public int MaxX => Width - 1;

        public int MinY { get; } = 0;

        public int MaxY => Height - 1;

        public DataPoint<char>[] Points { get; }

        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
                this.Print();
        }

        public bool Has(int x, int y)
        {
            return Index(x, y) >= 0;
        }

        private int Index(int x, int y)
        {
            if (y >= Height || y < 0 || x < 0 || x >= Width)
                return -1;

            return (y * Width) + x;
        }

        private (int, int) Position(int index)
        {
            return (index % Width, index / Width);
        }
    }
}
