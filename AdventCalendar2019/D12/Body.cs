using Advent.Utilities;
using Advent.Utilities.Data.Map;
using System;

namespace AdventCalendar2019.D12
{
    class Body
    {
        public int ID { get; set; }
        public Point Position { get; set; }
        public Vector3i Velocity { get; set; }

        public int PotentialEnergy => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        public int KineticEnergy => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        public override string ToString()
        {
            return $"pos=<x={Position.X}, y={Position.Y}, z={Position.Z}>, vel=<x={Velocity.X}, y={Velocity.Y}, z={Velocity.Z}>";
        }

        public string ToEnergyString()
        {
            return $"pot: {PotentialEnergy}   kin: {KineticEnergy};   total:  {TotalEnergy}";
        }

        public void ApplyGravity(Body other)
        {
            ApplyGravityX(other);
            ApplyGravityY(other);
            ApplyGravityZ(other);
        }

        public void ApplyGravityX(Body other)
        {
            if (other.ID != ID)
            {
                if (other.Position.X < Position.X)
                {
                    Velocity.X = Velocity.X - 1;
                }
                else if (other.Position.X > Position.X)
                {
                    Velocity.X = Velocity.X + 1;
                }
            }
        }

        public void ApplyGravityY(Body other)
        {
            if (other.ID != ID)
            {
                if (other.Position.Y < Position.Y)
                {
                    Velocity.Y = Velocity.Y - 1;
                }
                else if (other.Position.Y > Position.Y)
                {
                    Velocity.Y = Velocity.Y + 1;
                }
            }
        }

        public void ApplyGravityZ(Body other)
        {
            if (other.ID != ID)
            {
                if (other.Position.Z < Position.Z)
                {
                    Velocity.Z = Velocity.Z - 1;
                }
                else if (other.Position.Z > Position.Z)
                {
                    Velocity.Z = Velocity.Z + 1;
                }
            }
        }

        public void Move()
        {
            MoveX();
            MoveY();
            MoveZ();
        }

        public void MoveX()
        {
            this.Position.X += Velocity.X;
        }

        public void MoveY()
        {
            this.Position.Y += Velocity.Y;
        }

        public void MoveZ()
        {
            this.Position.Z += Velocity.Z;
        }
    }
}
