using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2018.D24
{
    public static class Battle
    {


        public static Team PerformBattle(IList<Squad> squads)
        {
            bool running = true;

            IEnumerable<Squad> immuneSquad = squads.Where(t => t.Team == Team.ImmuneSystem && t.Units > 0).OrderBy(t => t.Id);
            IEnumerable<Squad> infectionSquad = squads.Where(t => t.Team == Team.Infection && t.Units > 0).OrderBy(t => t.Id);
            int dmgDone = 0;

            while (running)
            {
                running = infectionSquad.Any() && immuneSquad.Any();

                if (DebugOutput || !running)
                {
                    Console.WriteLine("Immune System:");
                    if (immuneSquad.Any())
                        foreach (var squad in immuneSquad)
                            Console.WriteLine($"Group {squad.Id} contains {squad.Units} units");
                    else
                        Console.WriteLine("No groups remain.");

                    Console.WriteLine("Infection:");
                    if (infectionSquad.Any())
                        foreach (var squad in infectionSquad)
                            Console.WriteLine($"Group {squad.Id} contains {squad.Units} units");
                    else
                        Console.WriteLine("No groups remain.");

                    Console.WriteLine("");
                }

                if (running)
                {
                    dmgDone = PerformRound(squads);

                    if (dmgDone <= 0)
                    {
                        running = false;
                    }
                    else
                    {
                        immuneSquad = squads.Where(t => t.Team == Team.ImmuneSystem && t.Units > 0).OrderBy(t => t.Id);
                        infectionSquad = squads.Where(t => t.Team == Team.Infection && t.Units > 0).OrderBy(t => t.Id);
                    }
                }
            }

            var winner = dmgDone > 0 ? (immuneSquad.Any() ? Team.ImmuneSystem : Team.Infection) : Team.Tie;

            if (winner != Team.Tie)
            {
                Console.WriteLine($"{winner} wins with {squads.Where(s => s.Units > 0).Sum(s => s.Units)} total units");
            }
            else
            {
                Console.WriteLine("There was a tie...");
            }

            Console.WriteLine("");

            return winner;
        }

        public static bool DebugOutput { get; set; } = false;

        private static int PerformRound(IList<Squad> squads)
        {
            var targets = FindTargets(squads.Where(t => t.Units > 0).OrderBy(t => t.Team).ThenByDescending(t => t.AttackPower * t.Units).ThenByDescending(t => t.Initiative));

            int dmgDone = ExecuteAttacks(targets.OrderByDescending(t => t.Item1.Initiative));

            if (DebugOutput)
                Console.WriteLine("");

            return dmgDone;
        }


        private static IList<(Squad, Squad)> FindTargets(IEnumerable<Squad> squads)
        {

            IList<(Squad, Squad)> targets = new List<(Squad, Squad)>();

            foreach (var squad in squads)
            {
                var enemies = squads.Where(t => t.Team != squad.Team).OrderBy(t => t.Id);

                int maxDamage = -1;
                Squad target = null;

                foreach (var enemy in enemies)
                {
                    if (!targets.Any(t => t.Item2.Id == enemy.Id && t.Item2.Team == enemy.Team))
                    {
                        int multiplier = 1;
                        if (enemy.Weaknesses.Contains(squad.AttackType))
                        {
                            multiplier = 2;
                        }
                        else if (enemy.Immunities.Contains(squad.AttackType))
                        {
                            multiplier = 0;
                        }

                        var potentialDamage = multiplier * squad.EffectivePower;

                        if (DebugOutput)
                        {
                            Console.WriteLine($"{(squad.Team == Team.ImmuneSystem ? "Immune System" : "Infection")} group {squad.Id} would deal defending group {enemy.Id} {potentialDamage} damage");
                        }

                        if (potentialDamage > 0)
                        {
                            if (potentialDamage > maxDamage)
                            {
                                maxDamage = potentialDamage;
                                target = enemy;
                            }
                            else if (potentialDamage == maxDamage)
                            {
                                if (target.EffectivePower < enemy.EffectivePower)
                                    target = enemy;
                                else if (target.EffectivePower == enemy.EffectivePower)
                                    if (target.Initiative < enemy.Initiative)
                                        target = enemy;
                            }
                        }
                    }
                }

                if (target != null)
                {
                    targets.Add((squad, target));
                }
            }

            if (DebugOutput)
                Console.WriteLine("");

            return targets;
        }

        public static int ExecuteAttacks(IEnumerable<(Squad, Squad)> targets)
        {
            int dmgDone = 0;

            foreach (var targetInfo in targets)
            {
                var squad = targetInfo.Item1;
                var enemy = targetInfo.Item2;

                int multiplier = 1;
                if (enemy.Weaknesses.Contains(squad.AttackType))
                {
                    multiplier = 2;
                }
                else if (enemy.Immunities.Contains(squad.AttackType))
                {
                    multiplier = 0;
                }

                int damage = squad.AttackPower * squad.Units * multiplier;

                if (squad.Units > 0)
                {
                    int unitsKilled = Math.Min(damage / enemy.Health, enemy.Units);
                    enemy.Units -= unitsKilled;

                    dmgDone += unitsKilled * enemy.Health;

                    if (DebugOutput)
                    {
                        Console.WriteLine($"{(squad.Team == Team.ImmuneSystem ? "Immune System" : "Infection")} group {squad.Id} attacks defending group {enemy.Id}, killing {unitsKilled} units");
                    }
                }
            }

            if (DebugOutput)
                Console.WriteLine("");

            return dmgDone;
        }
    }
}
