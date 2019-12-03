namespace AdventCalendar2018.D13
{
    public enum TrackType
    {
        Curve,
        Intersection,
        Straight
    }


    public static class TrackTypeExtensions
    {
        public static TrackType ToTrackType(this char c)
        {
            TrackType type;
            switch (c)
            {
                case '\\':
                case '/':
                    type = TrackType.Curve;
                    break;
                case '+':
                    type = TrackType.Intersection;
                    break;
                case '|':
                case '-':
                default:
                    type = TrackType.Straight;
                    break;
            }

            return type;
        }
    }
}