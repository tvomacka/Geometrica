using Geometrica.DataStructures;
using System.Numerics;

namespace GeometricaTests
{
    [TestClass]
    public class MatrixTests
    {
        private readonly double delta = 1e-3;

        [TestMethod]
        public void Matrix3_IsZero_ByDefault()
        {
            var m = new Matrix3();

            Assert.AreEqual("[0;0;0][0;0;0][0;0;0]", m.ToString());
        }

        [TestMethod]
        public void Matrix3_Items_CanBeAccessedWithIndexer()
        {
            var m = new Matrix3();

            m[0, 0] = 5;
            Assert.AreEqual(5, m[0, 0], delta);
        }
    }
}