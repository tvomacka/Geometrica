using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class DelaunayTriangulation
{
    public Triangle[] Triangles { get; set; }
    public List<Point2> Points { get; set; }

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

                t0.SetNeighbors(target.GetNeighbor(2), t1, t2);
                t1.SetNeighbors(target.GetNeighbor(0), t2, t0);
                t2.SetNeighbors(target.GetNeighbor(1), t0, t1);

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

    public Triangle OrthogonalWalk(Triangle start, Point2 p)
    {
        var currentTriangle = OrthogonalWalkX(start, p);

        return currentTriangle;
    }

    public Triangle OrthogonalWalkX(Triangle start, Point2 p)
    {
        //determine if the starting triangle is left or right of the queried point p
        //traverse triangles to the determined direction of x-axis until the queried point's x-coordinate is reached
        //repeat in the y-direction

        var controlPointIndex = GetNearestVertexIndexX(start, p);
        var controlPoint = start[controlPointIndex]; //the point of the current node nearest to the tested point

        //Edge oppositeEdge = startingNode.T.GetOppositeEdge(controlPoint);   //opposite edge to the control point
        var currentTriangle = start;
        var previousTriangle = start.GetNeighbor(controlPointIndex);
        var middleX = (start[(controlPointIndex + 1) % 3].X + start[(controlPointIndex + 2) % 3].X) * 0.5;
        var middleY = (start[(controlPointIndex + 1) % 3].Y + start[(controlPointIndex + 2) % 3].Y) * 0.5;

        if (middleX < p.X) //approach the point from lower x values
        {
            while (controlPoint.X < p.X)
            {
                previousTriangle = currentTriangle;

                if (controlPoint.Y < middleY)
                    currentTriangle = currentTriangle.GetNeighbor((controlPointIndex + 1) % 3);
                else
                    currentTriangle = currentTriangle.GetNeighbor((controlPointIndex + 2) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }
                else
                {
                    controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                    controlPoint = currentTriangle[controlPointIndex];
                }
            }
        }
        else //approach from higher x-values
        {
            while (controlPoint.X > p.X)
            {
                previousTriangle = currentTriangle;

                if (controlPoint.Y < middleY)
                    currentTriangle = currentTriangle.GetNeighbor((controlPointIndex + 2) % 3);
                else
                    currentTriangle = currentTriangle.GetNeighbor((controlPointIndex + 1) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }
                else
                {
                    controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                    controlPoint = currentTriangle[controlPointIndex];
                }
            }
        }

        return currentTriangle;
    }

    public static int GetNearestVertexIndexX(Triangle triangle, Point2 p)
    {

        var distX1 = Math.Abs(triangle[0].X - p.X);
        var distX2 = Math.Abs(triangle[1].X - p.X);
        var distX3 = Math.Abs(triangle[2].X - p.X);

        if (distX1 < distX2 && distX1 < distX3)
        {
            return 0;
        }

        return distX2 < distX3 ? 1 : 2;
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

    public static int GetNeighborIndex(this Triangle t, Triangle neighbor)
    {
        if (neighbor == null)
        {
            throw new ArgumentNullException(nameof(neighbor), "Attempting to search a null neighbor. Please provide a valid triangle.");
        }
        for (var i = 0; i < 3; i++)
        {
            if (t.GetNeighbor(i) == neighbor) return i;
        }

        throw new ArgumentException($"Cannot determine the neighbor index.\n"
        + $"Target triangle: {t}\n"
        + $"Neighbor triangle: {neighbor}");
    }
}
