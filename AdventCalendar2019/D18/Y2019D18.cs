using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;

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

                var graph = BuildGraph(maze);
                var finishedMazes = ProcessMaze(maze, graph);

                Debug.WriteLine($"Finished :::");

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
            });
        }

        private IDictionary<PointData<char>, IList<PointData<int>>> BuildGraph(Maze maze)
        {
            IDictionary<PointData<char>, IList<PointData<int>>> graph = new Dictionary<PointData<char>, IList<PointData<int>>>();

            Queue<PointData<char>> points = new Queue<PointData<char>>(maze.KeyLocations);
            IList<PointData<char>> keyLocs = points.ToList();
            points.Enqueue(maze[maze.X, maze.Y]);

            bool isMatch(PointData<char> p)
            {
                return true;
            }

            while (points.Any())
            {
                var point = points.Dequeue();

                Console.WriteLine($"Building graph for {point.Data}.");

                var keyPaths = Pathfinding<int>.FindTargetPoints(maze, point.X, point.Y, isMatch, PointMode.Point, keyLocs.Where(x => x.Data != point.Data).ToArray()).ToList();

                graph.Add(point, keyPaths);
            }


            return graph;
        }

        private IList<MazeInstance> ProcessMaze(Maze initialMaze, IDictionary<PointData<char>, IList<PointData<int>>> graph)
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


                var keyPaths = graph[maze.Maze[maze.X, maze.Y]];
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
