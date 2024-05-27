namespace Geometrica.Algebra
{
    public class Matrix4
    {
        private readonly double[,] _matrix;

        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
        }

        public Matrix4()
        {
            _matrix = new double[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    _matrix[i, j] = 0;
                }
            }
        }

        public Matrix4(
            double m00, double m01, double m02, double m03, 
            double m10, double m11, double m12, double m13, 
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33)
        {
            _matrix = new double[4, 4];
            _matrix[0, 0] = m00;
            _matrix[0, 1] = m01;
            _matrix[0, 2] = m02;
            _matrix[0, 3] = m03;
            _matrix[1, 0] = m10;
            _matrix[1, 1] = m11;
            _matrix[1, 2] = m12;
            _matrix[1, 3] = m13;
            _matrix[2, 0] = m20;
            _matrix[2, 1] = m21;
            _matrix[2, 2] = m22;
            _matrix[2, 3] = m23;
            _matrix[3, 0] = m30;
            _matrix[3, 1] = m31;
            _matrix[3, 2] = m32;
            _matrix[3, 3] = m33;
        }

        public static Matrix4 Identity =>
            new(1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public override string ToString()
        {
            var s = "";
            for (var i = 0; i < 4; i++)
            {
                s += $"[{_matrix[i, 0]};{_matrix[i, 1]};{_matrix[i, 2]};{_matrix[i, 3]}]";
            }

            return s;
        }

        public double Determinant()
        {
            return _matrix[0, 0] * Submatrix3(0, 0) - _matrix[1, 0] * Submatrix3(1, 0) + _matrix[2, 0] * Submatrix3(2, 0) - _matrix[3, 0] * Submatrix3(3, 0);
        }
    }
}
