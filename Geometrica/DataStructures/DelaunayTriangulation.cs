using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class DelaunayTriangulation
{
    public Triangle[] Triangles { get; set; }
    public List<Point2> Points { get; }

    public ConvexHull ConvexHull { get; set; }

    public DelaunayTriangulation()
    {
        Points = new List<Point2>();
        Triangles = Array.Empty<Triangle>();
    }

    public DelaunayTriangulation(Point2[] pts)
    {
        Points = new List<Point2>(pts);
        Triangles = new Triangle[1];
        Triangles[0] = new Triangle(pts[0], pts[1], pts[2]);
        ConvexHull = new ConvexHull(Points);
    }

    public void Add(Point2 p)
    {
        if (ConvexHull?.Contains(p) ?? false)
        {
            //if the newly added point lies inside CH(p) find the triangle containing it
            //split this triangle
            var t = FindTriangleContainingPoint(Triangles, p);
            Triangles = SplitTriangle(Triangles, t, p);
        }
        else
        {
            //if the new point lies outside CH(p), reconstruct CH(p)
            //create new triangles by connecting the new point to the point that were removed from CH(p)
        }
        //check every new triangle and its neighbors for the delaunay condition
        //swap the diagonals if the condition is not met
        //repeat until no new triangle remains unchecked

        Points.Add(p);
    }

    public Triangle[] SplitTriangle(Triangle[] triangles, Triangle target, Point2 innerPoint)
    {
        return new Triangle[]
        {
            new(innerPoint, target[0], target[1]),
            new(innerPoint, target[1], target[2]),
            new(innerPoint, target[2], target[0])
        };
    }

    public Triangle FindTriangleContainingPoint(Triangle[] triangles, Point2 p)
    {
        return triangles[0];
    }
}

public static class TriangleExtensions
{
    public static Triangle? GetNeighbor(this Triangle t, int index)
    {
        return null;
    }

    public static void SetNeighbor(this Triangle t, int index, Triangle neighbor)
    {

    }
}
