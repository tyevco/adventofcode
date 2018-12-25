using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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

            SearchOctTree(overall, bots);
        }

        private static void SearchOctTree(OctTree tree, IList<NanoBot> bots)
        {
            IList<OctTree> searchRegions = new List<OctTree>();


            //tree.UNE;
        }
    }
}
