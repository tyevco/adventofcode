namespace Advent.Utilities
{
    public interface ITriangle : IShape
    {
        IVector Normal { get; }
        IVector A { get; }
        IVector B { get; }
        IVector C { get; }
    }
}
