using Advent.Utilities.Data;

namespace Day20
{
    public class Building
    {
        public Grid<Room> Rooms { get; }
        int StartX { get; }
        int StartY { get; }

        public Building()
        {
            Rooms = new Grid<Room>(1000, 1000);
            StartX = Rooms.Width / 2;
            StartY = Rooms.Height / 2;
            Rooms[StartX, StartY] = new Room()
            {
                X = StartX,
                Y = StartY
            };
        }

        public Room FirstRoom => Rooms[StartX, StartY];

        public Room AddRoom(int x, int y, Room room)
        {
            Rooms[x, y] = room;

            return room;
        }

        public Room GetRoomAt(int x, int y)
        {
            return Rooms[x, y];
        }
    }
}
