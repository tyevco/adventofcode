using System;
using System.IO;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2015.D02
{
    [Exercise("Day 2: I Was Told There Would Be No Math")]
    class Y2015D02 : FileSelectionConsole
    {
        public void Execute()
        {
            this.Start("D02/Data");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            int totalSqft = 0, totalRibLen = 0;

            foreach (var line in lines)
            {
                var sections = line.Split("=>");

                var surfaceArea = CalculateSurfaceArea(sections[0]);
                var ribbonLength = CalculateRibbonLength(sections[0]);
                totalSqft += surfaceArea;
                totalRibLen += ribbonLength;

                if (sections.Length == 2)
                {
                    var answers = sections[1].Split(",");
                    int expectedSqFt = int.Parse(answers[0]);
                    int expectedRibLen = int.Parse(answers[1]);
                    Console.WriteLine($"Square Feet: {surfaceArea}, expected {expectedSqFt} : {surfaceArea == expectedSqFt} | Ribbon: {ribbonLength}, expected {expectedRibLen} : {ribbonLength == expectedRibLen}");
                }
                else
                {
                    Console.WriteLine($"Square Feet: {surfaceArea} | Ribbon: {ribbonLength}");
                }
            }

            Console.WriteLine($"Total sqft: {totalSqft} | Total Ribbon Length: {totalRibLen}");
        }

        private int CalculateSurfaceArea(string input)
        {
            var dimensions = input.Split("x").Select(x => int.Parse(x)).ToArray();
            int total = 0;

            int side1 = dimensions[0] * dimensions[1];
            int side2 = dimensions[1] * dimensions[2];
            int side3 = dimensions[0] * dimensions[2];

            total = 2 * (side1 + side2 + side3) + Math.Min(side1, Math.Min(side2, side3));

            return total;
        }


        private int CalculateRibbonLength(string input)
        {
            var dimensions = input.Split("x").Select(x => int.Parse(x)).ToArray();
            int total = 0;

            int vol = dimensions[0] * dimensions[1] * dimensions[2];

            int side1 = dimensions[0];
            int side2 = dimensions[1];
            int side3 = dimensions[2];

            total = 2 * (side1 + side2 + side3 - Math.Max(side1, Math.Max(side2, side3))) + vol;

            return total;
        }
    }
}
