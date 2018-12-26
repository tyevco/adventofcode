namespace Advent.Utilities
{
    public interface IAABoundingBox : IShape
    {
        IVector Start { get; }
        IVector End { get; }
    }
}
