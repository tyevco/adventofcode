using System;
using System.Collections.Generic;
using Advent.Utilities;

namespace Day25
{
    class Program : SelectableConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
        }

        protected override void Execute(string file)
        {
            (IList<Star> stars, int expected) = new ConstellationParser().ParseData(file);
            IList<Constellation> constellations = new List<Constellation>();

            foreach (var star in stars)
            {
                Console.WriteLine(star);
                foreach (var other in stars)
                {
                    if (!star.Equals(other))
                    {
                        var distance = star.DistanceTo(other);

                        bool added = false;
                        if (distance <= 3)
                        {
                            if (other.Constellation == null && star.Constellation == null)
                            {
                                var constellation = new Constellation()
                                {
                                    Id = constellations.Count + 1
                                };
                                constellation.Stars.Add(star);
                                star.Constellation = constellation;
                                constellation.Stars.Add(other);
                                other.Constellation = constellation;

                                constellations.Add(constellation);
                                added = true;
                            }
                            else if (other.Constellation == null)
                            {
                                star.Constellation.Stars.Add(other);
                                other.Constellation = star.Constellation;
                                added = true;
                            }
                            else if (star.Constellation == null)
                            {
                                other.Constellation.Stars.Add(star);
                                star.Constellation = other.Constellation;
                                added = true;
                            }
                            else
                            {
                                if (other.Constellation.Equals(star.Constellation))
                                {
                                    Console.WriteLine("Same constellations");
                                }
                                else
                                {
                                    Console.WriteLine("TODO: merge constellations");
                                }
                            }
                        }

                        //Console.WriteLine($"{star} - {other} = {distance.ToString().PadLeft(2)} {(added ? "Y" : "N")}");
                    }
                }

                Console.WriteLine();
            }

            if (constellations.Count == expected)
            {
                Console.WriteLine($"The correct number of constellations were detected, {expected}.");
            }
            else
            {
                Console.WriteLine($"Found {constellations.Count} constellations, {expected} expected.");
            }
            // 293 is too low
        }
    }
}
