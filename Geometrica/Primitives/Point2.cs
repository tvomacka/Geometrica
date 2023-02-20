using Geometrica.Algebra;

namespace Geometrica.Primitives;

public struct Point2
{
    public double X;
    public double Y;

    public static Point2 operator -(Point2 a, Point2 b) => new(a.X - b.X, a.Y - b.Y);

    public Point2(double x, double y) : this()
    {
        X = x;
        Y = y;
    }

    public static double Orientation(Point2 p, Point2 q, Point2 r)
    {
        return (new Matrix3(p.X, p.Y, 1, q.X, q.Y, 1, r.X, r.Y, 1)).Determinant();
    }

    public static bool OrientedCcw(Point2 p, Point2 q, Point2 r)
    {
        return Orientation(p, q, r) > 0;
    }

    public double DistanceTo(Point2 p)
    {
        return Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
    }

    public override string ToString()
    {
        return $"[{X}; {Y}]";
    }

    public bool Inside(IPolygon polygon)
    {
        return true;
    }

    public Point2 Normalize()
    {
        var length = Math.Sqrt(X * X + Y * Y);
        return new Point2(X / length, Y / length);
    }
}
