namespace Geometrica.Primitives;

public class Triangle : IPolygon
{
    private Point2 _a;
    private Point2 _b;
    private Point2 _c;

    public Triangle(Point2 a, Point2 b, Point2 c)
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
        set
        {
            if (key == 0)
                _a = value;
            else if (key == 1)
                _b = value;
            else if (key == 2)
                _c = value;
        }
    }

    public int Count => 3;
    public bool IsConvex => true;
    public bool Contains(Point2 point)
    {
        return Point2.OrientedCcw(point, _a, _b) && Point2.OrientedCcw(point, _b, _c) && Point2.OrientedCcw(point, _c, _a);
    }

    public override string ToString()
    {
        return $"Triangle {_a} {_b} {_c}";
    }
}
