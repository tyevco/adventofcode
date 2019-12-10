using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2015.D04
{
    [Exercise("Day 4: The Ideal Stocking Stuffer")]
    class Y2015D04
    {
        public void Execute()
        {
            Console.WriteLine($"{Execute("abcdef")} == 609043");
            Console.WriteLine($"{Execute("pqrstuv")} == 1048970");

            Console.WriteLine($"Answer: {Execute("ckczppom")}");
            Console.WriteLine($"Answer: {Execute("ckczppom", 6)}");
        }

        protected int Execute(string secretKey, int minPrefix = 5)
        {
            var seekVal = string.Empty.PadLeft(minPrefix, '0');

            var md5 = MD5.Create();
            for (int i = 0; i < int.MaxValue; i++)
            {
                byte[] buf = Encoding.ASCII.GetBytes($"{secretKey}{i}");
                var hash = string.Join(string.Empty, md5.ComputeHash(buf).Select(b => b.ToString("x2")));
                if (hash.StartsWith(seekVal))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
