using System;
using System.Linq;

namespace Advent.Utilities.Mathematics
{
    public static class GCD
    {
        public static void Simplify(int[] numbers)
        {
            int gcd = Calculate(numbers);
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] /= gcd;
        }

        private static int Calculate(int a, int b)
        {
            while (b > 0)
            {
                int rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        public static int Calculate(int[] args)
        {
            return args.Select(i => Math.Abs(i)).Aggregate((gcd, arg) => Calculate(gcd, arg));
        }
    }
}
