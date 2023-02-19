namespace Geometrica.Primitives;

public struct Triangle : IPolygon
{
    private Point2 _a;
    private Point2 _b;
    private Point2 _c;

    public Triangle(Point2 a, Point2 b, Point2 c) : this()
    {
        _a = a;
        _b = b;
        _c = c;
    }

    public Point2 this[int key] => throw new NotImplementedException();

    public int Count { get; }
    public bool IsConvex { get; }

    public override string ToString()
    {
        return $"Triangle {_a} {_b} {_c}";
    }
}
