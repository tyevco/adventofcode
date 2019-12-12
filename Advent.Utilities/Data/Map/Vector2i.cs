using System;
using System.Diagnostics;
using Advent.Utilities.Mathematics;

namespace Advent.Utilities.Data.Map
{
    [DebuggerDisplay("Δ({Dx},{Dy})")]
    public class Vector2i
    {
        public Vector2i() { }

        public Vector2i(long dx, long dy)
        {
            long gcd = Mathematics.Mathematics.Calculate(new long[] { dx, dy });
            Dx = dx / gcd;
            Dy = dy / gcd;
        }

        public long Dx { get; set; }

        public long Dy { get; set; }

        public static bool operator ==(Vector2i first, Vector2i second)
        {
            return first.Dx == second.Dx && first.Dy == second.Dy;
        }

        public static bool operator !=(Vector2i first, Vector2i second)
        {
            return first.Dx != second.Dx || first.Dy != second.Dy;
        }

        public float Angle => 180f + (float)Math.Atan2(Dy, Dx) * (float)(180 / Math.PI);

        public override string ToString()
        {
            return $"Δ({Dx},{Dy})";
        }
    }
}
