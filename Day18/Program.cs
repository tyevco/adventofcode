using Advent.Utilities;
using System;
using System.Linq;

namespace Day18
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
            Grid grid = new ForestParser().ParseData(file);
            int i = 0;
            while (i < 200)
            {
                Console.WriteLine(grid);

                Console.WriteLine($"{grid.Locations.Count(x => x.Type == LocationType.Tree)} wooded locations.");
                Console.WriteLine($"{grid.Locations.Count(x => x.Type == LocationType.Lumberyard)} lumberyards.");
                Console.WriteLine($"Round {i} Score: {(grid.Locations.Count(x => x.Type == LocationType.Lumberyard) * grid.Locations.Count(x => x.Type == LocationType.Tree))}");

                Grid next = new Grid(grid.Width, grid.Height);
                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        Location l = grid[x, y];

                        switch (l.Type)
                        {
                            case LocationType.Open:
                                if (l.CountOfNeighborType(LocationType.Tree) >= 3)
                                {
                                    //tree
                                    next.AddLocation(x, y, LocationType.Tree);
                                }
                                else
                                {
                                    next.AddLocation(x, y, LocationType.Open);
                                }
                                break;
                            case LocationType.Tree:
                                if (l.CountOfNeighborType(LocationType.Lumberyard) >= 3)
                                {
                                    // lumberyard
                                    next.AddLocation(x, y, LocationType.Lumberyard);
                                }
                                else
                                {
                                    next.AddLocation(x, y, LocationType.Tree);
                                }
                                break;

                            case LocationType.Lumberyard:
                                if (l.CountOfNeighborType(LocationType.Tree) >= 1 && l.CountOfNeighborType(LocationType.Lumberyard) >= 1)
                                {
                                    // ly
                                    next.AddLocation(x, y, LocationType.Lumberyard);
                                }
                                else
                                {
                                    // open
                                    next.AddLocation(x, y, LocationType.Open);
                                }
                                break;
                        }
                    }
                }

                grid = next;
                i++;
            }

            Console.WriteLine(grid);

            Console.WriteLine($"{grid.Locations.Count(x => x.Type == LocationType.Tree)} wooded locations.");
            Console.WriteLine($"{grid.Locations.Count(x => x.Type == LocationType.Lumberyard)} lumberyards.");
            Console.WriteLine($"Round {i} Score: {(grid.Locations.Count(x => x.Type == LocationType.Lumberyard) * grid.Locations.Count(x => x.Type == LocationType.Tree))}");
        }
    }
}
