using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using AdventCalendar2019.D18;

namespace Advent.Calendar.Y2019D18
{
    [Exercise("Day 18: Many-Worlds Interpretation")]
    class Y2019D18 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start($"{nameof(Y2019D18)}/Data");
        }

        protected override void Execute(string file)
        {
            Maze maze = new Maze();
            var mazeDataText = File.ReadAllText(file);

            Timer.Monitor(() =>
            {
                string[] mazeData = mazeDataText.Split("\r\n");
                int yLen = mazeData.Length;
                int xLen = mazeData.Select(x => x.Length).Max();
                int entranceIndex = mazeDataText.Replace("\r\n", string.Empty).IndexOf('@');
                int entranceX = entranceIndex % xLen;
                int entranceY = entranceIndex / xLen;

                for (int y = 0; y < yLen; y++)
                {
                    for (int x = 0; x < xLen; x++)
                    {
                        maze.SetTile(x, y, mazeData[y][x]);
                    }
                }

                maze.X = entranceX;
                maze.Y = entranceY;

                Debug.WriteLine($"Entrance ({entranceX},{entranceY})");

                var finishedMazes = ProcessMaze(maze);

                Debug.WriteLine($"Finished :::");

                bool best = true;
                int i = 1;
                foreach (var bestFinishedMaze in finishedMazes.OrderBy(x => x.Moves.Select(y => y.Data).Sum()))
                {
                    if (best)
                    {
                        bestFinishedMaze.DebugPrint(true);
                        best = false;
                    }

                    Console.WriteLine($"{i++} ::: {bestFinishedMaze.Moves.Select(x => x.Data).Sum()} total moves.");
                    Console.WriteLine($"Move order: {string.Join(", ", bestFinishedMaze.Moves.Select(m => bestFinishedMaze.Maze[m.X, m.Y].Data))}");
                    Console.WriteLine();
                }

            });
        }

        private IList<MazeInstance> ProcessMaze(Maze initialMaze)
        {
            List<MazeInstance> instances = new List<MazeInstance>();
            instances.Add(new MazeInstance(initialMaze));
            ConcurrentBag<MazeInstance> nextMazes = new ConcurrentBag<MazeInstance>();
            IList<MazeInstance> finishedMazes = new List<MazeInstance>();

            int generation = 0;
            while (instances.Count > 0)
            {
                Console.WriteLine($"Generation {generation++} ::: {instances.Count} mazes to process.");

                Parallel.ForEach(instances, maze =>
                {
                    IList<Point<int>> keyPaths = new List<Point<int>>();

                    foreach (var keyLocation in maze.RemainingKeys)
                    {
                        var locationRegex = maze.ValidLocationRegex(keyLocation.Data);
                        bool isValid(char c)
                        {
                            return locationRegex.IsMatch(c.ToString());
                        }

                        Debug.WriteLine($"{keyLocation.Data} :: ({keyLocation.X},{keyLocation.Y}) : SEEK");
                        var point = Pathfinding.FindTargetPoint(maze.Maze, keyLocation.X, keyLocation.Y, maze.X, maze.Y, isValid);
                        if (point != null)
                        {
                            Debug.WriteLine($"{keyLocation.Data} :: ({keyLocation.X},{keyLocation.Y}) : d = {point.Data}");
                            keyPaths.Add(point);
                        }
                    }

                    if (keyPaths.Any())
                    {
                        foreach (var keyPath in keyPaths)
                        {
                            var key = maze.Maze[keyPath.X, keyPath.Y];
                            Debug.WriteLine($"{key.Data} :: ({keyPath.X},{keyPath.Y}) : d = {keyPath.Data}");

                            var nextMaze = maze.Clone();
                            nextMaze.Move(keyPath);
                            nextMaze.CollectKey(key.Data);

                            if (nextMaze.RemainingKeys.Any())
                            {
                                nextMazes.Add(nextMaze);
                            }
                            else
                            {
                                finishedMazes.Add(nextMaze);
                            }
                        }
                    }
                    maze.DebugPrint();
                });

                instances = nextMazes.ToList();
                nextMazes.Clear();
            }

            return finishedMazes;
        }
    }
}
