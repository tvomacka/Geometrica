using Geometrica.Algebra;

namespace GeometricaTests
{
    [TestClass]
    public class Matrix4Tests
    {
        [TestMethod]
        public void Matrix4_IsZero_ByDefault()
        {
            var m = new Matrix4();

            Assert.AreEqual("", m.ToString());
        }
    }
}
