using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class DelaunayTriangulation
{
    public Triangle[] Triangles => Array.Empty<Triangle>();
    public List<Point2> Points { get; }

    public DelaunayTriangulation()
    {
        Points = new List<Point2>();
    }

    public void Add(Point2 p)
    {
        Points.Add(p);
    }
}
