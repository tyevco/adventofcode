using Advent.Utilities;
using System;
using System.Collections.Generic;

namespace Day14
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
            int index = -1;
            int newRecipeCount = 0;
            bool seek = true;
            while (seek)
            {
                var newRecipe = (firstElf.RecipeValue + secondElf.RecipeValue).ToString().ToCharArray();
                foreach (var r in newRecipe)
                {
                    recipes.AddLast(r);

                    if (recipes.Count > 4)
                    {
                        var latestAddition = string.Join("", recipes.Last.Previous.Previous.Previous.Previous.Take(5));
                        if (recipeValue.Equals(latestAddition))
                        {
                            newRecipeCount = recipes.Count - 5;
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

            Console.WriteLine($"Answer: {index}");
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
