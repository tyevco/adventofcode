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
        }

        protected override void Execute(string file)
        {
            Console.WriteLine("Please enter an attack power: ");
            var input = Console.ReadLine();
            int attackPower = 3;
            int upperRange = 3;
            if (!string.IsNullOrEmpty(input))
            {
                if (!int.TryParse(input, out attackPower))
                {
                    var parts = input.Split(" to ");
                    attackPower = int.Parse(parts[0]);
                    upperRange = int.Parse(parts[1]);
                }
                else
                {
                    upperRange = attackPower;
                }
            }

            for (int ap = attackPower; ap <= upperRange; ap++)
            {
                var map = new BattleMapParser().ParseData(file);

                foreach (var entity in map.Entities.Where(e => e.Type == EntityType.Elf))
                {
                    entity.Attack = ap;
                }

                var system = new Game(map);

                var startTicks = DateTime.Now.Ticks;

                while (!system.Finished)
                {
                    system.Tick();
                }

                var ticks = DateTime.Now.Ticks - startTicks;
                Console.WriteLine($"Simulation took {ticks} ticks, or { Math.Round(ticks / (double)TimeSpan.TicksPerSecond, 3)} seconds.");

                Console.WriteLine($"Elves had \x1b[38;5;82m{ap}\x1b[38;5;255m attack power.");
                Console.WriteLine($"Combat ends after {system.Round} full rounds");
                var type = system.Entities.Where(e => e.Health > 0).Select(t => t.Type).FirstOrDefault();
                var healthRemaining = system.Entities.Where(e => e.Health > 0).Sum(e => e.Health);
                Console.WriteLine($"{(type == EntityType.Elf ? "Elves" : "Goblins")} win with {healthRemaining} hit points left");
                Console.WriteLine($"{map.Entities.Count(e => e.Health > 0 && e.Type == EntityType.Elf)} of {map.Entities.Count(e => e.Type == EntityType.Elf)} elves lived.");
                Console.WriteLine($"Outcome: {system.Round} * {healthRemaining} =  {healthRemaining * system.Round}");
                Console.WriteLine();
            }
        }
    }
}
