using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D14
{
    [Exercise("Day 14: Space Stoichiometry")]
    class Y2019D14 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D14/Data");
        }

        private const string FUEL = nameof(FUEL);
        private const string ORE = nameof(ORE);

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);


            Dictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();
            Dictionary<string, long> resources = new Dictionary<string, long>();

            Timer.Monitor(() =>
            {
                foreach (var line in lines)
                {
                    var split = line.Split(" => ");
                    var yields = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var reaction = new Reaction()
                    {
                        Yields = long.Parse(yields[0])
                    };

                    reactions.Add(yields[1], reaction);

                    var ingredients = split[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var ingredient in ingredients)
                    {
                        var intakes = ingredient.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        reaction.Ingredients.Add(intakes[1], long.Parse(intakes[0]));
                    }
                }

                long oreCount = ProduceFuel(reactions, resources);
                Console.WriteLine($"{oreCount} ORE");

                Console.WriteLine($"Resources remaining: {string.Join(", ", resources.Select(x => $"{x.Value} {x.Key}"))}");

                // binary search. need to revise method for getting ore
                long target = 1000000000000;
                long lower = (target / oreCount) - 1000;
                long higher = (target / oreCount) + 1000000000;
                while (lower < higher)
                {
                    long mid = (lower + higher) / 2;
                    long guess = ProduceFuel(reactions, new Dictionary<string, long>(), mid);
                    if (guess > target)
                    {
                        higher = mid;
                    }
                    else if (guess < target)
                    {
                        if (mid == lower) break;
                        lower = mid;
                    }
                    else
                    {
                        lower = mid;
                        break;
                    }
                }

                Console.WriteLine($"Most would be {lower}");

            });
        }

        private static long ProduceFuel(Dictionary<string, Reaction> reactions, Dictionary<string, long> resources, long target = 1)
        {
            long oreCount = 0;

            for (long i = 0; i < target; i++)
            {
                oreCount += ProduceIngredient(FUEL, reactions, resources);
            }

            return oreCount;
        }

        private static long ProduceIngredient(string ingredient, Dictionary<string, Reaction> reactions, Dictionary<string, long> resources)
        {
            var reaction = reactions[ingredient];
            long oreCount = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append("Consuming ");
            bool first = true;
            foreach (KeyValuePair<string, long> reactIngredient in reaction.Ingredients)
            {
                if (!resources.ContainsKey(reactIngredient.Key))
                {
                    resources.Add(reactIngredient.Key, 0);
                }

                if (reactIngredient.Key == ORE)
                {
                    oreCount += reactIngredient.Value;
                    resources[ORE] += reactIngredient.Value;
                }
                else
                {
                    if (resources[reactIngredient.Key] < reactIngredient.Value)
                    {
                        while (resources[reactIngredient.Key] < reactIngredient.Value)
                        {
                            oreCount += ProduceIngredient(reactIngredient.Key, reactions, resources);
                        }
                    }
                }

                var resourceCount = resources[reactIngredient.Key];
                resources[reactIngredient.Key] = resourceCount - reactIngredient.Value;
                builder.Append($"{(!first ? ", " : string.Empty)}{reactIngredient.Value} {reactIngredient.Key} [{resourceCount}]");

                first = false;
            }

            builder.Append($" to produce {reaction.Yields} {ingredient}");

            if (resources.ContainsKey(ingredient))
            {
                resources[ingredient] += reaction.Yields;
            }
            else
            {
                resources.Add(ingredient, reaction.Yields);
            }

            Debug.WriteLine(builder.ToString());

            return oreCount;
        }
    }
}
