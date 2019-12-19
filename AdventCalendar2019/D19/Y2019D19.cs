using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using System;
using System.IO;
using static Advent.Utilities.Intcode.IntcodeProcessor;

namespace AdventCalendar2019.D19
{
    [Exercise("Day 19: Tractor Beam")]
    class Y2019D19 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D19/Data");
        }

        protected override void Execute(string file)
        {
            var programData = File.ReadAllText(file);
            var processor = new IntcodeProcessor(programData);
            Space space = new Space();
            int width = 50;
            int height = 50;

            Timer.Monitor(() =>
            {
                long lastOutput = 0;
                OnOutput outputMethod = output =>
                {
                    lastOutput = output;

                    return false;
                };
                processor.Output += outputMethod;
                bool foundWidth = false;
                bool foundHeight = false;
                int y = 2000;
                int squareSize = 100;
                int adjSize = squareSize - 1;
                int cX = 0;
                int cY = 0;
                int minX = 1500;
                while (!foundHeight)
                {
                    lastOutput = -1;
                    int x = minX;
                    bool seekX = true;
                    while (seekX)
                    {
                        lastOutput = -1;
                        processor.Reset();
                        processor.Arguments.Add(x);
                        processor.Arguments.Add(y);
                        processor.Process();

                        space.SetTile(x, y, lastOutput);

                        if (lastOutput == 1 && seekX)
                        {
                            seekX = false;
                            cX = x;
                            minX = x - 2;

                            lastOutput = -1;
                            processor.Reset();
                            processor.Arguments.Add(x + adjSize);
                            processor.Arguments.Add(y);
                            processor.Process();

                            space.SetTile(x + adjSize, y, lastOutput);

                            if (lastOutput == 1)
                            {
                                foundWidth = true;
                            }
                        }
                        else
                        {
                            x++;
                        }
                    }

                    if (foundWidth)
                    {
                        lastOutput = -1;
                        processor.Reset();
                        processor.Arguments.Add(x);
                        processor.Arguments.Add(y - adjSize);
                        processor.Process();

                        space.SetTile(x, y - adjSize, lastOutput);

                        long nw = lastOutput;

                        lastOutput = -1;
                        processor.Reset();
                        processor.Arguments.Add(x + adjSize);
                        processor.Arguments.Add(y - adjSize);
                        processor.Process();

                        space.SetTile(x + adjSize, y - adjSize, lastOutput);

                        long ne = lastOutput;

                        if (ne == 1 && nw == 1)
                        {
                            foundHeight = true;
                            cY = y - adjSize;
                        }
                    }

                    Debug.Write($"{space[x, y - adjSize]} - ");
                    Debug.WriteLine($"{space[x + adjSize, y - adjSize]} ");
                    Debug.Write($"{space[x, y]} - ");
                    Debug.Write($"{space[x + adjSize, y]} ");
                    Debug.WriteLine($" :: {foundWidth} {foundHeight}");
                    Debug.WriteLine();
                    y++;
                }

                Console.WriteLine($"Square of {squareSize} located at {cX},{cY}");
                Console.WriteLine("Answer: " + (cX * 10000 + cY));

            });
        }
    }
}
