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
            var blurb = (string.IsNullOrEmpty(name) ? "" : $"[{ name}] ");
            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{blurb}Starting stopwatch.");
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{blurb}Stopped stopwatch.");
            }

            Console.WriteLine($"{blurb}Finished in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
