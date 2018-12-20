using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class Room
    {
        public Room North => Building.GetRoomAt(X, Y - 1);
        public Room South => Building.GetRoomAt(X, Y + 1);
        public Room East => Building.GetRoomAt(X - 1, Y);
        public Room West => Building.GetRoomAt(X + 1, Y);

        private IDictionary<Direction, bool> Doorways { get; } = new Dictionary<Direction, bool>()
        {
            { Direction.North, false },
            { Direction.West, false },
            { Direction.South, false },
            { Direction.East, false }
        };

        public int Y { get; }
        public int X { get; }
        private Building Building { get; }
        public int Id { get; set; }
        public bool HasSplit { get; set; }
        public int DoorCount => Doorways.Count(d => d.Value);


        public Room(int x, int y, Building building)
        {
            X = x;
            Y = y;
            Building = building;
        }

        public bool HasDoorwayTo(Direction direction)
        {
            return Doorways[direction];
        }

        public void AddDoorway(Direction direction)
        {
            Doorways[direction] = true;
        }
    }
}
