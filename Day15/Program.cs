using System;

namespace Day15
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var map = new BattleMapParser().ParseData(args[0]);

                Console.WriteLine(map);

                var system = new Game(map);

                while (!system.Finished)
                {
                    system.Tick();
                }
            }

            Console.ReadLine();
        }
    }
}
