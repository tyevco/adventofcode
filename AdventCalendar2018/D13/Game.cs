using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day13
{
    class Game
    {
        public Grid Grid { get; }
        public IList<Cart> Carts { get; private set; }
        public IList<Crash> Crashes { get; set; } = new List<Crash>();

        public int Ticks { get; private set; } = 0;

        public Game(Grid grid, IList<Cart> carts)
        {
            Grid = grid;
            Carts = carts;
        }

        public void Tick()
        {
            foreach (var cart in Carts)
            {
                if (!cart.IsCrashed)
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


                    var crashedCarts = Carts.Where(o => cart.X == o.X && cart.Y == o.Y && !cart.Equals(o));
                    if (crashedCarts.Any())
                    {
                        cart.IsCrashed = true;

                        var crash = new Crash
                        {
                            X = cart.X,
                            Y = cart.Y,
                            Ticks = Ticks
                        };
                        crash.Carts.Add(cart);

                        foreach (var c in crashedCarts)
                        {
                            c.IsCrashed = true;
                            crash.Carts.Add(cart);
                        }

                        Crashes.Add(crash);
                    }

                    if (!cart.IsCrashed)
                    {
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

            Carts = Carts.Where(c => !c.IsCrashed).OrderBy(x => x.X + x.Y * Grid.Width).ToList();

            Ticks++;
        }
    }
}
