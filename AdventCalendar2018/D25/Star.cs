using System;

namespace AdventCalendar2018.D25
{
    public class Star
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int W { get; private set; }

        public Constellation Constellation { get; set; } = null;

        public Star(int x, int y, int z, int w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(this.GetType()))
            {
                var other = obj as Star;
                return
                    other.X == X && other.Y == Y && other.Z == Z && other.W == W;
            }

            return false;
        }

        public int DistanceTo(Star other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z) + Math.Abs(W - other.W);
        }

        public override string ToString()
        {
            return $"[{(Constellation == null ? "  " : Constellation.Id.ToString().PadLeft(2))}] {X.ToString().PadLeft(2, ' ')},{Y.ToString().PadLeft(2, ' ')},{Z.ToString().PadLeft(2, ' ')},{W.ToString().PadLeft(2, ' ')}";
        }
    }
}