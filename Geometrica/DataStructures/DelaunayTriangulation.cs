﻿using Geometrica.Primitives;

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
        Points = new List<Point2>(pts);
        Triangles = new Triangle[1];
        Triangles[0] = new Triangle(pts[0], pts[1], pts[2]);
    }

    public void Add(Point2 p)
    {
        Points.Add(p);

        //if the newly added point lies inside CH(p) find the triangle containing it
        //split this triangle
        //if the new point lies outside CH(p), reconstruct CH(p)
        //create new triangles by connecting the new point to the point that were removed from CH(p)
        //check every new triangle and its neighbors for the delaunay condition
        //swap the diagonals if the condition is not met
        //repeat until no new triangle remains unchecked

        if (Triangles.Length > 0)
        {
            var t = FindTriangleContainingPoint(p);
            Triangles = SplitTriangle(t, p);
        }
    }

    public Triangle[] SplitTriangle(Triangle original, Point2 innerPoint)
    {
        return new Triangle[]
        {
            new(innerPoint, original[0], original[1]),
            new(innerPoint, original[1], original[2]),
            new(innerPoint, original[2], original[0])
        };
    }

    public Triangle FindTriangleContainingPoint(Point2 p)
    {
        return Triangles[0];
    }
}
