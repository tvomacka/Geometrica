using Geometrica.DataStructures;
using Geometrica.Primitives;

namespace GeometricaTests
{
    [TestClass]
    public class DelaunayTriangulationTests
    {
        [TestMethod]
        public void DT_HasNoTriangles_AfterInitialization()
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

        [TestMethod]
        public void FourthAddedPoint_Splits_OneTriangleInThree()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var dt = new DelaunayTriangulation(new Point2[] { p1, p2, p3 });
            dt.Add(new Point2(0.1, 0.1));

            var t = string.Join(" ", dt.Triangles);

            Assert.AreEqual("Triangle [0,1; 0,1] [0; 0] [1; 0] Triangle [0,1; 0,1] [1; 0] [0; 1] Triangle [0,1; 0,1] [0; 1] [0; 0]", t);
        }

        public void Samples()
        {
            // begin-snippet: DelaunayTriangulationConstructor
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var dt = new DelaunayTriangulation(new Point2[] { p1, p2, p3 });
            // end-snippet

            // begin-snippet: DelaunayTriangulationAddPoint
            dt.Add(new Point2(0.1, 0.1));
            // end-snippet
        }
    }
}
