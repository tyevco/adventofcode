using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using Advent.Utilities.Spatial;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventCalendar2019.D15
{
    [Exercise("Day 15: Oxygen System")]
    class Y2019D15 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D15/Data");
        }

        int Count = 0;

        protected override void Execute(string file)
        {
            var gameCode = File.ReadAllText(file);
            var ship = new Ship();
            var processor = new IntcodeProcessor(gameCode);
            Compass lastDir = Compass.North;
            Compass nextDir = Compass.North;
            bool started = false;

            ship.SetTile(0, 0, ShipTile.Unknown);

            Console.Clear();
            var r = new Random();
            Timer.Monitor(() =>
            {
                Queue<long> outputs = new Queue<long>();
                processor.Output += output =>
                {
                    if (output == 0)
                    {
                        if (lastDir == Compass.North)
                        {
                            ship.SetTile(ship.DroidX, ship.DroidY - 1, ShipTile.Wall);
                        }
                        else if (lastDir == Compass.South)
                        {
                            ship.SetTile(ship.DroidX, ship.DroidY + 1, ShipTile.Wall);
                        }
                        else if (lastDir == Compass.West)
                        {
                            ship.SetTile(ship.DroidX - 1, ship.DroidY, ShipTile.Wall);
                        }
                        else if (lastDir == Compass.East)
                        {
                            ship.SetTile(ship.DroidX + 1, ship.DroidY, ShipTile.Wall);
                        }
                    }
                    else if (output == 1)
                    {
                        if (lastDir == Compass.North)
                        {
                            ship.DroidY--;
                        }
                        else if (lastDir == Compass.South)
                        {
                            ship.DroidY++;
                        }
                        else if (lastDir == Compass.West)
                        {
                            ship.DroidX--;
                        }
                        else if (lastDir == Compass.East)
                        {
                            ship.DroidX++;
                        }

                        ship.SetTile(ship.DroidX, ship.DroidY, ShipTile.Open);
                    }
                    else if (output != 2)
                    {
                        if (lastDir == Compass.North)
                        {
                            ship.SetTile(ship.DroidX, ship.DroidY - 1, ShipTile.Oxygen);
                        }
                        else if (lastDir == Compass.South)
                        {
                            ship.SetTile(ship.DroidX, ship.DroidY + 1, ShipTile.Oxygen);
                        }
                        else if (lastDir == Compass.West)
                        {
                            ship.SetTile(ship.DroidX - 1, ship.DroidY, ShipTile.Oxygen);
                        }
                        else if (lastDir == Compass.East)
                        {
                            ship.SetTile(ship.DroidX + 1, ship.DroidY, ShipTile.Oxygen);
                        }
                        return false;
                    }

                    if (Count % 50000 == 0)
                    {
                        Count = 0;
                        Console.Clear();
                        ship.DebugPrint();
                    }

                    Count++;
                    if (ship.GetTile(ship.DroidX, ship.DroidY - 1) == ShipTile.Unknown)
                    {
                        nextDir = Compass.North;
                    }
                    else if (ship.GetTile(ship.DroidX, ship.DroidY + 1) == ShipTile.Unknown)
                    {
                        nextDir = Compass.South;
                    }
                    else if (ship.GetTile(ship.DroidX - 1, ship.DroidY) == ShipTile.Unknown)
                    {
                        nextDir = Compass.West;
                    }
                    else if (ship.GetTile(ship.DroidX + 1, ship.DroidY) == ShipTile.Unknown)
                    {
                        nextDir = Compass.East;
                    }
                    else
                    {
                        nextDir = (Compass)r.Next(1, 5);
                    }
                    return true;
                };

                processor.Input += () =>
                {
                    lastDir = nextDir;

                    //  ship.DebugPrint();
                    //    System.Threading.Thread.Sleep(50);

                    //Console.WriteLine($"{(Compass)nextDir}");

                    return (long)nextDir;
                };

                processor.Process();

                ship.DebugPrint();
            });
        }
    }
}
