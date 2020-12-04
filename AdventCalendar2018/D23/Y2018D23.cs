using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar2018.D23
{
    [Exercise("Day 23: Experimental Emergency Teleportation")]
    class Program : FileSelectionParsingConsole<IList<NanoBot>>, IExercise
    {
        public void Execute()
        {
            Start("D23/Data");
        }

        protected override IList<NanoBot> DeserializeData(IList<string> data)
        {
            IList<NanoBot> bots = new List<NanoBot>();
            Regex botRegex = new Regex(@"pos=<(-?[0-9]+),(-?[0-9]+),(-?[0-9]+)>, r=([0-9]+)");

            foreach (var line in data)
            {
                var match = botRegex.Match(line);

                NanoBot bot = new NanoBot(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value),
                    int.Parse(match.Groups[4].Value)
                    );

                bots.Add(bot);
            }

            return bots;
        }

        protected override void Execute(IList<NanoBot> bots)
        {
            // part 1
            {
                var botStrengthQueue = new Queue<NanoBot>(bots.OrderByDescending(b => b.R));
                var strongestSignalBot = botStrengthQueue.FirstOrDefault();
                int count = 0;

                int minX = strongestSignalBot.X;
                int maxX = strongestSignalBot.X;
                int minY = strongestSignalBot.Y;
                int maxY = strongestSignalBot.Y;
                int minZ = strongestSignalBot.Z;
                int maxZ = strongestSignalBot.Z;
                while (botStrengthQueue.Any())
                {
                    var bot = botStrengthQueue.Dequeue();

                    var distance = bot.DistanceTo(strongestSignalBot.X, strongestSignalBot.Y, strongestSignalBot.Z);

                    if (minX > bot.X)
                        minX = bot.X;

                    if (maxX < bot.X)
                        maxX = bot.X;

                    if (minY > bot.Y)
                        minY = bot.Y;

                    if (maxY < bot.Y)
                        maxY = bot.Y;

                    if (minZ > bot.Z)
                        minZ = bot.Z;

                    if (maxZ < bot.Z)
                        maxZ = bot.Z;

                    if (distance <= strongestSignalBot.R)
                    {
                        count++;
                    }
                }
                AnswerPartOne($"There are {count} bots in range.");
            }

            // part 2
            {
                int maxInRange = 0;
                int inRange = 0;
                int bestSum = int.MaxValue;
                (int x, int y, int z) bestLocation = (0, 0, 0);
                int distance = 0;
                (int minX, int minY, int minZ, int maxX, int maxY, int maxZ) bounds = (bots.Min(b => b.X), bots.Min(b => b.Y), bots.Min(b => b.Y), bots.Max(b => b.X), bots.Max(b => b.Y), bots.Max(b => b.Z));
                (int X, int Y, int Z) ranges = (bounds.maxX - bounds.minX, bounds.maxY - bounds.minY, bounds.maxZ - bounds.minZ);
                int d = (int)Math.Pow(2, 26);

                while (d >= 1)
                {
                    for (int x = bounds.minX; x < bounds.maxX; x += d)
                        for (int y = bounds.minY; y < bounds.maxY; y += d)
                            for (int z = bounds.minZ; z < bounds.maxZ; z += d)
                                if ((inRange = bots.Count(bot => bot.InRange(x, y, z))) > maxInRange || inRange == maxInRange && Math.Abs(x) + Math.Abs(y) + Math.Abs(z) < bestSum)
                                {
                                    maxInRange = inRange;
                                    bestLocation = (x, y, z);
                                    bestSum = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
                                }

                    Console.WriteLine("Grain {0}: Location: {1}, {2}, {3} InRange: {4} Sum: {5}", d, bestLocation.x, bestLocation.y, bestLocation.z, maxInRange, bestSum);

                    //for (int x = bounds.minX; x < bounds.maxX; x += d)
                    //{
                    //    for (int y = bounds.minY; y < bounds.maxY; y += d)
                    //    {
                    //        for (int z = bounds.minZ; z < bounds.maxZ; z += d)
                    //        {
                    //            Debug.WriteLine("Location: {0}, {1}, {2}", x, y, z);
                    //            inRange = bots.Count(b => b.InRange(x, y, z));
                    //            distance = (x, y, z).ManhattanDistance();

                    //            Debug.WriteLine("::: InRange: {0} Sum: {1}", inRange, distance);

                    //            if (inRange > maxInRange || (inRange == maxInRange && distance < bestSum))
                    //            {
                    //                maxInRange = inRange;
                    //                bestSum = distance;
                    //                bestLocation = (x, y, z);
                    //            }
                    //        }
                    //    }
                    //}

                    Debug.WriteLine("Grain {0}: Location: {1}, {2}, {3} InRange: {4} Sum: {5}", d, bestLocation.x, bestLocation.y, bestLocation.z, maxInRange, bestSum);

                    d /= 2; ranges.X /= 2; ranges.Y /= 2; ranges.Z /= 2;
                    bounds = (bestLocation.x - ranges.X / 2, bestLocation.y - ranges.Y / 2, bestLocation.z - ranges.Z / 2, bestLocation.x + ranges.X / 2, bestLocation.y + ranges.Y / 2, bestLocation.z + ranges.Z / 2);


                }

                AnswerPartTwo(bestSum);
            }
        }
    }
}
