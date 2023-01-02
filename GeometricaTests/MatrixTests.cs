using Geometrica.DataStructures;
using System.Numerics;

namespace GeometricaTests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Matrix3_IsZero_ByDefault()
        {
            var m = new Matrix3();

            Assert.AreEqual("", m.ToString());
        }
    }
}