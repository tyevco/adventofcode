namespace Advent.Utilities
{
    public interface ITriangle : IShape
    {
        IVector<double> Normal { get; }
        IVector<double> A { get; }
        IVector<double> B { get; }
        IVector<double> C { get; }
    }
}
