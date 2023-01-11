﻿using Geometrica.DataStructures;
using Geometrica.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometricaTests;

[TestClass]
public class ConvexHullTests
{
    [TestMethod]
    public void ConvexHull_OfTwoPoints_DoesNotExist()
    {
        var ch = new ConvexHull();
        ch.Add(new Point2());
        ch.Add(new Point2(10, 10));

        Assert.AreEqual(0, ch.Hull.Count);
    }

    [TestMethod]
    public void ConvexHull_OfThreeNonlinearPoints_AreTheSameThreePoints()
    {
        var ch = new ConvexHull();
        ch.Add(new Point2());
        ch.Add(new Point2(1, 0));
        ch.Add(new Point2(0, 1));

        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", ch.ToString());
    }

    [TestMethod]
    public void Point_AddedInsideTriangle_DoesNotChangeConvexHull()
    {
        var ch = new ConvexHull();
        ch.Add(new Point2());
        ch.Add(new Point2(1, 0));
        ch.Add(new Point2(0, 1));
        ch.Add(new Point2(0.1, 0.1));

        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", ch.ToString());
    }

    [TestMethod]
    public void CreateSimpleHull_ThreePointsCCW_CounterClockwise()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(1, 0),
            new Point2(0, 1)
        };

        var ch = ConvexHull.CreateSimpleHull(pts);
        var points = String.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void CreateSimpleHull_ThreePointsCW_IsCounterClockwise()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(0, 1),
            new Point2(1, 0)
        };

        var ch = ConvexHull.CreateSimpleHull(pts);
        var points = String.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_PointInside_ReturnsTriangle()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(0, 1),
            new Point2(1, 0),
            new Point2(0.1, 0.1)
        };

        var ch = ConvexHull.ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        var points = String.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    public void Sample()
    {
        // begin-snippet: CreateConvexHull
        var pts = new List<Point2>() 
        {
            new Point2(0, 0),
            new Point2(1, 0),
            new Point2(0, 1),
            new Point2(0.1, 0.1)
        };

        var ch = new ConvexHull(pts);
        // end-snippet
    }
}
