using Advent.Utilities;
using System;

namespace Day20
{
    class Program : SelectableConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
        }

        protected override void Execute(string file)
        {
            (Building building, ExpectedResults expected) = new BuildingParser().ParseData(file);

            Console.WriteLine();

            Console.WriteLine(building.Id);
            Console.WriteLine(building);

            Console.WriteLine();

            var actualOutput = building.GetLayout();

            bool match = false;

            if (expected != null)
            {
                match = actualOutput.Equals(expected.Building);
                Console.WriteLine($"Match: {(match ? ConsoleCodes.Colorize("YES", 0x0a) : ConsoleCodes.Colorize("NO", 0x4c))}");
            }

            if (expected != null && !match)
            {
                Console.WriteLine("Expected:");
                Console.WriteLine(expected.Building);
            }
            else
            {
                // calculate the distance to the rooms with only 1 entrance
                var point = PathFinding.FindTargetPoint(building, building.FirstRoom);

                if (expected != null)
                {
                    var doorCountMatch = expected.Doors == point.Distance;
                    if (doorCountMatch)
                    {
                        Console.WriteLine($"{ConsoleCodes.Colorize("Correctly", 0x0a)} matched the expected door count.");
                    }
                    else
                    {
                        Console.WriteLine($"{ConsoleCodes.Colorize("Incorrectly", 0x4c)} matched the expected door count.");
                    }
                }

                Console.WriteLine($"Navigated through {point.Distance} doors to ({point.X},{point.Y})");
            }
        }
    }
}
