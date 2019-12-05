using System;
using System.Diagnostics;

namespace Advent.Utilities
{
    public static class Timer
    {
        public static void Monitor(Action action)
        {
            StackTrace stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1);

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"Starting stopwatch for {frame.GetFileName()}:{frame.GetMethod().Name}:{frame.GetFileLineNumber()}");
            }
            else
            {
                Console.WriteLine();
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            if (Debug.EnableDebugOutput)
            {
                Console.WriteLine($"Stopped stopwatch for {frame.GetFileName()}:{frame.GetMethod().Name}");
            }

            Console.WriteLine($"{frame.GetFileName()}:{frame.GetMethod().Name} : {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
