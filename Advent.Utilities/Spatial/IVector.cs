namespace Advent.Utilities
{
    public interface IVector<T>
            where T : struct
    {
        T X { get; }
        T Y { get; }
        T Z { get; }
        T[] Coords { get; }
        T Dot(IVector<T> other);
        IVector<T> Minus(IVector<T> other);
        IVector<T> Cross(IVector<T> other);
    }
}