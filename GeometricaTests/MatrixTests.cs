using Geometrica.Algebra;

namespace GeometricaTests;

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

    [TestMethod]
    public void Matrix3_UnitMatrix_InitializedCorrectly()
    {
        var m = Matrix3.Identity;

        Assert.AreEqual("[1;0;0][0;1;0][0;0;1]", m.ToString());
    }

    [TestMethod]
    public void Matrix3_UnitDeterminant_EqualToOne()
    {
        var m = Matrix3.Identity;

        Assert.AreEqual(1, m.Determinant(), delta);
    }

    [TestMethod]
    public void Matrix3_Determinant_ComputedCorrectly()
    {
        var m = new Matrix3(9, 3, 5, -6, -9, 7, -1, -8, 1);

        Assert.AreEqual(615, m.Determinant(), delta);
    }

    [TestMethod]
    public void Matrix3_Transpose_ReturnsCorrectResult()
    {
        var m = new Matrix3(9, 3, 5, -6, -9, 7, -1, -8, 1);
        var t = m.Transpose();

        Assert.AreEqual("[9;-6;-1][3;-9;-8][5;7;1]", t.ToString());
    }
}