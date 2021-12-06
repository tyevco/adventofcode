using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2021.D03
{
    [Exercise("Day 3: Binary Diagnostic")]
    class Y2021D03 : FileSelectionParsingConsole<IList<int[]>>, IExercise
    {
        public void Execute()
        {
            Start("D03/Data");
        }

        protected override IList<int[]> DeserializeData(IList<string> data)
        {
            return data.Select(x => x.ToCharArray())
                        .Select(x => x.Select(c => int.Parse(c.ToString())).ToArray())
                        .ToList();
        }

        protected override void Execute(IList<int[]> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                var output = data
                                .Select(c => c.Select(n => 1 - (2 * (1 - n))).ToList())
                                .ToList();

                var cnt = output[0].Count;

                var gamma = new int[cnt];
                var epsilon = new int[cnt];

                for (int n = 0; n < cnt; n++)
                {
                    int pos = 0;
                    for (int i = 0; i < output.Count; i++)
                    {
                        pos += output[i][n];
                    }

                    if (pos > 0)
                    {
                        gamma[n] = 1;
                        epsilon[n] = 0;
                    }
                    else if (pos < 0)
                    {
                        gamma[n] = 0;
                        epsilon[n] = 1;
                    }
                }

                var gammaFinal = string.Join("", gamma.Select(x => x.ToString()));
                var gammaInt = Convert.ToInt32(gammaFinal, 2);
                var epsilonFinal = string.Join("", epsilon.Select(x => x.ToString()));
                var epsilonInt = Convert.ToInt32(epsilonFinal, 2);

                Console.WriteLine($"Γ:{gammaFinal} ({gammaInt}) | ε:{epsilonFinal} ({epsilonInt}) | Answer: {gammaInt * epsilonInt}");
            });

            Timer.Monitor("Part 2", () =>
            {
                int gammaInt = 0;
                int epsilonInt = 0;

                Timer.Monitor("Part 2 (O2 Generator)", () =>
                {
                    int bit = 0;
                    var filtered = FilterGamma(data, bit++);
                    while (filtered.Count() > 1)
                    {
                        filtered = FilterGamma(filtered, bit++);
                    }

                    var valueString = string.Join("", filtered.FirstOrDefault().Select(x => x.ToString()));
                    gammaInt = Convert.ToInt32(valueString, 2);
                    Console.WriteLine($"{valueString} : {gammaInt}");
                });

                Timer.Monitor("Part 2 (CO2 Scrubber)", () =>
                {
                    int bit = 0;
                    var filtered = FilterEpsilon(data, bit++);
                    while (filtered.Count() > 1)
                    {
                        filtered = FilterEpsilon(filtered, bit++);
                    }

                    var valueString = string.Join("", filtered.FirstOrDefault().Select(x => x.ToString()));
                    epsilonInt = Convert.ToInt32(valueString, 2);
                    Console.WriteLine($"{valueString} : {epsilonInt}");
                });

                Console.WriteLine($"Answer: {gammaInt * epsilonInt}");
            });
        }

        private IEnumerable<int[]> FilterGamma(IEnumerable<int[]> input, int detectBit)
        {
            var output = input
                            .Select(c => c.Select(n => 1 - (2 * (1 - n))).ToList())
                            .ToList();

            var cnt = output[0].Count;

            var gamma = new int[cnt];

            for (int n = 0; n < cnt; n++)
            {
                int pos = 0;
                for (int i = 0; i < output.Count; i++)
                {
                    pos += output[i][n];
                }

                if (pos >= 0)
                {
                    gamma[n] = 1;
                }
                else if (pos < 0)
                {
                    gamma[n] = 0;
                }
            }

            var gammaOutput = new List<int[]>();

            foreach (var item in input)
            {
                if (item[detectBit] == gamma[detectBit])
                {
                    gammaOutput.Add(item);
                }
            }

            return gammaOutput;
        }

        private IEnumerable<int[]> FilterEpsilon(IEnumerable<int[]> input, int detectBit)
        {
            var output = input
                            .Select(c => c.Select(n => 1 - (2 * (1 - n))).ToList())
                            .ToList();

            var cnt = output[0].Count;

            var epsilon = new int[cnt];

            for (int n = 0; n < cnt; n++)
            {
                int pos = 0;
                for (int i = 0; i < output.Count; i++)
                {
                    pos += output[i][n];
                }

                if (pos >= 0)
                {
                    epsilon[n] = 0;
                }
                else if (pos < 0)
                {
                    epsilon[n] = 1;
                }
            }

            var epsilonOutput = new List<int[]>();

            foreach (var item in input)
            {

                if (item[detectBit] == epsilon[detectBit])
                {
                    epsilonOutput.Add(item);
                }
            }

            return epsilonOutput;
        }
    }
}
