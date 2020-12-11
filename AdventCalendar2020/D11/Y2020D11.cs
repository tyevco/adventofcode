using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D11
{
    [Exercise("Day 11: Seating System")]
    class Y2020D11 : FileSelectionParsingConsole<char[,]>, IExercise
    {
        public void Execute()
        {
            Start("D11/Data");
        }

        const char Floor = '.';
        const char Empty = 'L';
        const char Occupied = '#';

        int Width = 0;
        int Height = 0;

        protected override char[,] DeserializeData(IList<string> data)
        {
            Width = data[0].Length;
            Height = data.Count;

            char[,] seats = new char[data[0].Length, data.Count];

            for (int y = 0; y < data.Count; y++)
            {
                var line = data[y];
                for (int x = 0; x < line.Length; x++)
                {
                    seats[x, y] = line[x];

                }
            }

            return seats;
        }


        protected override void Execute(char[,] seats)
        {
            PerformOne((char[,])seats.Clone());
            PerformTwo((char[,])seats.Clone());
        }

        protected void PerformOne(char[,] seats)
        {
            bool running = true;

            while (running)
            {
                char[,] next = (char[,])seats.Clone();

                bool modified = false;

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var totals = LookAtNeighbors(x, y, seats);

                        switch (seats[x, y])
                        {
                            case Empty:
                                if (totals.occupied == 0)
                                {
                                    next[x, y] = Occupied;
                                    modified = true;
                                }
                                break;
                            case Occupied:
                                if (totals.occupied >= 4)
                                {
                                    next[x, y] = Empty;

                                    modified = true;
                                }
                                break;
                        }
                    }
                }

                seats = (char[,])next.Clone();
                running = modified;
            }

            int totalOccupied = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (seats[x, y] == Occupied)
                        totalOccupied++;
                }
            }

            AnswerPartOne(totalOccupied);
        }

        protected void PerformTwo(char[,] seats)
        {
            bool running = true;

            while (running)
            {
                char[,] next = (char[,])seats.Clone();

                bool modified = false;

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var totals = LookAtVisible(x, y, seats);

                        switch (seats[x, y])
                        {
                            case Empty:
                                if (totals.occupied == 0)
                                {
                                    next[x, y] = Occupied;
                                    modified = true;
                                }
                                break;
                            case Occupied:
                                if (totals.occupied >= 5)
                                {
                                    next[x, y] = Empty;

                                    modified = true;
                                }
                                break;
                        }
                    }
                }

                seats = (char[,])next.Clone();
                running = modified;
            }

            int totalOccupied = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (seats[x, y] == Occupied)
                        totalOccupied++;
                }
            }

            AnswerPartTwo(totalOccupied);
        }


        private (int occupied, int empty, int floor) LookAtNeighbors(int x, int y, char[,] seats)
        {
            int count = 0;
            int empty = 0;
            int floor = 0;

            (int minX, int minY, int maxX, int maxY) range = (x, y, x, y);

            if (x > 0)
            {
                range.minX = x - 1;
            }

            if (x < Width - 1)
            {
                range.maxX = x + 1;
            }

            if (y > 0)
            {
                range.minY = y - 1;
            }

            if (y < Height - 1)
            {
                range.maxY = y + 1;
            }

            for (int cX = range.minX; cX <= range.maxX; cX++)
            {
                for (int cY = range.minY; cY <= range.maxY; cY++)
                {
                    // don't look at the base
                    if (cX == x && cY == y)
                    {
                        continue;
                    }

                    switch (seats[cX, cY])
                    {
                        case Empty:
                            empty++;
                            break;

                        case Occupied:
                            count++;
                            break;

                        default:
                            floor++;
                            break;
                    }
                }
            }

            return (count, empty, floor);
        }

        private (int occupied, int empty, int floor) LookAtVisible(int x, int y, char[,] seats)
        {
            var directions = new char[] {
                LookInDirection(x, y, 0, -1, seats),
                LookInDirection(x, y, 0, 1, seats),
                LookInDirection(x, y, 1, 0, seats),
                LookInDirection(x, y, -1, 0, seats),
                LookInDirection(x, y, 1, -1, seats),
                LookInDirection(x, y, -1, -1, seats),
                LookInDirection(x, y, 1, 1, seats),
                LookInDirection(x, y, -1, 1, seats)
            };


            return (directions.Count(c => c == Occupied),
                        directions.Count(c => c == Empty),
                        directions.Count(c => c == Floor));
        }

        private char LookInDirection(int x, int y, int dX, int dY, char[,] seats)
        {
            int cX = x;
            int cY = y;

            char value = Floor;

            bool seeking = true;
            while (seeking)
            {
                cX += dX;
                cY += dY;

                if ((cX < 0 || cX >= Width) || (cY < 0 || cY >= Height))
                {
                    break;
                }

                if (seats[cX, cY] != Floor)
                {
                    value = seats[cX, cY];
                    break;
                }
            }

            return value;
        }
    }
}
