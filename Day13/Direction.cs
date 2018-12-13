namespace Day13
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        public static Direction ToDirection(this char c)
        {
            Direction dir;
            switch (c)
            {
                case 'V':
                case 'v':
                    dir = Direction.South;
                    break;
                case '>':
                    dir = Direction.East;
                    break;
                case '<':
                    dir = Direction.West;
                    break;
                case '^':
                default:
                    dir = Direction.North;
                    break;
            }

            return dir;
        }

        public static Direction GetStraight(this Direction direction)
        {
            return direction;
        }

        public static Direction GetLeft(this Direction direction)
        {
            Direction value;
            switch (direction)
            {
                default:
                case Direction.North:
                    value = Direction.West;
                    break;
                case Direction.East:
                    value = Direction.North;
                    break;
                case Direction.South:
                    value = Direction.East;
                    break;
                case Direction.West:
                    value = Direction.South;
                    break;
            }

            return value;
        }

        public static Direction GetRight(this Direction direction)
        {
            Direction value;
            switch (direction)
            {
                default:
                case Direction.North:
                    value = Direction.East;
                    break;
                case Direction.East:
                    value = Direction.South;
                    break;
                case Direction.South:
                    value = Direction.West;
                    break;
                case Direction.West:
                    value = Direction.North;
                    break;
            }

            return value;
        }
    }
}