namespace Geometrica.Primitives;

public struct Triangle : IPolygon
{
    public Point2 this[int key] => throw new NotImplementedException();

    public int Count { get; }
    public bool IsConvex { get; }
}
