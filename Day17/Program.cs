using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    class Program : SelectableConsole
    {
        public bool EnableDebug { get; private set; }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>
            {
                new ConsoleOption
                {
                    Text = "Debug Output",
                    Handler = () => EnableDebug = !EnableDebug,
                    Enabled = () => EnableDebug
                }
            };
        }

        protected override void HandleOptions(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.D)
            {
                EnableDebug = !EnableDebug;
            }
        }

        protected override void Execute(string file)
        {
            IList<Material> active = null;

            var grid = new SpringParser().ParseData(file);

            bool finished = false;
            int i = 0;
            while (!finished)
            {
                grid.Propogate();
                i++;

                Console.WriteLine(grid);

                if (i % 50 == 0)
                {
                    active = grid.Active();
                    Console.WriteLine($"[{i.ToString("0000")}] Clay tiles: {active.Count(x => x != null && x.Type == MaterialType.Clay)}, Water tiles: {active.Count(x => x != null && x.Type == MaterialType.Water)}");
                }

                finished = !grid.Added;
                //System.Threading.Thread.Sleep(500);
            }

            active = grid.Active();
            Console.WriteLine($"[{i.ToString("0000")}] Clay tiles: {active.Count(x => x != null && x.Type == MaterialType.Clay)}, Water tiles: {active.Count(x => x != null && x.Type == MaterialType.Water)}");
            Console.WriteLine($"Flowing Water tiles: {active.Count(x => x != null && x.Type == MaterialType.Water && ((Water)x).IsFlowing)}");
            Console.WriteLine($"Top: {grid.Top} to {grid.Bottom}");
        }
    }
}
