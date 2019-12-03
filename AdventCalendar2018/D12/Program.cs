using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D12
{
    [Exercise("Day 12:  ")]
    class Program
    {
        const int HEADER_DISPLAY_VALUES = 5;

        public void Execute()
        {
            var data = sample;
            int generationCount = 20;
            var corridor = parseData(data.InitialState);

            IList<Generation> generations = new List<Generation>()
            {
                new Generation()
                {
                    Pots = corridor.Pots
                }
            };

            for (int i = 0; i < generationCount; i++)
            {
                var generation = corridor.SpawnGeneration(data.Rules);
                generations.Add(generation);
            }


            int left = generations.Min(g => g.FirstSpot);
            int right = generations.Max(g => g.LastSpot);
            PrintHeader(generationCount, left, right);
            PrintGenerations(generations, left, right);

            Console.ReadLine();
        }

        static Corridor parseData(string data)
        {
            var corridor = new Corridor();
            foreach (var p in data)
            {
                var pot = new Pot
                {
                    HasPlant = p.Equals('#'),
                    Id = corridor.Pots.Count
                };

                corridor.Pots.AddLast(pot);
            }

            return corridor;
        }

        static void PrintGenerations(IList<Generation> generations, int left, int right)
        {
            int plantCount = 0;
            int potIdTotal = 0;

            foreach (var generation in generations)
            {
                if (generation != null)
                {
                    Console.Write($"{(generation.Id < 10 ? (" " + generation.Id.ToString()) : generation.Id.ToString())}: ");

                    var leftSide = generation.FirstSpot - left;
                    for (int i = 0; i < leftSide; i++)
                    {
                        Console.Write(".");
                    }

                    Console.Write(generation.ToString());

                    var rightSide = (right - generation.LastSpot);
                    for (int i = 0; i < rightSide; i++)
                    {
                        Console.Write(".");
                    }

                    int currPlantCount = generation.Pots.Count(x => x.HasPlant);
                    int currPotIdTotal = generation.Pots.Where(x => x.HasPlant).Sum(x => x.Id);

                    Console.Write($" | {generation.Pots.FirstOrDefault(x => x.HasPlant)?.Id} - {generation.Pots.LastOrDefault(x => x.HasPlant)?.Id }");
                    Console.Write($" | {currPlantCount} { currPotIdTotal}");

                    plantCount += currPlantCount;
                    potIdTotal += currPotIdTotal;

                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Total Plants: {plantCount}");
            Console.WriteLine($"Pot Id Total: {potIdTotal}");
        }

        static void PrintHeader(int numberOfGenerations, int left, int right)
        {
            var len = Math.Max(Math.Abs(left), Math.Abs(right)).ToString().Length;

            for (int pos = len - 1; pos >= 0; pos--)
            {
                for (int i = 0; i < numberOfGenerations.ToString().Length; i++)
                {
                    Console.Write(" ");
                }

                Console.Write(" ");

                for (int i = left; i <= right; i++)
                {
                    if (i % HEADER_DISPLAY_VALUES == 0)
                    {
                        var d = (int)(i / Math.Pow(10, pos)) % 10;
                        if (d >= 0)
                        {

                            Console.Write($" {d}");
                        }
                        else
                        {
                            Console.Write($"{d}");
                        }
                    }
                    else if (Math.Abs(i % HEADER_DISPLAY_VALUES) >= HEADER_DISPLAY_VALUES - 1)
                    {

                    }
                    else
                    {
                        Console.Write(" ");
                    }


                }
                Console.WriteLine("");
            }
        }



        static readonly Data actual = new Data
        {
            InitialState = "##..##....#.#.####........##.#.#####.##..#.#..#.#...##.#####.###.##...#....##....#..###.#...#.#.#.#",
            Rules = new List<SpawnRule>()
            {
                new SpawnRule("##.#. => ."),
                new SpawnRule("##.## => ."),
                new SpawnRule("#..## => ."),
                new SpawnRule("#.#.# => ."),
                new SpawnRule("..#.. => #"),
                new SpawnRule("#.##. => ."),
                new SpawnRule("##... => #"),
                new SpawnRule(".#..# => ."),
                new SpawnRule("#.### => ."),
                new SpawnRule("..... => ."),
                new SpawnRule("...#. => #"),
                new SpawnRule("#..#. => #"),
                new SpawnRule("###.. => #"),
                new SpawnRule(".#... => #"),
                new SpawnRule("###.# => #"),
                new SpawnRule("####. => ."),
                new SpawnRule(".##.# => #"),
                new SpawnRule("#.#.. => #"),
                new SpawnRule(".###. => #"),
                new SpawnRule(".#.## => ."),
                new SpawnRule("##### => #"),
                new SpawnRule("....# => ."),
                new SpawnRule(".#### => ."),
                new SpawnRule(".##.. => #"),
                new SpawnRule("##..# => ."),
                new SpawnRule("#...# => ."),
                new SpawnRule("..### => #"),
                new SpawnRule("...## => ."),
                new SpawnRule("#.... => ."),
                new SpawnRule("..##. => ."),
                new SpawnRule(".#.#. => #"),
                new SpawnRule("..#.# => #")
            }
        };

        static readonly Data sample = new Data
        {
            InitialState = "#..#.#..##......###...###",
            Rules = new List<SpawnRule>()
            {
                new SpawnRule("...## => #"),
                new SpawnRule("..#.. => #"),
                new SpawnRule(".#... => #"),
                new SpawnRule(".#.#. => #"),
                new SpawnRule(".#.## => #"),
                new SpawnRule(".##.. => #"),
                new SpawnRule(".#### => #"),
                new SpawnRule("#.#.# => #"),
                new SpawnRule("#.### => #"),
                new SpawnRule("##.#. => #"),
                new SpawnRule("##.## => #"),
                new SpawnRule("###.. => #"),
                new SpawnRule("###.# => #"),
                new SpawnRule("####. => #")
            }
        };
    }
}
