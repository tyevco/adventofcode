using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventCalendar2020.D07
{
    [Exercise("Day 7: ")]
    class Y2020D07 : FileSelectionParsingConsole<IDictionary<string, IDictionary<string, int>>>, IExercise
    {
        public void Execute()
        {
            Start("D07/Data");
        }

        protected override IDictionary<string, IDictionary<string, int>> DeserializeData(IList<string> data)
        {
            IDictionary<string, IDictionary<string, int>> rules = new Dictionary<string, IDictionary<string, int>>();

            Regex containsRule = new Regex("([0-9]+) (.+) bags?");
            foreach (var rule in data)
            {
                var split = rule.Split(" contain ");
                var color = split[0].Substring(0, split[0].Length - 5);

                IDictionary<string, int> colorRules = new Dictionary<string, int>();
                var contains = split[1].Split(", ", System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var contain in contains)
                {
                    var trimmed = contain.Trim('.', ' ');
                    if (trimmed.StartsWith("no other", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        colorRules.Add("none", 0);
                    }
                    else
                    {
                        if (containsRule.IsMatch(trimmed))
                        {
                            var match = containsRule.Match(trimmed);
                            colorRules.Add(match.Groups[2].Value, int.Parse(match.Groups[1].Value));
                        }
                        else
                        {
                            Console.WriteLine("there's a problem");
                        }
                    }
                }

                rules.Add(color, colorRules);
            }

            return rules;
        }

        protected override void Execute(IDictionary<string, IDictionary<string, int>> data)
        {
            var myBag = "shiny gold";
            int total = 0;
            CanContain(data, myBag, ref total);

            int count = 0;
            Count(data, myBag, ref count);

            AnswerPartOne(total);
            AnswerPartTwo(count);
        }

        private void CanContain(IDictionary<string, IDictionary<string, int>> data, string bag, ref int total, ISet<string> searched = null)
        {
            if (searched == null)
            {
                searched = new HashSet<string>();
            }

            foreach (var rule in data)
            {
                if (rule.Value.ContainsKey(bag) && !searched.Contains(rule.Key))
                {
                    searched.Add(rule.Key);
                    total++;

                    CanContain(data, rule.Key, ref total, searched);
                }
            }
        }

        private void Count(IDictionary<string, IDictionary<string, int>> data, string bag, ref int total, ISet<string> searched = null)
        {
            if (searched == null)
            {
                searched = new HashSet<string>();
            }

            if (bag != "none")
            {
                var bagRule = data[bag];

                foreach (var contains in bagRule)
                {
                    int childTotal = 0;

                    total += contains.Value;

                    Count(data, contains.Key, ref childTotal, searched);

                    total += (contains.Value * childTotal);
                }
            }
        }
    }
}
