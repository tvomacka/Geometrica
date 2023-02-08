using Geometrica.DataStructures;
using Geometrica.Primitives;

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
        var points = string.Join(" ", ch);
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
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_PointInsideCWTriangle_ReturnsTriangle()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(0, 1),
            new Point2(1, 0),
            new Point2(0.1, 0.1)
        };

        var ch = ConvexHull.ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_PointInsideCCWTriangle_ReturnsTriangle()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(1, 0),
            new Point2(0, 1),
            new Point2(0.1, 0.1)
        };

        var ch = ConvexHull.ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_PointsOnCCWRectangle_ReturnsRectangle()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(1, 0),
            new Point2(1, 1),
            new Point2(0, 1)
        };

        var ch = ConvexHull.ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_PointsOnCWRectangle_ReturnsRectangle()
    {
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(0, 1),
            new Point2(1, 1),
            new Point2(1, 0)
        };

        var ch = ConvexHull.ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ABDC_CCW_ReturnsABDC()
    {
        var a = new Point2(0, 0);
        var b = new Point2(1, 0);
        var d = new Point2(1, 1);
        var c = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ABDC_CW_ReturnsABDC()
    {
        var a = new Point2(0, 0);
        var c = new Point2(1, 0);
        var d = new Point2(1, 1);
        var b = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ABD_CCW_ReturnsABD()
    {
        var a = new Point2(0, 0);
        var b = new Point2(1, 0);
        var d = new Point2(0, 1);
        var c = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ABD_CW_ReturnsABD()
    {
        var a = new Point2(0, 0);
        var b = new Point2(0, 1);
        var d = new Point2(1, 0);
        var c = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_BCD_CCW_ReturnsBCD()
    {
        var b = new Point2(0, 0);
        var c = new Point2(1, 0);
        var d = new Point2(0, 1);
        var a = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_BCD_CW_ReturnsBCD()
    {
        var b = new Point2(0, 0);
        var c = new Point2(0, 1);
        var d = new Point2(1, 0);
        var a = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_CAD_CCW_ReturnsCAD()
    {
        var c = new Point2(0, 0);
        var a = new Point2(1, 0);
        var d = new Point2(0, 1);
        var b = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_CAD_CW_ReturnsCAD()
    {
        var c = new Point2(0, 0);
        var a = new Point2(0, 1);
        var d = new Point2(1, 0);
        var b = new Point2(0.1, 0.1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ADBC_CCW_ReturnsADBC()
    {
        var a = new Point2(0, 0);
        var d = new Point2(1, 0);
        var b = new Point2(1, 1);
        var c = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull4_ADBC_CW_ReturnsADBC()
    {
        var a = new Point2(0, 0);
        var c = new Point2(1, 0);
        var b = new Point2(1, 1);
        var d = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
    }

    [TestMethod]
    public void ConvexHull5_ABCDE_ReturnsABCDE()
    {
        // begin-snippet: BruteForceConvexHull
        var a = new Point2(0, 0);
        var c = new Point2(2, 0);
        var b = new Point2(2, 3);
        var d = new Point2(1, 3);
        var e = new Point2(0, 2);

        var ch = ConvexHull.BruteForce(new[] { a, b, c, d, e });
        // end-snippet

        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [2; 0] [2; 3] [1; 3] [0; 2]", points);
    }

    [TestMethod]
    public void GrahamSearch_FourCHPoints_ReturnsCH4()
    {
        var a = new Point2(0, 0);
        var b = new Point2(1, 0);
        var c = new Point2(1, 1);
        var d = new Point2(0, 1);

        var ch = ConvexHull.GrahamSearch(new[] { a, b, c, d });

        var points = string.Join(" ", ch);
        Assert.AreEqual("", points);
    }

    [TestMethod]
    public void Points_CanBeSorted_ByAngle()
    {
        var p = new Point2(0, 0);
        
        var a = new Point2(1, 1);
        var b = new Point2(1, 0);
        var c = new Point2(0, 1);

        var sorted = ConvexHull.SortPointsByAngle(p, new[] { a, b, c });

        var pts = string.Join(" ", sorted);
        Assert.AreEqual("", pts);
    }

    public void Sample()
    {
        // begin-snippet: CreateConvexHull
        var pts = new List<Point2>() 
        {
            new(0, 0),
            new(1, 0),
            new(0, 1),
            new(0.1, 0.1)
        };

        var ch = new ConvexHull(pts);
        // end-snippet
    }
}
