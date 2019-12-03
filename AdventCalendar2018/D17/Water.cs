namespace AdventCalendar2018.D17
{
    public class Water : Material
    {
        //There is a minor issue where a piece of flowing water is marked as still. This is rare (1 spot out of the actual data)
        public bool IsFlowing { get; set; } = false;

        public override MaterialType Type => MaterialType.Water;

        public Water(int x, int y, Grid grid)
        : base(grid)
        {
            X = x;
            Y = y;
            IsFlowing = true;
        }

        public override void Propagate()
        {
            if (IsFlowing)
            {
                if (Below == null)
                {
                    if (Y + 1 <= Grid.Bottom)
                    {
                        AddWater(X, Y + 1);
                    }
                }
                else
                {
                    if (Below.Type == MaterialType.Water && !IsDiagonalEmptyToLeft() && !IsDiagonalEmptyToRight())
                    {
                        if (IsThereClayToLeft() && IsThereClayToRight())
                        {
                            if (DiagonalLeft.Type == MaterialType.Clay || (DiagonalLeft.Type == MaterialType.Water && !((Water)DiagonalLeft).IsFlowing))
                            {
                                if (DiagonalRight.Type == MaterialType.Clay || (DiagonalRight.Type == MaterialType.Water && !((Water)DiagonalRight).IsFlowing))
                                    if (Left == null)
                                    {
                                        AddWater(X - 1, Y).Propagate();
                                    }
                            }
                            if (DiagonalRight.Type == MaterialType.Clay || (DiagonalRight.Type == MaterialType.Water && !((Water)DiagonalRight).IsFlowing))
                            {
                                if (DiagonalLeft.Type == MaterialType.Clay || (DiagonalLeft.Type == MaterialType.Water && !((Water)DiagonalLeft).IsFlowing))
                                    if (Right == null)
                                    {
                                        AddWater(X + 1, Y).Propagate();
                                    }
                            }
                        }
                        else
                        {
                            if (DiagonalLeft.Type == MaterialType.Clay || (DiagonalLeft.Type == MaterialType.Water && !((Water)DiagonalLeft).IsFlowing))
                            {
                                if (DiagonalRight.Type == MaterialType.Clay || (DiagonalRight.Type == MaterialType.Water && !((Water)DiagonalRight).IsFlowing))
                                    if (Left == null)
                                    {
                                        AddWater(X - 1, Y).Propagate();
                                    }
                            }
                            if (DiagonalRight.Type == MaterialType.Clay || (DiagonalRight.Type == MaterialType.Water && !((Water)DiagonalRight).IsFlowing))
                            {
                                if (DiagonalLeft.Type == MaterialType.Clay || (DiagonalLeft.Type == MaterialType.Water && !((Water)DiagonalLeft).IsFlowing))
                                    if (Right == null)
                                    {
                                        AddWater(X + 1, Y).Propagate();
                                    }
                            }
                        }
                    }
                    else if (Below.Type == MaterialType.Clay)
                    {
                        if (Left == null)
                        {
                            AddWater(X - 1, Y).Propagate();
                        }

                        if (Right == null)
                        {
                            AddWater(X + 1, Y).Propagate();
                        }

                        if ((Above?.Type == MaterialType.Water || Above?.Type == MaterialType.Clay) &&
                            (Left.Type == MaterialType.Water || Left.Type == MaterialType.Clay) &&
                            (Right.Type == MaterialType.Water || Right.Type == MaterialType.Clay))
                        {
                            IsFlowing = false;
                        }
                    }
                    else
                    {

                    }
                }

                if (IsThereClayToLeft() && IsThereClayToRight())
                {
                    int left = ClayToLeft;
                    int right = ClayToRight;
                    bool foundAir = false;
                    for (int i = left; i <= right; i++)
                    {
                        if (Grid[i, Y] == null && Grid[i, Y + 1] == null)
                        {
                            foundAir = true;
                            break;
                        }
                    }

                    if (!foundAir)
                    {
                        IsFlowing = false;
                    }
                }
            }
        }

        private Water AddWater(int x, int y)
        {
            var water = new Water(x, y, Grid);

            if (y <= Grid.Bottom)
            {
                Grid[x, y] = water;
                Grid.Added = true;
            }

            return water;
        }

        private bool IsDiagonalEmptyToLeft()
        {
            return DiagonalLeft == null;
        }

        private bool IsDiagonalEmptyToRight()
        {
            return DiagonalRight == null;
        }

        private bool IsThereClayToLeft()
        {
            Material left = Left;
            bool foundClay = false;
            for (int x = X; x >= Grid.Left; x--)
            {
                left = Grid[x, Y];

                if (left?.Type == MaterialType.Clay)
                {
                    foundClay = true;
                    break;
                }
            }

            return foundClay;
        }

        private int ClayToLeft
        {
            get
            {
                Material left = Left;
                int fx = int.MinValue;
                for (int x = X; x >= Grid.Left; x--)
                {
                    left = Grid[x, Y];

                    if (left?.Type == MaterialType.Clay)
                    {
                        fx = x;
                        break;
                    }
                }

                return fx;
            }
        }

        private int ClayToRight
        {
            get
            {
                Material right = Right;
                int fx = int.MaxValue;
                for (int x = X; x <= Grid.Right; x++)
                {
                    right = Grid[x, Y];

                    if (right?.Type == MaterialType.Clay)
                    {
                        fx = x;
                        break;
                    }
                }

                return fx;
            }
        }

        private bool IsThereClayToRight()
        {
            Material right = Right;
            bool foundClay = false;
            for (int x = X; x <= Grid.Right; x++)
            {
                right = Grid[x, Y];

                if (right?.Type == MaterialType.Clay)
                {
                    foundClay = true;
                    break;
                }
            }


            return foundClay;
        }

        public override string ToString()
        {
            return IsFlowing ? "|" : "~";
        }
    }
}