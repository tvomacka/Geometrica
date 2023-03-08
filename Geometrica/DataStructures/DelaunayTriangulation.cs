using System.Runtime.CompilerServices;
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

    public static Triangle[] SplitTriangle(Triangle[] triangles, Triangle target, Point2 innerPoint)
    {
        var newTriangles = new Triangle[triangles.Length + 2];

        var index = 0;
        foreach (var t in triangles)
        {
            if (t != target)
            {
                newTriangles[index++] = t;
            }
            else
            {
                var t0 = new Triangle(innerPoint, target[0], target[1]);
                var t1 = new Triangle(innerPoint, target[1], target[2]);
                var t2 = new Triangle(innerPoint, target[2], target[0]);

                var targetNeighbor0 = target.GetNeighbor(0);
                var targetNeighbor1 = target.GetNeighbor(1);
                var targetNeighbor2 = target.GetNeighbor(2);

                TriangleExtensions.SetNeighbors(t0, targetNeighbor2, t1, t2);

                t1.SetNeighbor(0, targetNeighbor0);
                t1.SetNeighbor(1, t2);
                t1.SetNeighbor(2, t0);

                t2.SetNeighbor(0, targetNeighbor1);
                t2.SetNeighbor(1, t0);
                t2.SetNeighbor(2, t1);

                newTriangles[index++] = t0;
                newTriangles[index++] = t1;
                newTriangles[index++] = t2;
            }
        }

        return newTriangles;
    }

    public static Triangle FindTriangleContainingPoint(Triangle[] triangles, Point2 p)
    {
        if (triangles.Length == 1)
        {
            return triangles[0].Contains(p) ? triangles[0] : null;
        }

        var startTriangle = EstimateNearestTriangle(triangles, p, new Random());

        return startTriangle;

        //walk to near triangle using orthogonal walk, then use remembering stochastic walk to find the target triangle
    }

    public static Triangle EstimateNearestTriangle(Triangle[] triangles, Point2 p, Random random)
    {
        var minDistance = double.MaxValue;
        Triangle nearest = null;

        //search n^(1/3.5) triangles first and start the walk from the one, which is nearest to the point p
        var totalSteps = (int)(Math.Pow(triangles.Length, 0.28571) + 1);
        for (var i = 0; i < totalSteps; i++)
        {
            var tIndex = random.Next(triangles.Length);
            var vertex = triangles[tIndex][0];
            var distance = vertex.SquareDistanceTo(p);
            if (!(distance < minDistance)) continue;
            nearest = triangles[tIndex];
            minDistance = distance;
        }

        return nearest;
    }
}

public static class TriangleExtensions
{
    private static readonly ConditionalWeakTable<object, Triangle[]> Neighbors = new();

    public static Triangle GetNeighbor(this Triangle t, int index)
    {
        var n = Neighbors.GetValue(t, CreateNeighbors);
        return n?[index];
    }

    public static void SetNeighbor(this Triangle t, int index, Triangle neighbor)
    {
        var n = Neighbors.GetValue(t, CreateNeighbors);
        if (n != null)
        {
            n[index] = neighbor;
        }
        else
        {
            throw new NullReferenceException($"Cannot assign neighbor to triangle {t}.");
        }
    }

    public static Triangle[] CreateNeighbors(object _)
    {
        return new Triangle[] { null, null, null };
    }

    public static void SetNeighbors(this Triangle t, Triangle neighbor0, Triangle neighbor1, Triangle neighbor2)
    {
        t.SetNeighbor(0, neighbor0);
        t.SetNeighbor(1, neighbor1);
        t.SetNeighbor(2, neighbor2);
    }
}
