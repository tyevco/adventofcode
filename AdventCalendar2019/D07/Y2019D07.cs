using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventCalendar2019.D07
{
    [Exercise("Day 7: Amplification Circuit")]
    class Y2019D07 : FileSelectionConsole, IExercise
    {
        private bool FeedbackMode { get; set; } = false;

        public void Execute()
        {
            Start("D07/Data");
        }

        IList<int[]> Results = new List<int[]>();

        protected override void Execute(string file)
        {
            Results.Clear();

            var intcodeData = File.ReadAllText(file);

            int highestSignal = -1;
            int[] highestPhase = null;

            Timer.Monitor(() =>
            {
                if (!FeedbackMode)
                {
                    permute(new int[] { 0, 1, 2, 3, 4 }, 0, 4);
                }
                else
                {
                    permute(new int[] { 5, 6, 7, 8, 9 }, 0, 4);
                }

                foreach (var phases in Results)
                {
                    int lastOutput = -1;
                    IList<(int, IntcodeProcessor)> processors = new List<(int, IntcodeProcessor)>();

                    Console.WriteLine($"Phase: {string.Join(",", phases)}");
                    foreach (var phase in phases)
                    {
                        IntcodeProcessor processor = new IntcodeProcessor(intcodeData);
                        processors.Add((phase, processor));
                    }
                    
                    for (int i = 0; i < processors.Count; i++)
                    {
                        var processor = processors[i];
                        (int, IntcodeProcessor) nextProcessor;

                        if (i == processors.Count - 1)
                        {
                            nextProcessor = processors[0];
                        }
                        else
                        {
                            nextProcessor = processors[i + 1];
                        }

                        if (FeedbackMode)
                        {
                            processor.Item2.Arguments.Add(processor.Item1);
                            processor.Item2.Output += (output) =>
                            {
                                lastOutput = output;
                                nextProcessor.Item2.Arguments.Add(output);

                                return false;
                            };
                        }
                        else
                        {
                            if (i == processors.Count - 1)
                            {
                                processor.Item2.Arguments.Add(processor.Item1);
                                processor.Item2.Output += output =>
                                {
                                    lastOutput = output;
                                    return true;
                                };
                            }
                            else
                            {
                                processor.Item2.Arguments.Add(processor.Item1);
                                processor.Item2.Output += (output) =>
                                {
                                    nextProcessor.Item2.Arguments.Add(output);

                                    return true;
                                };
                            }
                        }
                    }

                    processors[0].Item2.Arguments.Add(0);

                    bool keepRunning = true;
                    while (keepRunning)
                    {
                        for (int i = 0; i < processors.Count; i++)
                        {
                            processors[i].Item2.Process();

                            if (i == processors.Count - 1)
                                keepRunning = !processors[i].Item2.Halted;
                        }
                    }


                    if (highestSignal < lastOutput)
                    {
                        highestSignal = lastOutput;
                        highestPhase = phases;
                        Console.WriteLine($"Last output higher than previous: {lastOutput} [Phase: {string.Join(",", phases)}]");
                    }

                    Console.WriteLine($"Highest signal detected from [{(highestPhase == null ? "ERROR" : string.Join(", ", highestPhase))}] of {highestSignal}");
                }
            });
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>
            {
                new ConsoleOption
                {
                    Text = "Feedback Mode",
                    Enabled = () => FeedbackMode,
                    Handler = () => FeedbackMode = !FeedbackMode,
                },
            };
        }

        private void permute(int[] str,
                                int l, int r)
        {
            if (l == r)
                Results.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
        }

        /** 
        * Swap Characters at position 
        * @param a string value 
        * @param i position 1 
        * @param j position 2 
        * @return swapped string 
        */
        public static int[] swap(int[] a,
                                  int i, int j)
        {
            int temp;
            int[] charArray = (int[])a.Clone();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            return charArray;
        }
    }
}
