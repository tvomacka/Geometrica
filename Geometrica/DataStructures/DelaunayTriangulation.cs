using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class DelaunayTriangulation
{
    public Triangle[] Triangles { get; set; }
    public List<Point2> Points { get; }

    public DelaunayTriangulation()
    {
        Points = new List<Point2>();
        Triangles = Array.Empty<Triangle>();
    }

    public DelaunayTriangulation(Point2[] pts)
    {
        Triangles = new Triangle[1];
        Triangles[0] = new Triangle(pts[0], pts[1], pts[2]);
    }

    public void Add(Point2 p)
    {
        Points.Add(p);
    }
}
