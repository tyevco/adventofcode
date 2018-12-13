﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Day13
{
    class CartMayhem
    {
        const bool CLEAR_OUTPUT = true;
        const bool DISPLAY_GAME = true;

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    var data = System.IO.File.ReadAllLines(args[0]);

                    (Grid grid, IList<Cart> carts) = ParseData(data);

                    if (grid != null)
                    {
                        Game game = new Game(grid, carts);
                        // do game stuff

                        bool finished = false;

                        if (DISPLAY_GAME)
                            PrintGameState(game);

                        while (!finished)
                        {
                            game.Tick();

                            if (DISPLAY_GAME)
                            {
                                //Thread.Sleep(250);
                                PrintGameState(game);
                            }

                            finished = game.Carts.Any(c => carts.Count(o => c.X == o.X && c.Y == o.Y) > 1);
                        }

                        var crashedCart = game.Carts.FirstOrDefault(c => carts.Count(o => c.X == o.X && c.Y == o.Y) > 1);

                        Console.WriteLine($"After {game.Ticks} ticks a crash was detected at: {crashedCart.X},{crashedCart.Y}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Console.ReadLine();
            }
        }


        static void PrintGameState(Game game)
        {
            if (CLEAR_OUTPUT)
            {
                Console.Clear();
            }


            for (int y = 0; y < game.Grid.Height; y++)
            {
                for (int x = 0; x < game.Grid.Width; x++)
                {
                    var carts = game.Carts.Where(c => c.X == x && c.Y == y).ToList();
                    if (carts.Any())
                    {
                        if (carts.Count > 1)
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write(carts[0]);
                        }
                    }
                    else
                    {
                        if (game.Grid[x, y] != null)
                        {
                            Console.Write(game.Grid[x, y]);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }

                Console.WriteLine();
            }

            if (!CLEAR_OUTPUT)
            {
                Console.WriteLine();
            }
        }

        static (Grid, IList<Cart>) ParseData(IList<string> rows)
        {
            Grid grid = null;
            IList<Cart> carts = null;

            if (rows.Count > 0 && rows[0] != null)
            {
                grid = new Grid(rows[0].Length, rows.Count);
                carts = new List<Cart>();

                for (int y = 0; y < rows.Count; y++)
                {
                    var row = rows[y];
                    for (int x = 0; x < row.Length; x++)
                    {
                        switch (row[x])
                        {
                            case 'V':
                            case 'v':
                            case '>':
                            case '<':
                            case '^':
                                carts.Add(createCart(x, y, row[x]));
                                grid[x, y] = createTrack(x, y, row[x]);
                                break;

                            case '|':
                            case '-':
                            case '/':
                            case '\\':
                            case '+':
                                grid[x, y] = createTrack(x, y, row[x]);
                                break;
                        }
                    }
                }
            }

            return (grid, carts);
        }

        private static Cart createCart(int x, int y, char c)
        {
            Direction dir = c.ToDirection();

            return new Cart()
            {
                X = x,
                Y = y,
                NextDecision = Decision.TurnLeft,
                Direction = dir
            };
        }

        public static Track createTrack(int x, int y, char c)
        {
            TrackType type = c.ToTrackType();

            return new Track
            {
                X = x,
                Y = y,
                Type = type,
                C = c
            };
        }
    }
}
