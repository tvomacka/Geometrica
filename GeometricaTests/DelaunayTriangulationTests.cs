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

        [TestMethod]
        public void SplitTriangle_SplitsSingleTriangle_IntoThree()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);
            var p4 = new Point2(0.1, 0.2);

            var triangles = new Triangle[]
            {
                new(p1, p2, p3)
            };

            triangles = DelaunayTriangulation.SplitTriangle(triangles, triangles[0], p4);

            var s = string.Join<Triangle>(" ", triangles);

            Assert.AreEqual("Triangle [0,1; 0,2] [0; 0] [1; 0] Triangle [0,1; 0,2] [1; 0] [0; 1] Triangle [0,1; 0,2] [0; 1] [0; 0]", s);
        }

        [TestMethod]
        public void SplitTriangle_WhenMultipleTrianglesExist()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(1, 0);
            var p3 = new Point2(0, 1);
            var p4 = new Point2(4, 4);
            var innerPoint = new Point2(0.1, 0.2);

            var triangles = new Triangle[]
            {
                new(p1, p2, p3),
                new(p2, p4, p3)
            };

            triangles = DelaunayTriangulation.SplitTriangle(triangles, triangles[0], innerPoint);

            var s = string.Join<Triangle>(" ", triangles);

            Assert.AreEqual(4, triangles.Length);
            Assert.AreEqual(
                "Triangle [0,1; 0,2] [0; 0] [1; 0] Triangle [0,1; 0,2] [1; 0] [0; 1] Triangle [0,1; 0,2] [0; 1] [0; 0] Triangle [1; 0] [4; 4] [0; 1]",
                s);
        }

        [TestMethod]
        public void SplitTriangle_ConservesNeighbors_AfterSplit()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(3, 0);
            var p3 = new Point2(0, 3);
            var p4 = new Point2(4, 4);
            var p5 = new Point2(-4, 1);
            var p6 = new Point2(2, -4);

            var innerPoint = new Point2(0.1, 0.2);

            var t1 = new Triangle(p1, p2, p3);
            var t2 = new Triangle(p4, p3, p2);
            var t3 = new Triangle(p5, p1, p3);
            var t4 = new Triangle(p6, p2, p1);

            t1.SetNeighbor(0, t2);
            t1.SetNeighbor(1, t3);
            t1.SetNeighbor(2, t4);

            t2.SetNeighbor(0, t1);
            t3.SetNeighbor(0, t1);
            t4.SetNeighbor(0, t1);

            var triangles = new Triangle[] { t1, t2, t3, t4 };
            triangles = DelaunayTriangulation.SplitTriangle(triangles, triangles[0], innerPoint);

            Assert.AreEqual(6, triangles.Length);

            Assert.AreEqual(t4, triangles[0].GetNeighbor(0));
            Assert.AreEqual(t2, triangles[1].GetNeighbor(0));
            Assert.AreEqual(t3, triangles[2].GetNeighbor(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchForNullNeighbor_ThrowsException()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(3, 0);
            var p3 = new Point2(0, 3);

            var t = new Triangle(p1, p2, p3);

            t.GetNeighborIndex(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchForNeighbor_ThrowsException_IfNoSuchNeighborIsConnected()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(3, 0);
            var p3 = new Point2(0, 3);
            var p4 = new Point2(3, 3);

            var t = new Triangle(p1, p2, p3);
            var n = new Triangle(p3, p4, p2);

            t.GetNeighborIndex(n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchForNeighbor_ReturnsCorrectIndex_IfNeighborsAreConnected()
        {
            var p1 = new Point2(0, 0);
            var p2 = new Point2(3, 0);
            var p3 = new Point2(0, 3);
            var p4 = new Point2(3, 3);

            var t = new Triangle(p1, p2, p3);
            var n = new Triangle(p3, p4, p2);

            t.SetNeighbor(0, n);

            var i = t.GetNeighborIndex(n);

            Assert.AreEqual(0, i);
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
