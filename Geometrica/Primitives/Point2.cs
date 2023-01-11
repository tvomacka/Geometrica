using Geometrica.Algebra;

namespace Geometrica.Primitives;

public struct Point2
{
    public double X;
    public double Y;

    public Point2(double x, double y) : this()
    {
        X = x;
        Y = y;
    }

    public static double Orientation(Point2 p, Point2 q, Point2 r)
    {
        return (new Matrix3(p.X, p.Y, 1, q.X, q.Y, 1, r.X, r.Y, 1)).Determinant();
    }

    public static bool OrientedCCW(Point2 p, Point2 q, Point2 r)
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

    public bool Inside(List<Point2> polygon)
    {
        return true;
    }
}
