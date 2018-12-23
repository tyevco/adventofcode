using System;
using Advent.Utilities;

namespace Day22
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
            (Cave cave, int riskLevel, string expectedLayout) = new CaveBuilder().ParseData(file);

            var actualCave = cave.ToString();

            Console.WriteLine(actualCave);

            if (actualCave.Equals(expectedLayout))
            {
                Console.WriteLine($"The cave layouts matched!");

                Console.WriteLine($"The risk level {(cave.RiskLevel == riskLevel ? "matches" : "does not match")}.");
            }
        }
    }
}
