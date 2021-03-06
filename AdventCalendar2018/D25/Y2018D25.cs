using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D25
{
    [Exercise("Day 25: Four-Dimensional Adventure")]
    class Program : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D25/Data");
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

                                Console.WriteLine($"Created a new constellation: {constellation.Id}");
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
                                if (!other.Constellation.Equals(star.Constellation))
                                {
                                    var otherConstellation = other.Constellation;

                                    foreach (var otherStar in otherConstellation.Stars)
                                    {
                                        star.Constellation.Stars.Add(otherStar);
                                        otherStar.Constellation = star.Constellation;
                                    }

                                    otherConstellation.Stars.Clear();
                                }
                            }
                        }

                        int color = distance <= 3 ? (added ? 0x2a : 0x25) : 0xaa;
                        Console.WriteLine(ConsoleCodes.Colorize($"{star} - {other} = {distance.ToString().PadLeft(2)} {(added ? "Y" : "N")}", color));
                    }
                }

                Console.WriteLine();
            }

            foreach (var star in stars.OrderBy(s => s.Constellation?.Id ?? 0))
            {
                Console.WriteLine(star);
            }

            int foundConstellations = constellations.Count(s => s.Stars.Count > 0);

            if (foundConstellations == expected)
            {
                Console.WriteLine($"The correct number of constellations were detected, {expected}.");
            }
            else
            {
                Console.WriteLine($"Found {foundConstellations} constellations, {expected} expected.");
            }
            // 293 is too low
        }
    }
}
