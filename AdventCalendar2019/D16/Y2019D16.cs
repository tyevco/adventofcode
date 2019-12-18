using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D16
{
    [Exercise("Day 16: Flawed Frequency Transmission")]
    class Y2019D16 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D16/Data");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            Timer.Monitor(() =>
            {
                string signal = lines[0];

                Debug.WriteLine("Initial signal: " + signal);

                int offset = int.Parse(signal.Substring(0, 7));

                StringBuilder builder = new StringBuilder();
                for (int e = 0; e < 10000; e++)
                {
                    builder.Append(signal);
                }

                signal = builder.ToString();

                ParallelOptions opt = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 1000
                };

                for (int p = 0; p < 100; p++)
                {
                    Console.WriteLine(p + 1);
                    char[] nextPhase = new char[signal.Length];

                    Parallel.ForEach(Pattern.GetNextIndex(signal.Length), opt, i =>
                    {
                        var pattern = Pattern.GetNextFrequency(i, signal.Length);

                        Debug.WriteLine(string.Join(", ", pattern));

                        ConcurrentBag<int> results = new ConcurrentBag<int>();
                        Parallel.ForEach(Pattern.GetNextIndex(signal.Length), opt, f =>
                        {
                            int freq = int.Parse(signal[f].ToString());

                            results.Add(freq * pattern[f]);
                        });

                        var newFreq = results.Sum().ToString();
                        Debug.WriteLine("Results: " + string.Join(", ", results) + " = " + newFreq);
                        var outputDigit = newFreq[newFreq.Length - 1];
                        nextPhase[i] = outputDigit;
                    });

                    signal = string.Join(string.Empty, nextPhase);
                    Debug.WriteLine("Next signal: " + signal);
                }


                var message = signal.Substring(offset, 8);

                Console.WriteLine("Final signal: " + signal);
                Console.WriteLine("Message: " + message);
            });
        }

        private static class Pattern
        {
            private static int[] BasePattern { get; } = new int[] { 0, 1, 0, -1 };

            public static IEnumerable<int> GetNextIndex(int length)
            {
                int start = 0;
                while (start < length)
                {
                    yield return start++;
                }
            }

            public static IList<int> GetNextFrequency(int index, int num)
            {
                int position = 1;

                IList<int> freqs = new List<int>();
                while (position < num + 1)
                {
                    int pos = (position++ / (index + 1)) % BasePattern.Length;

                    freqs.Add(BasePattern[pos]);
                }

                return freqs;
            }

        }
    }
}
