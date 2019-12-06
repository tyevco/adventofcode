using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.IO;
using System.Linq;

namespace AdventCalendar2019.D06
{
    [Exercise("Day 6: Universal Orbit Map")]
    class Y2019D06 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D06/Data");
        }

        protected override void Execute(string file)
        {
            var orbitMaps = File.ReadAllLines(file);
            Timer.Monitor(() =>
            {
                Process(orbitMaps);
            });
        }

        private void Process(string[] orbitMaps)
        {
            Diagram diagram = new Diagram();

            foreach (var orbitMap in orbitMaps.Select(s => s.Split(")")))
            {
                var center = orbitMap[0];
                var satellite = orbitMap[1];
                MassBody centerBody = diagram[center], satelliteBody = diagram[satellite];

                if (diagram[center] == null)
                {
                    centerBody = new MassBody()
                    {
                        Name = center,
                    };
                    diagram[center] = centerBody;
                }

                if (diagram[satellite] == null)
                {
                    satelliteBody = new MassBody
                    {
                        Name = satellite,
                    };
                    diagram[satellite] = satelliteBody;
                }

                centerBody.Satellites.Add(satelliteBody);
                satelliteBody.Orbitting = centerBody;
            }

            Console.WriteLine($"Total bodies: {diagram.Chart.Count}, Indirect orbits: {diagram.Chart.Select(b => b.Value.IndirectOrbits).Sum()}");

            Console.WriteLine(diagram["YOU"]);
            Console.WriteLine(diagram["SAN"]);

            Console.WriteLine(diagram.OrbitalTransfersBetween("YOU", "SAN"));
        }
    }
}
