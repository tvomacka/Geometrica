using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using Geometrica.Algebra;
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
        Triangle t0 = null;
        Triangle t1 = null;
        Triangle t2 = null;

        var index = 0;
        foreach (var t in triangles)
        {
            if (t != target)
            {
                newTriangles[index++] = t;
            }
            else
            {
                t0 = new Triangle(innerPoint, target[0], target[1]);
                t1 = new Triangle(innerPoint, target[1], target[2]);
                t2 = new Triangle(innerPoint, target[2], target[0]);

                t0.SetNeighbors(target.GetNeighbor(2), t1, t2);
                t1.SetNeighbors(target.GetNeighbor(0), t2, t0);
                t2.SetNeighbors(target.GetNeighbor(1), t0, t1);

                newTriangles[index++] = t0;
                newTriangles[index++] = t1;
                newTriangles[index++] = t2;
            }
        }

        newTriangles = LegalizeTriangle(newTriangles, t0, 0);
        newTriangles = LegalizeTriangle(newTriangles, t1, 0);
        newTriangles = LegalizeTriangle(newTriangles, t2, 0);

        return newTriangles;
    }

    public static Triangle[] LegalizeTriangle(Triangle[] triangles, Triangle targetTriangle, int neighborIndex)
    {
        var oppositeTriangle = targetTriangle.GetNeighbor(neighborIndex);
        var oppositePointIndex = oppositeTriangle.GetNeighborIndex(targetTriangle);

        if (InCircle(targetTriangle[0], targetTriangle[1], targetTriangle[2], oppositeTriangle[oppositePointIndex]))
            return triangles;

        var t1 = new Triangle(targetTriangle[neighborIndex], oppositeTriangle[oppositePointIndex], targetTriangle[(neighborIndex + 2) % 3]);
        var t2 = new Triangle(targetTriangle[neighborIndex], targetTriangle[(neighborIndex + 1) % 3], oppositeTriangle[oppositePointIndex]);

        //for each of the original neighbors, set their neighbor references to the newly created triangles
        
        t1.SetNeighbors(
            oppositeTriangle.GetNeighbor((oppositePointIndex + 2) %3),
            targetTriangle.GetNeighbor((neighborIndex + 1) % 3),
            t2);
        t2.SetNeighbors(
            oppositeTriangle.GetNeighbor((oppositePointIndex + 1) %3),
            t1,
            targetTriangle.GetNeighbor((oppositePointIndex + 1) % 3));
        //remove the original two triangles
        //add the newly created triangles

        return triangles;
    }

    public static Triangle FindTriangleContainingPoint(Triangle[] triangles, Point2 p)
    {
        if (triangles.Length == 1)
        {
            return triangles[0].Contains(p) ? triangles[0] : null;
        }

        var startTriangle = EstimateNearestTriangle(triangles, p, new Random());

        return RememberingWalk(OrthogonalWalk(startTriangle, p), p); 
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

    public static Triangle RememberingWalk(Triangle start, Point2 q)
    {
        return RememberingWalk(start, q, new Random());
    }

    public static Triangle RememberingWalk(Triangle start, Point2 q, Random r)
    {
        var current = start;
        var previous = start;
        var found = false;

        while (!found)
        {
            var randomPointIndex = r.Next(3);

            for (var i = 0; i < 3; i++)
            {
                randomPointIndex = (randomPointIndex + i) % 3;
                
                var randomPoint = current[randomPointIndex];
                var neigh = current.GetNeighbor(randomPointIndex);

                var detQ = 1.0;
                var detP = 1.0;
                if (neigh != null && neigh != previous)
                {
                    detP = Point2.Orientation(current[(randomPointIndex + 1) % 3], current[(randomPointIndex + 2) % 3], randomPoint);
                    detQ = Point2.Orientation(current[(randomPointIndex + 1) % 3], current[(randomPointIndex + 2) % 3], q);
                }
                if (detP * detQ < 0)
                {
                    previous = current;
                    current = neigh;
                    break;
                }

                if (i == 2)
                    found = true;
            }
        }
        return current;
    }

    public static Triangle OrthogonalWalk(Triangle start, Point2 p)
    {
        return OrthogonalWalkY(OrthogonalWalkX(start, p), p);
    }

    public static Triangle OrthogonalWalkY(Triangle start, Point2 p)
    {
        //determine if the starting triangle is left or right of the queried point p
        //traverse triangles to the determined direction of x-axis until the queried point's x-coordinate is reached
        //repeat in the y-direction

        var controlPointIndex = GetNearestVertexIndexY(start, p);
        var controlPoint = start[controlPointIndex]; //the point of the current node nearest to the tested point

        //Edge oppositeEdge = startingNode.T.GetOppositeEdge(controlPoint);   //opposite edge to the control point
        var currentTriangle = start;
        Triangle previousTriangle;
        var middleX = (start[(controlPointIndex + 1) % 3].X + start[(controlPointIndex + 2) % 3].X) * 0.5;
        var middleY = (start[(controlPointIndex + 1) % 3].Y + start[(controlPointIndex + 2) % 3].Y) * 0.5;

        if (middleY < p.Y) //approach the point from lower y values
        {
            while (controlPoint.Y < p.Y)
            {
                previousTriangle = currentTriangle;

                currentTriangle = controlPoint.X < middleX ? currentTriangle.GetNeighbor((controlPointIndex + 1) % 3) : currentTriangle.GetNeighbor((controlPointIndex + 2) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }

                controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                controlPoint = currentTriangle[controlPointIndex];
            }
        }
        else //approach from higher x-values
        {
            while (p.Y < controlPoint.Y)
            {
                previousTriangle = currentTriangle;

                currentTriangle = controlPoint.X < middleX ? currentTriangle.GetNeighbor((controlPointIndex + 2) % 3) : currentTriangle.GetNeighbor((controlPointIndex + 1) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }

                controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                controlPoint = currentTriangle[controlPointIndex];
            }
        }

        return currentTriangle;
    }

    public static Triangle OrthogonalWalkX(Triangle start, Point2 p)
    {
        //determine if the starting triangle is left or right of the queried point p
        //traverse triangles to the determined direction of x-axis until the queried point's x-coordinate is reached
        //repeat in the y-direction

        var controlPointIndex = GetNearestVertexIndexX(start, p);
        var controlPoint = start[controlPointIndex]; //the point of the current node nearest to the tested point

        //Edge oppositeEdge = startingNode.T.GetOppositeEdge(controlPoint);   //opposite edge to the control point
        var currentTriangle = start;
        Triangle previousTriangle;
        var middleX = (start[(controlPointIndex + 1) % 3].X + start[(controlPointIndex + 2) % 3].X) * 0.5;
        var middleY = (start[(controlPointIndex + 1) % 3].Y + start[(controlPointIndex + 2) % 3].Y) * 0.5;

        if (middleX < p.X) //approach the point from lower x values
        {
            while (controlPoint.X < p.X)
            {
                previousTriangle = currentTriangle;

                currentTriangle = controlPoint.Y < middleY ? currentTriangle.GetNeighbor((controlPointIndex + 2) % 3) : currentTriangle.GetNeighbor((controlPointIndex + 1) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }

                controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                controlPoint = currentTriangle[controlPointIndex];
            }
        }
        else //approach from higher x-values
        {
            while (p.X < controlPoint.X)
            {
                previousTriangle = currentTriangle;

                currentTriangle = controlPoint.Y < middleY ? currentTriangle.GetNeighbor((controlPointIndex + 1) % 3) : currentTriangle.GetNeighbor((controlPointIndex + 2) % 3);

                if (currentTriangle == null)
                {
                    currentTriangle = previousTriangle;
                    break;
                }

                controlPointIndex = currentTriangle.GetNeighborIndex(previousTriangle);
                controlPoint = currentTriangle[controlPointIndex];
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

    public static int GetNearestVertexIndexY(Triangle triangle, Point2 p)
    {

        var distY1 = Math.Abs(triangle[0].Y - p.Y);
        var distY2 = Math.Abs(triangle[1].Y - p.Y);
        var distY3 = Math.Abs(triangle[2].Y - p.Y);

        if (distY1 < distY2 && distY1 < distY3)
        {
            return 0;
        }

        return distY2 < distY3 ? 1 : 2;
    }

    public static bool InCircle(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
    {
        var m = new Matrix4(
            p1.X, p1.Y, p1.X * p1.X + p1.Y * p1.Y, 1,
            p2.X, p2.Y, p2.X * p2.X + p2.Y * p2.Y, 1,
            p3.X, p3.Y, p3.X * p3.X + p3.Y * p3.Y, 1,
            p4.X, p4.Y, p4.X * p4.X + p4.Y * p4.Y, 1);

        return m.Determinant() < -double.Epsilon;
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
