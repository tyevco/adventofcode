using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D10
{
    [Exercise("Day 10: Monitoring Station")]
    class Y2019D10 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D10/Data");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            Timer.Monitor(() =>
            {
                int answer = -1;
                int targettedAsteroid = 200;
                int mostAsteroids = 0;
                Point asteroidPoint = null;
                IList<Queue<Point>> targettingGroups = null;

                var points = GetPoints(lines);

                foreach (var point in points)
                {
                    var slopeGroups = points.Where(p => p != point)
                                         .Select(p => new KeyValuePair<Point, Vector2i>(p, point.CalculateVector(p)))
                                         .OrderBy(p => point.CalculateDistance(p.Key))
                                         .GroupBy(x => (x.Value.Angle + 270) % 360)
                                         .OrderBy(x => x.Key)
                                         .Select(x => new Queue<Point>(x.Select(v => v.Key)))
                                         .ToList();

                    var asteroidVisibleCount = slopeGroups.Count();
                    if (mostAsteroids < asteroidVisibleCount)
                    {
                        asteroidPoint = point;
                        mostAsteroids = asteroidVisibleCount;
                        targettingGroups = slopeGroups;
                    }
                }

                Console.WriteLine($"Best is {asteroidPoint} with {mostAsteroids} other asteroids detected.");

                if (targettingGroups != null)
                {
                    int asteroidsDestroyed = 0;
                    int asteroids = targettingGroups.Select(x => x.Count).Sum();
                    while (asteroids > 0)
                    {
                        for (int i = 0; i < targettingGroups.Count; i++)
                        {
                            var targetPoint = targettingGroups[i].Dequeue();
                            asteroidsDestroyed++;

                            if (asteroidsDestroyed == targettedAsteroid)
                            {
                                answer = targetPoint.X * 100 + targetPoint.Y;
                                break;
                            }
                        }

                        targettingGroups = targettingGroups.Where(x => x.Count > 0).ToList();

                        asteroids = targettingGroups.Select(x => x.Count).Sum();
                    }

                    Console.WriteLine($"Answer: {answer}");
                }
            });
        }

        private IList<Point> GetPoints(string[] lines)
        {
            IList<Point> points = new List<Point>();

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

            return points;
        }
    }
}
