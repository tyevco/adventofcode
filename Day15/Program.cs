﻿using Advent.Utilities;
using System;
using System.Linq;

namespace Day15
{
    class Program : SelectableConsole
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (args.Length > 0)
                {
                    new Program().Start(args[0]);
                }

                Console.WriteLine("Finished.");
                var info = Console.ReadKey();

                if (info.Key == ConsoleKey.Q)
                    break;

                Console.Clear();
            }
        }

        protected override void Execute(string file)
        {
            var map = new BattleMapParser().ParseData(file);

            var system = new Game(map);

            //Console.WriteLine(map);

            while (!system.Finished)
            {
                Console.WriteLine($"Round #{system.Round + 1}");
                system.Tick();

                //Console.Clear();
                //Console.WriteLine(map);
                //System.Threading.Thread.Sleep(100);
            }

            //Console.Clear();
            //Console.WriteLine(map);
            Console.WriteLine($"Combat ends after {system.Round} full rounds");
            var type = system.Entities.Where(e => e.Health > 0).Select(t => t.Type).FirstOrDefault();
            var healthRemaining = system.Entities.Where(e => e.Health > 0).Sum(e => e.Health);
            Console.WriteLine($"{(type == EntityType.Elf ? "Elves" : "Goblins")} win with {healthRemaining} hit points left");
            Console.WriteLine($"Outcome: {system.Round} * {healthRemaining} =  {healthRemaining * system.Round}");
        }
    }
}
