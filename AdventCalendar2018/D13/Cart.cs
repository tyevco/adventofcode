namespace Day13
{
    public class Cart
    {
        public bool IsCrashed { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public Decision NextDecision { get; set; }

        private static char ToChar(Direction dir)
        {
            switch (dir)
            {
                case Direction.East:
                    return '>';
                case Direction.South:
                    return 'V';
                case Direction.West:
                    return '<';
                case Direction.North:
                    return '^';
            }

            return ' ';
        }

        public override string ToString()
        {
            return $"{ToChar(Direction)}";
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
