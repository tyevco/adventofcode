using System;
using System.Diagnostics;

namespace Advent.Utilities
{
    public static class Timer
    {
        public static void Monitor(Action action)
        {
            Monitor(null, action);
        }

        public static void Monitor(string name, Action action)
        {
            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{(string.IsNullOrEmpty(name) ? "" : $"[{ name}] ")}Starting stopwatch.");
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{(string.IsNullOrEmpty(name) ? "" : $"[{ name}] ")}Stopped stopwatch.");
            }

            Console.WriteLine($"{(string.IsNullOrEmpty(name) ? "" : $"[{ name}] ")}Finished in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
