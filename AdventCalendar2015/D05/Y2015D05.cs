using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2015.D05
{
    [Exercise("Day 5: Doesn't He Have Intern-Elves For This?")]
    class Y2015D05 : FileSelectionConsole
    {
        private bool PerformPart2 = true;

        public void Execute()
        {
            Start("D05/Data");
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>
            {
                new ConsoleOption{
                    Text = "Perform Part 2",
                    Enabled = () => PerformPart2,
                    Handler = () => PerformPart2 = !PerformPart2,
                }
            };
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);
            if (!PerformPart2)
            {
                Console.WriteLine("First Try:");

                var regexVowels = new Regex(@"[aeiou]");
                var regexDouble = new Regex(@"(aa|bb|cc|dd|ee|ff|gg|hh|ii|jj|kk|ll|mm|nn|oo|pp|qq|rr|ss|tt|uu|vv|ww|xx|yy|zz)");
                var regexNoMatch = new Regex(@"(ab|cd|pq|xy)");

                TestExpressions(lines, value => !regexNoMatch.IsMatch(value) && regexDouble.IsMatch(value) && regexVowels.Matches(value).Count > 2);
            }
            else
            {
                Console.WriteLine("Second Try:");

                int a = (int)'a';
                StringBuilder builder = new StringBuilder();
                builder.Append("(");
                for (int i = 0; i < 26; i++)
                {
                    if (i != 0)
                        builder.Append("|");

                    char ci = (char)(a + i);
                    builder.Append($"{ci}[a-z]{ci}");
                }
                builder.Append(")");

                Console.WriteLine(builder.ToString());
                var doubleWithInbetween = new Regex(builder.ToString());

                builder = new StringBuilder();
                builder.Append("(");
                for (int i = 0; i < 26; i++)
                {
                    if (i != 0)
                        builder.Append("|");


                    char ci = (char)(a + i);

                    for (int j = 0; j < 26; j++)
                    {
                        if (j != 0)
                            builder.Append("|");

                        char cj = (char)(a + j);

                        builder.Append($"{ci}{cj}[a-z]*{ci}{cj}");
                    }
                }
                builder.Append(")");

                Console.WriteLine(builder.ToString());
                var twoLettersWithoutOverlap = new Regex(builder.ToString());

                TestExpressions(lines, value => doubleWithInbetween.IsMatch(value) && twoLettersWithoutOverlap.IsMatch(value));
            }
        }

        private void TestExpressions(string[] lines, Predicate<string> expr)
        {
            int niceStrings = 0;

            foreach (var line in lines)
            {
                var sections = line.Split("=>");

                int check = -1;
                int match = 0;
                if (sections.Length > 1)
                {
                    check = int.Parse(sections[1]);
                }

                var value = sections[0];
                if (expr(value))
                {
                    niceStrings++;
                    match = 1;
                }

                if (check >= 0)
                {
                    Console.WriteLine($"{value} is {(match == 1 ? "nice" : "naughty")}, expected {(check == 1 ? "nice" : "naughty")} [{check == match}]");
                }
            }

            Console.WriteLine($"{niceStrings} nice strings.");
        }
    }
}
