using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D23
{
    [Exercise("Day 23: Experimental Emergency Teleportation")]
    class Program : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D23/Data");
        }

        protected override void Execute(string file)
        {
            var bots = new NanoBotGroupParser().ParseData(file);

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

            OctTree overall = new OctTree(minX, minY, minZ, maxX, maxY, maxZ);

            Console.WriteLine($"There are {count} bots in range.");

            var best = SearchOctTree(overall, bots);

            Console.WriteLine($"Mid: {best.MidX},{best.MidY},{best.MidZ}");
            Console.WriteLine($"Min: {best.MinimumX},{best.MinimumY},{best.MinimumZ}");
            Console.WriteLine($"Max: {best.MaximumX},{best.MaximumY},{best.MaximumZ}");
            Console.WriteLine($"Hit: {best.Hits}");
            Console.WriteLine($"Vol: {best.Volume}");
        }

        private static OctTree SearchOctTree(OctTree tree, IList<NanoBot> bots)
        {
            OctTree bestCandidate = null;
            var searchRegions = tree.Regions;
            foreach (var bot in bots)
            {
                foreach (var region in searchRegions)
                {
                    if (region != null)
                    {
                        if (bot.Within(region))
                        {
                            region.Hits++;
                        }
                    }
                }
            }

            var maxHits = searchRegions.Max(r => r?.Hits ?? 0);

            if (maxHits > 0)
            {
                var maxRegions = searchRegions.Where(r => r?.Hits == maxHits);

                IList<OctTree> childMatches = new List<OctTree>();
                // drill down into sub-regions
                foreach (var region in maxRegions)
                {
                    if (region.Regions.Any(x => x != null))
                    {
                        var childTree = SearchOctTree(region, bots);

                        if (childTree != null)
                        {
                            childMatches.Add(childTree);
                        }
                    }
                    else
                    {
                        childMatches.Add(region);
                    }
                }

                if (childMatches.Any())
                {
                    var maxChildHits = childMatches.Max(s => s.Hits);

                    bestCandidate = childMatches.Where(s => s.Hits == maxChildHits).OrderBy(s => GetDistanceFromOrigin(s)).FirstOrDefault();
                }
                else
                {
                    bestCandidate = tree;
                }
            }

            return bestCandidate;
        }

        private static double GetDistanceFromOrigin(OctTree tree)
        {
            return Math.Abs(tree.MidX) + Math.Abs(tree.MidY) + Math.Abs(tree.MidZ);
        }
    }
}
