using System.Collections.Generic;

namespace Day20
{
    public class Room
    {
        public Room Entrance { get; private set; }
        public IDictionary<Direction, Room> Exits { get; } = new Dictionary<Direction, Room>();

        public void SetEntrance(Room room)
        {
            this.Entrance = room;
        }

        public void SetExit(Direction direction, Room room)
        {
            Exits.Add(direction, room);
        }
    }
}
