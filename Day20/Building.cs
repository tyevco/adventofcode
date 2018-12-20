using Advent.Utilities;
using Advent.Utilities.Data;
using System.Linq;
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
            Rooms = new Grid<Room>(150, 150);
            StartX = (Rooms.Width / 2) - 1;
            StartY = (Rooms.Height / 2) - 1;
            AddRoom(new Room(StartX, StartY, this));
        }

        public Room FirstRoom => Rooms[StartX, StartY];

        public string Id { get; set; }

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

            var rooms = Rooms.Data.Where(r => r != null);
            var left = rooms.Min(r => r.X);
            var right = rooms.Max(r => r.X);
            var top = rooms.Min(r => r.Y);
            var bottom = rooms.Max(r => r.Y);

            int padding = rooms.Max(r => r.Id.ToString().Length);

            for (int y = top; y <= bottom; y++)
            {
                int rowCount = y == bottom ? 3 : 2;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        var room = Rooms[x, y];

                        if (i == 0)
                        {
                            if (room == null)
                            {
                                sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding * 2), WALL_COLOR));
                                if (x == right)
                                {
                                    sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));
                                }
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.North))
                                {
                                    sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));
                                    sb.Append(ConsoleCodes.Colorize(Repeat("|", padding), DOOR_COLOR));
                                }
                                else
                                {
                                    sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding * 2), WALL_COLOR));
                                }

                                if (x == right)
                                {
                                    sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (room == null)
                            {
                                sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding * 2), WALL_COLOR));
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.West))
                                    sb.Append(ConsoleCodes.Colorize(Repeat("~", padding), DOOR_COLOR));
                                else
                                    sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));


                                if (room.X == StartX && room.Y == StartY)
                                    sb.Append(ConsoleCodes.Colorize(room.Id.ToString().PadLeft(padding, '0'), START_COLOR));
                                else
                                    sb.Append(ConsoleCodes.Colorize(room.Id.ToString().PadLeft(padding, '0'), ROOM_COLOR));
                            }

                            if (x == right)
                            {
                                sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));
                            }
                        }
                        else
                        {
                            sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding * 2), WALL_COLOR));

                            if (x == right)
                            {
                                sb.Append(ConsoleCodes.Colorize(Repeat(" ", padding), WALL_COLOR));
                            }
                        }
                    }

                    if (y < bottom || (y == bottom && i < 2))
                    {
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString();
        }

        private static string Repeat(string value, int count)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                b.Append(value);
            }

            return b.ToString();
        }

        public string GetLayout()
        {
            StringBuilder sb = new StringBuilder();

            var rooms = Rooms.Data.Where(r => r != null);
            var left = rooms.Min(r => r.X);
            var right = rooms.Max(r => r.X);
            var top = rooms.Min(r => r.Y);
            var bottom = rooms.Max(r => r.Y);

            for (int y = top; y <= bottom; y++)
            {
                int rowCount = y == bottom ? 3 : 2;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        var room = Rooms[x, y];

                        if (i == 0)
                        {
                            if (room == null)
                            {
                                sb.Append("##");
                                if (x == right)
                                {
                                    sb.Append("#");
                                }
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.North))
                                {
                                    sb.Append("#");
                                    sb.Append("-");
                                }
                                else
                                {
                                    sb.Append("##");
                                }

                                if (x == right)
                                {
                                    sb.Append("#");
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (room == null)
                            {
                                sb.Append("##");
                            }
                            else
                            {
                                if (room.HasDoorwayTo(Direction.West))
                                    sb.Append("|");
                                else
                                    sb.Append("#");


                                if (room.X == StartX && room.Y == StartY)
                                    sb.Append("X");
                                else
                                    sb.Append(".");
                            }

                            if (x == right)
                            {
                                sb.Append("#");
                            }
                        }
                        else
                        {
                            sb.Append("##");

                            if (x == right)
                            {
                                sb.Append("#");
                            }
                        }
                    }

                    if (y < bottom || (y == bottom && i < 2))
                    {
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString();
        }
    }
}
