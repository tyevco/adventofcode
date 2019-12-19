using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var mazeDataText = File.ReadAllText(file);

            Timer.Monitor(() =>
            {
                string[] mazeData = mazeDataText.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                int yLen = mazeData.Length;
                int xLen = mazeData.Select(x => x.Length).Max();

                Maze maze = new Maze(string.Join(string.Empty, mazeData), xLen, yLen);

                if (Debug.EnableDebugOutput)
                {
                    foreach (var robot in maze.Robots)
                    {
                        Debug.WriteLine($"Robot at ({robot.X},{robot.Y})");
                    }
                }
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
                    // Console.WriteLine($"Move order: {string.Join(", ", bestFinishedMaze.CollectedOrder)}");
                    Console.WriteLine();
                }
            });
        }

        private IDictionary<DataPoint<char>, IList<DataPoint<State>>> BuildGraph(Maze maze)
        {
            IDictionary<DataPoint<char>, IList<DataPoint<State>>> graph = new Dictionary<DataPoint<char>, IList<DataPoint<State>>>();

            Queue<DataPoint<char>> points = new Queue<DataPoint<char>>(maze.KeyLocations);
            IList<DataPoint<char>> keyLocs = points.ToList();
            foreach (var robot in maze.Robots)
            {
                points.Enqueue(maze[robot.X, robot.Y]);
            }

            while (points.Any())
            {
                var point = points.Dequeue();

                Debug.WriteLine($"Building graph for {point.Data}.");

                var keyPaths = Pathfinding<State>.FindTargetPoints(maze, point.X, point.Y, IsMatch, PointMode.Point, CreatePointData, keyLocs.Where(x => x.Data != point.Data).ToArray())
                                .OrderBy(p => p.Distance)
                                .ToList();

                graph.Add(point, keyPaths);
            }


            return graph;
        }

        private bool IsMatch(DataPoint<char> point)
        {
            return point.Data != '#';
        }

        private DataPoint<State> CreatePointData(DataPoint<char> point, DataPoint<State> lastDataPoint)
        {
            // check if this is a door, if it is set the bit on the Data object on the returned point.
            (int obstacles, int keys) = lastDataPoint.Data;

            if (char.IsUpper(point.Data))
            {
                obstacles |= (int)Math.Pow(2, char.ToLower(point.Data) - 'a');
            }
            else if (char.IsLower(point.Data))
            {
                keys |= (int)Math.Pow(2, point.Data - 'a');
            }

            var state = new State
            {
                Obstacles = obstacles,
                Keys = keys,
            };

            return new DataPoint<State>(point.X, point.Y, state)
            {
                Distance = lastDataPoint.Distance + 1,
            };
        }

        private IList<MazeInstance> ProcessMaze(Maze initialMaze, IDictionary<DataPoint<char>, IList<DataPoint<State>>> graph)
        {
            Queue<MazeInstance> instances = new Queue<MazeInstance>();
            instances.Enqueue(new MazeInstance(initialMaze));

            IList<MazeInstance> finishedMazes = new List<MazeInstance>();
            IDictionary<State, int> discoveredPaths = new Dictionary<State, int>();

            Console.SetCursorPosition(26, 0);
            Console.Write("/");

            while (instances.Any())
            {
                var maze = instances.Dequeue();

                if (instances.Count % 50000 == 0)
                {
                    string pathCount = discoveredPaths.Count.ToString();
                    Console.SetCursorPosition(25 - pathCount.Length, 0);
                    Console.Write(pathCount);

                    string instancesCount = instances.Count.ToString();
                    Console.SetCursorPosition(50 - instancesCount.Length, 0);
                    Console.Write(instancesCount);
                }

                var state = maze.State;
                if (discoveredPaths.ContainsKey(state))
                {
                    if (discoveredPaths[state] < maze.Distance)
                    {
                        continue;
                    }

                    discoveredPaths[state] = maze.Distance;
                }
                else
                {
                    discoveredPaths.Add(state, maze.Distance);
                }

                for (int i = 0; i < maze.Robots.Length; i++)
                {
                    var robot = maze.Robots[i];
                    var robotPoint = maze.Maze[robot.X, robot.Y];
                    var keyPaths = graph[robotPoint];
                    if (keyPaths.Any())
                    {
                        foreach (var keyPath in keyPaths)
                        {
                            var key = maze.Maze[keyPath.X, keyPath.Y];

                            // check to see if we have not collected this key.
                            if (!maze.IsKeyCollected(key.Data))
                            {
                                Debug.Write($"{robot} [{robotPoint.Data}] -> {keyPath.X},{keyPath.Y} [{key.Data}] | Keys: [{keyPath.Data.Keys}] Doors: [{keyPath.Data.Obstacles}]");

                                // check to see if there are any locked doors on this path.
                                if (keyPath.Data.Obstacles == 0 || (maze.Keys > 0 && (keyPath.Data.Obstacles & maze.Keys) == keyPath.Data.Obstacles))
                                {
                                    Debug.WriteLine(" ~ open");

                                    var nextMaze = maze.Clone();
                                    nextMaze.Move(i, keyPath);

                                    if (nextMaze.RemainingKeys.Any())
                                    {
                                        instances.Enqueue(nextMaze);
                                    }
                                    else
                                    {
                                        finishedMazes.Add(nextMaze);
                                    }
                                }
                                else
                                {
                                    Debug.WriteLine(" ~ closed");
                                }
                            }
                        }
                    }
                }
                maze.DebugPrint();
            }

            return finishedMazes;
        }
    }
}
