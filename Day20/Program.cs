﻿using Advent.Utilities;
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
            var building = new BuildingParser().ParseData(file);

            Console.WriteLine();

            Console.WriteLine(building.Id);
            Console.WriteLine(building);

            Console.WriteLine();

            var actualOutput = building.GetLayout();

            bool match = false;

            if (building.Expected != null && actualOutput.Length == building.Expected.Length)
            {
                match = actualOutput.Equals(building.Expected);
            }

            Console.WriteLine($"Match: {(match ? ConsoleCodes.Colorize("YES", 0x0a) : ConsoleCodes.Colorize("NO", 0x4c))}");

            if (!match)
            {
                Console.WriteLine("Expected:");
                Console.WriteLine(building.Expected);
            }
            else
            {
                // calculate the distance to the rooms with only 1 entrance
                var point = PathFinding.FindTargetPoint(building, building.FirstRoom);
                Console.WriteLine(point);
            }
        }
    }
}
