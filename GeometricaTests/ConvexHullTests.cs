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

        Assert.AreEqual(0, ch.Length);
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
        // begin-snippet: SimpleConvexHull
        var pts = new List<Point2>()
        {
            new Point2(0, 0),
            new Point2(0, 1),
            new Point2(1, 0)
        };

        var ch = ConvexHull.CreateSimpleHull(pts);
        // end-snippet
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
    public void GrahamScan_FourCHPoints_ReturnsCH4()
    {
        // begin-snippet: GrahamScanConvexHull
        var a = new Point2(0, 0);
        var b = new Point2(1, 0);
        var c = new Point2(1, 1);
        var d = new Point2(0, 1);

        var ch = ConvexHull.GrahamScan(new[] { a, b, c, d });
        // end-snippet

        var points = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [1; 0] [1; 1] [0; 1]", points);
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
        Assert.AreEqual("[1; 0] [1; 1] [0; 1]", pts);
    }

    [TestMethod]
    public void Point_AngleInFirstQuadrant()
    {
        const double angle = Math.PI * 0.25;
        var alpha = ConvexHull.GetPointAngle(new Point2(Math.Cos(angle), Math.Sin(angle)).Normalize());
        Assert.AreEqual(alpha, angle, 0.001);
    }

    [TestMethod]
    public void Point_AngleInSecondQuadrant()
    {
        const double angle = Math.PI * 0.75;
        var alpha = ConvexHull.GetPointAngle(new Point2(Math.Cos(angle), Math.Sin(angle)).Normalize());
        Assert.AreEqual(alpha, angle, 0.001);
    }

    [TestMethod]
    public void Point_AngleInThirdQuadrant()
    {
        const double angle = Math.PI * 1.25;
        var alpha = ConvexHull.GetPointAngle(new Point2(Math.Cos(angle), Math.Sin(angle)).Normalize());
        Assert.AreEqual(alpha, angle, 0.001);
    }

    [TestMethod]
    public void Point_AngleInFourthQuadrant()
    {
        const double angle = Math.PI * 1.75;
        var alpha = ConvexHull.GetPointAngle(new Point2(Math.Cos(angle), Math.Sin(angle)).Normalize());
        Assert.AreEqual(alpha, angle, 0.001);
    }

    [TestMethod]
    public void PointInside_ConvexHull_IsRecognized()
    {
        var a = new Point2(0, 0);
        var c = new Point2(1, 0);
        var b = new Point2(1, 1);
        var d = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);

        Assert.IsTrue(ConvexHull.IsPointInside(new(0.5, 0.5), ch));
    }

    [TestMethod]
    public void PointOutside_ConvexHull_IsRejected()
    {
        var a = new Point2(0, 0);
        var c = new Point2(1, 0);
        var b = new Point2(1, 1);
        var d = new Point2(0, 1);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);

        Assert.IsFalse(ConvexHull.IsPointInside(new(1.5, 1.5), ch));
    }

    [TestMethod]
    public void PointInside_ConvexHull_CanBeGenerated()
    {
        var a = new Point2(0, 0);
        var c = new Point2(1.5, 0);
        var b = new Point2(1.5, 3);
        var d = new Point2(0, 3);

        var ch = ConvexHull.ConvexHull4(a, b, c, d);
        var pIn = ConvexHull.GetPointInside(ch);

        Assert.AreEqual("[1; 1]", pIn.ToString());
    }

    [TestMethod]
    public void JoinHulls_PointInsideBothHulls()
    {
        var a = new Point2(1, 3);
        var b = new Point2(5, 3);
        var c = new Point2(5, 6);
        var d = new Point2(1, 6);
        var ch1 = ConvexHull.ConvexHull4(a, b, c, d);

        var e = new Point2(0, 0);
        var f = new Point2(4, 0);
        var g = new Point2(4, 5);
        var h = new Point2(0, 5);
        var ch2 = ConvexHull.ConvexHull4(e, f, g, h);

        var ch = ConvexHull.JoinHulls(ch1, ch2);

        var s = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [4; 0] [5; 3] [5; 6] [1; 6] [0; 5]", s);
    }

    [TestMethod]
    public void JoinHulls_PointInOneHullOnly()
    {
        var a = new Point2(0, 2);
        var b = new Point2(0, 0);
        var c = new Point2(2, 0);
        var d = new Point2(2, 2);
        var ch1 = ConvexHull.ConvexHull4(a, b, c, d);

        var e = new Point2(1, 1);
        var f = new Point2(4, 1);
        var g = new Point2(4, 4);
        var h = new Point2(1, 4);
        var ch2 = ConvexHull.ConvexHull4(e, f, g, h);

        var ch = ConvexHull.JoinHulls(ch1, ch2);

        var s = string.Join(" ", ch);
        Assert.AreEqual("[0; 0] [2; 0] [4; 1] [4; 4] [1; 4] [0; 2]", s);
    }

    [TestMethod]
    public void Points_OnConvexHull_CanBeAccessedWithIndex()
    {
        var a = new Point2(0, 2);
        var b = new Point2(0, 0);
        var c = new Point2(2, 0);
        var d = new Point2(2, 2);
        var e = new Point2(1, 1);
        var ch = new List<Point2>() { a, b, c, d, e };

        Assert.AreEqual(a, ch[0]);
        Assert.AreEqual(b, ch[1]);
        Assert.AreEqual(c, ch[2]);
        Assert.AreEqual(d, ch[3]);
    }

    public void Samples()
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

        // begin-snippet: ConvexHull4
        var a = new Point2(0, 0);
        var c = new Point2(1, 0);
        var b = new Point2(1, 1);
        var d = new Point2(0, 1);

        var convexHull4 = ConvexHull.ConvexHull4(a, b, c, d);
        // end-snippet

        //begin-snippet: AddPoint
        var convexHull = ConvexHull.ConvexHull4(a, b, c, d);
        convexHull.Add(new Point2(5, 5));
        //end-snippet
    }
}
