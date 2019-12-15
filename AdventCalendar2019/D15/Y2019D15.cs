using System;
using System.Collections.Generic;
using System.IO;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using Advent.Utilities.Spatial;

namespace AdventCalendar2019.D15
{
    [Exercise("Day 15: Oxygen System")]
    class Y2019D15 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D15/Data");
        }

        protected override void Execute(string file)
        {
            var gameCode = File.ReadAllText(file);
            var ship = new Ship();
            var processor = new IntcodeProcessor(gameCode);
            Compass lastDir = Compass.North;
            Compass nextDir = Compass.North;

            int Count = 1;

            var r = new Random();
            Timer.Monitor(() =>
            {
                Queue<long> outputs = new Queue<long>();
                processor.Output += output =>
                {
                    if (output != 2)
                    {
                        if (output == 1)
                            ship.Move(lastDir);
                        else
                            ship.SetTile(lastDir);

                        var westTile = ship.GetTile(Compass.West);
                        var eastTile = ship.GetTile(Compass.East);
                        var northTile = ship.GetTile(Compass.North);
                        var southTile = ship.GetTile(Compass.South);

                        if (lastDir == Compass.North)
                        {
                            if (eastTile == ShipTile.Unknown || eastTile == ShipTile.Open)
                            {
                                nextDir = Compass.East;
                            }
                            else if (northTile == ShipTile.Wall)
                            {
                                nextDir = Compass.West;
                            }
                        }
                        else if (lastDir == Compass.South)
                        {
                            if (westTile == ShipTile.Unknown || westTile == ShipTile.Open)
                            {
                                nextDir = Compass.West;
                            }
                            else if (southTile == ShipTile.Wall)
                            {
                                nextDir = Compass.East;
                            }
                        }
                        else if (lastDir == Compass.West)
                        {
                            if (northTile == ShipTile.Unknown || northTile == ShipTile.Open)
                            {
                                nextDir = Compass.North;
                            }
                            else if (westTile == ShipTile.Wall)
                            {
                                nextDir = Compass.South;
                            }
                        }
                        else if (lastDir == Compass.East)
                        {
                            if (southTile == ShipTile.Unknown || southTile == ShipTile.Open)
                            {
                                nextDir = Compass.South;
                            }
                            else if (eastTile == ShipTile.Wall)
                            {
                                nextDir = Compass.North;
                            }
                        }

                        processor.Arguments.Add((long)nextDir);
                        lastDir = nextDir;

                        if (Count % 50000 == 0)
                        {
                            Console.Clear();
                            ship.DebugPrint();
                            System.Threading.Thread.Sleep(15);
                            Count = 1;
                        }
                        else
                        {
                            Count++;
                        }
                        return true;
                    }
                    else
                    {
                        ship.SetTile(lastDir, ShipTile.Oxygen);

                        return false;
                    }
                };

                processor.Process();

                ship.DebugPrint();
            });
        }
    }
}
