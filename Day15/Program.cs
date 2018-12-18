using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day15
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var handle = GetStdHandle(-11);
                int mode;
                GetConsoleMode(handle, out mode);
                SetConsoleMode(handle, mode | 0x4);

                var map = new BattleMapParser().ParseData(args[0]);

                var system = new Game(map);

                Console.WriteLine(map);

                while (!system.Finished)
                {
                    system.Tick();

                    //Console.Clear();
                    Console.WriteLine($"Round #{system.Round}");
                    Console.WriteLine(map);
                    //System.Threading.Thread.Sleep(100);
                }

                //Console.Clear();
                //Console.WriteLine(map);
                Console.WriteLine($"Combat ends after {system.Round} full rounds");
                var type = system.Entities.Where(e => e.Health > 0).Select(t => t.Type).FirstOrDefault();
                var healthRemaining = system.Entities.Where(e => e.Health > 0).Sum(e => e.Health);
                Console.WriteLine($"{(type == EntityType.Elf ? "Elves" : "Goblins")} win with {healthRemaining} hit points left");
                Console.WriteLine($"Outcome: {system.Round} * {healthRemaining} =  {healthRemaining * system.Round}");
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}
