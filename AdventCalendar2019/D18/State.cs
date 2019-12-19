namespace AdventCalendar2019.D18
{
    public struct State
    {
        public int Obstacles { get; set; }
        public int Keys { get; set; }

        internal void Deconstruct(out int obstacles, out int keys)
        {
            obstacles = Obstacles;
            keys = Keys;
        }
    }
}
