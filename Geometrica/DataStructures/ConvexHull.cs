using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class ConvexHull
{
    private readonly List<Point2> points = new();
    public readonly List<Point2> Hull = new();

    public void Add(Point2 point)
    {
        points.Add(point);
    }
}
