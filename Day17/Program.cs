using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var grid = new SpringParser().ParseData(args[0]);

                Console.WriteLine(grid);

                var active = grid.Active();

                Console.WriteLine($"Water tiles: {active.Count(x => x != null && x.GetType().Equals(typeof(Water)))}");
                Console.WriteLine($" Clay tiles: {active.Count(x => x != null && x.GetType().Equals(typeof(Clay)))}");

                Console.ReadLine();
            }
        }

    }
}
