using Advent.Utilities;
using Advent.Utilities.Data;
using System.Text;

namespace Day20
{
    public class Building
    {
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

        int RoomCount = 0;
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
                                sb.Append(ConsoleCodes.Colorize("##", 1));
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.North))
                                {
                                    sb.Append($"{ConsoleCodes.Colorize("#", 1)}{ConsoleCodes.Colorize("-", 68)}");
                                } else
                                {
                                    sb.Append(ConsoleCodes.Colorize("##", 1));
                                }
                            }
                        }
                        else
                        {
                            if (room == null)
                            {
                                sb.Append(ConsoleCodes.Colorize("##", 1));
                            }
                            else
                            {
                                string roomMarker = ConsoleCodes.Colorize(".", 35);

                                if (room.X == StartX && room.Y == StartY)
                                    roomMarker = ConsoleCodes.Colorize("S", 45);

                                if (room.HasDoorwayTo(Direction.West))
                                {
                                    sb.Append(ConsoleCodes.Colorize("|", 56));
                                } else
                                {
                                    sb.Append(ConsoleCodes.Colorize("#", 1));
                                }

                                sb.Append(roomMarker);
                                //sb.Append(room.Id.ToString().PadLeft(2, '0'));
                                //else
                                //    sb.Append("+");
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
