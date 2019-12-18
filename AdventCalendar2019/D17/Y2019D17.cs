using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Intcode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Advent.Utilities.Intcode.IntcodeProcessor;

namespace AdventCalendar2019.D17
{
    [Exercise("Day 17: Set and Forget")]
    class Y2019D17 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D17/Data");
        }

        protected override void Execute(string file)
        {
            bool part2 = false;
            var programData = File.ReadAllText(file);
            var processor = new IntcodeProcessor(programData);
            Scaffolding scaf = new Scaffolding();
            Timer.Monitor(() =>
            {
                int x = 0;
                int y = 0;
                OnOutput outputMethod = output =>
                {
                    if (output > 128)
                    {
                        Console.WriteLine(output);
                    }
                    else
                    {
                        if (output == 10)
                        {
                            x = 0;
                            y++;
                        }
                        else
                        {
                            scaf.SetTile(x, y, (char)output);
                            x++;
                        }
                        if (part2)
                        {
                            // Console.Clear();
                            //  scaf.DebugPrint();
                        }
                    }
                    return true;
                };
                processor.Output += outputMethod;

                processor.Process();

                scaf.DebugPrint();

                IList<Point<char>> points = new List<Point<char>>();

                for (x = 0; x < scaf.Width; x++)
                {
                    for (y = 0; y < scaf.Height; y++)
                    {
                        var tile = scaf.GetTile(x, y);
                        if (tile == (char)Tile.Scaffold)
                        {
                            if (scaf.GetTile(x - 1, y) == (char)Tile.Scaffold
                                && scaf.GetTile(x + 1, y) == (char)Tile.Scaffold
                                && scaf.GetTile(x, y - 1) == (char)Tile.Scaffold
                                && scaf.GetTile(x, y + 1) == (char)Tile.Scaffold)
                            {
                                points.Add(scaf[x, y]);
                            }
                        }
                    }
                }

                Console.WriteLine(points.Count);
                Console.WriteLine("Calibration " + points.Select(p => p.X * p.Y).Sum());

                processor.WriteValue(0, 2);

                var movePattern = "A,B,A,C,C,A,B,C,B,B";
                var functionA = "L,8,R,10,L,8,R,8";
                var functionB = "L,12,R,8,R,8";
                var functionC = "L,8,R,6,R,6,R,10,L,8";

                foreach (var mp in movePattern)
                {
                    processor.Arguments.Add((long)mp);
                }
                processor.Arguments.Add(10);

                foreach (var mp in functionA)
                {
                    processor.Arguments.Add((long)mp);
                }
                processor.Arguments.Add(10);


                foreach (var mp in functionB)
                {
                    processor.Arguments.Add((long)mp);
                }
                processor.Arguments.Add(10);


                foreach (var mp in functionC)
                {
                    processor.Arguments.Add((long)mp);
                }
                processor.Arguments.Add(10);

                processor.Arguments.Add(121);
                processor.Arguments.Add(10);
                y++;
                x = 0;
                Console.WriteLine();
                part2 = true;
                // Debug.EnableDebugOutput = true;
                //    scaf.DebugPrint();
                //  processor.Output -= outputMethod;
                processor.Process();
            });
        }
    }
}
