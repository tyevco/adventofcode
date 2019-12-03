namespace AdventCalendar2018.D15
{
    public class Entity
    {
        public EntityType Type { get; set; }

        public string Id { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int Health { get; set; } = 200;
        public int Attack { get; set; } = 3;

        public Entity(int x, int y, EntityType type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }

        public override string ToString()
        {
            return Health > 0 ?
                "\x1b[38;5;" + (Type == EntityType.Elf ? "82" : "160") + "m" + (Type == EntityType.Elf ? "E" : "G") + Id + "\x1b[38;5;255m" :
                "   ";
        }
    }
}
