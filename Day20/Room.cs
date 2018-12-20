namespace Day20
{
    public class Room
    {
        public Room North => Building.GetRoomAt(X, Y - 1);
        public Room South => Building.GetRoomAt(X, Y + 1);
        public Room East => Building.GetRoomAt(X - 1, Y);
        public Room West => Building.GetRoomAt(X + 1, Y);

        public int Y { get; private set; }
        public int X { get; private set; }
        private Building Building { get; set; }
        public int Id { get; internal set; }

        public Room(int x, int y, Building building)
        {
            X = x;
            Y = y;
            Building = building;
        }
    }
}
