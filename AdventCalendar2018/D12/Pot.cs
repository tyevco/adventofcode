namespace AdventCalendar2018.D12
{
    public class Pot
    {
        public bool HasPlant { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return HasPlant ? "#" : ".";
        }
    }
}