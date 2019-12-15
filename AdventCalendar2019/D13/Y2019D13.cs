using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Intcode;

namespace AdventCalendar2019.D13
{
    [Exercise("Day 13: Care Package")]
    class Y2019D13 : FileSelectionConsole, IExercise
    {
        public bool RunSanityCheck { get; set; } = false;

        public void Execute()
        {
            Execute("D13/Data/Data.txt");
        }

        protected override void Execute(string file)
        {
            var gameCode = File.ReadAllText(file);

            var processor = new IntcodeProcessor(gameCode);

            ElfGame game = new ElfGame();
            Queue<long> gameOutput = new Queue<long>();
            long currentScore = 0;
            Timer.Monitor(() =>
            {
                //Point lastBall = null;
                //Point lastPaddle = null;

                processor.Output += output =>
                {
                    gameOutput.Enqueue(output);

                    if (gameOutput.Count >= 3)
                    {
                        int x = (int)gameOutput.Dequeue();
                        int y = (int)gameOutput.Dequeue();

                        if (x == -1 && y == 0)
                        {
                            currentScore = gameOutput.Dequeue();
                            Console.SetCursorPosition(0, game.MaxY + 1);
                            Console.WriteLine($"Score: {currentScore}                                ");
                            Console.SetCursorPosition(0, game.MaxY + 2);
                        }
                        else
                        {
                            GameTile tile = (GameTile)gameOutput.Dequeue();
                            game.SetTile(x, y, tile);
                        }

                        Console.WriteLine($"Ball: {game.Ball}                                ");
                        Console.WriteLine($"Paddle: {game.Paddle}                                ");
                    }

                    return true;
                };

                processor.Input += () =>
                {
                    long value = 0;
                    if (game.Ball != null && game.Paddle != null)
                    {
                        if (game.Ball.X < game.Paddle.X)
                        {
                            value = -1;
                            Console.WriteLine("Left           ");
                        }
                        else if (game.Ball.X > game.Paddle.X)
                        {
                            value = 1;
                            Console.WriteLine("Right            ");
                        }
                        else
                        {
                            Console.WriteLine("Stay           ");
                        }

                        Console.WriteLine("Running: Yes");
                    }

                    System.Threading.Thread.Sleep(10);

                    //lastBall = game.Ball;
                    //lastPaddle = game.Paddle;

                    return value;
                };

                processor.WriteValue(0, 2);
                processor.Process();

                Console.Clear();
                ElfGame.PrintGrid(game.Points);
                Console.WriteLine($"Score: {currentScore}                                ");
                Console.WriteLine($"Ball: {game.Ball}                                ");
                Console.WriteLine($"Paddle: {game.Paddle}                                ");
                Console.WriteLine($"Total blocks remaining: {game.Points.Count(x => ((GameTile)x.Value.Data) == GameTile.Block)}");
            });
        }
    }
}
