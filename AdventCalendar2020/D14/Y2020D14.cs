using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar2020.D14
{
    [Exercise("Day 14: Docking Data")]
    class Y2020D14 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D14/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        Regex MaskRegex = new Regex("mask = ([01X]+)");
        Regex MemRegex = new Regex(@"mem\[([0-9]+)\] = ([0-9]+)");

        protected override void Execute(IList<string> data)
        {
            PartOne(data);
            PartTwo(data);
        }

        private long ApplyMask(long value, int[] mask)
        {
            Debug.WriteLine($"Pre mask: {Convert.ToString(value, 2)}");
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 1)
                {
                    value |= (long)Math.Pow(2, i);
                }
                else if (mask[i] == 0)
                {
                    value &= ~(long)Math.Pow(2, i);
                }
            }

            Debug.WriteLine($"Post mask: {Convert.ToString(value, 2)}");

            return value;
        }

        protected void PartOne(IList<string> data)
        {
            int[] currentMask = new int[36]
            {
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
            };

            IDictionary<int, long> memory = new Dictionary<int, long>();

            foreach (var line in data)
            {
                if (MaskRegex.IsMatch(line))
                {
                    var maskText = MaskRegex.Match(line).Groups[1].Value;

                    for (int i = 0; i < maskText.Length; i++)
                    {
                        var d = maskText[i];

                        if (d == 'X')
                        {
                            currentMask[maskText.Length - 1 - i] = -1;
                        }
                        else
                        {

                            currentMask[maskText.Length - 1 - i] = int.Parse(d.ToString());
                        }
                    }
                }
                else if (MemRegex.IsMatch(line))
                {
                    var match = MemRegex.Match(line);

                    var memLoc = int.Parse(match.Groups[1].Value);
                    var memVal = long.Parse(match.Groups[2].Value);

                    var outVal = ApplyMask(memVal, currentMask);

                    memory[memLoc] = outVal;
                }
            }

            AnswerPartOne(memory.Sum(x => x.Value));
        }


        protected void PartTwo(IList<string> data)
        {
            int[] currentMask = new int[36]
            {
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
            };

            IDictionary<long, long> memory = new Dictionary<long, long>();

            foreach (var line in data)
            {
                if (MaskRegex.IsMatch(line))
                {
                    var maskText = MaskRegex.Match(line).Groups[1].Value;

                    for (int i = 0; i < maskText.Length; i++)
                    {
                        var d = maskText[i];

                        if (d == 'X')
                        {
                            currentMask[i] = -1;
                        }
                        else
                        {

                            currentMask[i] = int.Parse(d.ToString());
                        }
                    }
                }
                else if (MemRegex.IsMatch(line))
                {
                    var match = MemRegex.Match(line);

                    var memLoc = int.Parse(match.Groups[1].Value);
                    var memVal = long.Parse(match.Groups[2].Value);

                    var outLoc = ApplyMemLocMask(memLoc, currentMask);

                    foreach (var loc in outLoc)
                    {
                        memory[loc] = memVal;
                    }
                }
            }

            AnswerPartTwo(memory.Sum(x => x.Value));
        }

        private long[] ApplyMemLocMask(long value, int[] mask)
        {
            Debug.WriteLine($"Pre mask:  {Convert.ToString(value, 2).PadLeft(36, '0')}");
            char[] outVal = new char[36];

            var valueBits = Convert.ToString(value, 2)
                                .PadLeft(36, '0')
                                .Select(x => int.Parse(x.ToString()))
                                .ToArray();

            for (int i = 0; i < valueBits.Length; i++)
            {
                if (mask[i] == 1)
                {
                    outVal[i] = '1';
                }
                else if (mask[i] == 0)
                {
                    outVal[i] = valueBits[i] == 0 ? '0' : '1';
                }
                else
                {
                    outVal[i] = 'X';
                }
            }

            Debug.WriteLine($"Post mask: {string.Join("", outVal).PadLeft(36, '0')}");

            var built = new List<long>();
            BuildMemLocs(outVal, built, 0, string.Empty);

            return built.ToArray();
        }

        private void BuildMemLocs(char[] remainingBits, List<long> built, int i, string loc)
        {
            if (i < remainingBits.Length)
            {
                if (remainingBits[i] == 'X')
                {
                    BuildMemLocs(remainingBits, built, i + 1, $"{loc}0");
                    BuildMemLocs(remainingBits, built, i + 1, $"{loc}1");
                }
                else
                {
                    BuildMemLocs(remainingBits, built, i + 1, $"{loc}{remainingBits[i]}");
                }
            }
            else
            {
                built.Add(Convert.ToInt64(loc, 2));
            }
        }
    }
}
