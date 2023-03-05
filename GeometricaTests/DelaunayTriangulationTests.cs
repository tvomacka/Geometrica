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

            var t = string.Join<Triangle>(" ", dt.Triangles);

            Assert.AreEqual("Triangle [0,1; 0,1] [0; 0] [1; 0] Triangle [0,1; 0,1] [1; 0] [0; 1] Triangle [0,1; 0,1] [0; 1] [0; 0]", t);
        }

        [TestMethod]
        public void SingleTriangle_InTriangulation_HasNoNeighbors()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var dt = new DelaunayTriangulation(new Point2[] { p1, p2, p3 });

            var t = dt.Triangles[0];

            Assert.IsNull(t.GetNeighbor(0));
            Assert.IsNull(t.GetNeighbor(1));
            Assert.IsNull(t.GetNeighbor(2));
        }

        [TestMethod]
        public void Triangle_Neighbor_CanBeAssigned()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);
            var p4 = new Point2(1, 1);

            var t = new Triangle(p1, p2, p3);
            var t2 = new Triangle(p3, p2, p4);

            t.SetNeighbor(0, t2);

            Assert.AreEqual(t2, t.GetNeighbor(0));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void AccessingTriangleNeighbor_WithNegativeIndex_ThrowsException()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var t = new Triangle(p1, p2, p3);

            t.GetNeighbor(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void AccessingTriangleNeighbor_WithIndex_4ThrowsException()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);

            var t = new Triangle(p1, p2, p3);

            t.GetNeighbor(4);
        }

        [TestMethod]
        public void Estimating_NearestTriangle()
        {
            var t = new Triangle[1000];
            for (var i = 0; i < t.Length; i++)
            {
                t[i] = new Triangle(
                    new Point2(i, 0),
                    new Point2(i + 1, 0),
                    new Point2(i + 0.5, 1));
            }

            var p = new Point2(0.5, 0.1);

            var nearest = DelaunayTriangulation.EstimateNearestTriangle(t, p, new Random(0));

            Assert.AreEqual("Triangle [206; 0] [207; 0] [206,5; 1]", nearest.ToString());
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
