using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    class Game
    {
        public Grid Grid { get; }
        public IList<Cart> Carts { get; }

        public Game(Grid grid, IList<Cart> carts)
        {
            Grid = grid;
            Carts = carts;
        }

        public void Tick()
        {
            foreach (var cart in Carts)
            {
                var dir = cart.Direction;
                int dx = 0, dy = 0;
                switch (dir)
                {
                    case Direction.North:
                        dy = -1;
                        break;
                    case Direction.East:
                        dx = 1;
                        break;
                    case Direction.South:
                        dy = 1;
                        break;
                    case Direction.West:
                        dx = -1;
                        break;
                }

                cart.Move(dx, dy);

                var track = Grid[cart.X, cart.Y];

                if (track != null)
                {
                    switch (track.Type)
                    {
                        case TrackType.Straight:
                            break;

                        case TrackType.Intersection:
                            switch (cart.NextDecision)
                            {
                                case Decision.TurnLeft:
                                    cart.Direction = cart.Direction.GetLeft();
                                    cart.NextDecision = Decision.GoStraight;
                                    break;

                                case Decision.GoStraight:
                                    cart.NextDecision = Decision.TurnRight;
                                    break;

                                case Decision.TurnRight:
                                    cart.Direction = cart.Direction.GetRight();
                                    cart.NextDecision = Decision.TurnLeft;
                                    break;
                            }
                            break;

                        case TrackType.Curve:

                            switch (cart.Direction)
                            {
                                case Direction.North:
                                    if (track.IsSouthEast)
                                    {
                                        cart.Direction = Direction.East;
                                    }
                                    else if (track.IsSouthWest)
                                    {
                                        cart.Direction = Direction.West;
                                    }
                                    break;
                                case Direction.East:
                                    if (track.IsNorthWest)
                                    {
                                        cart.Direction = Direction.North;
                                    }
                                    else if (track.IsSouthWest)
                                    {
                                        cart.Direction = Direction.South;
                                    }
                                    break;
                                case Direction.South:
                                    if (track.IsNorthEast)
                                    {
                                        cart.Direction = Direction.East;
                                    }
                                    else if (track.IsNorthWest)
                                    {
                                        cart.Direction = Direction.West;
                                    }
                                    break;
                                case Direction.West:
                                    if (track.IsNorthEast)
                                    {
                                        cart.Direction = Direction.North;
                                    }
                                    else if (track.IsSouthEast)
                                    {
                                        cart.Direction = Direction.South;
                                    }
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    throw new Exception($"Unable to find a track at {cart.X},{cart.Y}.");
                }
            }
        }
    }
}
