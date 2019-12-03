using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    public class Crash
    {
        public int Ticks { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public IList<Cart> Carts { get; set; } = new List<Cart>();
    }
}
