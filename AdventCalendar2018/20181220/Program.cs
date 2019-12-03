using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    class Program : FileSelectionConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
        }

        private bool DisplayOutput { get; set; }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>()
            {
                CreateOption("Display Output", () => DisplayOutput = !DisplayOutput, () => DisplayOutput)
            };
        }

        protected override void Execute(string file)
        {
            (Building building, ExpectedResults expected) = new BuildingParser().ParseData(file);

            if (DisplayOutput)
            {
                Console.WriteLine();

                Console.WriteLine(building.Id);
                Console.WriteLine(building);
            }

            Console.WriteLine();


            if (expected != null)
            {
                var actualOutput = building.GetLayout();

                bool match = false;

                match = actualOutput.Equals(expected.Building);
                Console.WriteLine($"Match: {(match ? ConsoleCodes.Colorize("YES", 0x0a) : ConsoleCodes.Colorize("NO", 0x4c))}");

                if (!match)
                {
                    Console.WriteLine("Expected:");
                    Console.WriteLine(expected.Building);
                }
            }

            Console.WriteLine("Which problem to solve? [1] Room with Most Doors, [2] Rooms with at least 1000 doors.");
            var key = Console.ReadKey();
            Console.WriteLine();

            var start = DateTime.Now.Ticks;

            if (key.Key == ConsoleKey.D1)
            {
                // calculate the distance to the rooms with only 1 entrance
                var point = building.Rooms.Data.Where(r => r != null && r.DoorCount == 1).OrderByDescending(r => r.DoorsNavigated).FirstOrDefault();

                if (expected != null)
                {
                    var doorCountMatch = expected.Doors == point.DoorsNavigated;
                    if (doorCountMatch)
                    {
                        Console.WriteLine($"{ConsoleCodes.Colorize("Correctly", 0x0a)} matched the expected door count.");
                    }
                    else
                    {
                        Console.WriteLine($"{ConsoleCodes.Colorize("Incorrectly", 0x4c)} matched the expected door count.");
                    }
                }


                Console.WriteLine($"Navigated through {point.DoorsNavigated} doors to ({point.X},{point.Y})");
            }
            else if (key.Key == ConsoleKey.D2)
            {
                int doorAmount = 1000;

                var points = building.Rooms.Data.Where(r => r != null && r.DoorsNavigated >= doorAmount);

                if (points != null && points.Any())
                {
                    Console.WriteLine($"Found {points.Count()} rooms with at least {doorAmount} doors.");
                }
                else
                {
                    Console.WriteLine("No rooms had at least {doorAmount} doors.");
                }
            }

            var ticks = DateTime.Now.Ticks - start;

            Console.WriteLine($"Processing took {ticks} ticks, or {Math.Round((double)ticks / TimeSpan.TicksPerSecond, 3)} seconds.");
        }
    }
}

