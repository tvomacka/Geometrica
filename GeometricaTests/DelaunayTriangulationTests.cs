using Geometrica.DataStructures;
using Geometrica.Primitives;

namespace GeometricaTests
{
    [TestClass]
    public class DelaunayTriangulationTests
    {
        [TestMethod]
        public void DT_HasNotriangles_AfterInitialization()
        {
            var dt = new DelaunayTriangulation();

            Assert.AreEqual(0, dt.Triangles.Length);
        }

        [TestMethod]
        public void Point_CanBeAdded_ToEmptyTriangulation()
        {
            var dt = new DelaunayTriangulation();

            var p = new Point2();
            dt.Add(p);

            Assert.AreEqual(p, dt.Points[0]);
        }

        [TestMethod]
        public void DT_CanBeCreated_WithThreeGivenPoints()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var dt = new DelaunayTriangulation(new Point2[] { p1, p2, p3 });

            Assert.AreEqual(1, dt.Triangles.Length);
            Assert.AreEqual("Triangle [0; 0] [1; 0] [0; 1]", dt.Triangles[0].ToString());
        }
    }
}
