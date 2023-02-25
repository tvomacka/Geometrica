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
    }
}
