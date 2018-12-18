using System;
using System.Text.RegularExpressions;

namespace Day03
{
    public class FabricClaim
    {
        Regex claimRegex = new Regex("#(.+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)");

        public string Id { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public FabricClaim(string claimString)
        {
            var match = claimRegex.Match(claimString);

            Id = match.Groups[1].Value;
            X = int.Parse(match.Groups[2].Value);
            Y = int.Parse(match.Groups[3].Value);
            Width = int.Parse(match.Groups[4].Value);
            Height = int.Parse(match.Groups[5].Value);
        }

        public bool HasClaim(int x, int y)
        {
            if (x >= X && x < X + Width &&
                y >= Y && y < Y + Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Intersects(FabricClaim other)
        {
            return 
                 ((this.X < other.X + other.Width) && (this.X + this.Width > other.X) &&
                    (this.Y < other.Y + other.Height) &&(this.Y + this.Height > other.Y));
        }
    }
}