namespace Advent.Utilities
{
    public interface IAABoundingBox : IShape
    {
        IVector<double> Start { get; }
        IVector<double> End { get; }
    }
}
