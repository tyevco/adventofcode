using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class BuildingParser : DataParser<Building>
    {
        protected override Building DeserializeData(IList<string> data)
        {
            Building building = new Building();

            if (data.Any())
            {
                var line = data.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));

                Queue<char> roomQueue = new Queue<char>(line);

                Room firstRoom = building.FirstRoom;

                AddRooms(building, firstRoom, roomQueue);
            }

            return building;
        }

        //^WNE(E|N(E|W))$
        private void AddRooms(Building building, Room lastRoom, Queue<char> roomQueue)
        {
            bool forceReturn = false;
            while (roomQueue.Any() && !forceReturn)
            {
                var command = roomQueue.Dequeue();

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
                    AddRooms(building, nextRoom, roomQueue);
                }
            }
        }
    }
}