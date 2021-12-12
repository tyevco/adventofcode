using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2021.D10
{
    [Exercise("Day 10: Syntax Scoring")]
    class Y2021D10 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D10/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            Timer.Monitor("Part 1", () =>
            {
                int score = 0;
                foreach (var line in data)
                {
                    var invalidChar = '0';

                    var chunks = new Stack<char>();
                    foreach (var c in line)
                    {
                        if (chunks.Count == 0 || c == '(' || c == '[' || c == '{' || c == '<')
                        {
                            chunks.Push(c);
                        }
                        else if (c == ')')
                        {
                            if (chunks.Peek() == '(')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalidChar = ')';
                                break;
                            }
                        }
                        else if (c == ']')
                        {
                            if (chunks.Peek() == '[')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalidChar = ']';
                                break;
                            }
                        }
                        else if (c == '}')
                        {
                            if (chunks.Peek() == '{')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalidChar = '}';
                                break;
                            }
                        }
                        else if (c == '>')
                        {
                            if (chunks.Peek() == '<')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalidChar = '>';
                                break;
                            }
                        }
                    }

                    score += GetValue(invalidChar);
                }

                AnswerPartOne(score);
            });

            Timer.Monitor("Part 2", () =>
            {
                List<long> scores = new List<long>();
                foreach (var line in data)
                {
                    long score = 0;
                    bool invalid = false;

                    var chunks = new Stack<char>();
                    foreach (var c in line)
                    {
                        if (chunks.Count == 0 || c == '(' || c == '[' || c == '{' || c == '<')
                        {
                            chunks.Push(c);
                        }
                        else if (c == ')')
                        {
                            if (chunks.Peek() == '(')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalid = true;
                                break;
                            }
                        }
                        else if (c == ']')
                        {
                            if (chunks.Peek() == '[')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalid = true;
                                break;
                            }
                        }
                        else if (c == '}')
                        {
                            if (chunks.Peek() == '{')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalid = true;
                                break;
                            }
                        }
                        else if (c == '>')
                        {
                            if (chunks.Peek() == '<')
                            {
                                chunks.Pop();
                            }
                            else
                            {
                                invalid = true;
                                break;
                            }
                        }
                    }

                    if (!invalid)
                    {
                        // close out the lines
                        while (chunks.TryPeek(out char c))
                        {
                            if (c == '(')
                            {
                                score = (score * 5) + GetScore(')');

                            }
                            else if (c == '[')
                            {
                                score = (score * 5) + GetScore(']');

                            }
                            else if (c == '{')
                            {
                                score = (score * 5) + GetScore('}');

                            }
                            else if (c == '<')
                            {
                                score = (score * 5) + GetScore('>');

                            }

                            chunks.Pop();
                        }

                        scores.Add(score);
                    }
                }

                int scoreIndex = scores.Count / 2;

                AnswerPartTwo(scores.OrderBy(x=> x).ToList()[scoreIndex]);
            });
        }

        private int GetValue(char c)
        {
            switch (c)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
            }

            return 0;
        }

        private int GetScore(char c)
        {
            switch (c)
            {
                case ')': return 1;
                case ']': return 2;
                case '}': return 3;
                case '>': return 4;
            }

            return 0;
        }
    }
}
