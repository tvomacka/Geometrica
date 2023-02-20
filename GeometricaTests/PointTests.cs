using Geometrica.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometricaTests;

[TestClass]
public class PointTests
{
    [TestMethod]
    public void Point_Default_IsZero()
    {
        var p = new Point2();

        Assert.AreEqual("[0; 0]", p.ToString());
    }

    [TestMethod]
    public void Point_DistanceToSelf_IsZero()
    {
        var p = new Point2();

        Assert.AreEqual(0, p.DistanceTo(p), 3);
    }

    [TestMethod]
    public void Point_DistanceTo_ReturnsCorrectValue()
    {
        var p = new Point2();
        var q = new Point2(5, 0);

        Assert.AreEqual(5, p.DistanceTo(q), 3);
    }

    [TestMethod]
    public void Point_CanBeNormalized_ToUnitLength()
    {
        var p = new Point2(2, 1);

        Assert.AreEqual("[0,8944271909999159; 0,4472135954999579]", p.Normalize().ToString());
    }

    [TestMethod]
    public void TwoPoints_CanBeSubstracted()
    {
        var p = new Point2(10, 1);
        var q = new Point2(1, 5);

        var r = p - q;

        Assert.AreEqual("[9; -4]", r.ToString());
    }

    [TestMethod]
    public void Point_OrientationTest_ReturnsCorrectValue()
    {
        // begin-snippet: OrientationTest
        var p = new Point2(0, 0);
        var q = new Point2(0, 1);
        var r = new Point2(1, 0);

        Assert.IsTrue(Point2.Orientation(p, q, r) < 0);
        // end-snippet
    }

    [TestMethod]
    public void Point_CCWOrientationTest_ReturnsTrue()
    {
        // begin-snippet: CCWOrientationTest
        var p = new Point2(0, 0);
        var q = new Point2(1, 0);
        var r = new Point2(0, 1);

        Assert.IsTrue(Point2.OrientedCcw(p, q, r));
        // end-snippet
    }

    [TestMethod]
    public void Point_InsidePolygon_ReturnsTrue()
    {
        var t = new Triangle(new Point2(0, 0), new Point2(1, 0), new Point2(0, 1));
        var p = new Point2(0.1, 0.1);

        Assert.IsTrue(p.Inside(t));
    }

    [TestMethod]
    public void Point_OutsidePolygon_ReturnsFalse()
    {
        var t = new Triangle(new Point2(0, 0), new Point2(1, 0), new Point2(0, 1));
        var p = new Point2(1, 1);

        Assert.IsFalse(p.Inside(t));
    }
}
