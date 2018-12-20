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
            //^WNE$
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

        private void AddRooms(Building building, Room lastRoom, Queue<char> roomQueue)
        {
            if (roomQueue.Any())
            {
                var command = roomQueue.Dequeue();

                Console.WriteLine($"Managing {command}.");

                switch (command)
                {
                    case '^':
                    case '$':
                    default:
                        AddRooms(building, lastRoom, roomQueue);
                        break;
                }

            }
        }
    }
}