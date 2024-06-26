﻿using Geometrica.Algebra;

namespace GeometricaTests
{
    [TestClass]
    public class Matrix4Tests
    {
        [TestMethod]
        public void Submatrix3_RemovesGiven_ColAndRow()
        {
            var m = new Matrix4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16);

            var m3 = m.SubMatrix3(0, 0);
            var s = "[6;7;8][10;11;12][14;15;16]";

            Assert.AreEqual(s, m3.ToString());
        }

        [TestMethod]
        public void Matrix4_Returns_Negative022()
        {
            var m = new Matrix4(
                0, 0, 0, 1,
                1, 0, 1, 1,
                0, 1, 1, 1,
                1.1, 1.1, 2.42, 1);

            var det = m.Determinant();

            Assert.AreEqual(-0.22, det, 1e-3);
        }

        [TestMethod]
        public void Matrix4_Members_CanBeEditedWithIndexers()
        {
            var m = new Matrix4();

            m[1, 2] = 10;

            Assert.AreEqual(10, m[1, 2]);
        }

        [TestMethod]
        public void Matrix4_IsZero_ByDefault()
        {
            var m = new Matrix4();

            Assert.AreEqual("[0;0;0;0][0;0;0;0][0;0;0;0][0;0;0;0]", m.ToString());
        }

        [TestMethod]
        public void Matrix4_Values_CanBeInitialized()
        {
            var m = new Matrix4(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

            Assert.AreEqual(0, m[0, 0]);
            Assert.AreEqual(15, m[3, 3]);
        }

        [TestMethod]
        public void Matrix4_DeterminantOfZero_IsZero()
        {
            var m = new Matrix4();
            Assert.AreEqual(0, m.Determinant(), double.Epsilon);
        }

        [TestMethod]
        public void Matrix4_DeterminantOfIdentity_IsOne()
        {
            var m = Matrix4.Identity;

            Assert.AreEqual(1, m.Determinant(), double.Epsilon);
        }
    }
}
