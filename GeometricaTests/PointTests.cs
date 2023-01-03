﻿using Geometrica.Primitives;

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
}