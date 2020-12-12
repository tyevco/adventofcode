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
                items.Add((line[0], int.Parse(line.Substring(1))));
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
                var item = data[i];

                switch (item.command)
                {
                    case 'L':
                        dir = turn(dir, -item.amount);
                        break;
                    case 'R':
                        dir = turn(dir, item.amount);
                        break;
                    case 'F':
                        pos = move(dir, item.amount, pos);
                        break;
                    default:
                        pos = move(item.command, item.amount, pos);
                        break;
                }
            }

            AnswerPartOne(pos.ManhattanDistance());
        }

        protected void PartTwo(IList<(char command, int amount)> data)
        {
            char dir = 'E';
            (int x, int y) pos = (0, 0);
            (int x, int y) wp = (10, -1);

            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];

                switch (item.command)
                {
                    case 'L':
                        (dir, wp) = turnWaypoint(dir, wp, -item.amount);
                        break;
                    case 'R':
                        (dir, wp) = turnWaypoint(dir, wp, item.amount);
                        break;
                    case 'F':
                        pos = moveToward(item.amount, pos, wp);
                        break;
                    default:
                        wp = move(item.command, item.amount, wp);
                        break;
                }
            }

            AnswerPartOne(pos.ManhattanDistance());
        }

        private (int x, int y) move(char dir, int amount, (int x, int y) pos)
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

        private (int x, int y) moveToward(int amount, (int x, int y) pos, (int x, int y) waypoint)
        {
            Console.WriteLine($"moving {amount * waypoint.x}, {amount * waypoint.y}");

            pos.x += (amount * waypoint.x);
            pos.y += (amount * waypoint.y);

            return pos;
        }

        List<char> directions = new List<char> { 'N', 'E', 'S', 'W' };

        private char turn(char facing, int degrees)
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

        private (char dir, (int x, int y) pos) turnWaypoint(char dir, (int x, int y) pos, int degrees)
        {
            dir = turn(dir, degrees);

            var turns = degrees / 90;

            (int x, int y) m = (1, 1);
            bool flipXY = false;

            if (turns > 0)
            {
                (int x, int y)[] mult = new (int x, int y)[] { (-1, 1), (-1, -1), (1, -1), (1, 1) };
                flipXY = turns % 2 == 1;
                m = mult[turns - 1];
            }
            else if (turns < 0)
            {
                (int x, int y)[] mult = new (int x, int y)[] { (1, -1), (-1, -1), (-1, 1), (1, 1) };
                flipXY = -turns % 2 == 1;
                m = mult[-turns - 1];
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

            return (dir, pos);
        }
    }
}
