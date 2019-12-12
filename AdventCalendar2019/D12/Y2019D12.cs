using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
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
            int cycles = Helper.ReadIntInput("Number of cycles");
            int bodyCount = 0;
            Timer.Monitor(() =>
            {
                IList<Body> bodies = new List<Body>();
                foreach (string bodyData in lines)
                {
                    var bodyMatch = BodyParser.Match(bodyData);
                    if (bodyMatch.Success)
                    {
                        var body = new Body
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
            });
        }

        private static void WriteBodies(IList<Body> bodies)
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
    }
}
