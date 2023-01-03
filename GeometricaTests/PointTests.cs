using Geometrica.Primitives;

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
}
