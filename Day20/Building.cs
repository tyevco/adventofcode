using Advent.Utilities;
using Advent.Utilities.Data;
using System.Text;

namespace Day20
{
    public class Building
    {
        public int RoomCount { get; private set; } = 0;
        public Grid<Room> Rooms { get; private set; }
        int StartX { get; set; }
        int StartY { get; set; }

        public Building()
        {
            Rooms = new Grid<Room>(20, 20);
            StartX = Rooms.Width / 2;
            StartY = Rooms.Height / 2;
            AddRoom(new Room(StartX, StartY, this));
        }

        public Room FirstRoom => Rooms[StartX, StartY];

        public Room GetRoomAt(int x, int y)
        {
            return Rooms[x, y];
        }

        public void AddRoom(Room room)
        {
            room.Id = RoomCount++;
            Rooms[room.X, room.Y] = room;
        }

        public Room GetOrCreateRoomAt(int x, int y)
        {
            Room room;
            if (Rooms[x, y] != null)
            {
                room = Rooms[x, y];
            }
            else
            {
                room = new Room(x, y, this);
                AddRoom(room);
            }

            return room;
        }

        private const int WALL_COLOR = 0x41;
        private const int START_COLOR = 0x2a;
        private const int DOOR_COLOR = 0x2e;
        private const int ROOM_COLOR = 0x90;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Rooms.Height; y++)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int x = 0; x < Rooms.Width; x++)
                    {
                        var room = Rooms[x, y];

                        if (i == 0)
                        {
                            if (room == null)
                            {
                                sb.Append(ConsoleCodes.Colorize("##", WALL_COLOR));
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.North))
                                {
                                    sb.Append(ConsoleCodes.Colorize("#", WALL_COLOR));
                                    sb.Append(ConsoleCodes.Colorize("-", DOOR_COLOR));
                                }
                                else
                                {
                                    sb.Append(ConsoleCodes.Colorize("##", WALL_COLOR));
                                }
                            }
                        }
                        else
                        {
                            if (room == null)
                            {
                                sb.Append(ConsoleCodes.Colorize("##", WALL_COLOR));
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.West))
                                    sb.Append(ConsoleCodes.Colorize("|", DOOR_COLOR));
                                else
                                    sb.Append(ConsoleCodes.Colorize("#", WALL_COLOR));


                                if (room.X == StartX && room.Y == StartY)
                                    sb.Append(ConsoleCodes.Colorize("S", START_COLOR));
                                else
                                    sb.Append(ConsoleCodes.Colorize(".", ROOM_COLOR));

                            }
                        }
                    }

                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}
