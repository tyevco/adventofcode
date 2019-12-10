using System;
using System.Diagnostics;
using Advent.Utilities.Mathematics;

namespace Advent.Utilities.Data.Map
{
    [DebuggerDisplay("Δ({Dx},{Dy})")]
    public class Vector2i
    {
        public Vector2i() { }

        public Vector2i(int dx, int dy)
        {
            int gcd = GCD.Calculate(new int[] { dx, dy });
            Dx = dx / gcd;
            Dy = dy / gcd;
        }

        public int Dx { get; set; }

        public int Dy { get; set; }

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
