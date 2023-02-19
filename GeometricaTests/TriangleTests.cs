using Geometrica.Primitives;

namespace GeometricaTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void Triangle_CanBeInitialized_WithThreePoints()
        {
            var a = new Point2(0, 0);
            var b = new Point2(1, 0);
            var c = new Point2(0, 1);

            var t = new Triangle(a, b, c);
            var s = t.ToString();

            Assert.AreEqual("", s);
        }
    }
}
