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

        public static class ChineseRemainderTheorem
        {
            public static long Solve(long[] n, long[] a)
            {
                long prod = n.Aggregate((long)1, (i, j) => i * j);
                long p;
                long sm = 0;
                for (long i = 0; i < n.Length; i++)
                {
                    p = prod / n[i];
                    sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
                }
                return sm % prod;
            }

            private static long ModularMultiplicativeInverse(long a, long mod)
            {
                long b = a % mod;
                for (long x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)
                    {
                        return x;
                    }
                }
                return 1;
            }
        }
    }
}
