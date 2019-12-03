using System;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D22
{
    [Exercise("Day 22:  ")]
    class Program : FileSelectionConsole
    {
        public void Execute()
        {
            Start("D22/Data");
        }

        protected override void Execute(string file)
        {
            (Cave cave, int riskLevel, string expectedLayout) = new CaveBuilder().ParseData(file);

            var actualCave = cave.ToString();

            Console.WriteLine(actualCave);

            if (actualCave.Equals(expectedLayout))
            {
                Console.WriteLine($"The cave layouts matched!");
            }
            else
            {
                Console.WriteLine("The cave layouts do not match.");
            }

            if (riskLevel > 0)
            {
                Console.WriteLine($"The risk level [{cave.RiskLevel}] {(cave.RiskLevel == riskLevel ? "matches" : "does not match")}.");
            }
            else
            {
                Console.WriteLine($"The risk level is {(cave.RiskLevel)}.");
            }
        }
    }
}
