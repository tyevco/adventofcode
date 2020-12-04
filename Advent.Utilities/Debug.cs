using Advent.Utilities.Extensions;
using System;

namespace Advent.Utilities
{
    public static class Debug
    {
        public static bool EnableDebugOutput { get; set; } = false;

        public static void Write(object obj)
        {
            if (EnableDebugOutput)
                Console.Write(obj);
        }

        public static void WriteLine(string obj, params object[] values)
        {
            if (EnableDebugOutput)
                Console.WriteLine(obj, values);
        }

        public static void WriteLine(string obj, (string, object)[] values)
        {
            if (EnableDebugOutput)
                Console.WriteLine(obj.Interpolate(values));
        }

        public static void WriteLine(object obj)
        {
            if (EnableDebugOutput)
                Console.WriteLine(obj);
        }

        public static void WriteLine()
        {
            if (EnableDebugOutput)
                Console.WriteLine();
        }
    }
}
