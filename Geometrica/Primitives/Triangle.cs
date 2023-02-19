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

    public Point2 this[int key]
    {
        get
        {
            return key switch
            {
                0 => _a,
                1 => _b,
                2 => _c,
                _ => throw new ArgumentException(
                    $"The provided index {key} was out of bounds, please use index values 0, 1, 2 for triangle point access.")
            };
        }
    }

    public int Count => 3;
    public bool IsConvex => true;

    public override string ToString()
    {
        return $"Triangle {_a} {_b} {_c}";
    }
}
