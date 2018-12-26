namespace Advent.Utilities
{
    public interface IVector
    {
        double X { get; }
        double Y { get; }
        double Z { get; }
        double[] Coords { get; }
        double Dot(IVector other);
        IVector Minus(IVector other);
        IVector Cross(IVector other);
    }
}