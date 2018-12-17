using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    public class Entity
    {
        public EntityType Type { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Entity(int x, int y, EntityType type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }
    }
}
