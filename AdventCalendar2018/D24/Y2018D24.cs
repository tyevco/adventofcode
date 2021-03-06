using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D24
{
    [Exercise("Day 24: Immune System Simulator 20XX")]
    class Program : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D24/Data");
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>()
            {
                CreateOption("Debug Output", () => Battle.DebugOutput = !Battle.DebugOutput, () => Battle.DebugOutput)
            };
        }

        protected override void Execute(string file)
        {
            Console.WriteLine("Please choose a boost power: ");
            var input = Console.ReadLine();
            int attackPower = 0;
            int upperRange = 0;
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
                Console.WriteLine($"Immune Boost: {ap}");

                var squads = new SquadParser().ParseData(file);
                foreach (var squad in squads.Where(s => s.Team == Team.ImmuneSystem))
                    squad.AttackPower += ap;

                var winner = Battle.PerformBattle(squads);

                if (winner == Team.ImmuneSystem)
                    break;
            }
        }


    }
}
