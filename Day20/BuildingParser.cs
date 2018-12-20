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

                building.Id = line;

                Room firstRoom = building.FirstRoom;

                AddRooms(building, firstRoom, roomQueue);

                if (data.Count > 1)
                {
                    building.Expected = string.Join("\r\n", data.Skip(2).Take(data.Count - 2).Select(l=>l.Trim()));
                }
            }



            return building;
        }

        //^WNE(E|N(E|W))$
        private char AddRooms(Building building, Room lastRoom, Queue<char> roomQueue)
        {
            char lastProcessedChildRoom = ' ';
            bool forceReturn = false;
            while (roomQueue.Any() && !forceReturn)
            {
                var command = roomQueue.Dequeue();
                lastProcessedChildRoom = command;

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
                    case ')':
                        // no-op, return to parent
                        forceReturn = true;
                        break;
                    case '(':
                        lastRoom.HasSplit = true;
                        lastProcessedChildRoom = AddRooms(building, lastRoom, roomQueue);
                        break;
                    case '^':
                    case '$':
                    default:
                        lastProcessedChildRoom = AddRooms(building, lastRoom, roomQueue);
                        break;
                }

                if (nextRoom != null)
                {
                    lastProcessedChildRoom = AddRooms(building, nextRoom, roomQueue);
                }

                if (!lastRoom.HasSplit && lastProcessedChildRoom == '|')
                {
                    forceReturn = true;
                }
                else if (!lastRoom.HasSplit && lastProcessedChildRoom == ')')
                {
                    forceReturn = true;
                }
            }

            return lastProcessedChildRoom;
        }
    }
}