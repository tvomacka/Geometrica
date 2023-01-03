namespace Geometrica.Primitives;

public struct Point2
{
    public double X;
    public double Y;

    public double DistanceTo(Point2 p)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"[{X}; {Y}]";
    }
}
