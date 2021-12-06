using System;
using System.Collections.Generic;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D14
{
    [Exercise("Day 14: Chocolate Charts")]
    class Program : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D14/Data");
        }

        protected void ExecuteOld(string file)
        {
            LinkedList<char> recipes = new LinkedList<char>();

            var recipeLength = int.Parse(System.IO.File.ReadAllText(file));

            var firstElf = new Elf
            {
                Node = recipes.AddLast('3'),
            };

            var secondElf = new Elf
            {
                Node = recipes.AddLast('7')
            };

            // PrintRecipeList(recipes, firstElf, secondElf);

            int newRecipeCount = 0;

            while (recipes.Count < recipeLength + 10)
            {
                var newRecipe = (firstElf.RecipeValue + secondElf.RecipeValue).ToString().ToCharArray();
                foreach (var r in newRecipe)
                    recipes.AddLast(r);

                newRecipeCount += newRecipe.Length;

                LinkedListNode<char> nextFirstNode = firstElf.Node;
                for (int f = 0; f < firstElf.NextRecipe; f++)
                {
                    nextFirstNode = nextFirstNode.NextOrFirst();
                }
                firstElf.Node = nextFirstNode;

                LinkedListNode<char> nextSecondNode = secondElf.Node;
                for (int f = 0; f < secondElf.NextRecipe; f++)
                {
                    nextSecondNode = nextSecondNode.NextOrFirst();
                }
                secondElf.Node = nextSecondNode;

                // PrintRecipeList(recipes, firstElf, secondElf);
            }

            PrintRecipeList(recipes, firstElf, secondElf);


            var end = recipes.Skip(recipeLength);
            IList<char> answer = end.Take(10);
            Console.WriteLine($"Answer: {string.Join("", answer)}");
        }

        protected override void Execute(string file)
        {
            LinkedList<char> recipes = new LinkedList<char>();

            var recipeValue = System.IO.File.ReadAllText(file);

            var firstElf = new Elf
            {
                Node = recipes.AddLast('3'),
            };

            var secondElf = new Elf
            {
                Node = recipes.AddLast('7')
            };

            // PrintRecipeList(recipes, firstElf, secondElf);
            int newRecipeCount = 0;
            bool seek = true;
            while (seek)
            {
                var newRecipe = (firstElf.RecipeValue + secondElf.RecipeValue).ToString().ToCharArray();
                foreach (var r in newRecipe)
                {
                    recipes.AddLast(r);

                    if (recipes.Count > recipeValue.Length)
                    {
                        var latestAddition = string.Join("", recipes.Last.Rewind(recipeValue.Length - 1).Take(recipeValue.Length));
                        if (recipeValue.Equals(latestAddition))
                        {
                            newRecipeCount = recipes.Count - recipeValue.Length;
                            seek = false;
                            break;
                        }
                    }
                }
                if (seek)
                {

                    LinkedListNode<char> nextFirstNode = firstElf.Node;
                    for (int f = 0; f < firstElf.NextRecipe; f++)
                    {
                        nextFirstNode = nextFirstNode.NextOrFirst();
                    }
                    firstElf.Node = nextFirstNode;

                    LinkedListNode<char> nextSecondNode = secondElf.Node;
                    for (int f = 0; f < secondElf.NextRecipe; f++)
                    {
                        nextSecondNode = nextSecondNode.NextOrFirst();
                    }
                    secondElf.Node = nextSecondNode;
                }
            }

            Console.WriteLine($"{recipeValue} first appears after {newRecipeCount} recipes.");
        }

        private void PrintRecipeList(LinkedList<char> recipes, Elf firstElf, Elf secondElf)
        {
            var item = recipes.First;
            while (item != null)
            {
                if (firstElf.Node.Equals(item))
                {
                    Console.Write($"({item.Value})");
                }
                else if (secondElf.Node.Equals(item))
                {
                    Console.Write($"[{item.Value}]");
                }
                else
                {
                    Console.Write($" {item.Value} ");
                }

                item = item.Next;
            }

            Console.WriteLine();
        }
    }
}
