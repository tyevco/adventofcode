using System;

namespace Day25
{
    public class Star
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int R { get; private set; }

        public Constellation Constellation { get; set; } = null;

        public Star(int x, int y, int z, int r)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(this.GetType()))
            {
                var other = obj as Star;
                return
                    other.X == X && other.Y == Y && other.Z == Z && other.R == R;
            }

            return false;
        }

        public int DistanceTo(Star other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z) + Math.Abs(R - other.R);
        }

        public override string ToString()
        {
            return $"[{(Constellation == null ? "  " : Constellation.Id.ToString().PadLeft(2))}] {X.ToString().PadLeft(2, ' ')},{Y.ToString().PadLeft(2, ' ')},{Z.ToString().PadLeft(2, ' ')},{R.ToString().PadLeft(2, ' ')}";
        }
    }
}