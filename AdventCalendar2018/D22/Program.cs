using Advent.Utilities;
using System;

namespace Day22
{
    class Program : FileSelectionConsole
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
            (Cave cave, int riskLevel, string expectedLayout) = new CaveBuilder().ParseData(file);

            var actualCave = cave.ToString();

            Console.WriteLine(actualCave);

            if (actualCave.Equals(expectedLayout))
            {
                Console.WriteLine($"The cave layouts matched!");
            } else
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
