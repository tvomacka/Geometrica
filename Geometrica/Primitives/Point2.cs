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

    public double DistanceTo(Point2 p)
    {
        return Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
    }

    public override string ToString()
    {
        return $"[{X}; {Y}]";
    }
}
