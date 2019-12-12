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

            Timer.Monitor(() =>
            {
                IList<Body> bodies = new List<Body>();
                foreach (string bodyData in lines)
                {
                    var bodyMatch = BodyParser.Matches(bodyData);
                    if (bodyMatch.All(x => x.Success))
                    {
                        var body = new Body
                        {
                            Position = new Point(
                                            int.Parse(bodyMatch[0].Value),
                                            int.Parse(bodyMatch[1].Value),
                                            int.Parse(bodyMatch[2].Value)),
                            Velocity = new Vector3i(0, 0, 0),
                        };

                        bodies.Add(body);
                    }
                    else
                    {
                        throw new InvalidDataException("Input data had an issue.");
                    }
                }

                WriteBodies(bodies);
            });
        }

        private static void WriteBodies(IList<Body> bodies)
        {
            foreach (var body in bodies)
            {
                Console.WriteLine(body.ToString());
            }
        }
    }
}
