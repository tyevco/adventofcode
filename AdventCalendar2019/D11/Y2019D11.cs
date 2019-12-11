using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Intcode;

namespace AdventCalendar2019.D11
{
    [Exercise("Day 11: Space Police")]
    class Y2019D11 : FileSelectionConsole, IExercise
    {
        public bool RunSanityCheck { get; set; } = false;

        public void Execute()
        {
            if (RunSanityCheck)
            {
                SanityCheck();
            }
            else
            {
                Execute("D11/Data/Data.txt");
            }
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption> {
                new ConsoleOption {
                    Text = "Run Sanity Check",
                    Handler = () => RunSanityCheck = !RunSanityCheck,
                    Enabled = () => RunSanityCheck
                }
            };
        }

        protected void SanityCheck()
        {
            var robot = new PaintRobot();
            robot.DebugPrint();
            Console.WriteLine();

            robot.Operate(1, 0);
            robot.DebugPrint();
            Console.WriteLine();

            robot.Operate(0, 0);
            robot.DebugPrint();
            Console.WriteLine();

            robot.Operate(1, 0);
            robot.Operate(1, 0);
            robot.DebugPrint();
            Console.WriteLine();

            robot.Operate(0, 1);
            robot.Operate(1, 0);
            robot.Operate(1, 0);

            PaintRobot.PrintGrid(robot.Points, robot.X, robot.Y, robot.Direction);
            Console.WriteLine();

            Console.WriteLine($"{robot.Points.Count} unique panels painted.");
        }

        protected override void Execute(string file)
        {
            var intcodeData = File.ReadAllText(file);

            var robot = new PaintRobot();
            robot.DebugPrint();

            int totalWhite = 0, totalBlack = 0;

            Timer.Monitor(() =>
            {
                IntcodeProcessor processor = new IntcodeProcessor(intcodeData);

                long lastOutput = -1;
                processor.Output += output =>
                {
                    lastOutput = output;
                    return false;
                };

                while (!processor.Halted)
                {
                    processor.Process();
                    long paintColor = lastOutput;
                    processor.Process();
                    long turnDir = lastOutput;

                    if (paintColor == 1)
                    {
                        totalWhite++;
                    }
                    else
                    {
                        totalBlack++;
                    }

                    processor.Arguments.Add(robot.Operate(paintColor, turnDir));
                }
            });

            PaintRobot.PrintGrid(robot.Points, robot.X, robot.Y, robot.Direction);

            Console.WriteLine($"{robot.Points.Count} unique panels painted. {totalWhite} {totalBlack}");
        }
    }
}
