using System;

namespace Day23
{
    public class NanoBot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int R { get; private set; }

        public NanoBot(int X, int Y, int Z, int R)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.R = R;
        }

        public int DistanceTo(int x, int y, int z)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y) + Math.Abs(Z - z);
        }

        public int DistanceTo(NanoBot other)
        {
            return DistanceTo(other.X, other.Y, other.Z);
        }

        public bool Overlap(int x, int y, int z, int r)
        {
            int distSq = (X - x) * (X - x)
                        + (Y - y) * (Y - y)
                        + (Z - z) * (Z - z);
            int radSumSq = (R + r) * (R + r);

            return distSq >= radSumSq;
        }

        public bool Overlap(NanoBot other)
        {
            return Overlap(other.X, other.Y, other.Z, other.R);
        }

        public bool Within(OctTree region)
        {
            return false;
        }
    }
}