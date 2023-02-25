using Geometrica.DataStructures;

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
    }
}
