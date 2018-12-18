using Advent.Utilities;
using System;
using System.Linq;

namespace Day15
{
    class Program : SelectableConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        protected override void Execute(string file)
        {
            var map = new BattleMapParser().ParseData(file);

            var system = new Game(map);

            Console.WriteLine(map);

            while (!system.Finished)
            {
                system.Tick();

                //Console.Clear();
                Console.WriteLine($"Round #{system.Round}");
                Console.WriteLine(map);
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
