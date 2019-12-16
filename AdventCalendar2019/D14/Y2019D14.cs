using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.IO;

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


            IDictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();

            Timer.Monitor(() =>
            {
                foreach (var line in lines)
                {
                    var split = line.Split(" => ");
                    var yields = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var reaction = new Reaction()
                    {
                        Yields = int.Parse(yields[0])
                    };

                    reactions.Add(yields[1], reaction);

                    var ingredients = split[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var ingredient in ingredients)
                    {
                        var intakes = ingredient.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        reaction.Ingredients.Add(intakes[1], int.Parse(intakes[0]));
                    }
                }

                Console.WriteLine(reactions.Count);

                var fuel = reactions[FUEL];

                int oreCount = 0;
                foreach (var ingredient in fuel.Ingredients)
                {
                    oreCount += GetOreForIngredient(reactions, ingredient.Key);
                }
            });
        }

        private static int GetOreForIngredient(IDictionary<string, Reaction> reactions, string ingredient)
        {
            if (ingredient == ORE)
            {
                return 1;
            }
            else
            {
                int count = 0;

                var reaction = reactions[ingredient];
                foreach (var reactIngredient in reaction.Ingredients)
                {
                    count += GetOreForIngredient(reactions, reactIngredient.Key) * reactIngredient.Value;
                }

                return count;
            }
            return 0;
        }
    }
}
