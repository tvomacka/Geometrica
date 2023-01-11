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

        Assert.IsTrue(Point2.OrientedCCW(p, q, r));
        // end-snippet
    }
}
