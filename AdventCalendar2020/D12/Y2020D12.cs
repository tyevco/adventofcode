using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Extensions;
using System;
using System.Collections.Generic;

namespace AdventCalendar2020.D12
{
    [Exercise("Day 12: Rain Risk")]
    class Y2020D12 : FileSelectionParsingConsole<IList<(char command, int amount)>>, IExercise
    {
        public void Execute()
        {
            Start("D12/Data");
        }

        protected override IList<(char command, int amount)> DeserializeData(IList<string> data)
        {
            IList<(char, int)> items = new List<(char, int)>();
            foreach (var line in data)
            {
                items.Add((line[0], int.Parse(line[1..])));
            }

            return items;
        }

        protected override void Execute(IList<(char command, int amount)> data)
        {
            PartOne(data);
            PartTwo(data);
        }

        protected void PartOne(IList<(char command, int amount)> data)
        {
            char dir = 'E';
            (int x, int y) pos = (0, 0);


            for (int i = 0; i < data.Count; i++)
            {
                var (command, amount) = data[i];

                switch (command)
                {
                    case 'L':
                        dir = Turn(dir, -amount);
                        break;
                    case 'R':
                        dir = Turn(dir, amount);
                        break;
                    case 'F':
                        pos = Move(dir, amount, pos);
                        break;
                    default:
                        pos = Move(command, amount, pos);
                        break;
                }
            }

            AnswerPartOne(pos.ManhattanDistance());
        }

        protected void PartTwo(IList<(char command, int amount)> data)
        {
            (int x, int y) pos = (0, 0);
            (int x, int y) wp = (10, -1);

            for (int i = 0; i < data.Count; i++)
            {
                var (command, amount) = data[i];

                switch (command)
                {
                    case 'L':
                        wp = TurnWaypoint(wp, -amount);
                        break;
                    case 'R':
                        wp = TurnWaypoint(wp, amount);
                        break;
                    case 'F':
                        pos = MoveToward(amount, pos, wp);
                        break;
                    default:
                        wp = Move(command, amount, wp);
                        break;
                }
            }

            AnswerPartOne(pos.ManhattanDistance());
        }

        private static (int x, int y) Move(char dir, int amount, (int x, int y) pos)
        {
            Console.Write($"moving {amount} {dir} from {pos.x},{pos.y}");

            switch (dir)
            {
                case 'N':
                    pos.y -= amount;
                    break;
                case 'S':
                    pos.y += amount;
                    break;
                case 'E':
                    pos.x += amount;
                    break;
                case 'W':
                    pos.x -= amount;
                    break;
            }

            Console.WriteLine($" to {pos.x},{pos.y}");

            return pos;
        }

        private static (int x, int y) MoveToward(int amount, (int x, int y) pos, (int x, int y) waypoint)
        {
            Console.WriteLine($"moving {amount * waypoint.x}, {amount * waypoint.y}");

            pos.x += (amount * waypoint.x);
            pos.y += (amount * waypoint.y);

            return pos;
        }

        private static readonly List<char> directions = new List<char> { 'N', 'E', 'S', 'W' };

        private static char Turn(char facing, int degrees)
        {
            var turns = degrees / 90;

            var currentIndex = directions.IndexOf(facing);

            var newIndex = (currentIndex + turns) % 4;

            if (newIndex < 0)
            {
                newIndex = directions.Count + newIndex;
            }

            facing = directions[newIndex];

            return facing;
        }

        private static readonly (int x, int y)[] TurnMultiples = new (int x, int y)[] { (-1, 1), (-1, -1), (1, -1), (1, 1), (-1, 1), (-1, -1), (1, -1) };

        private static (int x, int y) TurnWaypoint((int x, int y) pos, int degrees)
        {
            var turns = degrees / 90;

            (int x, int y) m = (1, 1);
            bool flipXY = Math.Abs(turns % 2) == 1;

            if (turns > 0)
            {
                m = TurnMultiples[turns - 1];
            }
            else if (turns < 0)
            {
                m = TurnMultiples[TurnMultiples.Length + turns];
            }

            if (flipXY)
            {
                int y = pos.x * m.y;
                int x = pos.y * m.x;
                pos = (x, y);
            }
            else
            {
                pos.x *= m.x;
                pos.y *= m.y;
            }

            return pos;
        }
    }
}
