using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    class Track
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char C { get; set; }

        public Grid Grid { get; set; }

        public TrackType Type { get; set; }

        public Track North => Grid[X, Y - 1];
        public Track East => Grid[X + 1, Y];
        public Track South => Grid[X, Y + 1];
        public Track West => Grid[X - 1, Y];

        // TODO fix issues where the edges may have invalid pieces.

        public bool IsNorthSouth
        {
            get
            {
                return Type == TrackType.Straight && North != null && South != null;
            }
        }

        public bool IsEastWest
        {
            get
            {
                return Type == TrackType.Straight && East != null && West != null;
            }
        }

        public bool IsNorthEast
        {
            get
            {
                return Type == TrackType.Curve && C == '\\' && North != null && East != null;
            }
        }

        public bool IsNorthWest
        {
            get
            {
                return Type == TrackType.Curve && C == '/' && North != null && West != null;
            }
        }

        public bool IsSouthEast
        {
            get
            {

                return Type == TrackType.Curve && C == '/' && South != null && East != null;
            }
        }

        public bool IsSouthWest
        {
            get
            {

                return Type == TrackType.Curve && C == '\\' && South != null && West != null;
            }
        }


        public override string ToString()
        {
            if (Type == TrackType.Intersection)
                return @"+";
            else if (IsNorthSouth)
                return @"|";
            else if (IsEastWest)
                return @"-";
            else if (IsNorthEast)
                return @"\";
            else if (IsNorthWest)
                return @"/";
            else if (IsSouthEast)
                return @"/";
            else if (IsSouthWest)
                return @"\";
            else
                return @" ";
        }
    }
}
