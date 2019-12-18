using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Intcode;
using Advent.Utilities.Spatial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var ship = new Scaffolding();
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
                    if (output == 1)
                        ship.SetTile(lastDir, ShipTile.Open);
                    else if (output == 2)
                        ship.SetTile(lastDir, ShipTile.Oxygen);
                    else
                        ship.SetTile(lastDir);

                    if (output != 0)
                        ship.Move(lastDir);

                    var westTile = ship.GetTile(Compass.West);
                    var eastTile = ship.GetTile(Compass.East);
                    var northTile = ship.GetTile(Compass.North);
                    var southTile = ship.GetTile(Compass.South);

                    if (lastDir == Compass.North)
                    {
                        if (eastTile == ShipTile.Unknown || eastTile == ShipTile.Open || eastTile == ShipTile.Oxygen)
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
                        if (westTile == ShipTile.Unknown || westTile == ShipTile.Open || westTile == ShipTile.Oxygen)
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
                        if (northTile == ShipTile.Unknown || northTile == ShipTile.Open || northTile == ShipTile.Oxygen)
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
                        if (southTile == ShipTile.Unknown || southTile == ShipTile.Open || southTile == ShipTile.Oxygen)
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

                    Count++;

                    return Count < 1000000;
                };

                processor.Process();

                var oxygenPoint = ship.Points.FirstOrDefault(p => ((ShipTile)p.Value.Data) == ShipTile.Oxygen);

                ship.DebugPrint();

                var path = Pathfinding.FindTargetPoint(ship, 0, 0, oxygenPoint.Value.X, oxygenPoint.Value.Y, tile =>
                {
                    return tile == ShipTile.Open || tile == ShipTile.Oxygen;
                });


                Console.WriteLine($"{path.Data + 1} steps to oxygen tank.");
            });
        }
    }
}
