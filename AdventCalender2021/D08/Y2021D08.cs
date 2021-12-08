using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D08
{
    [Exercise("Day 8: Seven Segment Search")]
    class Y2021D08 : FileSelectionParsingConsole<IList<(string[] patterns, string[] output)>>, IExercise
    {
        public void Execute()
        {
            Start("D08/Data");
        }

        protected override IList<(string[] patterns, string[] output)> DeserializeData(IList<string> data)
        {
            var output = new List<(string[] patterns, string[] output)>();
            foreach (var line in data)
            {
                var input = line.Split(" | ", StringSplitOptions.RemoveEmptyEntries);

                ;

                output.Add((input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries), input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)));
            };

            return output;
        }

        protected override void Execute(IList<(string[] patterns, string[] output)> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                var outputs = data.SelectMany(i => i.output);
                var easyDigits = outputs.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);

                AnswerPartOne(easyDigits.Count());
            });

            Timer.Monitor("Part 2", () =>
            {
                int total = 0;

                foreach (var entry in data)
                {
                    string[] mappings = new string[10];

                    // map the known patterns.
                    foreach (var pattern in entry.patterns.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7))
                    {
                        switch (pattern.Length)
                        {
                            case 2:
                                mappings[1] = pattern;
                                break;
                            case 3:
                                mappings[7] = pattern;
                                break;
                            case 4:
                                mappings[4] = pattern;
                                break;
                            case 7:
                                mappings[8] = pattern;
                                break;
                        }
                    }

                    // deduce the unknown 6 segments
                    foreach (var pattern in entry.patterns.Where(x => x.Length == 6))
                    {
                        // if pattern has only one of the segments from "1", then it must be 6.
                        if (!mappings[1].All(x => pattern.Contains(x)))
                        {
                            mappings[6] = pattern;
                        }
                        // if the pattern has all the segments of "4", then it is a 9.
                        else if (mappings[4].All(x => pattern.Contains(x)))
                        {
                            mappings[9] = pattern;
                        }
                        // otherwise it is 0
                        else
                        {
                            mappings[0] = pattern;
                        }
                    }

                    // deduce the unknown 5 segments
                    foreach (var pattern in entry.patterns.Where(x => x.Length == 5))
                    {
                        // if pattern has both segments as "1", then it must be 3.
                        if (mappings[1].All(x => pattern.Contains(x)))
                        {
                            mappings[3] = pattern;
                        }
                        // if the pattern has an overlap as "9", then it is 5
                        else if (pattern.All(x => mappings[9].Contains(x)))
                        {
                            mappings[5] = pattern;
                        }
                        // otherwise it is a 2.
                        else
                        {
                            mappings[2] = pattern;
                        }
                    }

                    //for (var index = 0; index < mappings.Length; index++)
                    //{
                    //    Console.WriteLine($"{index} : {mappings[index]}");
                    //}

                    int i = 0;
                    var dict = mappings.ToDictionary(x => x, x => i++);

                    var display = entry.output.Select(output =>
                    {
                        var item = dict.Where(x => x.Key.Length == output.Length && x.Key.All(k => output.Contains(k)));
                        if (item != null && item.Any())
                        {
                            var mapping = item.FirstOrDefault();
                            return mapping.Value;
                        }
                        else
                        {
                            throw new Exception($"{output} not found.");
                        }
                    });

                    total += int.Parse(string.Join("", display));
                }

                AnswerPartTwo(total);
            });
        }
    }
}
