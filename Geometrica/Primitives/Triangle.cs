namespace Geometrica.Primitives;

public struct Triangle : IPolygon
{
    private Point2 a;
    private Point2 b;
    private Point2 c;

    public Triangle(Point2 a, Point2 b, Point2 c) : this()
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public Point2 this[int key] => throw new NotImplementedException();

    public int Count { get; }
    public bool IsConvex { get; }
}
