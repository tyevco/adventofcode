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

                var system = new Game(map);

                Console.WriteLine(map);

                while (!system.Finished)
                {
                    system.Tick();

                    Console.Clear();
                    Console.WriteLine(map);
                    System.Threading.Thread.Sleep(500);
                }

                Console.Clear();
                Console.WriteLine(map);
            }

            Console.ReadLine();
        }
    }
}
