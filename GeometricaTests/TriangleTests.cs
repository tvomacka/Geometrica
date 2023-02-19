using Geometrica.Primitives;

namespace GeometricaTests
{
    [TestClass]
    public class TriangleTests
    {
        private static Triangle CreateSimpleTriangle()
        {
            var a = new Point2(0, 0);
            var b = new Point2(1, 0);
            var c = new Point2(0, 1);

            return new Triangle(a, b, c);
        }

        [TestMethod]
        public void Triangle_CanBeInitialized_WithThreePoints()
        {
            var t = CreateSimpleTriangle();
            var s = t.ToString();

            Assert.AreEqual("Triangle [0; 0] [1; 0] [0; 1]", s);
        }


        [TestMethod]
        public void TrianglePoints_CanBeAccessed_WithIndex()
        {
            var a = new Point2(0, 0);
            var b = new Point2(1, 0);
            var c = new Point2(0, 1);

            var t = new Triangle(a, b, c);

            Assert.AreEqual(a, t[0]);
            Assert.AreEqual(b, t[1]);
            Assert.AreEqual(c, t[2]);
        }

        [TestMethod]
        public void Triangle_Has_ThreePoints()
        {
            var t = CreateSimpleTriangle();

            Assert.AreEqual(3, t.Count);
        }
    }
}
