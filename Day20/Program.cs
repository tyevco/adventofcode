using Advent.Utilities;
using System;

namespace Day20
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
            var building = new BuildingParser().ParseData(file);

            Console.WriteLine("Actual:");
            Console.WriteLine(building);

            Console.WriteLine();

            Console.WriteLine("Expected:");
            Console.WriteLine(building.Expected);
        }
    }
}
