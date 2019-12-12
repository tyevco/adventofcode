using System;
using System.Linq;

namespace Advent.Utilities.Mathematics
{
    public static class Mathematics
    {
        public static void Simplify(long[] numbers)
        {
            long gcd = Calculate(numbers);
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] /= gcd;
        }

        public static long GCD(long a, long b)
        {
            while (b > 0)
            {
                long rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        public static long Calculate(long[] args)
        {
            return args.Select(i => Math.Abs(i)).Aggregate((gcd, arg) => GCD(gcd, arg));
        }

        public static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }
    }
}
