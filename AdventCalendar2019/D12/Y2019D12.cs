using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar2019.D12
{
    [Exercise("Day 12: The N-Body Problem")]
    class Y2019D12 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D12/Data");
        }

        private static readonly Regex BodyParser = new Regex(@"^<x=([-0-9]+), y=([-0-9]+), z=([-0-9]+)>$");

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);
            Timer.Monitor(() =>
            {
                //PartOne(lines);
                PartTwo(lines);
            });
        }

        private void PartOne(string[] lines)
        {
            int cycles = Helper.ReadIntInput("Number of cycles");
            int bodyCount = 0;
            IList<Moon> bodies = new List<Moon>();
            foreach (string bodyData in lines)
            {
                var bodyMatch = BodyParser.Match(bodyData);
                if (bodyMatch.Success)
                {
                    var body = new Moon
                    {
                        Position = new Point(
                                        int.Parse(bodyMatch.Groups[1].Value),
                                        int.Parse(bodyMatch.Groups[2].Value),
                                        int.Parse(bodyMatch.Groups[3].Value)),
                        Velocity = new Vector3i(0, 0, 0),
                        ID = bodyCount++,
                    };

                    bodies.Add(body);
                }
                else
                {
                    throw new InvalidDataException("Input data had an issue.");
                }
            }

            int i = 0;
            for (i = 0; i < cycles; i++)
            {
                Console.WriteLine($"After {i} steps:");
                WriteBodies(bodies);

                foreach (var body in bodies)
                {
                    foreach (var other in bodies)
                    {
                        body.ApplyGravity(other);
                    }
                }

                foreach (var body in bodies)
                {
                    body.Move();
                }
            }

            Console.WriteLine($"After {i} steps:");
            WriteBodies(bodies);

            Console.WriteLine($"Sum of total energies: {bodies.Select(x => x.TotalEnergy).Sum()}");
        }


        private void PartTwo(string[] lines)
        {
            int bodyCount = 0;
            IList<Moon> bodies = new List<Moon>();
            foreach (string bodyData in lines)
            {
                var bodyMatch = BodyParser.Match(bodyData);
                if (bodyMatch.Success)
                {
                    var body = new Moon
                    {
                        Position = new Point(
                                        int.Parse(bodyMatch.Groups[1].Value),
                                        int.Parse(bodyMatch.Groups[2].Value),
                                        int.Parse(bodyMatch.Groups[3].Value)),
                        Velocity = new Vector3i(0, 0, 0),
                        ID = bodyCount++,
                    };

                    bodies.Add(body);
                }
                else
                {
                    throw new InvalidDataException("Input data had an issue.");
                }
            }

            int cycleX = GetCycleCountUntilRepeat(
                    bodies,
                    (b1, b2) => b1.ApplyGravityX(b2),
                    b => b.MoveX(),
                    b => b.Position.X.GetHashCode(),
                    b => b.Velocity.X.GetHashCode());

            int cycleY = GetCycleCountUntilRepeat(
                    bodies,
                    (b1, b2) => b1.ApplyGravityY(b2),
                    b => b.MoveY(),
                    b => b.Position.Y.GetHashCode(),
                    b => b.Velocity.Y.GetHashCode());

            int cycleZ = GetCycleCountUntilRepeat(
                    bodies,
                    (b1, b2) => b1.ApplyGravityZ(b2),
                    b => b.MoveZ(),
                    b => b.Position.Z.GetHashCode(),
                    b => b.Velocity.Z.GetHashCode());

            long cycleMax = Mathematics.LCM(cycleX, Mathematics.LCM(cycleY, cycleZ));

            Console.WriteLine($"X:{cycleX}, Y:{cycleY}, Z:{cycleZ} | Repeats after {cycleMax} cycles.");
        }

        private int GetCycleCountUntilRepeat(IList<Moon> bodies, Action<Moon, Moon> gravity, Action<Moon> move, Func<Moon, long> position, Func<Moon, long> velocity)
        {
            IDictionary<long, long> cycles = new Dictionary<long, long>();
            bool running = true;
            while (running)
            {
                foreach (var body in bodies)
                {
                    foreach (var other in bodies)
                    {
                        gravity(body, other);
                    }
                }

                foreach (var body in bodies)
                {
                    move(body);
                }

                long hash = GetHash(bodies, position, velocity);
                if (cycles.ContainsKey(hash))
                {
                    running = false;
                }
                else
                {
                    cycles[hash] = 1;
                }
            }

            return cycles.Count;
        }

        private static void WriteBodies(IList<Moon> bodies)
        {
            foreach (var body in bodies)
            {
                Console.WriteLine(body.ToString());
            }

            foreach (var body in bodies)
            {
                Console.WriteLine(body.ToEnergyString());
            }
        }

        private long GetHash(IList<Moon> bodies, Func<Moon, long> GetFirstFunc, Func<Moon, long> GetSecondFunc)
        {
            long hash = 93525;

            foreach (var body in bodies)
            {
                hash = hash * 7848 + GetFirstFunc(body);
                hash = hash * 7848 + GetSecondFunc(body);
            }

            return hash;
        }
    }
}
