using System;
using System.Diagnostics;

namespace Advent.Utilities
{
    public static class Timer
    {
        public static void Monitor(string name, Action action)
        {
            Perform(action, name);
        }

        public static void Monitor(Action action)
        {
            Perform(action);
        }

        private static void Perform(Action action, string actionName = null)
        {
            string actionBlurb = actionName == null ? string.Empty : $"{actionName} : ";

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{actionBlurb}Starting stopwatch");
            }
            else
            {
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"{actionBlurb}Stopped stopwatch");
            }

            Console.WriteLine($"{actionBlurb}{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
