using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2021.D02
{
    [Exercise("Day 2: Dive!")]
    class Y2021D02 : FileSelectionParsingConsole<IList<(Directions direction, int amount)>>, IExercise
    {
        public void Execute()
        {
            Start("D02/Data");
        }

        protected override IList<(Directions direction, int amount)> DeserializeData(IList<string> data)
        {
            return data.Select(x => Split(x, " ")).ToList();
        }

        private (Directions direction, int amount) Split(string value, string delim)
        {
            var values = value.Split(delim);

            return (direction: Enum.Parse<Directions>(values[0], true), amount: int.Parse(values[1]));
        }

        protected override void Execute(IList<(Directions direction, int amount)> data)
        {
            Timer.Monitor(() =>
            {
                int y = 0;
                int x = 0;

                foreach (var move in data)
                {
                    switch (move.direction)
                    {
                        case Directions.Forward:
                            x += move.amount;
                            break;
                        case Directions.Up:
                            y -= move.amount;
                            break;

                        case Directions.Down:
                            y += move.amount;
                            break;
                    }
                }

                Console.WriteLine($"Part 1: {x},{y}: {x * y}");
            });


            Timer.Monitor(() =>
            {
                int y = 0;
                int x = 0;
                int aim = 0;

                foreach (var move in data)
                {
                    switch (move.direction)
                    {
                        case Directions.Forward:
                            x += move.amount;
                            y += aim * move.amount;
                            break;

                        case Directions.Up:
                            aim -= move.amount;
                            break;

                        case Directions.Down:
                            aim += move.amount;
                            break;
                    }
                }

                Console.WriteLine($"Part 2: {x},{y}: {x * y}");
            });

        }
    }

    public enum Directions
    {
        Forward,
        Up,
        Down,
    }
}
