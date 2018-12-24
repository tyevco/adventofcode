using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Z3;

namespace Day23
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
            var bots = new NanoBotGroupParser().ParseData(file);

            var botStrengthQueue = new Queue<NanoBot>(bots.OrderByDescending(b => b.R));
            var strongestSignalBot = botStrengthQueue.FirstOrDefault();
            int count = 0;
            int avgX = strongestSignalBot.X;
            int avgY = strongestSignalBot.Y;
            int avgZ = strongestSignalBot.Z;

            while (botStrengthQueue.Any())
            {
                var bot = botStrengthQueue.Dequeue();

                var distance = bot.DistanceTo(strongestSignalBot.X, strongestSignalBot.Y, strongestSignalBot.Z);

                if (distance <= strongestSignalBot.R)
                {
                    avgX += bot.X / 2;
                    avgY += bot.Y / 2;
                    avgZ += bot.Z / 2;
                    count++;
                }
            }
            
            Z3

            Console.WriteLine($"There are {count} bots in range.");
            
            //IList<(int, int, int)> seekPoints = new List<(int, int, int)>();
            //// mutate to all points within manhatten distance of a bot
            //foreach (var point in startingPoints)
            //{
            //    Console.WriteLine($"Mutating points to test: {point.Item1},{point.Item2},{point.Item3}");
            //    foreach (var bot in bots)
            //    {
            //        var ps = new (int, int, int)[] {
            //            (point.Item1 + bot.R, point.Item2, point.Item3),
            //            (point.Item1 - bot.R, point.Item2, point.Item3),
            //            (point.Item1, point.Item2 + bot.R, point.Item3),
            //            (point.Item1, point.Item2 - bot.R, point.Item3),
            //            (point.Item1, point.Item2, point.Item3 + bot.R),
            //            (point.Item1, point.Item2, point.Item3 - bot.R)
            //        };

            //        foreach (var p in ps)
            //        {
            //            if (!seekPoints.Any(s => s.Item1 == p.Item1 && s.Item2 == p.Item2 && s.Item3 == p.Item3))
            //            {
            //                seekPoints.Add(p);
            //            }
            //        }
            //    }
            //}

            //IList<(int, int, int, int)> finalCounts = new List<(int, int, int, int)>();

            //foreach (var point in seekPoints)
            //{
            //    int botsInRange = 0;

            //    foreach (var bot in bots)
            //    {
            //        if (bot.DistanceTo(point.Item1, point.Item2, point.Item3) <= bot.R)
            //        {
            //            botsInRange++;
            //        }
            //    }

            //    finalCounts.Add((point.Item1, point.Item2, point.Item3, botsInRange));
            //}

            //var ordered = finalCounts.OrderByDescending(p => p.Item4).ThenBy(p => Math.Abs(p.Item1) + Math.Abs(p.Item2) + Math.Abs(p.Item3));

            //foreach (var point in ordered)
            //{
            //    Console.WriteLine($"Point tested: {point.Item1},{point.Item2},{point.Item3} has {point.Item4} bots in range, with distance from origin: {Math.Abs(point.Item1) + Math.Abs(point.Item2) + Math.Abs(point.Item3)}.");
            //}
        }
    }
}
