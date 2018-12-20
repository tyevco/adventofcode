using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class BuildingParser : DataParser<(Building, ExpectedResults)>
    {
        protected override (Building, ExpectedResults) DeserializeData(IList<string> data)
        {
            Building building = new Building();
            ExpectedResults results = null;

            if (data.Any())
            {
                var line = data.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));

                Queue<char> roomQueue = new Queue<char>(line);

                building.Id = line;

                Room firstRoom = building.FirstRoom;

                AddRooms(building, firstRoom, roomQueue);

                if (data.Count > 1)
                {
                    results = new ExpectedResults
                    {
                        Building = string.Join("\r\n", data.Skip(2).Take(data.Count - 4).Select(l => l.Trim())),
                        Doors = int.Parse(data.FirstOrDefault(s=>s.StartsWith("Doors:"))?.Substring(6) ?? "0")
                    };
                }
            }



            return (building, results);
        }

        //^WNE(E|N(E|W))$
        private void AddRooms(Building building, Room parentRoom, Queue<char> roomQueue)
        {
            bool forceReturn = false;
            Room lastRoom = parentRoom;
            while (roomQueue.Any() && !forceReturn)
            {
                var command = roomQueue.Dequeue();

                Console.WriteLine(building);

                Console.WriteLine();
                Console.WriteLine($"Managing {command}.");

                Room nextRoom = null;

                switch (command)
                {
                    case 'N':
                        nextRoom = building.GetOrCreateRoomAt(lastRoom.X, lastRoom.Y - 1);
                        lastRoom.AddDoorway(Direction.North);
                        nextRoom.AddDoorway(Direction.South);
                        break;
                    case 'E':
                        nextRoom = building.GetOrCreateRoomAt(lastRoom.X + 1, lastRoom.Y);
                        lastRoom.AddDoorway(Direction.East);
                        nextRoom.AddDoorway(Direction.West);
                        break;
                    case 'S':
                        nextRoom = building.GetOrCreateRoomAt(lastRoom.X, lastRoom.Y + 1);
                        lastRoom.AddDoorway(Direction.South);
                        nextRoom.AddDoorway(Direction.North);
                        break;
                    case 'W':
                        nextRoom = building.GetOrCreateRoomAt(lastRoom.X - 1, lastRoom.Y);
                        lastRoom.AddDoorway(Direction.West);
                        nextRoom.AddDoorway(Direction.East);
                        break;
                    case '|':
                        var nextChar = roomQueue.Peek();
                        if (nextChar == ')')
                        {
                            roomQueue.Dequeue();
                        }
                        // no-op, return to parent
                        forceReturn = true;
                        break;
                    case ')':
                        // no-op, return to parent
                        forceReturn = true;
                        break;
                    case '(':
                    case '^':
                    case '$':
                    default:
                        AddRooms(building, lastRoom, roomQueue);
                        break;
                }

                if (nextRoom != null)
                {
                    lastRoom = nextRoom;
                }
            }
        }
    }
}