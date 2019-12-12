using Advent.Utilities;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D12
{
    class Body
    {
        public Point Position { get; set; }
        public Vector3i Velocity { get; set; }

        public override string ToString()
        {
            return $"pos=<x={Position.X}, y={Position.Y}, z={Position.Z}>, vel=<x={Velocity.X}, y={Velocity.Y}, z={Velocity.Z}>";
        }
    }
}
