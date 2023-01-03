namespace Geometrica.Primitives;

public struct Point2
{
    public double X;
    public double Y;

    public double DistanceTo(Point2 p)
    {
        return 0;
    }

    public override string ToString()
    {
        return $"[{X}; {Y}]";
    }
}
