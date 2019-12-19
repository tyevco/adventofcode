using Advent.Calendar.Y2019D18;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using AdventCalendar2019.D18;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar2019.D18
{
    [Exercise("Day 18: Many-Worlds Interpretation")]
    class Y2019D18 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D18/Data");
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

                StringBuilder builder = new StringBuilder();

                bool best = true;
                int i = 1;
                foreach (var bestFinishedMaze in finishedMazes.OrderBy(x => x.Distance).Take(5))
                {
                    if (best)
                    {
                        bestFinishedMaze.DebugPrint(true);
                        best = false;
                    }

                    Console.WriteLine($"{i++} ::: {bestFinishedMaze.Distance} total moves.");
                    Console.WriteLine($"Move order: {string.Join(", ", bestFinishedMaze.CollectedOrder)}");
                    Console.WriteLine();
                }

                Console.WriteLine(builder.ToString());
                File.WriteAllText($"../../../{DateTime.Now.Ticks}_lastrun.log", builder.ToString());

            });
        }

        private IList<MazeInstance> ProcessMaze(Maze initialMaze)
        {
            Queue<MazeInstance> instances = new Queue<MazeInstance>();
            instances.Enqueue(new MazeInstance(initialMaze)
            {
                X = initialMaze.X,
                Y = initialMaze.Y,
            });

            IList<MazeInstance> finishedMazes = new List<MazeInstance>();
            IDictionary<int, int> discoveredPaths = new Dictionary<int, int>();

            while (instances.Any())
            {
                var maze = instances.Dequeue();

                if (discoveredPaths.ContainsKey(maze.Keys))
                {
                    if (discoveredPaths[maze.Keys] < maze.Distance)
                    {
                        continue;
                    }

                    discoveredPaths[maze.Keys] = maze.Distance;
                }
                else
                {
                    discoveredPaths[maze.Keys] = maze.Distance;
                }

                IEnumerable<Point<int>> keyPaths = Pathfinding.FindTargetPoints(maze.Maze, maze.X, maze.Y, maze.IsMatch, PointMode.Point, maze.RemainingKeys);

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
                            instances.Enqueue(nextMaze);
                        }
                        else
                        {
                            finishedMazes.Add(nextMaze);
                        }
                    }
                }
                maze.DebugPrint();
            }

            return finishedMazes;
        }
    }
}
