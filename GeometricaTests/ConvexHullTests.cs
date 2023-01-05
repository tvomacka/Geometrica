using Geometrica.DataStructures;
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
}
