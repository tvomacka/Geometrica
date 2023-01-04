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

        Assert.IsNull(ch.Hull);
    }
}
